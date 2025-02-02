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
    public class CoachTest
    {
        private ClientController clientController;

        private ClientCommunication fakeCommunication = A.Fake<ClientCommunication>();

        public CoachTest()
        {
            clientController = new ClientController();
            clientController.Communication = fakeCommunication;
        }

        [TestMethod]
        public void GetAllCoaches_ReturnsListOfCoaches_WhenResponseIsSuccessful()
        {
            // Arrange
            var coachList = new List<Coach>
        {
            new Coach { FirstName = "Pera", LastName = "Peric" },
            new Coach { FirstName = "Mika", LastName = "Mikic" }
        };

            var response = new Package
            {
                ItemList = coachList.ConvertAll(x => (object)x)
            };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(response);

            // Act
            var result = clientController.GetAllCoaches();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Pera", result[0].FirstName);
            Assert.AreEqual("Mika", result[1].FirstName);

            // Verify that SendRequest was called
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p => p.Operation == Operation.GetAllCoaches)))
                .MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void GetAllCoaches_ReturnsEmptyList_WhenResponseHasNoCoaches()
        {
            // Arrange
            var response = new Package
            {
                ItemList = new List<object>() 
            };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(response);

            // Act
            var result = clientController.GetAllCoaches();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);

            // Verify that SendRequest was called
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p => p.Operation == Operation.GetAllCoaches)))
                .MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void GetCoachSearchedByName_ReturnsCorrectCoaches_WhenMatchExists()
        {
            // Arrange
            var fakeClientController = A.Fake<ClientController>();
            var allCoaches = new List<Coach>
        {
            new Coach { FirstName = "Pera", LastName = "Peric", Education = new Education() },
            new Coach { FirstName = "Mika", LastName = "Mikic", Education = new Education() },
            new Coach { FirstName = "Perica", LastName = "Zikic", Education = new Education() }
        };

            A.CallTo(() => fakeClientController.GetAllCoaches()).Returns(allCoaches);

            // Act
            var result = fakeClientController.GetCoachSearchedByName("per");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(c => c.FirstName == "Pera" && c.LastName == "Peric"));
            Assert.IsTrue(result.Any(c => c.FirstName == "Perica" && c.LastName == "Zikic"));
        }

        [TestMethod]
        public void GetCoachSearchedByName_ReturnsEmptyList_WhenNoMatchExists()
        {
            // Arrange
            var fakeClientController = A.Fake<ClientController>();

            var allCoaches = new List<Coach>
        {
            new Coach { FirstName = "Pera", LastName = "Peric", Education = new Education() },
            new Coach { FirstName = "Mika", LastName = "Mikic", Education = new Education() },
            new Coach { FirstName = "Perica", LastName = "Zikic", Education = new Education() }
        };

            A.CallTo(() => fakeClientController.GetAllCoaches()).Returns(allCoaches);

            // Act
            var result = fakeClientController.GetCoachSearchedByName("zika");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetCoachSearchedByName_ReturnsAllCoaches_WhenSearchStrIsEmpty()
        {
            // Arrange
            var fakeClientController = A.Fake<ClientController>();

            var allCoaches = new List<Coach>
        {
            new Coach { FirstName = "Pera", LastName = "Peric", Education = new Education() },
            new Coach { FirstName = "Mika", LastName = "Mikic", Education = new Education() },
            new Coach { FirstName = "Perica", LastName = "Zikic", Education = new Education() }
        };

            A.CallTo(() => fakeClientController.GetAllCoaches()).Returns(allCoaches);

            // Act
            var result = fakeClientController.GetCoachSearchedByName("");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void GetAllCoachByEducation_ReturnsCorrectCoaches_WhenMatchExists()
        {
            // Arrange
            var education = new Education { EducationID = 1 };
            var allCoaches = new List<Coach>
            {
                new Coach { FirstName = "Pera", LastName = "Peric", Education = new Education { EducationID = 1 } },
                new Coach { FirstName = "Mika", LastName = "Mikic", Education = new Education { EducationID = 2 } },
                new Coach { FirstName = "Perica", LastName = "Zikic", Education = new Education { EducationID = 1 } }
            };

            var filteredCoaches = allCoaches
                .Where(coach => coach.Education.EducationID == education.EducationID)
                .Cast<object>()
                .ToList();

            var fakeResponse = new Package
            {
                ItemList = filteredCoaches
            };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.GetAllCoachByEducation(education);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(c => c.FirstName == "Pera" && c.LastName == "Peric"));
            Assert.IsTrue(result.Any(c => c.FirstName == "Perica" && c.LastName == "Zikic"));
        }

        [TestMethod]
        public void GetAllCoachByEducation_ReturnsEmptyList_WhenNoMatchExists()
        {
            // Arrange
            var education = new Education { EducationID = 3 };
            var allCoaches = new List<Coach>
            {
                new Coach { FirstName = "Pera", LastName = "Peric", Education = new Education { EducationID = 1 } },
                new Coach { FirstName = "Mika", LastName = "Mikic", Education = new Education { EducationID = 2 } },
                new Coach { FirstName = "Perica", LastName = "Zikic", Education = new Education { EducationID = 1 } }
            };

            var fakeResponse = new Package
            {
                ItemList = new List<object>()
            };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.GetAllCoachByEducation(education);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void DeleteCoach_SendsRequest_WithCorrectOperation()
        {
            // Arrange
            var coachToDelete = new Coach { FirstName = "Pera", LastName = "Peric", Education = new Education { EducationID = 1 } };

            // Act
            clientController.DeleteCoach(coachToDelete);

            // Assert
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p =>
                p.Item == coachToDelete && p.Operation == Operation.DeleteCoach))).MustHaveHappened();
        }

        [TestMethod]
        public void DeleteCoach_ReceivesResponse_AndReturnsDeletedCoach()
        {
            // Arrange
            var coachToDelete = new Coach { FirstName = "Pera", LastName = "Peric", Education = new Education { EducationID = 1 } };

            var fakeResponse = new Package { Item = coachToDelete };
            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.DeleteCoach(coachToDelete);

            // Assert
            A.CallTo(() => fakeCommunication.RecieveResponse()).MustHaveHappened();
            Assert.IsNotNull(result);
            Assert.AreEqual(coachToDelete.Name, result.Name);
        }

        [TestMethod]
        public void DeleteCoach_ReturnsNull_WhenResponseItemIsNull()
        {
            // Arrange
            var coachToDelete = new Coach { FirstName = "Pera", LastName = "Peric", Education = new Education { EducationID = 1 } };

            var fakeResponse = new Package { Item = null };
            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.DeleteCoach(coachToDelete);

            // Assert
            A.CallTo(() => fakeCommunication.RecieveResponse()).MustHaveHappened();
            Assert.IsNull(result);
        }

        [TestMethod]
        public void UpdateCoach_SendsRequest_WithCorrectOperation()
        {
            // Arrange
            var coachToUpdate = new Coach { FirstName = "Pera", LastName = "Peric", Education = new Education { EducationID = 1 } };

            // Act
            clientController.UpdateCoach(coachToUpdate);

            // Assert
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p =>
                p.Item == coachToUpdate && p.Operation == Operation.UpdateCoach))).MustHaveHappened();
        }

        [TestMethod]
        public void UpdateCoach_ReceivesResponse_AndReturnsTrue_WhenUpdateSuccessful()
        {
            // Arrange
            var coachToUpdate = new Coach { FirstName = "Pera", LastName = "Peric", Education = new Education { EducationID = 1 } };

            var fakeResponse = new Package { Operation = Operation.UpdateCoachOk };
            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.UpdateCoach(coachToUpdate);

            // Assert
            A.CallTo(() => fakeCommunication.RecieveResponse()).MustHaveHappened();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateCoach_ReceivesResponse_AndReturnsFalse_WhenUpdateFails()
        {
            // Arrange
            var coachToUpdate = new Coach { FirstName = "", LastName = "Peric", Education = null };

            var fakeResponse = new Package { Operation = Operation.UpdateCoachNotOk };
            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.UpdateCoach(coachToUpdate);

            // Assert
            A.CallTo(() => fakeCommunication.RecieveResponse()).MustHaveHappened();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateCoach_SendsRequest_WithCorrectOperation()
        {
            // Arrange
            var newCoach = new Coach { FirstName = "Pera", LastName = "Peric", Education = new Education { EducationID = 1 } };

            // Act
            clientController.CreateCoach(newCoach);

            // Assert
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p =>
                p.Item == newCoach && p.Operation == Operation.AddCoach))).MustHaveHappened();
        }

        [TestMethod]
        public void CreateCoach_ReceivesResponse_AndReturnsTrue_WhenAddCoachOk()
        {
            // Arrange
            var newCoach = new Coach { FirstName = "Pera", LastName = "Peric", Education = new Education { EducationID = 1 } };

            var fakeResponse = new Package { Operation = Operation.AddCoachOk };
            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.CreateCoach(newCoach);

            // Assert
            A.CallTo(() => fakeCommunication.RecieveResponse()).MustHaveHappened();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateCoach_ReceivesResponse_AndReturnsFalse_WhenAddCoachFails()
        {
            // Arrange
            var newCoach = new Coach { FirstName = "", LastName = "Peric", Education = null };

            var fakeResponse = new Package { Operation = Operation.AddCoachNotOk };
            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.CreateCoach(newCoach);

            // Assert
            A.CallTo(() => fakeCommunication.RecieveResponse()).MustHaveHappened();
            Assert.IsFalse(result);
        }




    }

}

