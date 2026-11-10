using BusinessLogicLayer;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace VolcaniProject.Controllers
{
    [RoutePrefix("messages")]
    public class MessageController : ApiController
    {
        private MessageService service;

        public MessageController()
        {
            service = MessageService.Instance;
        }

        [HttpPost]
        [Route]
        public async Task<IHttpActionResult> AddMessage([FromBody] Message message)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await service.AddMessage(message);
                return StatusCode(HttpStatusCode.Created);
            }
            catch (Exception excption)
            {
                return InternalServerError(excption);
            }
        }

        [HttpGet]
        [Route]
        public IHttpActionResult GetMessagesByStatus([FromUri] int status)
        {
            return Ok(service.GetMessagesByStatus(status));
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetMessageStatus(long id)
        {
            return Ok(service.GetMessageStatus(id));
        }

        [Route("statuses")]
        [HttpGet]
        public IHttpActionResult GetAllMessageStatuses()
        {
            return Ok(service.GetAllMessageStatuses());
        }

        

    }
}
