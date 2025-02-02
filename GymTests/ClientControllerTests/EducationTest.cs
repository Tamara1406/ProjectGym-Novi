using Client;
using CommunicationClasses;
using Domain;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GymTests.ClientControllerTests
{
    [TestClass]
    public class EducationTest
    {
        private ClientController clientController;

        private ClientCommunication fakeCommunication = A.Fake<ClientCommunication>();

        public EducationTest()
        {
            clientController = new ClientController();
            clientController.Communication = fakeCommunication;
        }

        [TestMethod]
        public void GetAllEducations_ReturnsListOfEducations_WhenResponseIsSuccessful()
        {
            // Arrange
            var educationList = new List<Education>
        {
            new Education { Qualifications = "osnovne studije" },
            new Education { Qualifications = "master studije" }
        };

            var response = new Package
            {
                ItemList = educationList.ConvertAll(x => (object)x)
            };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(response);

            // Act
            var result = clientController.GetAllEducations();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("osnovne studije", result[0].Qualifications);
            Assert.AreEqual("master studije", result[1].Qualifications);

            // Verify that SendRequest was called
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p => p.Operation == Operation.GetAllEducations)))
                .MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void GetAllEducations_ReturnsEmptyList_WhenResponseHasNoEducations()
        {
            // Arrange
            var response = new Package
            {
                ItemList = new List<object>()
            };

            A.CallTo(() => fakeCommunication.RecieveResponse()).Returns(response);

            // Act
            var result = clientController.GetAllEducations();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);

            // Verify that SendRequest was called
            A.CallTo(() => fakeCommunication.SendRequest(A<Package>.That.Matches(p => p.Operation == Operation.GetAllEducations)))
                .MustHaveHappenedOnceExactly();
        }


    }
}
