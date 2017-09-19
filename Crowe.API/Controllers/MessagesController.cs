using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crowe.Models;
using Crowe.Services;

namespace Crowe.Controllers
{
    [Route("api/message")]
    public class MessagesController : Controller
    {
        private readonly MessagesService messagesService;

        public MessagesController(MessagesService messagesService)
        {
            //dependency injection 
            this.messagesService = messagesService;
        }

        // GET api/message/1
        [HttpGet("{messageId}", Name = "MessageGet")]
        public Messages Voter(int messageId)
        {
            return messagesService.GetMessage(messageId);
        }

        // GET api/message
        [HttpGet]
        public IEnumerable<Messages> Messages()
        {
            return messagesService.GetMessages();
        }

        [HttpPost]
        public async Task<IActionResult> PostMessage(Messages message)
        {
            var saveCount = await messagesService.SaveMessage(message);

            if(saveCount != 0)
            {
                var saveMessageUri = Url.Link("MessageGet", new { messageId = message.Id });
                return Created(saveMessageUri, message);
            }
            else
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
