using DatingSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Drawing;
using DatingSite.Models.ViewModels;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace DatingSite.Controllers
{
    public class ProfileController : Controller
    {
        public ApplicationDbContext _dbcontext;

        public ProfileController()
        {
            _dbcontext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _dbcontext.Dispose();
        }

        [HttpPost]
        public ActionResult ValidateDateEqualOrGreater(DateTime Date)
        {
            // Custom validation for BirthDate
            if (Date <= DateTime.Now)
            {
                return Json(true);
            }
            return Json(false);
        }

        // GET: Profile
        public ActionResult Index()
        {
            var Users = _dbcontext.Users.ToList();
            return View(Users);
        }

        // GET: Profile/Details/
        [Authorize]
        public ActionResult Details(string Id)
        {
            var VisitorId = User.Identity.GetUserId();

            var FriendshipAsFRReceiver = _dbcontext.FriendsModels.FirstOrDefault(f => f.ProfileOwnerId == Id && f.ProfileVisitorId == VisitorId);
            var FriendshipAsFRSender = _dbcontext.FriendsModels.FirstOrDefault(f => f.ProfileVisitorId == Id && f.ProfileOwnerId == VisitorId);
            
            var user = _dbcontext.Users.SingleOrDefault(u => u.Id == Id);
            if (user == null)
                return HttpNotFound();

            bool friendRequest = false;
            bool friendsCheck = false;

            //logged in user can be Friend request Sender or Receiver. 
            //If somehow logged in user has Sent AND Received FR from the same user
            if(FriendshipAsFRReceiver != null && FriendshipAsFRSender != null)
            {
                if(FriendshipAsFRReceiver.FriendRequest || FriendshipAsFRSender.FriendRequest)
                {
                    friendRequest = true;
                }
                if (FriendshipAsFRReceiver.Friends || FriendshipAsFRSender.Friends)
                {
                    friendsCheck = true;
                }
            }
            //If logged in user has Received FR from the same user
            else if (FriendshipAsFRReceiver != null && FriendshipAsFRSender == null)
            {
                friendRequest = FriendshipAsFRReceiver.FriendRequest;
                friendsCheck = FriendshipAsFRReceiver.Friends;
            }
            //If logged in user has Sent FR from the same user
            else if (FriendshipAsFRReceiver == null && FriendshipAsFRSender != null)
            {
                friendRequest = FriendshipAsFRSender.FriendRequest;
                friendsCheck = FriendshipAsFRSender.Friends;
            }
            //No FR and friendship between the logged in user and the profile user
            else if (FriendshipAsFRReceiver == null && FriendshipAsFRSender == null)
            {
                friendRequest = false;
                friendsCheck = false;
            }
            
            return View(new ProfileDetailViewModel
            {
                User = user,
                MessageItems = _dbcontext.MessageItems
                               .Where(m => m.MessageReceiverId == Id)
                               .OrderByDescending(m => m.messageTime)
                               .Take(5).ToList(),
                FriendStatus = friendRequest,
                Friends = friendsCheck
            });
        }

        // GET: Profile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profile/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Profile/Edit/
        public ActionResult Edit(string Id)
        {
            var User = _dbcontext.Users.SingleOrDefault(u => u.Id == Id);
            if (User == null)
                return HttpNotFound();
            return View(User);
        }

        [HttpPost]
        public ActionResult AssignFriendCategory(int FriendModelId, string FriendRequestSide)
        {
            var FriendCategoryName = Request.Form["ddlFriendCategoryName"].ToString();

            var FriendModel = _dbcontext.FriendsModels.SingleOrDefault(f => f.Id == FriendModelId);

            var LoggedInUserId = User.Identity.GetUserId();
            
            var FriendCategory = _dbcontext.FriendCategories.SingleOrDefault(f => f.CategoryOwnerId == LoggedInUserId && f.CategoryName== FriendCategoryName);           
            var FriendCategoryId = FriendCategory.Id;


            if (FriendRequestSide == "ProfileOwner")
            {
                FriendModel.ProfileOwnerCategoryId = FriendCategoryId;
            }
            else if(FriendRequestSide == "ProfileVisitor")
            {
                FriendModel.ProfileVisitorCategoryId = FriendCategoryId;
            }
            _dbcontext.SaveChanges();

            return RedirectToAction("FriendsDetails", "Profile");
        }

        [HttpPost]
        public ActionResult Save(ApplicationUser User, HttpPostedFileBase file)
        {
            if (User.Id == "")
            {
                _dbcontext.Users.Add(User);
            }
            else
            {
                var userInDb = _dbcontext.Users.Single(u => u.Id == User.Id);
                userInDb.FirstName = User.FirstName;
                userInDb.LastName = User.LastName;
                userInDb.Description = User.Description;
                userInDb.Gender = User.Gender;
                userInDb.LookingForGender = User.LookingForGender;
                userInDb.BirthDate = User.BirthDate;

                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        file.SaveAs(HttpContext.Server.MapPath("~/Images/") + file.FileName);
                        userInDb.ImagePath = file.FileName;
                    }
                }
            }

            _dbcontext.SaveChanges();
            return RedirectToAction("Details", "Profile", new { Id = User.Id });
        }

        // POST: Profile/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Profile/Delete/5
        public ActionResult Delete(string Id)
        {
            var applicationUser = _dbcontext.Users.SingleOrDefault(u => u.Id == Id);
            if (applicationUser == null)
                return HttpNotFound();
            return View(applicationUser);
        }

        // POST: Profile/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        public ActionResult SearchResult(string SearchInput)
        {
            List<ApplicationUser> EmailMatched = _dbcontext.Users.Where(u => u.UserName.Equals(SearchInput) && u.IsActive).ToList();

            List<ApplicationUser> FirstNameMatched = _dbcontext.Users.Where(u => (SearchInput.Contains(u.FirstName) || u.FirstName.Contains(SearchInput)) && u.IsActive).ToList();

            List<ApplicationUser> LastNameMatched = _dbcontext.Users.Where(u => (SearchInput.Contains(u.LastName)|| u.LastName.Contains(SearchInput)) && u.IsActive).ToList();

            //Union the lists together, get disctinct users
            List<ApplicationUser> SearchResult = EmailMatched.Union(FirstNameMatched).Union(LastNameMatched).ToList();

            return View(SearchResult);
        }

        public ActionResult MyMatches(string id)
        {
            var LoggedInUser = _dbcontext.Users.SingleOrDefault(u => u.Id == id);

            var AllOtherUsers = _dbcontext.Users.Where(u => u.Id != id).OrderBy(u =>u.UserName);
            var MatchedUsers = new List<ApplicationUser>();

            //loop through
            var matchedUsers = new List<MatchResultForListViewModel>();

            foreach (var otherUser in AllOtherUsers)
            {
                if (Match(LoggedInUser, otherUser))
                {
                    matchedUsers.Add(new MatchResultForListViewModel
                    {
                        MatchedUserId = otherUser.Id,
                        MatchedUserFullName = otherUser.FirstName + " " + otherUser.LastName,
                        MatchedUserDescription = otherUser.Description,
                        MatchedUserEmail = otherUser.Email
                    });
                }
            }

            return View(matchedUsers);
        }

        // A method that returns true if two users match with each other
        public bool Match(ApplicationUser user1, ApplicationUser user2)
        {
            bool Match;
            //matching rule: user1's gender matches what user2 is looking for and vice versa
            if (user1.LookingForGender == user2.Gender && user2.LookingForGender == user1.Gender)
            {
                Match = true;
            }
            else
            {
                Match = false;
            }

            return Match;
        }

        public ActionResult FriendRequestPartialView()
        {
            var loggedInUserId = User.Identity.GetUserId();

            var nrFriendRequests = _dbcontext.FriendsModels
                                .Where(f => !f.Friends && f.FriendRequest && f.ProfileOwnerId == loggedInUserId && f.ProfileOwner.IsActive && f.ProfileVisitor.IsActive)
                                .Count();
            ViewBag.NrFriendRequests = nrFriendRequests;
            return PartialView();
        }
        
        [Authorize]
        public ActionResult FriendsDetails()
        {
            var LoggedInUserId = User.Identity.GetUserId();

            //A list of FriendModels where prop Friends is true for active users
            var friendsAsFRSender = _dbcontext.FriendsModels
                                    .Where(x => x.ProfileVisitorId == LoggedInUserId && x.Friends && x.ProfileOwner.IsActive && x.ProfileVisitor.IsActive)
                                    .ToList();

            var friendsAsFRReceiver = _dbcontext.FriendsModels
                                    .Where(x => (x.ProfileOwnerId == LoggedInUserId) && x.Friends && x.ProfileOwner.IsActive && x.ProfileVisitor.IsActive)
                                    .ToList();
            
            var friendListViewModels = new List<FriendListViewModel>();

            if (friendsAsFRSender != null)
            {
                if (friendsAsFRSender.Any())
                {
                    foreach (var item in friendsAsFRSender)
                    {
                        if (item.ProfileVisitorCategory != null)
                        {
                            friendListViewModels.Add(new FriendListViewModel
                            {
                                FriendModelId = item.Id,
                                FriendFullName = item.ProfileOwner.FirstName + " " + item.ProfileOwner.LastName,
                                CategoryName = item.ProfileVisitorCategory.CategoryName,
                                FriendUserId = item.ProfileOwnerId,
                                FriendRequestSide = "ProfileVisitor"
                            });
                        }
                        else
                        {
                            friendListViewModels.Add(new FriendListViewModel
                            {
                                FriendModelId = item.Id,
                                FriendFullName = item.ProfileOwner.FirstName + " " + item.ProfileOwner.LastName,
                                FriendUserId = item.ProfileOwnerId,
                                FriendRequestSide = "ProfileVisitor"
                            });
                        }
                    }
                }
            }

            if (friendsAsFRReceiver != null)
            {
                if (friendsAsFRReceiver.Any())
                {
                    foreach (var item in friendsAsFRReceiver)
                    {
                        if (item.ProfileOwnerCategory != null)
                        {
                            friendListViewModels.Add(new FriendListViewModel
                            {
                                FriendModelId = item.Id,
                                FriendFullName = item.ProfileVisitor.FirstName + " " + item.ProfileVisitor.LastName,
                                CategoryName = item.ProfileOwnerCategory.CategoryName,
                                FriendUserId = item.ProfileVisitorId,
                                FriendRequestSide = "ProfileOwner"
                            });
                        }
                        else
                        {
                            friendListViewModels.Add(new FriendListViewModel
                            {
                                FriendModelId = item.Id,
                                FriendFullName = item.ProfileVisitor.FirstName + " " + item.ProfileVisitor.LastName,
                                FriendUserId = item.ProfileVisitorId,
                                FriendRequestSide = "ProfileOwner"

                            });
                        }
                    }
                }
            }

            var orderedFriendListViewModels = friendListViewModels.OrderBy(f => f.CategoryName).ToList();
                
            //A list of FriendModels where prop FriendRequest is true for active users
            var friendRequests = _dbcontext.FriendsModels.Where(x => x.ProfileOwnerId == LoggedInUserId && !x.Friends && x.FriendRequest && x.ProfileOwner.IsActive && x.ProfileVisitor.IsActive).ToList();

            //A List of FriendCategories 
            var friendCategories = _dbcontext.FriendCategories.Where(x => x.CategoryOwnerId == LoggedInUserId).ToList();

            return View(new FriendsFriendRequestsViewModel
            {
                FriendListViewModels = orderedFriendListViewModels,
                FriendRequests = friendRequests,
                FriendCategories = friendCategories
            });
        }
        
        public class XmlResult : ActionResult
        {
            private readonly object _data;
            public XmlResult(object data)
            {
                _data = data;
            }

            public override void ExecuteResult(ControllerContext context)
            {
                if (_data != null)
                {
                    var response = context.HttpContext.Response;
                    response.ContentType = "text/xml";
                    var serializer = new XmlSerializer(_data.GetType());
                    serializer.Serialize(response.OutputStream, _data);
                }
            }
        }
        
        public ActionResult ExportUserData()
        {
            var UserId = User.Identity.GetUserId();
            var user = _dbcontext.Users.SingleOrDefault(u => u.Id == UserId);

            //latest vists to logged-in user's profile
            var latestProfileVisits = _dbcontext.ProfileVisits
                            .Where(p => p.ProfileUserId == UserId && p.VisitorUserId != UserId)
                            .OrderByDescending(p => p.VisitDateTime)
                            .Take(5).ToList();

            var exporProfileVisits = new List<ExporProfileVisit>();


            foreach (var item in latestProfileVisits)
            {
                exporProfileVisits.Add(new ExporProfileVisit
                {
                    VisitorUserUserName = item.VisitorUser.UserName,
                    VisitDateTime = item.VisitDateTime
                });
            };


            //messages to and from logged-in user
            var messageItems = _dbcontext.MessageItems
                           .Where(m => m.MessageReceiverId == UserId || m.MessageSenderId == UserId)
                           .OrderByDescending(m => m.messageTime)
                           .Take(5).ToList();

            var exportMessageViewModels = new List<ExportMessageViewModel>();

            foreach (var item in messageItems)
            {
                exportMessageViewModels.Add(new ExportMessageViewModel
                {
                    MessageSenderUserName = item.MessageSender.UserName,
                    MessageReceiverUserName = item.MessageReceiver.UserName,
                    MessageTime = item.messageTime,
                    Text= item.Text
                });
            };

            //Friend list
            var friendsModels = _dbcontext.FriendsModels.Where(f => f.ProfileOwnerId == UserId || f.ProfileVisitorId == UserId && f.Friends).ToList();
                
            var exportFriends = new List<ExportFriend>();
            string friendUserName;

            foreach (var item in friendsModels)
            {
                if(item.ProfileOwnerId == UserId)
                {
                    friendUserName = item.ProfileVisitor.UserName;
                }
                else
                {
                    friendUserName = item.ProfileOwner.UserName;
                }

                exportFriends.Add(new ExportFriend
                {
                    FriendUserName = friendUserName
                });
            };



            var exportUserDataViewModel = new ExportUserDataViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                LookingForGender = user.LookingForGender,
                Description = user.Description,
                BirthDate = user.BirthDate,
                Email = user.Email,

                ExportMessageViewModels = exportMessageViewModels,
                ExportProfileVisits = exporProfileVisits,
                ExportFriends = exportFriends
            };

            var txtFilePathName = HttpContext.Server.MapPath("~/XMLFiles/UserDataXML_") + user.FirstName + "_" + user.LastName + ".txt";

            var xml_serializer = new System.Xml.Serialization.XmlSerializer(typeof(ExportUserDataViewModel));
            using (var s = System.IO.File.Open(txtFilePathName, System.IO.FileMode.Create))
            {
                xml_serializer.Serialize(s, exportUserDataViewModel);
                s.Flush();
                s.Close();
            }

            //return View();
            string contentType = "text/plain";
            return File(txtFilePathName, contentType, Path.GetFileName(txtFilePathName));
        }

        public class MinimumAgeAttribute : ValidationAttribute
        {
            int _minimumAge;

            public MinimumAgeAttribute(int minimumAge)
            {
                _minimumAge = minimumAge;
            }

            public override bool IsValid(object value)
            {

                DateTime date;
                if (DateTime.TryParse(value.ToString(), out date))
                {
                    return date.AddYears(_minimumAge) < DateTime.Now;
                }

                return false;

            }
        }
    }
}
