using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Crowe.Models;
using Crowe.Services;
using System.Threading.Tasks;

namespace Crowe.Tests
{
    [TestClass]
    class MessageServiceTests
    {
        private DbContextOptions<MessagesContext> options;

        public MessageServiceTests()
        {
            options = new DbContextOptionsBuilder<MessagesContext>()
                .UseInMemoryDatabase(this.ToString()).Options;
        }

        [TestMethod]
        public async Task CanSaveMessage()
        {
            // Arrange
            var dbContext = new MessagesContext(options);
            var messageService = new MessagesService(dbContext);

            var message = new Messages
            {
                Id = 100,
                Message = "TestMessage"
              
            };

            // Act
            var savedCount = await messageService.SaveMessage(message);

            // Assert
            Assert.AreEqual(1, savedCount);
        }
    }
}
