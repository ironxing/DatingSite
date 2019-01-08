using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DatingSite.Models
{
    public class MessageItem
    {
        [Key]
        public int id { get; set; }

        public DateTime messageTime { get; set; }

        public string MessageReceiverId { get; set; }
        public virtual ApplicationUser MessageReceiver { get; set; }

        public string MessageSenderId { get; set; }
        public virtual ApplicationUser MessageSender { get; set; }

        public string Text { get; set; }
    }
}