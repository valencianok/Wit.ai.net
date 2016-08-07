using com.valgut.libs.bots.Wit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.valgut.libs.bots.Wit
{
    public class WitConversation<T>
    {
        WitClient client = null;
        string conversationId = null;
        T context = default(T);

        public delegate T MergeHandler(string conversationId, T context, Dictionary<string, List<Entity>> entities, double confidence);
        public delegate void SayHandler(string conversationId, T context, string msg, double confidence);
        public delegate T ActionHandler(string conversationId, T context, string action, Dictionary<string, List<Entity>> entities, double confidence);
        public delegate T StopHandler(string conversationId, T context);

        MergeHandler _merge;
        SayHandler _say;
        ActionHandler _action;
        StopHandler _stop;

        public WitConversation(string token, string conversationId, T initialContext, 
            MergeHandler merge, SayHandler say, ActionHandler action, StopHandler stop)
        {
            if (token == null || conversationId == null || merge == null || say == null || action == null)
                throw new Exception("Please check WitConversation constructor parameters.");

            client = new WitClient(token);
            this.conversationId = conversationId;
            _merge = merge;
            _say = say;
            _action = action;
            _stop = stop;
            context = initialContext;
        }

        public Task<bool> SendMessageAsync(string q)
        {
            var t = Task.Factory.StartNew<bool>(() =>
            {
                ConverseResponse response = client.Converse(conversationId, q, context);
                return recurringConverse(response);
            });
            return t;
        }

        private bool recurringConverse(ConverseResponse prevResponse)
        {
            bool doOneMoreStep = false;
            T tempContext = default(T);
            switch (prevResponse.typeCode)
            {
                case ConverseResponse.Types.merge:
                    tempContext = _merge.Invoke(conversationId, context, prevResponse.entities, prevResponse.confidence);
                    doOneMoreStep = true;
                    break;
                case ConverseResponse.Types.msg:
                    _say.Invoke(conversationId, context, prevResponse.msg, prevResponse.confidence);
                    doOneMoreStep = true;
                    break;
                case ConverseResponse.Types.action:
                    tempContext = _action.Invoke(conversationId, context, prevResponse.action, prevResponse.entities, prevResponse.confidence);
                    doOneMoreStep = true;
                    break;
                case ConverseResponse.Types.stop:
                    tempContext = _stop.Invoke(conversationId, context);
                    doOneMoreStep = false;
                    break;
                default:
                    break;
            }

            if (tempContext != null)
                context = tempContext;

            if (doOneMoreStep)
            {
                ConverseResponse response = client.Converse(conversationId, null, context);
                return recurringConverse(response);
            }

            return true;
        }
    }
}
