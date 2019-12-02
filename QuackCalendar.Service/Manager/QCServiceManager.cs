using System.Threading.Tasks;
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

        internal async Task<QCAddEventResponse> AddEventAsync(QCAddEventRequest qcAddEventRequest)
            => await SqlGateway.AddEventAsync(qcAddEventRequest);

        internal async Task<QCDeleteEventResponse> DeleteEventAsync(QCDeleteEventRequest qcDeleteEventRequest)
            => await SqlGateway.DeleteEventAsync(qcDeleteEventRequest);

        internal async Task<QCGetEventResponse> GetEventAsync(QCGetEventRequest qcGetEventRequest)
            => await SqlGateway.GetEventAsync(qcGetEventRequest);

        internal async Task<QCGetEventsResponse> GetEventsAsync(QCGetEventsRequest qcGetEventsRequest)
            => await SqlGateway.GetEventsAsync(qcGetEventsRequest);

        internal async Task<QCUpdateEventResponse> UpdateEventAsync(QCUpdateEventRequest qcUpdateEventRequest)
            => await SqlGateway.UpdateEventAsync(qcUpdateEventRequest);
    }
}