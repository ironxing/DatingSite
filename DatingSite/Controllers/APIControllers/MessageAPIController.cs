using DatingSite.Models;
using DatingSite.Models.ViewModels;
using Microsoft.AspNet.Identity;
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

        // Post ('api/Message/Message/Add?')
        [Route("Message/Add")]
        [HttpPost]
        public void AddMessage(SendMessageViewModel sendMessageViewModel)
        {
            var DateTimeNow = DateTime.Now;
            _dbcontext.MessageItems.Add(new MessageItem
            {
                messageTime = DateTimeNow,
                MessageReceiverId = sendMessageViewModel.ReceiverId,
                MessageSenderId = sendMessageViewModel.SenderId,
                Text = sendMessageViewModel.MessageText
            });
            _dbcontext.SaveChanges();
        }

        [Route("Message/Delete")]
        [HttpDelete]
        public void DeleteMessage(int MessageId)
        {
            var messageItem = _dbcontext.MessageItems.SingleOrDefault(m => m.id == MessageId);
            
            if(messageItem!= null)
            {
                _dbcontext.MessageItems.Remove(messageItem);
                _dbcontext.SaveChanges();
            }
        }
    }
}
