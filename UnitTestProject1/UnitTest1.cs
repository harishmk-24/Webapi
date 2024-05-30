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

            // Add test data to the in-memory database
            using (var context = new context(options))
            {
                context.emps.AddRange(new List<Emp>
                {
                    new Emp { id = 1, Name = "John" },
                    new Emp { id = 2, Name = "Alice" }
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
            // Arrange
            using (var context = new context(options))
            {
                var controller = new ValuesController(context);

                // Act
                var result = controller.GetUser(1);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.id);
            }
        }

        [TestMethod]
        public void AddUser_AddsUser()
        {
            // Arrange
            using (var context = new context(options))
            {
                var controller = new ValuesController(context);
                var newUser = new Emp { id = 3, Name = "Bob" };

                // Act
                var result = controller.AddUser(newUser);

                // Assert
                Assert.AreEqual("User Added", result);
                Assert.AreEqual(3, context.emps.Count());
            }
        }

        [TestMethod]
        public void UpdateUser_UpdatesUser()
        {
            // Arrange
            using (var context = new context(options))
            {
                var controller = new ValuesController(context);
                var existingUser = context.emps.FirstOrDefault(u => u.id == 1);
                existingUser.Name = "UpdatedName";

                // Act
                var result = controller.UpdateUser(existingUser);

                // Assert
                Assert.AreEqual("User Updated", result);
                Assert.AreEqual("UpdatedName", context.emps.FirstOrDefault(u => u.id == 1).Name);
            }
        }

        [TestMethod]
        public void DeleteUser_RemovesUser()
        {
            // Arrange
            using (var context = new context(options))
            {
                var controller = new ValuesController(context);

                // Act
                var result = controller.DeleteUser(1);

                // Assert
                Assert.AreEqual("User Deleted", result);
                Assert.AreEqual(1, context.emps.Count());
            }
        }
    }
}