using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.valgut.libs.bots.Wit;

namespace Wit.ai.net.Tests
{
    [TestClass]
    public class WitClient_Tests
    {
        string token = "<your_wit_token>";

        [TestMethod]
        public void WitClient_GetMessage_Test()
        {
            var client = new WitClient(token);
            var message = client.GetMessage("hello");

            Assert.IsNull(message.error);
        }
    }
}
