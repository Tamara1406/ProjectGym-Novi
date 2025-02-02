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
    public class ClientTest
    {
        private ClientController clientController;

        private ClientCommunication fakeCommunication = A.Fake<ClientCommunication>();

        public ClientTest()
        {
            clientController = new ClientController();
            clientController.Communication = fakeCommunication;
        }

        [TestMethod]
        public void GetAllClients_ReturnsListOfClients_WhenResponseIsSuccessful()
        {
            // Arrange
            var clientList = new List<Domain.Client>
        {
            new Domain.Client { FirstName = "Pera", LastName = "Peric", Gender = Gender.Muski, Height = 187, Weight = 86, Group = new Group(){GroupID = 1 } },
            new Domain.Client { FirstName = "Mika", LastName = "Mikic", Gender = Gender.Muski, Height = 179, Weight = 81, Group = new Group(){GroupID = 2 }  }
        };

            var response = new Package
            {
                ItemList = clientList.ConvertAll(x => (object)x)
            };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(response);

            // Act
            var result = clientController.GetAllClients();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Pera", result[0].FirstName);
            Assert.AreEqual("Mika", result[1].FirstName);
            Assert.AreEqual("Peric", result[0].LastName);
            Assert.AreEqual("Mikic", result[1].LastName);
            Assert.AreEqual(Gender.Muski, result[0].Gender);
            Assert.AreEqual(Gender.Muski, result[1].Gender);
            Assert.AreEqual(187, result[0].Height);
            Assert.AreEqual(179, result[1].Height);
            Assert.AreEqual(86, result[0].Weight);
            Assert.AreEqual(81, result[1].Weight);
            Assert.AreEqual(1, result[0].Group.GroupID);
            Assert.AreEqual(2, result[1].Group.GroupID);

            // Verify that SendRequest was called
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p => p.Operation == Operation.GetAllClients)))
                .MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void GetAllClients_ReturnsEmptyList_WhenResponseHasNoClients()
        {
            // Arrange
            var response = new Package
            {
                ItemList = new List<object>()
            };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(response);

            // Act
            var result = clientController.GetAllClients();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);

            // Verify that SendRequest was called
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p => p.Operation == Operation.GetAllClients)))
                .MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void GetClientSearchedByName_ReturnsCorrectClients_WhenMatchExists()
        {
            // Arrange
            var fakeClientController = A.Fake<ClientController>();
            var allClients = new List<Domain.Client>
            {
                new Domain.Client { FirstName = "Pera", LastName = "Peric", Gender = Gender.Muski, Height = 187, Weight = 86, Group = new Group(){GroupID = 1 } },
                new Domain.Client { FirstName = "Mika", LastName = "Mikic", Gender = Gender.Muski, Height = 179, Weight = 81, Group = new Group(){GroupID = 2 } },
                new Domain.Client { FirstName = "Perica", LastName = "Zikic", Gender = Gender.Muski, Height = 172, Weight = 80, Group = new Group(){GroupID = 1 } }
            };

            A.CallTo(() => fakeClientController.GetAllClients()).Returns(allClients);

            // Act
            var result = fakeClientController.GetClientSearchedByName("per");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(c => c.FirstName == "Pera" && c.LastName == "Peric"));
            Assert.IsTrue(result.Any(c => c.FirstName == "Perica" && c.LastName == "Zikic"));
        }

        [TestMethod]
        public void GetClientSearchedByName_ReturnsEmptyList_WhenNoMatchExists()
        {
            // Arrange
            var fakeClientController = A.Fake<ClientController>();

            var allClients = new List<Domain.Client>
            {
                new Domain.Client { FirstName = "Pera", LastName = "Peric", Gender = Gender.Muski, Height = 187, Weight = 86, Group = new Group(){GroupID = 1 } },
                new Domain.Client { FirstName = "Mika", LastName = "Mikic", Gender = Gender.Muski, Height = 179, Weight = 81, Group = new Group(){GroupID = 2 } },
                new Domain.Client { FirstName = "Perica", LastName = "Zikic", Gender = Gender.Muski, Height = 172, Weight = 80, Group = new Group(){GroupID = 1 } }
            };

            A.CallTo(() => fakeClientController.GetAllClients()).Returns(allClients);

            // Act
            var result = fakeClientController.GetClientSearchedByName("zika");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetClientSearchedByName_ReturnsAllClients_WhenSearchStrIsEmpty()
        {
            // Arrange
            var fakeClientController = A.Fake<ClientController>();

            var allClients = new List<Domain.Client>
            {
                new Domain.Client { FirstName = "Pera", LastName = "Peric", Gender = Gender.Muski, Height = 187, Weight = 86, Group = new Group(){GroupID = 1 } },
                new Domain.Client { FirstName = "Mika", LastName = "Mikic", Gender = Gender.Muski, Height = 179, Weight = 81, Group = new Group(){GroupID = 2 } },
                new Domain.Client { FirstName = "Perica", LastName = "Zikic", Gender = Gender.Muski, Height = 172, Weight = 80, Group = new Group(){GroupID = 1 } }
            };

            A.CallTo(() => fakeClientController.GetAllClients()).Returns(allClients);

            // Act
            var result = fakeClientController.GetClientSearchedByName("");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void GetAllClientByGroup_ReturnsCorrectClients_WhenMatchExists()
        {
            // Arrange
            var group = new Group { GroupID = 1 };
            var allClients = new List<Domain.Client>
            {
                new Domain.Client { FirstName = "Pera", LastName = "Peric", Gender = Gender.Muski, Height = 187, Weight = 86, Group = new Group(){GroupID = 1 } },
                new Domain.Client { FirstName = "Mika", LastName = "Mikic", Gender = Gender.Muski, Height = 179, Weight = 81, Group = new Group(){GroupID = 2 } },
                new Domain.Client { FirstName = "Perica", LastName = "Zikic", Gender = Gender.Muski, Height = 172, Weight = 80, Group = new Group(){GroupID = 1 } }
            };

            var filteredClients = allClients
                .Where(client => client.Group.GroupID == group.GroupID)
                .Cast<object>()
                .ToList();

            var fakeResponse = new Package
            {
                ItemList = filteredClients
            };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.GetAllClientsByGroup(group);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(c => c.FirstName == "Pera" && c.LastName == "Peric"));
            Assert.IsTrue(result.Any(c => c.FirstName == "Perica" && c.LastName == "Zikic"));
        }

        [TestMethod]
        public void GetAllClientByGroup_ReturnsEmptyList_WhenNoMatchExists()
        {
            // Arrange
            var group = new Group { GroupID = 3 };
            var allClients = new List<Domain.Client>
            {
                new Domain.Client { FirstName = "Pera", LastName = "Peric", Gender = Gender.Muski, Height = 187, Weight = 86, Group = new Group(){GroupID = 1 } },
                new Domain.Client { FirstName = "Mika", LastName = "Mikic", Gender = Gender.Muski, Height = 179, Weight = 81, Group = new Group(){GroupID = 2 } },
                new Domain.Client { FirstName = "Perica", LastName = "Zikic", Gender = Gender.Muski, Height = 172, Weight = 80, Group = new Group(){GroupID = 1 } }
            };

            var fakeResponse = new Package
            {
                ItemList = new List<object>()
            };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.GetAllClientsByGroup(group);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void DeleteClient_SendsRequest_WithCorrectOperation()
        {
            // Arrange
            var clientToDelete = new Domain.Client { FirstName = "Pera", LastName = "Peric", Gender = Gender.Muski, Height = 187, Weight = 86, Group = new Group() { GroupID = 1 } };

            // Act
            clientController.DeleteClient(clientToDelete);

            // Assert
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p =>
                p.Item == clientToDelete && p.Operation == Operation.DeleteClient))).MustHaveHappened();
        }

        [TestMethod]
        public void DeleteClient_ReceivesResponse_AndReturnsDeletedClient()
        {
            // Arrange
            var clientToDelete = new Domain.Client { FirstName = "Pera", LastName = "Peric", Gender = Gender.Muski, Height = 187, Weight = 86, Group = new Group() { GroupID = 1 } };

            var fakeResponse = new Package { Item = clientToDelete };
            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.DeleteClient(clientToDelete);

            // Assert
            A.CallTo(() => fakeCommunication.RecieveResponse()).MustHaveHappened();
            Assert.IsNotNull(result);
            Assert.AreEqual(clientToDelete.Name, result.Name);
        }

        [TestMethod]
        public void DeleteClient_ReturnsNull_WhenResponseItemIsNull()
        {
            // Arrange
            var clientToDelete = new Domain.Client { FirstName = "Pera", LastName = "Peric", Gender = Gender.Muski, Height = 187, Weight = 86, Group = new Group() { GroupID = 1 } };

            var fakeResponse = new Package { Item = null };
            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.DeleteClient(clientToDelete);

            // Assert
            A.CallTo(() => fakeCommunication.RecieveResponse()).MustHaveHappened();
            Assert.IsNull(result);
        }

        [TestMethod]
        public void UpdateClient_SendsRequest_WithCorrectOperation()
        {
            // Arrange
            var clientToUpdate = new Domain.Client { FirstName = "Pera", LastName = "Peric", Gender = Gender.Muski, Height = 187, Weight = 86, Group = new Group() { GroupID = 1 } };

            // Act
            clientController.UpdateClient(clientToUpdate);

            // Assert
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p =>
                p.Item == clientToUpdate && p.Operation == Operation.UpdateClient))).MustHaveHappened();
        }

        [TestMethod]
        public void UpdateClient_ReceivesResponse_AndReturnsTrue_WhenUpdateSuccessful()
        {
            // Arrange
            var clientToUpdate = new Domain.Client { FirstName = "Pera", LastName = "Peric", Gender = Gender.Muski, Height = 187, Weight = 86, Group = new Group() { GroupID = 1 } };

            var fakeResponse = new Package { Operation = Operation.UpdateClientOk };
            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.UpdateClient(clientToUpdate);

            // Assert
            A.CallTo(() => fakeCommunication.RecieveResponse()).MustHaveHappened();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateClient_ReceivesResponse_AndReturnsFalse_WhenUpdateFails()
        {
            // Arrange
            var clientToUpdate = new Domain.Client { FirstName = "", LastName = "Peric", Gender = Gender.Muski, Height = 187, Weight = 86, Group = null };

            var fakeResponse = new Package { Operation = Operation.UpdateClientNotOk };
            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.UpdateClient(clientToUpdate);

            // Assert
            A.CallTo(() => fakeCommunication.RecieveResponse()).MustHaveHappened();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateClient_SendsRequest_WithCorrectOperation()
        {
            // Arrange
            var newClient = new Domain.Client { FirstName = "Pera", LastName = "Peric", Gender = Gender.Muski, Height = 187, Weight = 86, Group = new Group() { GroupID = 1 } };

            // Act
            clientController.CreateClient(newClient);

            // Assert
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p =>
                p.Item == newClient && p.Operation == Operation.AddClient))).MustHaveHappened();
        }

        [TestMethod]
        public void CreateClient_ReceivesResponse_AndReturnsTrue_WhenAddClientOk()
        {
            // Arrange
            var newClient = new Domain.Client { FirstName = "Pera", LastName = "Peric", Gender = Gender.Muski, Height = 187, Weight = 86, Group = new Group() { GroupID = 1 } };

            var fakeResponse = new Package { Operation = Operation.AddClientOk };
            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.CreateClient(newClient);

            // Assert
            A.CallTo(() => fakeCommunication.RecieveResponse()).MustHaveHappened();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateClient_ReceivesResponse_AndReturnsFalse_WhenAddClientFails()
        {
            // Arrange
            var newClient = new Domain.Client { FirstName = "", LastName = "Peric", Gender = Gender.Muski, Height = 187, Weight = 86, Group = null };

            var fakeResponse = new Package { Operation = Operation.AddClientNotOk };
            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.CreateClient(newClient);

            // Assert
            A.CallTo(() => fakeCommunication.RecieveResponse()).MustHaveHappened();
            Assert.IsFalse(result);
        }

    }
}
