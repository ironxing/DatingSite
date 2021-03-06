﻿using DatingSite.Models;
using DatingSite.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DatingSite.Controllers.APIControllers
{
    [RoutePrefix("API/Profile")]
    public class ProfileAPIController : ApiController
    {
        private ApplicationDbContext _dbcontext;

        public ProfileAPIController()
        {
            _dbcontext = new ApplicationDbContext();
        }

        // GET'/API/Profile/ToggerActiveStatus'
        [Route("ToggerActiveStatus")]
        [HttpGet]
        public void ToggleUserActiveStatus()
        {
            var UserId = User.Identity.GetUserId();
            var user = _dbcontext.Users.SingleOrDefault(u => u.Id == UserId);
            if (user != null)
            {
                user.IsActive = !user.IsActive;
            }
            _dbcontext.SaveChanges();
        }

        // GET'/API/Profile/DoWeMatch'
        [Route("DoWeMatch")]
        [HttpGet]
        public MatchResultViewModel DoWeMatch(string ProfileOwnerId)
        {
            var matchResult = new MatchResultViewModel();

            var LoggedInUserId = User.Identity.GetUserId();
            var LoggedInUser = _dbcontext.Users.SingleOrDefault(u => u.Id == LoggedInUserId);
            var ProfileOnwerUser = _dbcontext.Users.SingleOrDefault(u => u.Id == ProfileOwnerId);

            if (ProfileOnwerUser != null && LoggedInUser!= null)
            {

                matchResult.ProfileOwnerFullName = ProfileOnwerUser.FirstName + " " + ProfileOnwerUser.LastName;

                //matching rule: 
                if (ProfileOnwerUser.LookingForGender == LoggedInUser.Gender && LoggedInUser.LookingForGender == ProfileOnwerUser.Gender)
                {
                    matchResult.Match = true;
                }
                else
                {
                    matchResult.Match = false;
                }
            }
            return matchResult; //Return Full name of profile owner and match result
        }
    }
}
