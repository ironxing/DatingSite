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
                UserName = "MariaAndersson@hotmail.com",
                Email = "MariaAndersson@hotmail.com",
                PasswordHash = passwordHasher.HashPassword("Abc123@@"),
                FirstName = "Maria",
                LastName = "Andersson",
                Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula" +
                " eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur" +
                " ridiculus mus. Donec quam",
                ImagePath = "Avatar1.png",
                IsActive= true
            };
            UserManager.Create(user1);

            var user2 = new ApplicationUser

            {
                UserName = "WilliamLarsson@hotmail.com",
                Email = "WilliamLarsson@hotmail.com",
                PasswordHash = passwordHasher.HashPassword("Bbc123@@"),
                FirstName = "William",
                LastName = "Larsson",
                Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula" +
                " eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur" +
                " ridiculus mus. Donec quam",
                ImagePath = "Avatar2.png",
                IsActive = true
            };
            UserManager.Create(user2);

            var user3 = new ApplicationUser

            {
                UserName = "ElisabetGustafsson@hotmail.com",
                Email = "ElisabetGustafsson@hotmail.com",
                PasswordHash = passwordHasher.HashPassword("Cbc123@@"),
                FirstName = "Elisabet",
                LastName = "Gustafsson",
                Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula" +
                " eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur" +
                " ridiculus mus. Donec quam",
                ImagePath = "Avatar3.png",
                IsActive = true
            };
            UserManager.Create(user3);

            var user4 = new ApplicationUser

            {
                UserName = "AnnaPersson@hotmail.com",
                Email = "AnnaPersson@hotmail.com",
                PasswordHash = passwordHasher.HashPassword("Dbc123@@"),
                FirstName = "Anna",
                LastName = "Persson",
                Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. " +
                "Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus " +
                "et magnis dis parturient montes, nascetur ridiculus mus. Donec quam",
                ImagePath = "Avatar4.png",
                IsActive = false
            };
            UserManager.Create(user4);

            var user5 = new ApplicationUser

            {
                UserName = "KarlJohansson@hotmail.com",
                Email = "KarlJohansson@hotmail.com",
                PasswordHash = passwordHasher.HashPassword("Ebc123@@"),
                FirstName = "Karl",
                LastName = "Johansson",
                Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. " +
                "Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus " +
                "et magnis dis parturient montes, nascetur ridiculus mus. Donec quam",
                ImagePath = "Avatar5.png",
                IsActive = true
            };
            UserManager.Create(user5);

            var user6 = new ApplicationUser

            {
                UserName = "MagaretaLarsson@hotmail.com",
                Email = "MagaretaLarsson@hotmail.com",
                PasswordHash = passwordHasher.HashPassword("Fc123@@"),
                FirstName = "Magareta",
                LastName = "Larsson",
                Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. " +
                "Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus " +
                "et magnis dis parturient montes, nascetur ridiculus mus. Donec quam",
                ImagePath = "Avatar6.png",
                IsActive = true
            };
            UserManager.Create(user6);

            var user7 = new ApplicationUser

            {
                UserName = "JohanEriksson@hotmail.com",
                Email = "JohanEriksson@hotmail.com",
                PasswordHash = passwordHasher.HashPassword("Gbc123@@"),
                FirstName = "Johan",
                LastName = "Eriksson",
                Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula" +
                " eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur" +
                " ridiculus mus. Donec quam",
                ImagePath = "Avatar7.png",
                IsActive = true
            };
            UserManager.Create(user7);

            var user8 = new ApplicationUser

            {
                UserName = "KristinaNilsson@hotmail.com",
                Email = "KristinaNilsson@hotmail.com",
                PasswordHash = passwordHasher.HashPassword("Hbc123@@"),
                FirstName = "Kristina",
                LastName = "Nilsson",
                Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula" +
                " eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur" +
                " ridiculus mus. Donec quam",
                ImagePath = "Avatar8.png",
                IsActive = true
            };
            UserManager.Create(user8);

            var user9 = new ApplicationUser

            {
                UserName = "KarinOlsson@hotmail.com",
                Email = "KarinOlsson@hotmail.com",
                PasswordHash = passwordHasher.HashPassword("Ibc123@@"),
                FirstName = "Karin",
                LastName = "Olsson",
                Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula" +
                " eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur" +
                " ridiculus mus. Donec quam",
                ImagePath = "Avatar9.png",
                IsActive = true
            };
            UserManager.Create(user9);

            var user10 = new ApplicationUser

            {
                UserName = "HansPettersson@hotmail.com",
                Email = "HansPettersson@hotmail.com",
                PasswordHash = passwordHasher.HashPassword("Jbc123@@"),
                FirstName = "Hans",
                LastName = "Pettersson",
                Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula" +
                " eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur" +
                " ridiculus mus. Donec quam",
                ImagePath = "Avatar10.png",
                IsActive = true
            };
            UserManager.Create(user10);

            context.ProfileVisits.Add(new ProfileVisit
            {
                VisitDateTime = Convert.ToDateTime("2019-01-07 11:07:10"),
                ProfileUserId = user1.Id,
                VisitorUserId = user2.Id
            });

            context.ProfileVisits.Add(new ProfileVisit
            {
                VisitDateTime = Convert.ToDateTime("2019-01-08 11:07:10"),
                ProfileUserId = user3.Id,
                VisitorUserId = user2.Id
            });

            context.ProfileVisits.Add(new ProfileVisit
            {
                VisitDateTime = Convert.ToDateTime("2019-01-06 11:07:10"),
                ProfileUserId = user1.Id,
                VisitorUserId = user5.Id
            });

            context.ProfileVisits.Add(new ProfileVisit
            {
                VisitDateTime = Convert.ToDateTime("2019-01-05 11:07:10"),
                ProfileUserId = user7.Id,
                VisitorUserId = user2.Id
            });

            context.MessageItems.Add(new MessageItem
            {
                messageTime = DateTime.Parse("2019-01-09 11:07:09"),
                MessageReceiverId = user1.Id,
                MessageSenderId = user2.Id,
                Text = "Men gud vad fin du är!!"
            });

            context.MessageItems.Add(new MessageItem
            {
                messageTime = DateTime.Parse("2019-01-09 11:07:09"),
                MessageReceiverId = user2.Id,
                MessageSenderId = user1.Id,
                Text = "Hejj Hej!!"
            });

            context.MessageItems.Add(new MessageItem
            {
                messageTime = DateTime.Parse("2019-01-09 11:07:09"),
                MessageReceiverId = user3.Id,
                MessageSenderId = user8.Id,
                Text = "Men gud vad fin du är!!"
            });

            context.MessageItems.Add(new MessageItem
            {
                messageTime = DateTime.Parse("2019-01-09 11:07:09"),
                MessageReceiverId = user4.Id,
                MessageSenderId = user3.Id,
                Text = "hallå!!"
            });

            context.MessageItems.Add(new MessageItem
            {
                messageTime = DateTime.Parse("2019-01-09 11:07:09"),
                MessageReceiverId = user5.Id,
                MessageSenderId = user4.Id,
                Text = "Men gud vad fin du är!!"
            });

            context.MessageItems.Add(new MessageItem
            {
                messageTime = DateTime.Parse("2019-01-09 11:07:09"),
                MessageReceiverId = user6.Id,
                MessageSenderId = user5.Id,
                Text = "Är du glad?"
            });

            context.MessageItems.Add(new MessageItem
            {
                messageTime = DateTime.Parse("2019-01-09 11:07:09"),
                MessageReceiverId = user7.Id,
                MessageSenderId = user6.Id,
                Text = "Va lite glad!!"
            });

            context.MessageItems.Add(new MessageItem
            {
                messageTime = DateTime.Parse("2019-01-09 11:07:09"),
                MessageReceiverId = user8.Id,
                MessageSenderId = user2.Id,
                Text = "Men gud vad fin du är!!"
            });

            context.MessageItems.Add(new MessageItem
            {
                messageTime = DateTime.Parse("2019-01-09 11:07:09"),
                MessageReceiverId = user9.Id,
                MessageSenderId = user2.Id,
                Text = "Men gud vad fin du är!!"
            });

            context.MessageItems.Add(new MessageItem
            {
                messageTime = DateTime.Parse("2019-01-09 11:07:09"),
                MessageReceiverId = user10.Id,
                MessageSenderId = user2.Id,
                Text = "Finis!"
            });

            context.FriendsModels.Add(new FriendsModel
            {
                FriendRequest = true,
                ProfileOwnerId = user1.Id,
                ProfileVisitorId = user2.Id

            });

            context.FriendsModels.Add(new FriendsModel
            {
                FriendRequest = true,
                ProfileOwnerId = user2.Id,
                ProfileVisitorId = user3.Id

            });

            context.FriendsModels.Add(new FriendsModel
            {
                FriendRequest = true,
                ProfileOwnerId = user4.Id,
                ProfileVisitorId = user2.Id

            });

            context.FriendsModels.Add(new FriendsModel
            {
                FriendRequest = true,
                ProfileOwnerId = user5.Id,
                ProfileVisitorId = user2.Id

            });

            context.FriendsModels.Add(new FriendsModel
            {
                FriendRequest = true,
                ProfileOwnerId = user6.Id,
                ProfileVisitorId = user2.Id

            });

            context.FriendsModels.Add(new FriendsModel
            {
                Friends = true,
                ProfileOwnerId = user7.Id,
                ProfileVisitorId = user2.Id

            });

            context.FriendsModels.Add(new FriendsModel
            {
                Friends = true,
                ProfileOwnerId = user8.Id,
                ProfileVisitorId = user2.Id

            });

            context.FriendsModels.Add(new FriendsModel
            {
                Friends = true,
                ProfileOwnerId = user9.Id,
                ProfileVisitorId = user2.Id

            });
            context.FriendsModels.Add(new FriendsModel
            {
                Friends = true,
                ProfileOwnerId = user10.Id,
                ProfileVisitorId = user2.Id

            });
            base.Seed(context);


        }
    }
}