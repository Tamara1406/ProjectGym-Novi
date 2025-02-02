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
using SystemOperations.GroupSO;

namespace GymTests.ServerControllerTests
{
    [TestClass]
    public class GroupTests
    {
        private ServerController serverController;

        private BaseSO so = A.Fake<BaseSO>();
        public GroupTests()
        {
            serverController = new ServerController();
        }

        [TestMethod]
        public void GetAllGroups_ReturnSuccess()
        {
            // Arrange
            var groups = new List<Group>
            {
                new Group { GroupName = "Pera"}
            };
            A.CallTo(() => so.ExecuteOperation()).Invokes(() =>
            {
                so.ResultList = groups.Cast<AbsEntity>().ToList();
            });


            // Act
            var result = serverController.GetAllGroups(so);

            // Assert
            Assert.AreEqual(groups.Count, result.Count);
            foreach (var group in groups)
            {
                Assert.IsTrue(result.Any(c => c.GroupName == group.GroupName));
            }
        }

        [TestMethod]
        public void GetAllGroups_Returns_Null_When_Not_Found()
        {
            // Arrange
            A.CallTo(() => so.ExecuteOperation()).Invokes(() =>
            {
                so.ResultList = new List<AbsEntity>();
            });


            // Act
            var result = serverController.GetAllGroups(so);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void CreateGroup_ReturnSuccess()
        {
            // Arrange
            var newGroup = new Group { GroupName = "Grupa1", Coach = new Coach() };

            // Act
            var result = serverController.CreateGroup(newGroup, so);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateGroup_With_InvalidData()
        {
            // Arrange
            var invalidGroup = new Group { GroupName = "", Coach = null };

            // Act
            var result = serverController.CreateGroup(invalidGroup, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateGroup_ReturnsFalse_WhenGroupNameIsNull()
        {
            // Arrange
            var group = new Group
            {
                GroupName = null, 
                Coach = new Coach { FirstName = "Pera", LastName = "Peric" }
            };

            // Act
            var result = serverController.CreateGroup(group, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateGroup_ReturnsFalse_WhenGroupNameIsEmpty()
        {
            // Arrange
            var group = new Group
            {
                GroupName = "", 
                Coach = new Coach { FirstName = "Mika", LastName = "Mikic" } 
            };

            // Act
            var result = serverController.CreateGroup(group, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateGroup_ReturnsFalse_WhenCoachIsNull()
        {
            // Arrange
            var group = new Group
            {
                GroupName = "Group1", 
                Coach = null 
            };

            // Act
            var result = serverController.CreateGroup(group, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateGroup_ReturnsTrue_WhenDataIsValid()
        {
            // Arrange
            var group = new Group
            {
                GroupName = "Group1",
                Coach = new Coach { FirstName = "Pera", LastName = "Peric" } 
            };

            // Act
            var result = serverController.CreateGroup(group, so);

            // Assert
            Assert.IsTrue(result);
        }

    }
}
