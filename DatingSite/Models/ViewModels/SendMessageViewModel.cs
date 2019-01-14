using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatingSite.Models.ViewModels
{
    public class SendMessageViewModel
    {
        public string MessageText { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
    }
}