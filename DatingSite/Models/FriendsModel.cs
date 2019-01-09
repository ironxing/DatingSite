using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DatingSite.Models
{
    public class FriendsModel
    {
        [Key]
        public int Id { get; set; }

        public bool FriendRequest { get; set; }
        public bool Friends { get; set; }

        public string ProfileOwnerId { get; set; }
        public virtual ApplicationUser ProfileOwner { get; set; }

        public string ProfileVisitorId{ get; set; }
        public virtual ApplicationUser ProfileVisitor { get; set; }
    }
}