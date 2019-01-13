using System;
using System.Collections.Generic;
using System.Linq;

using System.Net;
using System.Net.Http;
using System.Web.Http;
using DatingSite.Models;
using DatingSite.Models.ViewModels;
using Microsoft.AspNet.Identity;
using MoreLinq;

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


        // GET/API/ProfileVisits/VISIT/Add
        [Route("VISIT/Add")]
        [HttpGet]
        public void AddProfileVisit(String profileUserId, String visitorUserId)
        {
            if(profileUserId!= visitorUserId) { 
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

        // GET/API/ProfileVisits/VISIT/GetVisitors
        [Route("VISIT/GetVisitors")]
        public HttpResponseMessage GetLatestVisits()
        {
            var LoggedInUserId = User.Identity.GetUserId();

            var LatestProfileVisits = _dbcontext.ProfileVisits
                            .Where(p => p.ProfileUserId == LoggedInUserId && p.VisitorUserId != LoggedInUserId)
                            .OrderByDescending(p => p.VisitDateTime)
                            .DistinctBy(p => p.VisitorUserId) //A method from MoreLinq library
                            .Take(5)
                            .ToList();
                            
            var visitorNamesViewModels = new List<VisitorNamesViewModel>();
            
            for (int i = 0; i < LatestProfileVisits.Count; i++)
            {
                var visitorId = LatestProfileVisits[i].VisitorUserId;
                var visitorFullName = LatestProfileVisits[i].VisitorUser.FirstName + " " + LatestProfileVisits[i].VisitorUser.LastName;
                var visitDateTime = LatestProfileVisits[i].VisitDateTime;

                visitorNamesViewModels.Add(new VisitorNamesViewModel
                {
                    VisitorId = visitorId,
                    VisitorFullName = visitorFullName,
                    VisitDateTime = visitDateTime
                });
            }

            //Format to json then return
            return Request.CreateResponse(HttpStatusCode.OK, visitorNamesViewModels, Configuration.Formatters.JsonFormatter);  
        }
    }
}
