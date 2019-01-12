using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DatingSite.Models
{
    public class FriendCategory
    {
        [Key]
        public int Id { get; set; }

        public string CategoryName { get; set; }


        public string CategoryOwnerId { get; set; }
        public virtual ApplicationUser CategoryOwner { get; set; }
    }
}