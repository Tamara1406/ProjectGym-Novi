using Domain;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using SystemOperations;
using SystemOperations.UserSO;

namespace GymTests.ServerControllerTests
{
    [TestClass]
    public class UserTests
    {

        private ServerController serverController;

        private BaseSO so = A.Fake<BaseSO>();
        public UserTests()
        {
            serverController = new ServerController();
        }

        [TestMethod]
        public void Login_ReturnSuccess()
        {

            A.CallTo(() => so.ExecuteOperation());
            A.CallTo(() => so.ResultList).Returns(new List<AbsEntity>
            {
                new User { Username = "ime", Password = "ime123" }
            });

            // Act
            var result = serverController.Login(new User { Username = "ime", Password = "ime123" }, so);

            // Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void Login_ReturnError()
        {

            A.CallTo(() => so.ExecuteOperation());
            A.CallTo(() => so.ResultList).Returns(new List<AbsEntity>
            {
                new User { Username = "ime", Password = "ime123" }
            });

            // Act
            var result = serverController.Login(new User { Username = "ime", Password = "ime333" }, so);

            // Assert
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void RegisterUser_ReturnSuccess()
        {
            // Arrange
            var newUser = new User { Username = "ime", Password = "ime123", FirstName = "ime", LastName = "prezime", Email = "ime@gmail.com" };

            // Act
            var result = serverController.RegisterUser(newUser, so);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RegisterUser_With_InvalidData()
        {
            // Arrange
            var invalidUser = new User { Username = "", Password = "", FirstName = "", LastName = "", Email = "" };

            // Act
            var result = serverController.RegisterUser(invalidUser, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RegisterUser_ReturnsFalse_WhenUsernameIsNull()
        {
            // Arrange
            var newUser = new User
            {
                Username = null,
                Password = "validPassword",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            // Act
            var result = serverController.RegisterUser(newUser, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RegisterUser_ReturnsFalse_WhenPasswordIsEmpty()
        {
            // Arrange
            var newUser = new User
            {
                Username = "johndoe",
                Password = "",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            // Act
            var result = serverController.RegisterUser(newUser, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RegisterUser_ReturnsFalse_WhenFirstNameIsNull()
        {
            // Arrange
            var newUser = new User
            {
                Username = "johndoe",
                Password = "validPassword",
                FirstName = null,
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            // Act
            var result = serverController.RegisterUser(newUser, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RegisterUser_ReturnsFalse_WhenLastNameIsEmpty()
        {
            // Arrange
            var newUser = new User
            {
                Username = "johndoe",
                Password = "validPassword",
                FirstName = "John",
                LastName = "",
                Email = "john.doe@example.com"
            };

            // Act
            var result = serverController.RegisterUser(newUser, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RegisterUser_ReturnsFalse_WhenEmailIsNull()
        {
            // Arrange
            var newUser = new User
            {
                Username = "johndoe",
                Password = "validPassword",
                FirstName = "John",
                LastName = "Doe",
                Email = null
            };

            // Act
            var result = serverController.RegisterUser(newUser, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RegisterUser_ReturnsTrue_WhenAllFieldsAreValid()
        {
            // Arrange
            var newUser = new User
            {
                Username = "johndoe",
                Password = "validPassword",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            // Act
            var result = serverController.RegisterUser(newUser, so);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetUserByUsername_ReturnSuccess()
        {
            // Arrange
            var user = new User { Username = "existingUsername" };
            var expectedResult = new User { Username = "existingUsername", Password = "ime123", FirstName = "ime", LastName = "prezime", Email = "ime@gmail.com" };

            A.CallTo(() => so.ExecuteOperation());
            A.CallTo(() => so.Result).Returns(expectedResult);


            // Act
            var result = serverController.GetUserByUsername(user, so);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void GetUserByUsername_Returns_Null_When_Not_Found()
        {
            // Arrange
            var user = new User { Username = "nonExistingUsername" };

            A.CallTo(() => so.ExecuteOperation());
            A.CallTo(() => so.Result).Returns((User)null); 

            var serverController = new ServerController();

            // Act
            var result = serverController.GetUserByUsername(user, so);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void UpdateUser_ReturnSuccess()
        {
            // Arrange
            var newData = new User { Username = "ime", Password = "ime123", FirstName = "ime", LastName = "prezime", Email = "ime@gmail.com" };

            // Act
            var result = serverController.UpdateUser(newData, so);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateUser_With_InvalidData()
        {
            // Arrange
            var invalidUser = new User { Username = "", Password = "", FirstName = "", LastName = "", Email = "" };

            // Act
            var result = serverController.UpdateUser(invalidUser, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateUser_ReturnsFalse_WhenUsernameIsNull()
        {
            // Arrange
            var user = new User
            {
                Username = null,
                Password = "validPassword",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            // Act
            var result = serverController.UpdateUser(user, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateUser_ReturnsFalse_WhenPasswordIsEmpty()
        {
            // Arrange
            var user = new User
            {
                Username = "johndoe",
                Password = "",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            // Act
            var result = serverController.UpdateUser(user, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateUser_ReturnsFalse_WhenFirstNameIsNull()
        {
            // Arrange
            var user = new User
            {
                Username = "johndoe",
                Password = "validPassword",
                FirstName = null,
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            // Act
            var result = serverController.UpdateUser(user, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateUser_ReturnsFalse_WhenLastNameIsEmpty()
        {
            // Arrange
            var user = new User
            {
                Username = "johndoe",
                Password = "validPassword",
                FirstName = "John",
                LastName = "",
                Email = "john.doe@example.com"
            };

            // Act
            var result = serverController.UpdateUser(user, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateUser_ReturnsFalse_WhenEmailIsNull()
        {
            // Arrange
            var user = new User
            {
                Username = "johndoe",
                Password = "validPassword",
                FirstName = "John",
                LastName = "Doe",
                Email = null
            };

            // Act
            var result = serverController.UpdateUser(user, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateUser_ReturnsTrue_WhenAllFieldsAreValid()
        {
            // Arrange
            var user = new User
            {
                Username = "johndoe",
                Password = "validPassword",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            // Act
            var result = serverController.UpdateUser(user, so);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAllUsers_ReturnSuccess()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Username = "user1" },
                new User { Username = "user2" }
            };
            A.CallTo(() => so.ExecuteOperation()).Invokes(() =>
            {
                so.ResultList = users.Cast<AbsEntity>().ToList();
            });


            // Act
            var result = serverController.GetAllUsers(so);

            // Assert
            Assert.AreEqual(users.Count, result.Count);
            foreach (var user in users)
            {
                Assert.IsTrue(result.Any(u => u.Username == user.Username));
            }
        }

        [TestMethod]
        public void GetAllUsers_Returns_Null_When_Not_Found()
        {
            // Arrange
            A.CallTo(() => so.ExecuteOperation()).Invokes(() =>
            {
                so.ResultList = new List<AbsEntity>();
            });


            // Act
            var result = serverController.GetAllUsers(so);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        
        

    }
}
