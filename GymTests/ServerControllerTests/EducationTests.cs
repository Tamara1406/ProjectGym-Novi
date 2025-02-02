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
    public class EducationTests
    {
        private ServerController serverController;

        private BaseSO so = A.Fake<BaseSO>();
        public EducationTests()
        {
            serverController = new ServerController();
        }

        [TestMethod]
        public void GetAllEducations_ReturnSuccess()
        {
            // Arrange
            var educations = new List<Education>
            {
                new Education { Qualifications = "Osnovne studije"},
                new Education { Qualifications = "Master studije"}
            };
            A.CallTo(() => so.ExecuteOperation()).Invokes(() =>
            {
                so.ResultList = educations.Cast<AbsEntity>().ToList();
            });


            // Act
            var result = serverController.GetAllEducations(so);

            // Assert
            Assert.AreEqual(educations.Count, result.Count);
            foreach (var educ in educations)
            {
                Assert.IsTrue(result.Any(c => c.Qualifications == educ.Qualifications));
            }
        }

        [TestMethod]
        public void GetAllEducations_Returns_Null_When_Not_Found()
        {
            // Arrange
            A.CallTo(() => so.ExecuteOperation()).Invokes(() =>
            {
                so.ResultList = new List<AbsEntity>();
            });


            // Act
            var result = serverController.GetAllEducations(so);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

    }
}
