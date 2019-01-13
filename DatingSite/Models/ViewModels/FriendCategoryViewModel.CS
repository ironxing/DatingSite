using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatingSite.Models.ViewModels
{
    public class FriendCategoryViewModel
    {
        public string CategoryName { get; set; }
    }

    public class FriendListViewModel
    {
        public int FriendModelId { get; set; }
        public string FriendFullName { get; set; }
        public string FriendUserId { get; set; }
        public string CategoryName { get; set; }
        public string FriendRequestSide { get; set; }
    }
}