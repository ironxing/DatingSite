using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DatingSite.Models;

namespace DatingSite.Controllers.APIControllers
{
    [RoutePrefix("API/ProfileVisits")]
    public class ProfileVisitAPIController : ApiController
    {
        private ApplicationDbContext _dbcontext;

        public ProfileVisitAPIController()
        {
            _dbcontext = new ApplicationDbContext();
        }


        // GET/API/ProfileVisit
        [Route("VISIT/Add")]
        [HttpGet]
        public void AddProfileVisit(String profileUserId, String visitorUserId)
        {
            var DateTimeNow = DateTime.Now;
            _dbcontext.ProfileVisits.Add(
                new ProfileVisit
                {
                    VisitDateTime = DateTimeNow,
                    ProfileUserId = profileUserId,
                    VisitorUserId = visitorUserId
                });
            _dbcontext.SaveChanges();
        }
    }
}
