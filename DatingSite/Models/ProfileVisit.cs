using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [ForeignKey("ProfileUserId")]
        public virtual ApplicationUser ProfileUser { get; set; }

        public string VisitorUserId { get; set; }
        [ForeignKey("VisitorUserId")]
        public virtual ApplicationUser VisitorUser { get; set; }

    }
}