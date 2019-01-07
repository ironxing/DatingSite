using DatingSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DatingSite.Controllers.APIControllers
{
    [RoutePrefix("api/Message")]
    public class MessageAPIController : ApiController
    {
        private ApplicationDbContext _dbcontext;

        public MessageAPIController()
        {
            _dbcontext = new ApplicationDbContext();
        }
        

        [Route("Message/Add")]
        [HttpGet]
        public void AddMessage(string sender, string reciever, string message)
        {
            var DateTimeNow = DateTime.Now;
            _dbcontext.MessageItems.Add(new MessageItem
            {
                messageTime = DateTimeNow,
                ProfileUserId = reciever,
                MessageSenderId = sender,
                Text = message
            });
            _dbcontext.SaveChanges();
        }

    }
}
