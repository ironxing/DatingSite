using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatingSite.Models.ViewModels
{
    public class VisitViewModel
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public DateTime VisitDateTime { get; set; }
    }
}