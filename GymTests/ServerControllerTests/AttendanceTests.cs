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
using SystemOperations.Attendance;

namespace GymTests.ServerControllerTests
{
    [TestClass]
    public class AttendanceTests
    {
        private ServerController serverController;

        private BaseSO so = A.Fake<BaseSO>();
        public AttendanceTests()
        {
            serverController = new ServerController();
        }

        [TestMethod]
        public void GetAllAttendances_ReturnSuccess()
        {
            // Arrange
            var attendances = new List<Attendance>
            {
                new Attendance { Client = new Domain.Client(), Appointment = new Appointment(), IsAttend = true },
                new Attendance { Client = new Domain.Client(), Appointment = new Appointment(), IsAttend = true }
            };
            A.CallTo(() => so.ExecuteOperation()).Invokes(() =>
            {
                so.ResultList = attendances.Cast<AbsEntity>().ToList();
            });


            // Act
            var result = serverController.GetAllAttendances(so);

            // Assert
            Assert.AreEqual(attendances.Count, result.Count);
            foreach (var attendance in attendances)
            {
                Assert.IsTrue(result.Any(c => c.Client == attendance.Client && c.Appointment == attendance.Appointment && c.IsAttend == attendance.IsAttend));
            }
        }

        [TestMethod]
        public void GetAllAttendances_Returns_Null_When_Not_Found()
        {
            // Arrange
            A.CallTo(() => so.ExecuteOperation()).Invokes(() =>
            {
                so.ResultList = new List<AbsEntity>();
            });


            // Act
            var result = serverController.GetAllAttendances(so);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void CreateAttendances_ReturnSuccess()
        {
            // Arrange
            var newAttendances = new List<Attendance> {new Attendance { Client = new Domain.Client(), Appointment = new Appointment(), IsAttend = true },
                                                       new Attendance { Client = new Domain.Client(), Appointment = new Appointment(), IsAttend = true } };
            // Act
            var result = serverController.CreateAttendances(newAttendances, so);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateAttendances_With_InvalidData()
        {
            // Arrange
            var invalidAttendance = new List<Attendance> { new Attendance { Client = null, Appointment = null, IsAttend = false },
                                                           new Attendance { Client = null, Appointment = null, IsAttend = false } };

            // Act
            var result = serverController.CreateAttendances(invalidAttendance, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateAttendances_ReturnsFalse_WhenAnyAttendanceClientIsNull()
        {
            // Arrange
            var attendances = new List<Domain.Attendance>
            {
                new Attendance { Client = null, Appointment = new Appointment() }, 
                new Attendance { Client = new Domain.Client(), Appointment = new Domain.Appointment() } 
            };

            // Act
            var result = serverController.CreateAttendances(attendances, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateAttendances_ReturnsFalse_WhenAnyAttendanceAppointmentIsNull()
        {
            // Arrange
            var attendances = new List<Domain.Attendance>
            {
                new Attendance { Client = new Domain.Client(), Appointment = null }, 
                new Attendance { Client = new Domain.Client(), Appointment = new Appointment() }
            };

            // Act
            var result = serverController.CreateAttendances(attendances, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateAttendances_ReturnsTrue_WhenAllAttendancesAreValid()
        {
            // Arrange
            var attendances = new List<Attendance>
            {
                new Attendance { Client = new Domain.Client(), Appointment = new Appointment() }, 
                new Attendance { Client = new Domain.Client(), Appointment = new Appointment() } 
            };

            // Act
            var result = serverController.CreateAttendances(attendances, so);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteAttendance_Success()
        {
            // Arrange
            var fakeAttendanceToDelete = new Attendance { Client = new Domain.Client(), Appointment = new Appointment(), IsAttend = true };
            A.CallTo(() => so.Result).Returns(fakeAttendanceToDelete);

            // Act
            var deletedAttendance = serverController.DeleteAttendance(fakeAttendanceToDelete, so);

            // Assert
            Assert.IsNotNull(deletedAttendance);
            Assert.AreEqual(fakeAttendanceToDelete.Client.ClientID, deletedAttendance.Client.ClientID);
            Assert.AreEqual(fakeAttendanceToDelete.Appointment.AppointmentID, deletedAttendance.Appointment.AppointmentID);
        }

        [TestMethod]
        public void DeleteAttendance_ThrowsException_WhenCoachNotFound()
        {
            // Arrange
            A.CallTo(() => so.Result).Returns(null);

            var fakeAttendanceToDelete = new Attendance { Client = new Domain.Client(), Appointment = new Appointment(), IsAttend = true };

            // Act & Assert
            Assert.ThrowsException<Exception>(() => serverController.DeleteAttendance(fakeAttendanceToDelete, so));
        }

    }
}
