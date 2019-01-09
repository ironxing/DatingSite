using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DatingSite.Models;
using DatingSite.Models.ViewModels;

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

        [Route("VISIT/GetVisitors")]
        public IEnumerable<VisitViewModel> GetLatestVisits()
        {
            //String profileUserId
            //var LatestProfileVisits = _dbcontext.ProfileVisits
            //                .Where(p => p.ProfileUserId == profileUserId)
            //                .OrderByDescending(p => p.VisitDateTime)
            //                .Take(5).ToList();
            //return LatestProfileVisits;
            
            var Visits = _dbcontext.ProfileVisits.ToList();

            var VisitViewModels = new List<VisitViewModel>();
            
            for (int i = 0; i < Visits.Count; i++)
            {
                var senderId = Visits[i].VisitorUserId;
                var receiverId = Visits[i].ProfileUserId;
                var visitDateTime = Visits[i].VisitDateTime;

                VisitViewModels.Add(new VisitViewModel
                {
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    VisitDateTime = visitDateTime
                });
            }

            return VisitViewModels;
        }
    }
}
