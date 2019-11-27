using QuackCalendar.Model;
using QuackCalendar.Service.Manager.Gateway;
using QuackCalendar.Service.ServiceLocator;

namespace QuackCalendar.Service.Manager
{
    internal sealed class QCServiceManager
    {
        private readonly ServiceLocatorBase serviceLocator;
        private SqlGatewayBase sqlGateway;

        private SqlGatewayBase SqlGateway
            => sqlGateway ?? (sqlGateway = serviceLocator.CreateSqlGateway());

        internal QCServiceManager(ServiceLocatorBase serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        internal QCGetEventsResponse GetEvents(QCGetEventsRequest qcGetEventsRequest)
            => SqlGateway.GetEvents(qcGetEventsRequest);
    }
}