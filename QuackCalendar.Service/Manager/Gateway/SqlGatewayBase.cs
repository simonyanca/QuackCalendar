using System.Threading.Tasks;
using QuackCalendar.Model;

namespace QuackCalendar.Service.Manager.Gateway
{
    internal abstract class SqlGatewayBase
    {
        internal async Task<QCAddEventResponse> AddEventAsync(QCAddEventRequest qcAddEventRequest)
            => await AddEventCoreAsync(qcAddEventRequest);

        internal async Task<QCGetEventResponse> GetEventAsync(QCGetEventRequest qcGetEventRequest)
            => await GetEventCoreAsync(qcGetEventRequest);

        internal async Task<QCGetEventsResponse> GetEventsAsync(QCGetEventsRequest qcGetEventsRequest)
            => await GetEventsCoreAsync(qcGetEventsRequest);

        internal async Task<QCUpdateEventResponse> UpdateEventAsync(QCUpdateEventRequest qcUpdateEventRequest)
            => await UpdateEventCoreAsync(qcUpdateEventRequest);

        protected abstract Task<QCAddEventResponse> AddEventCoreAsync(QCAddEventRequest qcAddEventRequest);
        protected abstract Task<QCGetEventResponse> GetEventCoreAsync(QCGetEventRequest qcGetEventRequest);
        protected abstract Task<QCGetEventsResponse> GetEventsCoreAsync(QCGetEventsRequest qcGetEventsRequest);
        protected abstract Task<QCUpdateEventResponse> UpdateEventCoreAsync(QCUpdateEventRequest qcUpdateEventRequest);
    }
}