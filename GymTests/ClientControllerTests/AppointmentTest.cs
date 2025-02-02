using Client;
using CommunicationClasses;
using Domain;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GymTests.ClientControllerTests
{
    [TestClass]
    public class AppointmentTest
    {

        private ClientController clientController;

        private ClientCommunication fakeCommunication = A.Fake<ClientCommunication>();

        public AppointmentTest()
        {
            clientController = new ClientController();
            clientController.Communication = fakeCommunication;
        }

        [TestMethod]
        public void CreateAppointment_SendsRequest_WithCorrectOperation()
        {
            // Arrange
            var newAppointment = new Appointment { Time = DateTime.Today, NumberOfAppointments = 6, Group = new Group() };

            // Act
            var result = clientController.CreateAppointment(newAppointment);

            // Assert
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p =>
                p.Item == newAppointment && p.Operation == Operation.AddAppointment))).MustHaveHappened();
        }

        [TestMethod]
        public void CreateAppointment_ReceivesResponse_AndReturnsTrue_WhenAddClientOk()
        {
            // Arrange
            var newAppointment = new Appointment { Time = DateTime.Today, NumberOfAppointments = 6, Group = new Group() };

            var fakeResponse = new Package { Operation = Operation.AddAppointmentOk };
            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.CreateAppointment(newAppointment);

            // Assert
            A.CallTo(() => fakeCommunication.RecieveResponse()).MustHaveHappened();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateAppointment_ReceivesResponse_AndReturnsFalse_WhenAddAppointmentFails()
        {
            // Arrange
            var newAppointment = new Appointment { Time = DateTime.Today, NumberOfAppointments = 0, Group = null };

            var fakeResponse = new Package { Operation = Operation.AddAppointmentNotOk };
            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.CreateAppointment(newAppointment);

            // Assert
            A.CallTo(() => fakeCommunication.RecieveResponse()).MustHaveHappened();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetAllAppointments_ReturnsListOfAppointments_WhenResponseIsSuccessful()
        {
            // Arrange
            var appointmentList = new List<Appointment>
        {
            new Appointment { Time = DateTime.Today, NumberOfAppointments = 6, Group = new Group() { GroupID = 1 } },
            new Appointment { Time = DateTime.Today, NumberOfAppointments = 5, Group = new Group() { GroupID = 2 } }
        };

            var response = new Package
            {
                ItemList = appointmentList.ConvertAll(x => (object)x)
            };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(response);

            // Act
            var result = clientController.GetAllAppointments();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(6, result[0].NumberOfAppointments);
            Assert.AreEqual(5, result[1].NumberOfAppointments);
            Assert.AreEqual(DateTime.Today, result[0].Time);
            Assert.AreEqual(DateTime.Today, result[1].Time);
            Assert.AreEqual(1, result[0].Group.GroupID);
            Assert.AreEqual(2, result[1].Group.GroupID);

            // Verify that SendRequest was called
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p => p.Operation == Operation.GetAllAppointments)))
                .MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void GetAllAppointments_ReturnsEmptyList_WhenResponseHasNoAppointments()
        {
            // Arrange
            var response = new Package
            {
                ItemList = new List<object>()
            };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(response);

            // Act
            var result = clientController.GetAllAppointments();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);

            // Verify that SendRequest was called
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p => p.Operation == Operation.GetAllAppointments)))
                .MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void GetAllAppointmentByGroup_ReturnsCorrectAppointments_WhenMatchExists()
        {
            // Arrange
            var group = new Group { GroupID = 1 };
            var allAppointments = new List<Appointment>
            {
                 new Appointment { Time = DateTime.Today, NumberOfAppointments = 6, Group = new Group() { GroupID = 1 } },
                 new Appointment { Time = DateTime.Today, NumberOfAppointments = 5, Group = new Group() { GroupID = 2 } },
                 new Appointment { Time = DateTime.Today, NumberOfAppointments = 4, Group = new Group() { GroupID = 1 } }
            };

            var fakeResponse = new Package
            {
                ItemList = allAppointments.ConvertAll(x => (object)x)
            };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.GetAllAppointmentsByGroup(group);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(c => c.NumberOfAppointments == 6));
            Assert.IsTrue(result.Any(c => c.NumberOfAppointments == 4));
        }

        [TestMethod]
        public void GetAllAppointmentByGroup_ReturnsEmptyList_WhenNoMatchExists()
        {
            // Arrange
            var group = new Group { GroupID = 3 };
            var allAppointments = new List<Appointment>
            {
                 new Appointment { Time = DateTime.Today, NumberOfAppointments = 6, Group = new Group() { GroupID = 1 } },
                 new Appointment { Time = DateTime.Today, NumberOfAppointments = 5, Group = new Group() { GroupID = 2 } },
                 new Appointment { Time = DateTime.Today, NumberOfAppointments = 4, Group = new Group() { GroupID = 1 } }
            };

            var fakeResponse = new Package
            {
                ItemList = allAppointments.ConvertAll(x => (object)x)
            };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.GetAllAppointmentsByGroup(group);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void DeleteAppointment_SendsRequest_WithCorrectOperation()
        {
            // Arrange
            var appointmentToDelete = new Appointment { Time = DateTime.Today, NumberOfAppointments = 6, Group = new Group() { GroupID = 1 } };

            // Act
            clientController.DeleteAppointment(appointmentToDelete);

            // Assert
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p =>
                p.Item == appointmentToDelete && p.Operation == Operation.DeleteAppointment))).MustHaveHappened();
        }

        [TestMethod]
        public void DeleteAppointment_ReceivesResponse_AndReturnsDeletedAppointment()
        {
            // Arrange
            var appointmentToDelete = new Appointment { Time = DateTime.Today, NumberOfAppointments = 6, Group = new Group() { GroupID = 1 } };

            var fakeResponse = new Package { Item = appointmentToDelete };
            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.DeleteAppointment(appointmentToDelete);

            // Assert
            A.CallTo(() => fakeCommunication.RecieveResponse()).MustHaveHappened();
            Assert.IsNotNull(result);
            Assert.AreEqual(appointmentToDelete.NumberOfAppointments, result.NumberOfAppointments);
        }

        [TestMethod]
        public void DeleteAppointment_ReturnsNull_WhenResponseItemIsNull()
        {
            // Arrange
            var appointmentToDelete = new Appointment { Time = DateTime.Today, NumberOfAppointments = 6, Group = new Group() { GroupID = 1 } };

            var fakeResponse = new Package { Item = null };
            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.DeleteAppointment(appointmentToDelete);

            // Assert
            A.CallTo(() => fakeCommunication.RecieveResponse()).MustHaveHappened();
            Assert.IsNull(result);
        }


    }
}
