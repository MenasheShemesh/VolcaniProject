using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using VolcaniProject.Controllers;
using System.Web.Http;
using Infrastructure;
using System.Threading.Tasks;

namespace UnitTestMessage
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void GetMessages_ShouldReturnOk()
        {
            MessageController controller = new MessageController();

            IHttpActionResult result = controller.GetMessagesByStatus(1);
            Assert.AreEqual(result.GetType(), Type.GetType("OkNegotiatedContentResult"));
        }

        [TestMethod]
        public void GetMessages_ShouldReturnCreated()
        {
            Message  message = new Message();
            message.Worker = new Worker("ישראל", "ישראלי", "חולון", "בן גוריון", 1);
            message.Phone = new Phone("054", "123457");
            message.Content = "בדיקת תקינות";
            MessageController controller = new MessageController();

            Task<IHttpActionResult> result = controller.AddMessage(message);
            Assert.AreEqual(result.Result.GetType(), Type.GetType("StatusCodeResult"));
        }

        public void GetMessage_ShouldReturnOk()
        {
            MessageController controller = new MessageController();

            IHttpActionResult result = controller.GetMessageStatus(1);
            Assert.AreEqual(result.GetType(), Type.GetType("OkNegotiatedContentResult"));
        }
    }
}
