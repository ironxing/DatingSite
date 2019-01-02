﻿using DatingSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace DatingSite.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext _dbcontext;

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

        // GET: Profile/Details/5
        [Authorize]
        public ActionResult Details(string Id)
        {
            //var userId = User.Identidy.GetUserId();
            var User = _dbcontext.Users.SingleOrDefault(u => u.Id == Id);
            if (User == null)
                return HttpNotFound();
            return View(User);
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

        // GET: Profile/Edit/5
        public ActionResult Edit(string Id)
        {
            var User = _dbcontext.Users.SingleOrDefault(u => u.Id == Id);
            if (User == null)
                return HttpNotFound();
            return View(User);
        }

        [HttpPost]
        public ActionResult Save(ApplicationUser User)
        {
            if(User.Id == "")
            {
                _dbcontext.Users.Add(User);
            }
            else
            {
                var userInDb = _dbcontext.Users.Single(u => u.Id == User.Id);
                //Mapper.Map(User, userInDb);
                userInDb.FirstName = User.FirstName;
                userInDb.LastName = User.LastName;
                userInDb.Description = User.Description;
            }
            _dbcontext.SaveChanges();
            return RedirectToAction("Details","Profile", new { Id = User.Id });
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
    }
}