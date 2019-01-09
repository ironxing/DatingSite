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
    [RoutePrefix("API/Friends")]
    public class FriendsAPIController : ApiController

    {
        private ApplicationDbContext _dbcontext;

        public FriendsAPIController()
        {
            _dbcontext = new ApplicationDbContext();
        }
        [Route("Friend/ADD")]
        [HttpGet]
        public void sendFriendRequest(string Id)
        {
            var VisitorId = User.Identity.GetUserId();
            var Friendship = _dbcontext.FriendsModels.FirstOrDefault(f => f.ProfileOwnerId == Id && f.ProfileVisitorId == VisitorId);

            if (Friendship != null)
            {
                Friendship.FriendRequest = true;

            }

            else
            {
                _dbcontext.FriendsModels.Add(
                new FriendsModel
                {
                    ProfileOwnerId = Id,
                    ProfileVisitorId = VisitorId,
                    FriendRequest = true

                });
            }

            _dbcontext.SaveChanges();
        }

    }
}
