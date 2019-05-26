using Lab2B.Models;
using Lab2B.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
   

        public class UsersServiceTests
        {
            private IOptions<AppSettings> config;
                    
            [SetUp]
        public void Setup()
        {
            config = Options.Create(new AppSettings
            {
                Secret = "dsadggggggggghhhhhhh"
            });
        }

         /// <summary>
         /// TODO: AAA - Arrange, Act, Assert
         /// </summary>
            [Test]
            public void ValidRegisterShouldCreateANewUser()
            {
                var options = new DbContextOptionsBuilder<MoviesDbContext>()
                  .UseInMemoryDatabase(databaseName: nameof(ValidRegisterShouldCreateANewUser))// "ValidRegisterShouldCreateANewUser")
                  .Options;

                using (var context = new MoviesDbContext(options))
                {
                    var userService = new UserService(context, config);
                    var added = new Lab2B.ViewModels.RegisterPostModel
                    {
                        Email = "a@a.b",
                        FirstName = "fdsfsdfs",
                        LastName = "fdsfs",
                        Password = "1234567",
                        Username = "test_username"

        };
                    var result = userService.Register(added);

                    Assert.IsNotNull(result);
                    Assert.AreEqual(added.Username, result.Username);
                }
            }
    }
}