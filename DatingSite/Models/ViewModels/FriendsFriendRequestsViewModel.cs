using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatingSite.Models.ViewModels
{
    public class FriendsFriendRequestsViewModel
    {
        public List<FriendListViewModel> FriendListViewModels { get; set; }
        public List<FriendsModel> FriendRequests { get; set; }
        public List<FriendCategory> FriendCategories { get; set; }

    }
}