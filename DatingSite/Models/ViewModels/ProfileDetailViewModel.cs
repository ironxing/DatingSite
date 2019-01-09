using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatingSite.Models.ViewModels
{
    public class ProfileDetailViewModel
    {
        public ApplicationUser User { get; set; }
        public List<ProfileVisit> LatestProfileVisits {get; set;}
        public List<MessageItem> MessageItems { get; set; }
        public bool FriendStatus { get; set; }
        public bool Friends { get; set; }
    }
}