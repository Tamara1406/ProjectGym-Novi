using Domain;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using SystemOperations;
using SystemOperations.ClientSO;

namespace GymTests.ServerControllerTests
{
    [TestClass]
    public class ClientTests
    {
        private ServerController serverController;

        private BaseSO so = A.Fake<BaseSO>();
        public ClientTests()
        {
            serverController = new ServerController();
        }

        [TestMethod]
        public void GetAllClients_ReturnSuccess()
        {
            // Arrange
            var clients = new List<Domain.Client>
            {
                new Domain.Client { FirstName = "Pera", LastName = "Peric", Gender = Gender.Muski, Height = 185, Weight = 85, Group = new Group() },
                new Domain.Client { FirstName = "Mila", LastName = "Milic", Gender = Gender.Zenski, Height = 175, Weight = 65, Group = new Group() }
            };
            A.CallTo(() => so.ExecuteOperation()).Invokes(() =>
            {
                so.ResultList = clients.Cast<AbsEntity>().ToList();
            });


            // Act
            var result = serverController.GetAllClients(so);

            // Assert
            Assert.AreEqual(clients.Count, result.Count);
            foreach (var client in clients)
            {
                Assert.IsTrue(result.Any(c => c.FirstName == client.FirstName && c.LastName == client.LastName && c.Gender == client.Gender && c.Height == client.Height && c.Weight == client.Weight && c.Group == client.Group) );
            }
        }

        [TestMethod]
        public void GetAllClients_Returns_Null_When_Not_Found()
        {
            // Arrange
            A.CallTo(() => so.ExecuteOperation()).Invokes(() =>
            {
                so.ResultList = new List<AbsEntity>();
            });


            // Act
            var result = serverController.GetAllClients(so);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void DeleteClient_Success()
        {
            // Arrange
            var fakeClientToDelete = new Domain.Client { FirstName = "Pera", LastName = "Peric", Gender = Gender.Muski, Height = 185, Weight = 85, Group = new Group() };
            A.CallTo(() => so.Result).Returns(fakeClientToDelete);

            // Act
            var deletedClient = serverController.DeleteClient(fakeClientToDelete, so);

            // Assert
            Assert.IsNotNull(deletedClient);
            Assert.AreEqual(fakeClientToDelete.FirstName, deletedClient.FirstName);
            Assert.AreEqual(fakeClientToDelete.LastName, deletedClient.LastName);
        }

        [TestMethod]
        public void DeleteClient_ThrowsException_WhenCoachNotFound()
        {
            // Arrange
            A.CallTo(() => so.Result).Returns(null);

            var fakeClientToDelete = new Domain.Client { FirstName = "Pera", LastName = "Peric", Gender = Gender.Muski, Height = 185, Weight = 85, Group = new Group() };

            // Act & Assert
            Assert.ThrowsException<Exception>(() => serverController.DeleteClient(fakeClientToDelete, so));
        }

        [TestMethod]
        public void UpdateClient_ReturnSuccess()
        {
            // Arrange
            var newData = new Domain.Client { FirstName = "Pera", LastName = "Peric", Gender = Gender.Muski, Height = 185, Weight = 85, Group = new Group() };

            // Act
            var result = serverController.UpdateClient(newData, so);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateClient_With_InvalidData()
        {
            // Arrange
            var invalidClient = new Domain.Client { FirstName = null, LastName = "", Gender = Gender.Muski, Height = 0, Weight = 0, Group = null };

            // Act
            var result = serverController.UpdateClient(invalidClient, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateClient_ReturnsFalse_WhenFirstNameIsNull()
        {
            // Arrange
            var client = new Domain.Client
            {
                FirstName = null,
                LastName = "Petrovic",
                Weight = 70,
                Height = 180,
                Group = new Group { GroupID = 1 } 
            };

            // Act
            var result = serverController.UpdateClient(client, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateClient_ReturnsFalse_WhenLastNameIsNull()
        {
            // Arrange
            var client = new Domain.Client
            {
                FirstName = "Mika",
                LastName = null,
                Weight = 70,
                Height = 180,
                Group = new Group { GroupID = 1 } 
            };

            // Act
            var result = serverController.UpdateClient(client, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateClient_ReturnsFalse_WhenWeightIsZero()
        {
            // Arrange
            var client = new Domain.Client
            {
                FirstName = "Mika",
                LastName = "Petrovic",
                Weight = 0, 
                Height = 180,
                Group = new Group { GroupID = 1 }
            };

            // Act
            var result = serverController.UpdateClient(client, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateClient_ReturnsFalse_WhenHeightIsZero()
        {
            // Arrange
            var client = new Domain.Client
            {
                FirstName = "Mika",
                LastName = "Petrovic",
                Weight = 70,
                Height = 0, 
                Group = new Group { GroupID = 1 } 
            };

            // Act
            var result = serverController.UpdateClient(client, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateClient_ReturnsFalse_WhenGroupIsNull()
        {
            // Arrange
            var client = new Domain.Client
            {
                FirstName = "Mika",
                LastName = "Petrovic",
                Weight = 70,
                Height = 180,
                Group = null 
            };

            // Act
            var result = serverController.UpdateClient(client, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateClient_ReturnsFalse_WhenFirstNameIsEmpty()
        {
            // Arrange
            var client = new Domain.Client
            {
                FirstName = "",
                LastName = "Petrovic",
                Weight = 70,
                Height = 180,
                Group = new Group { GroupID = 1 } 
            };

            // Act
            var result = serverController.UpdateClient(client, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateClient_ReturnsFalse_WhenLastNameIsEmpty()
        {
            // Arrange
            var client = new Domain.Client
            {
                FirstName = "Mika",
                LastName = "",
                Weight = 70,
                Height = 180,
                Group = new Group { GroupID = 1 } 
            };

            // Act
            var result = serverController.UpdateClient(client, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateClient_ReturnSuccess()
        {
            // Arrange
            var newClient = new Domain.Client { FirstName = "Pera", LastName = "Peric", Gender = Gender.Muski, Height = 185, Weight = 85, Group = new Group() };

            // Act
            var result = serverController.CreateClient(newClient, so);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateClient_With_InvalidData()
        {
            // Arrange
            var invalidClient = new Domain.Client { FirstName = null, LastName = "", Gender = Gender.Muski, Height = 0, Weight = 0, Group = null };

            // Act
            var result = serverController.CreateClient(invalidClient, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateClient_ReturnsFalse_WhenFirstNameIsNull()
        {
            // Arrange
            var client = new Domain.Client
            {
                FirstName = null,
                LastName = "Petrovic",
                Weight = 70,
                Height = 180,
                Group = new Domain.Group { GroupID = 1 } 
            };

            // Act
            var result = serverController.CreateClient(client, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateClient_ReturnsFalse_WhenLastNameIsNull()
        {
            // Arrange
            var client = new Domain.Client
            {
                FirstName = "Mika",
                LastName = null,
                Weight = 70,
                Height = 180,
                Group = new Domain.Group { GroupID = 1 } 
            };

            // Act
            var result = serverController.CreateClient(client, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateClient_ReturnsFalse_WhenWeightIsZero()
        {
            // Arrange
            var client = new Domain.Client
            {
                FirstName = "Mika",
                LastName = "Petrovic",
                Weight = 0, 
                Height = 180,
                Group = new Domain.Group { GroupID = 1 } 
            };

            // Act
            var result = serverController.CreateClient(client, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateClient_ReturnsFalse_WhenHeightIsZero()
        {
            // Arrange
            var client = new Domain.Client
            {
                FirstName = "Mika",
                LastName = "Petrovic",
                Weight = 70,
                Height = 0, 
                Group = new Domain.Group { GroupID = 1 } 
            };

            // Act
            var result = serverController.CreateClient(client, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateClient_ReturnsFalse_WhenGroupIsNull()
        {
            // Arrange
            var client = new Domain.Client
            {
                FirstName = "Mika",
                LastName = "Petrovic",
                Weight = 70,
                Height = 180,
                Group = null 
            };

            // Act
            var result = serverController.CreateClient(client, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateClient_ReturnsFalse_WhenFirstNameIsEmpty()
        {
            // Arrange
            var client = new Domain.Client
            {
                FirstName = "", 
                LastName = "Petrovic",
                Weight = 70,
                Height = 180,
                Group = new Group { GroupID = 1 } 
            };

            // Act
            var result = serverController.CreateClient(client, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateClient_ReturnsFalse_WhenLastNameIsEmpty()
        {
            // Arrange
            var client = new Domain.Client
            {
                FirstName = "Mika",
                LastName = "", // Empty string
                Weight = 70,
                Height = 180,
                Group = new Group { GroupID = 1 } 
            };

            // Act
            var result = serverController.CreateClient(client, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetAllClientsByGroup_ReturnsCorrectList_WhenClientsExist()
        {
            // Arrange
            var clients = new List<Domain.Client>
            {
                new Domain.Client { FirstName = "Pera", LastName = "Peric", Group = new Group { GroupName = "Group1" } },
                new Domain.Client { FirstName = "Mika", LastName = "Mikic", Group = new Group { GroupName = "Group1" } }
            };
            A.CallTo(() => so.ExecuteOperation()).Invokes(() =>
            {
                so.ResultList = clients.Cast<AbsEntity>().ToList();
            });

            // Act
            var result = serverController.GetAllClientsByGroup(so);

            // Assert
            Assert.AreEqual(clients.Count, result.Count);
            foreach (var client in clients)
            {
                Assert.IsTrue(result.Any(c => c.FirstName == client.FirstName && c.LastName == client.LastName && c.Group.GroupName == "Group1"));
            }
        }

        [TestMethod]
        public void GetAllClientsByGroup_ReturnsEmptyList_WhenNoClientsInGroup()
        {
            // Arrange
            A.CallTo(() => so.ExecuteOperation()).Invokes(() =>
            {
                so.ResultList = new List<AbsEntity>();
            });

            // Act
            var result = serverController.GetAllClientsByGroup(so);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetAllClientsByGroup_ReturnsOnlyClientsWithSpecifiedGroup()
        {
            // Arrange
            var clients = new List<Domain.Client>
            {
                new Domain.Client { FirstName = "Pera", LastName = "Peric", Group = new Group { GroupName = "Group1" } },
                new Domain.Client { FirstName = "Mika", LastName = "Mikic", Group = new Group { GroupName = "Group1" } },
                new Domain.Client { FirstName = "Zika", LastName = "Zikic", Group = new Group { GroupName = "Group2" } }
            };
            A.CallTo(() => so.ExecuteOperation()).Invokes(() =>
            {
                so.ResultList = clients
                   .Where(client => client.Group.GroupName == "Group1")  // Filtriraj samo klijente sa grupom "Group1"
                   .Cast<AbsEntity>()
                   .ToList();
            });

            // Act
            var result = serverController.GetAllClientsByGroup(so);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(c => c.Group.GroupName == "Group1"));
        }

        [TestMethod]
        public void GetClient_ReturnsClient_WhenClientExists()
        {
            // Arrange
            var expectedClient = new Domain.Client { FirstName = "Pera", LastName = "Peric", Gender = Gender.Muski, Height = 185, Weight = 85 };
            A.CallTo(() => so.ExecuteOperation()).Invokes(() =>
            {
                so.Result = expectedClient;
            });

            // Act
            var result = serverController.GetClient(so);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedClient.FirstName, result.FirstName);
            Assert.AreEqual(expectedClient.LastName, result.LastName);
            Assert.AreEqual(expectedClient.Gender, result.Gender);
            Assert.AreEqual(expectedClient.Height, result.Height);
            Assert.AreEqual(expectedClient.Weight, result.Weight);
        }

        [TestMethod]
        public void GetClient_ReturnsNull_WhenClientDoesNotExist()
        {
            // Arrange
            A.CallTo(() => so.ExecuteOperation()).Invokes(() =>
            {
                so.Result = null;  // Simulira da klijent nije pronađen
            });

            // Act
            var result = serverController.GetClient(so);

            // Assert
            Assert.IsNull(result);
        }

    }

}
