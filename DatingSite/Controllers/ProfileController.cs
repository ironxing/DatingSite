using DatingSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Drawing;
using DatingSite.Models.ViewModels;

namespace DatingSite.Controllers
{
    public class ProfileController : ApplicationMasterController
    {
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
            List<ApplicationUser> EmailMatched = _dbcontext.Users.Where(u => u.UserName.Equals(SearchInput) /*&& u.Id != User.Identity.GetUserId()*/).ToList();

            List<ApplicationUser> FirstNameMatched = _dbcontext.Users.Where(u => SearchInput.Contains(u.FirstName)/*&& u.Id != User.Identity.GetUserId()*/).ToList();

            List<ApplicationUser> LastNameMatched = _dbcontext.Users.Where(u => SearchInput.Contains(u.LastName) /*&& u.Id != User.Identity.GetUserId()*/).ToList();
            
            //Union the lists together, get disctinct users
            List<ApplicationUser> SearchResult = EmailMatched.Union(FirstNameMatched).Union(LastNameMatched).ToList();

            return View(SearchResult);
        }
    }
}
