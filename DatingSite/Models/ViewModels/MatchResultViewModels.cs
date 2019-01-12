using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatingSite.Models.ViewModels
{
    public class MatchResultViewModel
    {
        public string ProfileOwnerFullName { get; set; }
        public bool Match { get; set; }
    }

    public class MatchResultForListViewModel
    {
        public string MatchedUserId { get; set; }
        public string MatchedUserFullName { get; set; }
    }
}