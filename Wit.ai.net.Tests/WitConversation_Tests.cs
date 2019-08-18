using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.valgut.libs.bots.Wit;
using System.Threading.Tasks;
using System.Collections.Generic;
using com.valgut.libs.bots.Wit.Models;
using System.Configuration;
using System.Xml.Linq;
using System.IO;
using System.Linq;

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
            var appConfigContent = File.ReadAllText("../../../app.config");
            var witToken = XElement.Parse(appConfigContent)
                                .Element(XName.Get("applicationSettings"))
                                .Element(XName.Get("Wit.ai.net.Tests.Properties.Settings"))
                                .Elements(XName.Get("setting"))
                                .First(e => e.Attribute("name")?.Value == "WitToken")
                                .Value;

            var client = new WitConversation<DemoContext>(witToken, conversationId, null,doMerge, doSay, doAction, doStop);

            Task<bool> t = client.SendMessageAsync("hello");
            t.Wait();

            Assert.IsTrue(t.Result && didMerge && didStop);
        }

        public DemoContext doMerge(string conversationId, DemoContext context, object entities, double confidence)
        {
            didMerge = true;
            return context;
        }

        public void doSay(string conversationId, DemoContext context, string msg, double confidence)
        {
            Console.WriteLine(msg);
        }

        public DemoContext doAction(string conversationId, DemoContext context, string action, Dictionary<string, List<Entity>> entities, double confidence)
        {
            return context;
        }

        public DemoContext doStop(string conversationId, DemoContext context)
        {
            didStop = true;
            return context;
        }

        public class DemoContext
        {
            public string someField { get; set; }
        }
    }
}
