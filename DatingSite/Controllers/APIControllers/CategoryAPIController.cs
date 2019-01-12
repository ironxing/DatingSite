using DatingSite.Models;
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
    [RoutePrefix("api/Category")]
    public class CategoryAPIController : ApiController
    {
        private ApplicationDbContext _dbcontext;

        public CategoryAPIController()
        {
            _dbcontext = new ApplicationDbContext();
        }

        // GET ('api/Category/Category/Add')
        [Route("Category/Add")]
        [HttpGet]
        public void AddCategory(string categoryName)
        {


            var categoryownerid = User.Identity.GetUserId();
            var categoryList = _dbcontext.FriendCategories
               .Where(x => x.CategoryOwnerId == categoryownerid)
               .OrderBy(x => x.CategoryName)
               .ToList();

            if (!categoryList.Any(x => x.CategoryName == categoryName))
            {
                _dbcontext.FriendCategories.Add(new FriendCategory
                {
                    CategoryName = categoryName,
                    CategoryOwnerId = categoryownerid,

                });
            }

            _dbcontext.SaveChanges();

        }

        // GET ('api/Category/Category/ShowList')
        [Route("Category/ShowList")]
        [HttpGet]
        public HttpResponseMessage ListCategories()
        {
            var categoryownerid = User.Identity.GetUserId();
            var categoryList = _dbcontext.FriendCategories
                .Where(x => x.CategoryOwnerId == categoryownerid)
                .OrderBy(x => x.CategoryName)
                .ToList();
            var friendCategoryViewModels = new List<FriendCategoryViewModel>();

            for (int i = 0; i < categoryList.Count; i++)
            {
                var categoryName = categoryList[i].CategoryName;
                friendCategoryViewModels.Add(new FriendCategoryViewModel
                {
                    CategoryName = categoryName
                });
            }
            return Request.CreateResponse(HttpStatusCode.OK, friendCategoryViewModels, Configuration.Formatters.JsonFormatter);
        }

    }
}