using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DatingSite.Models.ViewModels
{
    public class ExportUserDataViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }

        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public Gender? LookingForGender { get; set; }

        public List<ExporProfileVisit> ExportProfileVisits { get; set; }
        public List<ExportMessageViewModel> ExportMessageViewModels { get; set; }
        public List<ExportFriend> ExportFriends { get; set; }
    }

    public class ExportMessageViewModel
    {
        public string MessageSenderUserName { get; set; }
        public string MessageReceiverUserName { get; set; }
        public DateTime MessageTime { get; set; }
        public string Text { get; set; }
    }

    public class ExporProfileVisit
    {
        public DateTime VisitDateTime { get; set; }
        public string VisitorUserUserName { get; set; }
    }

    public class ExportFriend
    {
        public string FriendUserName { get; set; }
    }

}