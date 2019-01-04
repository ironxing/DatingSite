using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DatingSite.Models
{
    public class DatingSiteDBinitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var passwordHasher = new PasswordHasher();

            var user1 = new ApplicationUser

            {
                UserName = "123@hotmail.com",
                Email = "123@hotmail.com",
                PasswordHash = passwordHasher.HashPassword("Abc123@@"),
                FirstName = "Danny",
                LastName = "Smith",
                Description = "A happy person",
                ImagePath = "Avatar1.png"
            };
            UserManager.Create(user1);

            var user2 = new ApplicationUser

            {
                UserName = "223@hotmail.com",
                Email = "223@hotmail.com",
                PasswordHash = passwordHasher.HashPassword("Abc123@@"),
                FirstName = "Marie",
                LastName = "Svenson",
                Description = "A energetic person",
                ImagePath = "Avatar2.png"
            };
            UserManager.Create(user2);

            var user3 = new ApplicationUser

            {
                UserName = "323@hotmail.com",
                Email = "323@hotmail.com",
                PasswordHash = passwordHasher.HashPassword("Abc123@@"),
                FirstName = "Andreas",
                LastName = "Larsson",
                Description = "A nice person",
                ImagePath = "Avatar3.png"
            };
            UserManager.Create(user3);
        }
    }
}