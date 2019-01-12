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

        // $.get('/API/Friends/FriendRequest/Accept?FriendModelId=NN')
        [Route("FriendRequest/Accept")]
        [HttpGet]
        public void AcceptFriendRequest(int FriendModelId)
        {
            var friendModel = _dbcontext.FriendsModels.FirstOrDefault(f => f.Id == FriendModelId);
            if (friendModel != null)
            {
                friendModel.Friends = true;
                _dbcontext.SaveChanges();
            }
        }

        // $.get('/API/Friends/FriendRequest/Decline?FriendModelId=NN')
        [Route("FriendRequest/Decline")]
        [HttpGet]
        public void DeclineFriendRequest(int FriendModelId)
        {
            var friendModel = _dbcontext.FriendsModels.FirstOrDefault(f => f.Id == FriendModelId);
            if (friendModel != null)
            {
                friendModel.FriendRequest = false;
                _dbcontext.SaveChanges();
            }
        }
        
    }
}
