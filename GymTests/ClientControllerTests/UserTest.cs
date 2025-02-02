using Client;
using CommunicationClasses;
using Domain;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemOperations;

namespace GymTests.ClientControllerTests
{
    [TestClass]
    public class UserTest
    {
        private ClientController clientController;

        private ClientCommunication fakeCommunication = A.Fake<ClientCommunication>();

        public UserTest()
        {
            clientController = new ClientController();
            clientController.Communication = fakeCommunication;
        }

        [TestMethod]
        public void LoginClient_Returns_1_When_Login_Successful()
        {
            // Arrange
            var user = new User { Username = "testUser", Password = "testPass" };

            var loginOkResponse = new Package
            {
                Operation = Operation.LoginOk
            };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(loginOkResponse);

            // Act
            var result = clientController.LoginClient(user);

            // Assert
            Assert.AreEqual(1, result);
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p => p.Operation == Operation.LoginClient && p.Item == user)))
                .MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void LoginClient_Returns_2_When_Already_Logged()
        {
            // Arrange
            var user = new User { Username = "testUser", Password = "testPass" };

            var alreadyLoggedResponse = new Package
            {
                Operation = Operation.AlreadyLogged
            };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(alreadyLoggedResponse);

            // Act
            var result = clientController.LoginClient(user);

            // Assert
            Assert.AreEqual(2, result);
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p => p.Operation == Operation.LoginClient && p.Item == user)))
                .MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void LoginClient_Returns_3_When_Login_Fails()
        {
            // Arrange
            var user = new User { Username = "testUser", Password = "testPass" };

            var loginFailedResponse = new Package
            {
                Operation = Operation.LoginNotOk
            };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(loginFailedResponse);

            // Act
            var result = clientController.LoginClient(user);

            // Assert
            Assert.AreEqual(3, result);
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p => p.Operation == Operation.LoginClient && p.Item == user)))
                .MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void GetUserByUsername_ReturnsUser_WhenUserIsFound()
        {
            // Arrange
            var userToFind = new User { Username = "testUser" };
            var expectedUser = new User { Username = "testUser", Password = "password123" };

            var response = new Package
            {
                Operation = Operation.GetUserByUsername,
                Item = expectedUser
            };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(response);

            // Act
            var result = clientController.GetUserByUsername(userToFind);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUser.Username, result.Username);
            Assert.AreEqual(expectedUser.Password, result.Password);

            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p => p.Operation == Operation.GetUserByUsername && p.Item == userToFind))).MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void GetUserByUsername_ReturnsNull_WhenUserIsNotFound()
        {
            // Arrange
            var userToFind = new User { Username = "testUser" };

            var response = new Package
            {
                Operation = Operation.GetUserByUsername,
                Item = null
            };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(response);

            // Act
            var result = clientController.GetUserByUsername(userToFind);

            // Assert
            Assert.IsNull(result);

            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p => p.Operation == Operation.GetUserByUsername && p.Item == userToFind))).MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void CreateAccount_ReturnsTrue_WhenRegisterOk()
        {
            // Arrange
            var user = new User { Username = "testUser", Password = "testPass" };

            var response = new Package { Operation = Operation.RegisterOk };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(response);

            // Act
            var result = clientController.CreateAccount(user);

            // Assert
            Assert.IsTrue(result);
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p => p.Operation == Operation.RegisterUser && p.Item == user))).MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void CreateAccount_ReturnsFalse_WhenRegisterNotOk()
        {
            // Arrange
            var user = new User { Username = "testUser", Password = "testPass" };


            var response = new Package { Operation = Operation.RegisterNotOk };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(response);

            // Act
            var result = clientController.CreateAccount(user);

            // Assert
            Assert.IsFalse(result);
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p => p.Operation == Operation.RegisterUser && p.Item == user))).MustHaveHappenedOnceExactly();
        }

        //[Fact]
        //public void SaveAccount_ReturnsTrue_WhenUserUpdateOk()
        //{
        //    // Arrange
        //    var user = new User { Username = "testUser", Password = "testPass" };

        //    var response = new Package
        //    {
        //        Operation = Operation.UserUpdateOk
        //    };

        //    A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(response);
        //    A.CallTo(() => fakeValidator.CheckUserData(user)).Returns(true);

        //    // Act
        //    var result = clientController.SaveAccount(user);

        //    // Assert
        //    Assert.True(result);

        //    // Verify that SendRequest was called
        //    A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p => p.Operation == Operation.UpdateUser && p.Item == user)))
        //        .MustHaveHappenedOnceExactly();

        //    // Verify that CheckUserData was called
        //    A.CallTo(() => fakeValidator.CheckUserData(user)).MustHaveHappenedOnceExactly();
        //}

        //[Fact]
        //public void SaveAccount_ReturnsFalse_WhenUserUpdateNotOk()
        //{
        //    // Arrange
        //    var user = new User { Username = "testUser", Password = "testPass" };
        //    var response = new Package
        //    {
        //        Operation = Operation.UserUpdateNotOk
        //    };

        //    A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(response);
        //    A.CallTo(() => fakeValidator.CheckUserData(user)).Returns(true);

        //    // Act
        //    var result = clientController.SaveAccount(user);

        //    // Assert
        //    Assert.False(result);

        //    // Verify that SendRequest was called
        //    A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p => p.Operation == Operation.UpdateUser && p.Item == user)))
        //        .MustHaveHappenedOnceExactly();

        //    // Verify that CheckUserData was called
        //    A.CallTo(() => fakeValidator.CheckUserData(user)).MustHaveHappenedOnceExactly();
        //}

    }

}

