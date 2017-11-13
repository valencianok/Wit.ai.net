using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.valgut.libs.bots.Wit;
using com.valgut.libs.bots.Wit.Models;
using System.IO;
using System.Xml.Linq;
using System.Linq;

namespace Wit.ai.net.Tests
{
    [TestClass]
    public class WitClient_Tests
    {
        
        [TestMethod]
        public void WitClient_GetMessage_Test()
        {
            var appConfigContent = File.ReadAllText("../../../app.config");
            var witToken = XElement.Parse(appConfigContent)
                                .Element(XName.Get("applicationSettings"))
                                .Element(XName.Get("Wit.ai.net.Tests.Properties.Settings"))
                                .Elements(XName.Get("setting"))
                                .First(e => e.Attribute("name")?.Value == "WitToken")
                                .Value;
            WitClient client = new WitClient(witToken);
            Message message = client.GetMessage("hello");

            Assert.IsNull(message.error);
        }
    }
}
