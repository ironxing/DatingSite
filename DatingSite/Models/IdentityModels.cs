using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DatingSite.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }

        //Custome validation on BirthDate, needs to before current date
        //[Remote("ValidateDateEqualOrGreater", HttpMethod = "Post", ErrorMessage = "Date isn't equal or greater than current date.")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public Gender? LookingForGender { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }

     //   public virtual ICollection<ProfileVisit> ProfileVisit { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public enum Gender
    {
        Male = 0,
        Female = 1,
        Other = 2
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ProfileVisit> ProfileVisits { get; set; }
        public DbSet<MessageItem> MessageItems { get; set; }
        public DbSet<FriendsModel> FriendsModels { get; set; }
        public DbSet<FriendCategory> FriendCategories { get; set; }

        public ApplicationDbContext()
            : base("DatingSiteDB", throwIfV1Schema: false)
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}