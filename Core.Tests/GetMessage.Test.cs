using System;
using Moq;
using Notifier.Core.Entities;
using Notifier.Core.Gateways;
using Notifier.Core.UseCases;
using Xunit;

namespace Core.Tests
{
    public class GetMessageTest
    {
        private Mock<IRepositoryGateway<string, Message>> _messages;
        public GetMessageTest()
        {
            _messages = new Mock<IRepositoryGateway<string, Message>>();
        }

        [Fact]
        public void get_message_by_id()
        {
            // Given
            var message = new Message()
            {
                Id = "abc",
                CommunityId = "fmdc",
                Content = "Hello there",
                DateTimeToSend = DateTime.Now,
                WasSentOn = DateTime.Now,
                JobId = "long-bson-id-mongodb-related"
            };

            _messages.Setup(x => x.Get(
                It.Is<string>(id => id == message.Id)
            )).Returns(message);

            // When
            var getMessage = new GetMessageInteractor(_messages.Object);
            var response = getMessage.Handle(
                new GetMessageRequest()
                {
                    MessageId = "abc"
                }
            );

            // Then
            Assert.Equal(response.Message, message);
            _messages.VerifyAll();
        }
    }
}
