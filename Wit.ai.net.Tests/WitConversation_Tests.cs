using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.valgut.libs.bots.Wit;
using System.Threading.Tasks;

namespace Wit.ai.net.Tests
{
    [TestClass]
    public class WitConversation_Tests
    {
        private string conversationId = "test";
        private bool didMerge = false;
        private bool didStop = false;

        [TestMethod]
        public void WitConversation_Conversation_Test()
        {
            var client = new WitConversation(Properties.Settings.Default.WitToken, conversationId, doMerge, doSay, doAction, doStop);
            Task<bool> t = client.SendMessageAsync("hello");
            t.Wait();

            Assert.IsTrue(t.Result && didMerge && didStop);
        }

        public object doMerge(string conversationId, object context, object entities, double confidence)
        {
            didMerge = true;
            return context;
        }

        public void doSay(string conversationId, object context, string msg, double confidence)
        {
            Console.WriteLine(msg);
        }

        public object doAction(string conversationId, object context, string action, double confidence)
        {
            return context;
        }

        public object doStop(string conversationId, object context)
        {
            didStop = true;
            return context;
        }
    }
}
