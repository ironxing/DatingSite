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
    }
}