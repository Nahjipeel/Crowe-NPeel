using Microsoft.VisualStudio.TestTools.UnitTesting;
using Crowe.Controllers;
using System.Threading.Tasks;

namespace Crowe.Tests
{
    [TestClass]
    public class MessagesControllerTests
    {       
        [TestMethod]
        public async Task VoterPost_ReturnsCreatedResult_OnSaveSuccess()
        {
            // Arrange
            var controller = new MessagesController(null);

            // Act

            // Assert

            Assert.Inconclusive();
        }
    }
}
