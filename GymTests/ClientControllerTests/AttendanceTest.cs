using Client;
using CommunicationClasses;
using Domain;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GymTests.ClientControllerTests
{
    [TestClass]
    public class AttendanceTest
    {

        private ClientController clientController;

        private ClientCommunication fakeCommunication = A.Fake<ClientCommunication>();

        public AttendanceTest()
        {
            clientController = new ClientController();
            clientController.Communication = fakeCommunication;
        }

        [TestMethod]
        public void CreateAttendances_SendsRequest_WithCorrectOperationAndItems()
        {
            // Arrange
            var attendances = new List<Attendance>
        {
            new Attendance { Client = new Domain.Client(){ClientID=1}, Appointment = new Appointment(){AppointmentID=1}, IsAttend = true },
            new Attendance { Client = new Domain.Client(){ClientID=1}, Appointment = new Appointment(){AppointmentID=1}, IsAttend = true }
        };

            // Act
            clientController.CreateAttendances(attendances);

            // Assert
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p =>
                p.Operation == Operation.AddAttendances &&
                p.ItemList.Count == attendances.Count &&
                p.ItemList[0] == attendances[0] &&
                p.ItemList[1] == attendances[1]))).MustHaveHappened();
        }

        [TestMethod]
        public void CreateAttendances_ReceivesResponse_AndReturnsTrue_WhenAddAttendancesOk()
        {
            // Arrange
            var attendances = new List<Attendance>
        {
            new Attendance { Client = new Domain.Client(){ClientID=1}, Appointment = new Appointment(){AppointmentID=1}, IsAttend = true },
            new Attendance { Client = new Domain.Client(){ClientID=1}, Appointment = new Appointment(){AppointmentID=1}, IsAttend = true }
        };

            var fakeResponse = new Package { Operation = Operation.AddAttendancesOk };
            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.CreateAttendances(attendances);

            // Assert
            A.CallTo(() => fakeCommunication.RecieveResponse()).MustHaveHappened();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateAttendances_ReceivesResponse_AndReturnsFalse_WhenAddAttendancesFails()
        {
            // Arrange
            var attendances = new List<Attendance>
            {
                new Attendance { Client = new Domain.Client(){ClientID=1}, Appointment = new Appointment(){AppointmentID=1}, IsAttend = true },
                new Attendance { Client = new Domain.Client(){ClientID=1}, Appointment = new Appointment(){AppointmentID=1}, IsAttend = true }
            };

            var fakeResponse = new Package { Operation = Operation.AddAttendancesNotOk };
            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.CreateAttendances(attendances);

            // Assert
            A.CallTo(() => fakeCommunication.RecieveResponse()).MustHaveHappened();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetAllAttendances_ReceivesResponse_AndReturnsAttendanceList()
        {
            // Arrange

            var expectedAttendances = new List<Attendance>
        {
            new Attendance { Client = new Domain.Client { ClientID = 1 }, Appointment = new Appointment { AppointmentID = 1 }, IsAttend = true },
            new Attendance { Client = new Domain.Client { ClientID = 2 }, Appointment = new Appointment { AppointmentID = 2 }, IsAttend = false }
        };

            var fakeResponse = new Package
            {
                ItemList = expectedAttendances.ConvertAll(x => (object)x)
            };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.GetAllAttendances();

            // Assert
            A.CallTo(() => fakeCommunication.RecieveResponse()).MustHaveHappened();
            Assert.AreEqual(expectedAttendances.Count, result.Count);

            for (int i = 0; i < expectedAttendances.Count; i++)
            {
                Assert.AreEqual(expectedAttendances[i].Client.ClientID, result[i].Client.ClientID);
                Assert.AreEqual(expectedAttendances[i].Appointment.AppointmentID, result[i].Appointment.AppointmentID);
                Assert.AreEqual(expectedAttendances[i].IsAttend, result[i].IsAttend);
            }
        }

        [TestMethod]
        public void GetAllAttendances_ReturnsEmptyList_WhenNoAttendancesExist()
        {
            // Arrange
            var responsePackage = new Package
            {
                ItemList = new List<object>()
            };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(responsePackage);

            // Act
            var result = clientController.GetAllAttendances();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void DeleteAttendance_SendsRequest_WithCorrectOperation()
        {
            // Arrange

            var attendanceToDelete = new Attendance
            {
                Client = new Domain.Client { ClientID = 1 },
                Appointment = new Appointment { AppointmentID = 1 },
                IsAttend = true
            };

            // Act
            clientController.DeleteAttendance(attendanceToDelete);

            // Assert
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p =>
                p.Operation == Operation.DeleteAttendance && p.Item == attendanceToDelete))).MustHaveHappened();
        }

        [TestMethod]
        public void DeleteAttendance_ReceivesResponse_AndReturnsDeletedAttendance()
        {
            // Arrange

            var attendanceToDelete = new Attendance
            {
                Client = new Domain.Client { ClientID = 1 },
                Appointment = new Appointment { AppointmentID = 1 },
                IsAttend = true
            };

            var fakeResponse = new Package
            {
                Item = attendanceToDelete,
                Operation = Operation.DeleteAttendance
            };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(fakeResponse);

            // Act
            var result = clientController.DeleteAttendance(attendanceToDelete);

            // Assert
            A.CallTo(() => fakeCommunication.RecieveResponse()).MustHaveHappened();
            Assert.IsNotNull(result);
            Assert.AreEqual(attendanceToDelete.Client.ClientID, result.Client.ClientID);
            Assert.AreEqual(attendanceToDelete.Appointment.AppointmentID, result.Appointment.AppointmentID);
            Assert.AreEqual(attendanceToDelete.IsAttend, result.IsAttend);
        }
    }
}
