using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.valgut.libs.bots.Wit;

namespace Wit.ai.net.Tests
{
    [TestClass]
    public class WitClient_Tests
    {
        
        [TestMethod]
        public void WitClient_GetMessage_Test()
        {
            var client = new WitClient(Properties.Settings.Default.WitToken);
            var message = client.GetMessage("hello");

            Assert.IsNull(message.error);
        }
    }
}
