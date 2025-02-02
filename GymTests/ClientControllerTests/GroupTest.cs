using Client;
using CommunicationClasses;
using Domain;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymTests.ClientControllerTests
{
    [TestClass]
    public class GroupTest
    {
        private ClientController clientController;

        private ClientCommunication fakeCommunication = A.Fake<ClientCommunication>();

        public GroupTest()
        {
            clientController = new ClientController();
            clientController.Communication = fakeCommunication;
        }

        [TestMethod]
        public void GetAllGroups_ReturnsListOfGroups_WhenResponseIsSuccessful()
        {
            // Arrange
            var groupList = new List<Group>
        {
            new Group { GroupName = "grupa 1", Coach = new Coach() { CoachID = 1 } },
            new Group { GroupName = "grupa 2", Coach = new Coach() { CoachID = 2 } }
        };

            var response = new Package
            {
                ItemList = groupList.ConvertAll(x => (object)x)
            };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(response);

            // Act
            var result = clientController.GetAllGroups();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("grupa 1", result[0].GroupName);
            Assert.AreEqual(1, result[0].Coach.CoachID);
            Assert.AreEqual("grupa 2", result[1].GroupName);
            Assert.AreEqual(2, result[1].Coach.CoachID);

            // Verify that SendRequest was called
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p => p.Operation == Operation.GetAllGroups)))
                .MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void GetAllGroups_ReturnsEmptyList_WhenResponseHasNoGroups()
        {
            // Arrange
            var response = new Package
            {
                ItemList = new List<object>()
            };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(response);

            // Act
            var result = clientController.GetAllGroups();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);

            // Verify that SendRequest was called
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p => p.Operation == Operation.GetAllGroups)))
                .MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void CreateGroup_SendsRequest_WithCorrectOperation()
        {
            // Arrange
            var newGroup = new Group { GroupName = "grupa 1", Coach = new Coach() { CoachID = 1 } };

            // Act
            clientController.CreateGroup(newGroup);

            // Assert
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p =>
                p.Item == newGroup && p.Operation == Operation.AddGroup))).MustHaveHappened();
        }

        [TestMethod]
        public void CreateGroup_ReceivesResponse_AndReturnsTrue_WhenAddGroupOk()
        {
            // Arrange
            var newGroup = new Group { GroupName = "grupa 1", Coach = new Coach() { CoachID = 1 } };

            var fakeResponse = new Package { Operation = Operation.AddGroupOk };
            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.CreateGroup(newGroup);

            // Assert
            A.CallTo(() => fakeCommunication.RecieveResponse()).MustHaveHappened();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateGroup_ReceivesResponse_AndReturnsFalse_WhenAddGroupFails()
        {
            // Arrange
            var newGroup = new Group { GroupName = "grupa 1", Coach = new Coach() { CoachID = 1 } };

            var fakeResponse = new Package { Operation = Operation.AddGroupNotOk };
            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.CreateGroup(newGroup);

            // Assert
            A.CallTo(() => fakeCommunication.RecieveResponse()).MustHaveHappened();
            Assert.IsFalse(result);
        }


    }
}
