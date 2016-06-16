using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.valgut.libs.bots.Wit;
using com.valgut.libs.bots.Wit.Models;

namespace Wit.ai.net.Tests
{
    [TestClass]
    public class WitClient_Tests
    {
        
        [TestMethod]
        public void WitClient_GetMessage_Test()
        {
            WitClient client = new WitClient(Properties.Settings.Default.WitToken);
            Message message = client.GetMessage("hello");

            Assert.IsNull(message.error);
        }
    }
}
