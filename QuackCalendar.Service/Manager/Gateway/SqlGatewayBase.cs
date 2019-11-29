using System.Threading.Tasks;
using QuackCalendar.Model;

namespace QuackCalendar.Service.Manager.Gateway
{
    internal abstract class SqlGatewayBase
    {
        internal async Task<QCAddEventResponse> AddEventAsync(QCAddEventRequest qcAddEventRequest)
            => await AddEventCoreAsync(qcAddEventRequest);

        internal async Task<QCGetEventsResponse> GetEventsAsync(QCGetEventsRequest qcGetEventsRequest)
            => await GetEventsCoreAsync(qcGetEventsRequest);

        protected abstract Task<QCAddEventResponse> AddEventCoreAsync(QCAddEventRequest qcAddEventRequest);
        protected abstract Task<QCGetEventsResponse> GetEventsCoreAsync(QCGetEventsRequest qcGetEventsRequest);
    }
}