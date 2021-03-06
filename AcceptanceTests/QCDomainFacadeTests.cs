using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuackCalendar.Model;
using QuackCalendar.Model.Constant;
using QuackCalendar.Service;

namespace AcceptanceTests
{
    [TestClass]
    public class QCDomainFacadeTests
    {
        [TestMethod]
        public async Task QCDomainFacade_AddEventAsync_GivenValidEvent_AddsEventSuccessfully()
        {
            // Arrange
            var domainFacade = new QCDomainFacade();
            var request = new QCAddEventRequest();

            request.Event.Description = "tdesc";
            request.Event.EndDateTime = DateTime.Parse("2020-1-1 13:13:00");
            request.Event.Name = "tname";
            request.Event.StartDateTime = DateTime.Parse("2020-1-1 14:14:00");
            request.UserId = 1;

            // Act
            var response = await domainFacade.AddEventAsync(request);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode.Equals(QCStatusCodes.SuccessfulStatusCode));
            Assert.IsTrue(response.StatusMessage.Equals(QCStatusMessages.SuccessfulStatusMessage));
        }

        [TestMethod]
        public async Task QCDomainFacade_GetEventsAsync_GivenInvalidSearchDates_ReturnsNoEventsSuccessfully()
        {
            // Arrange
            var domainFacade = new QCDomainFacade();
            var request = new QCGetEventsRequest
            {
                EndDate = DateTime.UnixEpoch,
                StartDate = DateTime.Today,
                UserId = 1
            };

            // Act
            var response = await domainFacade.GetEventsAsync(request);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Events.Count == 0);
            Assert.IsTrue(response.StatusCode.Equals(QCStatusCodes.SuccessfulStatusCode));
            Assert.IsTrue(response.StatusMessage.Equals(QCStatusMessages.SuccessfulStatusMessage));
        }

        [TestMethod]
        public async Task QCDomainFacade_GetEventsAsync_GivenValidSearchDates_ReturnsOneEventSuccessfully()
        {
            // Arrange
            var domainFacade = new QCDomainFacade();
            var expectedDescription = "test description";
            var expectedName = "test name";
            var request = new QCGetEventsRequest
            {
                EndDate = new DateTime(2019, 12, 25, 23, 59, 59),
                StartDate = new DateTime(2019, 12, 24, 0, 0, 0),
                UserId = 1
            };

            // Act
            var response = await domainFacade.GetEventsAsync(request);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode.Equals(QCStatusCodes.SuccessfulStatusCode));
            Assert.IsTrue(response.StatusMessage.Equals(QCStatusMessages.SuccessfulStatusMessage));
            Assert.IsTrue(response.Events.Count == 1);
            var firstEvent = response.Events.First();
            Assert.IsTrue(firstEvent.Description.Equals(expectedDescription));
            Assert.IsTrue(firstEvent.EndDateTime.Equals(new DateTime(2019, 12, 25, 23, 59, 59)));
            Assert.IsTrue(firstEvent.Name.Equals(expectedName));
            Assert.IsTrue(firstEvent.StartDateTime.Equals(new DateTime(2019, 12, 24, 0, 0, 0)));
        }
    }
}