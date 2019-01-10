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

            var Friendship = _dbcontext.FriendsModels.FirstOrDefault(f => f.ProfileOwnerId == Id && f.ProfileVisitorId == VisitorId);


            var user = _dbcontext.Users.SingleOrDefault(u => u.Id == Id);
            if (user == null)
                return HttpNotFound();

            bool friendRequest;
            bool friendsCheck;

            if (Friendship != null)


            {
                friendRequest = Friendship.FriendRequest;
                friendsCheck = Friendship.Friends;
            }
            else
            {
                friendRequest = false;
                friendsCheck = false;

            }

            return View(new ProfileDetailViewModel
            {
                User = user,
                LatestProfileVisits = _dbcontext.ProfileVisits
                                .Where(p => p.ProfileUserId == Id && p.VisitorUserId != Id)
                                .OrderByDescending(p => p.VisitDateTime)
                                .Take(5).ToList(),
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

            List<ApplicationUser> FirstNameMatched = _dbcontext.Users.Where(u => SearchInput.Contains(u.FirstName) && u.IsActive).ToList();

            List<ApplicationUser> LastNameMatched = _dbcontext.Users.Where(u => SearchInput.Contains(u.LastName) && u.IsActive).ToList();
            
            //Union the lists together, get disctinct users
            List<ApplicationUser> SearchResult = EmailMatched.Union(FirstNameMatched).Union(LastNameMatched).ToList();

            return View(SearchResult);
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
        
        public ActionResult FriendsDetails()
        {
            var LoggedInUserId = User.Identity.GetUserId();

            //A list of FriendModels where prop Friends is true for active users
            var friends = _dbcontext.FriendsModels.Where(x => (x.ProfileVisitorId == LoggedInUserId || x.ProfileOwnerId == LoggedInUserId) && x.Friends && x.ProfileOwner.IsActive && x.ProfileVisitor.IsActive).ToList();

            //A list of FriendModels where prop FriendRequest is true for active users
            var friendRequests = _dbcontext.FriendsModels.Where(x => x.ProfileOwnerId == LoggedInUserId && !x.Friends && x.FriendRequest && x.ProfileOwner.IsActive && x.ProfileVisitor.IsActive).ToList();
            
            return View(new FriendsFriendRequestsViewModel
            {
                Friends = friends,
                FriendRequests = friendRequests
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
                Description = user.Description,
                Email = user.Email,

                ExportMessageViewModels = exportMessageViewModels,
                ExporProfileVisits = exporProfileVisits,
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

            return View();
        }
    }
}
