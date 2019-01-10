using DatingSite.Models;
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
    }

}
