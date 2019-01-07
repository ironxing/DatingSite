using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DatingSite.Models
{
    public class ProfileVisit
    {
        public int Id { get; set; }
        public DateTime VisitDateTime { get; set; }
        
        public string ProfileUserId { get; set; }
        public virtual ApplicationUser ProfileUser { get; set; }

        public string VisitorUserId { get; set; }
        public virtual ApplicationUser VisitorUser { get; set; }

    }
}