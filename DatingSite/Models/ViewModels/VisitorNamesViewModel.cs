﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatingSite.Models.ViewModels
{
    public class VisitorNamesViewModel
    {
        public string VisitorId { get; set; }
        public string VisitorFullName { get; set; }
        public DateTime VisitDateTime { get; set; }
    }
}