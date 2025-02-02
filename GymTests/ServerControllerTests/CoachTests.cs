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

namespace GymTests.ServerControllerTests
{
    [TestClass]
    public class CoachTests
    {
        private ServerController serverController;

        private BaseSO so = A.Fake<BaseSO>();
        public CoachTests()
        {
            serverController = new ServerController();
        }


        [TestMethod]
        public void GetAllCoaches_ReturnSuccess()
        {
            // Arrange
            var coaches = new List<Coach>
            {
                new Coach { FirstName = "Pera", LastName = "Peric"},
                new Coach { FirstName = "Mika", LastName = "Mikic" }
            };
            A.CallTo(() => so.ExecuteOperation()).Invokes(() =>
            {
                so.ResultList = coaches.Cast<AbsEntity>().ToList();
            });


            // Act
            var result = serverController.GetAllCoaches(so);

            // Assert
            Assert.AreEqual(coaches.Count, result.Count);
            foreach (var coach in coaches)
            {
                Assert.IsTrue(result.Any(c => c.FirstName == coach.FirstName && c.LastName == coach.LastName));
            }
        }

        [TestMethod]
        public void GetAllCoaches_Returns_Null_When_Not_Found()
        {
            // Arrange
            A.CallTo(() => so.ExecuteOperation()).Invokes(() =>
            {
                so.ResultList = new List<AbsEntity>();
            });


            // Act
            var result = serverController.GetAllCoaches(so);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void DeleteCoach_Success()
        {
            // Arrange
            var fakeCoachToDelete = new Coach { FirstName = "Pera", LastName = "Peric" };
            A.CallTo(() => so.Result).Returns(fakeCoachToDelete);

            // Act
            var deletedCoach = serverController.DeleteCoach(fakeCoachToDelete, so);

            // Assert
            Assert.IsNotNull(deletedCoach);
            Assert.AreEqual(fakeCoachToDelete.FirstName, deletedCoach.FirstName);
            Assert.AreEqual(fakeCoachToDelete.LastName, deletedCoach.LastName);
        }

        [TestMethod]
        public void DeleteCoach_ThrowsException_WhenCoachNotFound()
        {
            // Arrange
            A.CallTo(() => so.Result).Returns(null);

            var fakeCoachToDelete = new Coach { FirstName = "Pera", LastName = "Peric" };

            // Act & Assert
            Assert.ThrowsException<Exception>(() => serverController.DeleteCoach(fakeCoachToDelete, so));
        }

        [TestMethod]
        public void UpdateCoach_ReturnSuccess()
        {
            // Arrange
            var newData = new Coach { FirstName = "ime", LastName = "prezime", Education = new Education() };

            // Act
            var result = serverController.UpdateCoach(newData, so);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateCoach_With_InvalidData()
        {
            // Arrange
            var invalidUser = new Coach { FirstName = "", LastName = "", Education = null };

            // Act
            var result = serverController.UpdateCoach(invalidUser, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateCoach_ReturnsFalse_WhenFirstNameIsNull()
        {
            // Arrange
            var coach = new Coach
            {
                FirstName = null,
                LastName = "Petrovic",
                Education = new Education { EducationID = 1 } 
            };

            // Act
            var result = serverController.UpdateCoach(coach, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateCoach_ReturnsFalse_WhenLastNameIsNull()
        {
            // Arrange
            var coach = new Coach
            {
                FirstName = "Mika",
                LastName = null,
                Education = new Education { EducationID = 1 } 
            };

            // Act
            var result = serverController.UpdateCoach(coach, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateCoach_ReturnsFalse_WhenEducationIsNull()
        {
            // Arrange
            var coach = new Coach
            {
                FirstName = "Mika",
                LastName = "Petrovic",
                Education = null
            };

            // Act
            var result = serverController.UpdateCoach(coach, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateCoach_ReturnsFalse_WhenFirstNameIsEmpty()
        {
            // Arrange
            var coach = new Coach
            {
                FirstName = "", 
                LastName = "Petrovic",
                Education = new Education { EducationID = 1 }
            };

            // Act
            var result = serverController.UpdateCoach(coach, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateCoach_ReturnsFalse_WhenLastNameIsEmpty()
        {
            // Arrange
            var coach = new Coach
            {
                FirstName = "Mika",
                LastName = "", 
                Education = new Education { EducationID = 1 } 
            };

            // Act
            var result = serverController.UpdateCoach(coach, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateCoach_ReturnSuccess()
        {
            // Arrange
            var newCoach = new Coach { FirstName = "ime", LastName = "prezime", Education = new Education() };

            // Act
            var result = serverController.CreateCoach(newCoach, so);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateCoach_With_InvalidData()
        {
            // Arrange
            var invalidCoach = new Coach { FirstName = "", LastName = "", Education = null };

            // Act
            var result = serverController.CreateCoach(invalidCoach, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateCoach_ReturnsFalse_WhenFirstNameIsNull()
        {
            // Arrange
            var coach = new Coach
            {
                FirstName = null,
                LastName = "Petrovic",
                Education = new Education { EducationID = 1 }
            };

            // Act
            var result = serverController.CreateCoach(coach, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateCoach_ReturnsFalse_WhenLastNameIsNull()
        {
            // Arrange
            var coach = new Coach
            {
                FirstName = "Mika",
                LastName = null,
                Education = new Education { EducationID = 1 } 
            };

            // Act
            var result = serverController.CreateCoach(coach, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateCoach_ReturnsFalse_WhenEducationIsNull()
        {
            // Arrange
            var coach = new Coach
            {
                FirstName = "Mika",
                LastName = "Petrovic",
                Education = null
            };

            // Act
            var result = serverController.CreateCoach(coach, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateCoach_ReturnsFalse_WhenFirstNameIsEmpty()
        {
            // Arrange
            var coach = new Coach
            {
                FirstName = "",
                LastName = "Petrovic",
                Education = new Education { EducationID = 1 }
            };

            // Act
            var result = serverController.CreateCoach(coach, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateCoach_ReturnsFalse_WhenLastNameIsEmpty()
        {
            // Arrange
            var coach = new Coach
            {
                FirstName = "Mika",
                LastName = "",
                Education = new Education { EducationID = 1 }
            };

            // Act
            var result = serverController.CreateCoach(coach, so);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetCoach_ReturnsCoach_WhenCoachExists()
        {
            // Arrange
            var expectedCoach = new Coach { FirstName = "Marko", LastName = "Markovic", Education = new Education() { EducationID = 1 } };
            A.CallTo(() => so.ExecuteOperation()).Invokes(() =>
            {
                so.Result = expectedCoach;
            });

            // Act
            var result = serverController.GetCoach(so);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedCoach.FirstName, result.FirstName);
            Assert.AreEqual(expectedCoach.LastName, result.LastName);
            Assert.AreEqual(expectedCoach.Education.EducationID, result.Education.EducationID);
        }

        [TestMethod]
        public void GetCoach_ReturnsNull_WhenCoachDoesNotExist()
        {
            // Arrange
            A.CallTo(() => so.ExecuteOperation()).Invokes(() =>
            {
                so.Result = null;  
            });

            // Act
            var result = serverController.GetCoach(so);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetAllCoachesByEducation_ReturnsCorrectList_WhenCoachesExist()
        {
            // Arrange
            var educationId = 1;
            var coaches = new List<Domain.Coach>
            {
                new Domain.Coach { FirstName = "Marko", LastName = "Markovic", Education = new Education { EducationID = 1 } },
                new Domain.Coach { FirstName = "Jovan", LastName = "Jovanovic", Education = new Education { EducationID = 1 } }
            };
            A.CallTo(() => so.ExecuteOperation()).Invokes(() =>
            {
                so.ResultList = coaches.Cast<AbsEntity>().ToList();
            });

            // Act
            var result = serverController.GetAllCoachByEducation(so);

            // Assert
            Assert.AreEqual(coaches.Count, result.Count);
            foreach (var coach in coaches)
            {
                Assert.IsTrue(result.Any(c => c.FirstName == coach.FirstName && c.LastName == coach.LastName && c.Education.EducationID == educationId));
            }
        }

        [TestMethod]
        public void GetAllCoachesByEducation_ReturnsEmptyList_WhenNoCoachesWithEducation()
        {
            // Arrange
            A.CallTo(() => so.ExecuteOperation()).Invokes(() =>
            {
                so.ResultList = new List<AbsEntity>();
            });

            // Act
            var result = serverController.GetAllCoachByEducation(so);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetAllCoachesByEducation_ReturnsOnlyCoachesWithSpecifiedEducation()
        {
            // Arrange
            var educationId = 1;
            var coaches = new List<Coach>
            {
                new Domain.Coach { FirstName = "Marko", LastName = "Markovic", Education = new Education { EducationID = 1 } },
                new Domain.Coach { FirstName = "Jovan", LastName = "Jovanovic", Education = new Education { EducationID = 2 } },
                new Domain.Coach { FirstName = "Ana", LastName = "Anic", Education = new Education { EducationID = 1 } }
            };
            A.CallTo(() => so.ExecuteOperation()).Invokes(() =>
            {
                so.ResultList = coaches
                   .Where(coach => coach.Education.EducationID == 1)  
                   .Cast<AbsEntity>()
                   .ToList();
            });

            // Act
            var result = serverController.GetAllCoachByEducation(so);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(c => c.Education.EducationID == educationId));
        }

    }
}
