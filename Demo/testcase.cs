using Demo.Controllers;
using Demo.Models;
using Demo.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Tests
{
    [TestClass]
    public class ValuesControllerTests
    {
        private DbContextOptions<context> options;

        [TestInitialize]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<context>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;

            using (var context = new context(options))
            {
                context.emps.AddRange(new List<Emp>
                {
                    new Emp { id = 1, name = "Ajay", number=1234, address="27-453", city="try" },
                    new Emp { id = 2, name = "Vijay" ,number=987, address="2-3421", city="ctr" }
                });
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void GetUsers_ReturnsAllUsers()
        {
            // Arrange
            using (var context = new context(options))
            {
                var controller = new ValuesController(context);

                // Act
                var result = controller.GetUsers();

                // Assert
                Assert.AreEqual(2, result.Count);
            }
        }

        [TestMethod]
        public void GetUser_ReturnsUserById()
        {
            
            using (var context = new context(options))
            {
                var controller = new ValuesController(context);

                
                var result = controller.GetUser(1);

                
                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.id);
            }
        }

        [TestMethod]
        public void AddUser_AddsUser()
        {
            
            using (var context = new context(options))
            {
                var controller = new ValuesController(context);
                var newUser = new Emp { id = 3, name = "Bob" };

               
                var result = controller.AddUser(newUser);

               
                Assert.AreEqual("User Added", result);
                Assert.AreEqual(3, context.emps.Count());
            }
        }

        [TestMethod]
        public void UpdateUser_UpdatesUser()
        {
            
            using (var context = new context(options))
            {
                var controller = new ValuesController(context);
                var existingUser = context.emps.FirstOrDefault(u => u.id == 1);
                existingUser.name = "Sabrina";

                var result = controller.UpdateUser(existingUser);

               
                Assert.AreEqual("User Updated", result);
                Assert.AreEqual("Sabrina", context.emps.FirstOrDefault(u => u.id == 1).name);
            }
        }

        [TestMethod]
        public void DeleteUser_RemovesUser()
        {
            
            using (var context = new context(options))
            {
                var controller = new ValuesController(context);

                
                var result = controller.DeleteUser(1);

                
                Assert.AreEqual("User Deleted", result);
                Assert.AreEqual(1, context.emps.Count());
            }
        }
    }
}