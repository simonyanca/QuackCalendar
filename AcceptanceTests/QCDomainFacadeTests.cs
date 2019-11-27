using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuackCalendar.Model;
using QuackCalendar.Service;

namespace AcceptanceTests
{
    [TestClass]
    public class QCDomainFacadeTests
    {
        [TestMethod]
        public void QCDomainFacade_DeveloperTest()
        {
            // Arrange
            var domainFacade = new QCDomainFacade();
            var request = new QCGetEventsRequest();

            // Act
            var response = domainFacade.GetEvents(request);

            // Assert
            Assert.IsNotNull(response);
        }
    }
}