using DatingSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DatingSite.Controllers
{
    public abstract class ApplicationMasterController : Controller
    {
        public ApplicationDbContext _dbcontext;

        public ApplicationMasterController()
        {
            _dbcontext = new ApplicationDbContext();
            ViewData["ProfileVisits"] = _dbcontext.MessageItems.ToList();
            ViewData.ToList();
            ViewBag.test = "walala";
        }
    }
}