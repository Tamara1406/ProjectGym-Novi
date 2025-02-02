using Domain;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using SystemOperations;
using SystemOperations.AppointmentSO;

namespace GymTests.ServerControllerTests
{
    [TestClass]
    public class AppointmentTests
    {
        private ServerController serverController;

        private BaseSO so = A.Fake<BaseSO>();
        public AppointmentTests()
        {
            serverController = new ServerController();
        }

        [TestMethod]
        public void CreateAppointment_ReturnSuccess()
        {
            // Arrange
            var newAppointment = new Appointment { Time = new DateTime(2024, 04, 15), NumberOfAppointments = 3, Group =  new Group() };

            // Act
            var result = serverController.CreateAppointment(newAppointment, so);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateAppointment_With_InvalidData()
        {
            // Arrange
            var invalidAppointment = new Appointment { NumberOfAppointments = 0, Group = null};

            // Act
            var result = serverController.CreateAppointment(invalidAppointment, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateAppointment_ReturnsFalse_WhenNumberOfAppointmentsIsZero()
        {
            // Arrange
            var appointment = new Appointment
            {
                Time = DateTime.Now,
                NumberOfAppointments = 0, 
                Group = new Domain.Group { GroupID = 1 } 
            };

            // Act
            var result = serverController.CreateAppointment(appointment, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateAppointment_ReturnsFalse_WhenGroupIsNull()
        {
            // Arrange
            var appointment = new Appointment
            {
                Time = DateTime.Now, 
                NumberOfAppointments = 5,
                Group = null 
            };

            // Act
            var result = serverController.CreateAppointment(appointment, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetAllAppointments_ReturnSuccess()
        {
            // Arrange
            var appointments = new List<Appointment>
            {
                new Appointment { Time = new DateTime(2024, 04, 15), NumberOfAppointments = 3, Group =  new Group() },
                new Appointment { Time = new DateTime(2024, 04, 16), NumberOfAppointments = 4, Group =  new Group() }
            };
            A.CallTo(() => so.ExecuteOperation()).Invokes(() =>
            {
                so.ResultList = appointments.Cast<AbsEntity>().ToList();
            });


            // Act
            var result = serverController.GetAllAppointments(so);

            // Assert
            Assert.AreEqual(appointments.Count, result.Count);
            foreach (var appointment in appointments)
            {
                Assert.IsTrue(result.Any(c => c.Time == appointment.Time && c.NumberOfAppointments == appointment.NumberOfAppointments && c.Group == appointment.Group ));
            }
        }

        [TestMethod]
        public void GetAllAppointments_Returns_Null_When_Not_Found()
        {
            // Arrange
            A.CallTo(() => so.ExecuteOperation()).Invokes(() =>
            {
                so.ResultList = new List<AbsEntity>();
            });


            // Act
            var result = serverController.GetAllAppointments(so);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void DeleteAppointment_Success()
        {
            // Arrange
            var fakeAppointmentToDelete = new Appointment { Time = new DateTime(2024, 04, 15), NumberOfAppointments = 3, Group = new Group() };
            A.CallTo(() => so.Result).Returns(fakeAppointmentToDelete);

            // Act
            var deletedAppointment = serverController.DeleteAppointment(fakeAppointmentToDelete, so);

            // Assert
            Assert.IsNotNull(deletedAppointment);
            Assert.AreEqual(fakeAppointmentToDelete.Time, deletedAppointment.Time);
            Assert.AreEqual(fakeAppointmentToDelete.NumberOfAppointments, deletedAppointment.NumberOfAppointments);
        }

        [TestMethod]
        public void DeleteAppointment_ThrowsException_WhenCoachNotFound()
        {
            // Arrange
            A.CallTo(() => so.Result).Returns(null);

            var fakeAppointmentToDelete = new Appointment { Time = new DateTime(2024, 04, 15), NumberOfAppointments = 3, Group = new Group() };

            // Act & Assert
            Assert.ThrowsException<Exception>(() => serverController.DeleteAppointment(fakeAppointmentToDelete, so));
        }

        [TestMethod]
        public void UpdateAppointment_ReturnSuccess()
        {
            // Arrange
            var newData = new Appointment { Time = new DateTime(2024, 04, 15), NumberOfAppointments = 3, Group = new Group() };

            // Act
            var result = serverController.UpdateAppointment(newData, so);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateAppointment_With_InvalidData()
        {
            // Arrange
            var invalidAppointment = new Appointment { NumberOfAppointments = 0, Group = null };

            // Act
            var result = serverController.UpdateAppointment(invalidAppointment, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateAppointment_ReturnsFalse_WhenNumberOfAppointmentsIsZero()
        {
            // Arrange
            var appointment = new Appointment
            {
                NumberOfAppointments = 0, 
                Group = new Group { GroupID = 1 } 
            };

            // Act
            var result = serverController.UpdateAppointment(appointment, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateAppointment_ReturnsFalse_WhenGroupIsNull()
        {
            // Arrange
            var appointment = new Appointment
            {
                NumberOfAppointments = 5, 
                Group = null 
            };

            // Act
            var result = serverController.UpdateAppointment(appointment, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateAppointment_ReturnsTrue_WhenDataIsValid()
        {
            // Arrange
            var appointment = new Appointment
            {
                NumberOfAppointments = 5, 
                Group = new Group { GroupID = 1 } 
            };

            // Act
            var result = serverController.UpdateAppointment(appointment, so);

            // Assert
            Assert.IsTrue(result);
        }

    }
}
