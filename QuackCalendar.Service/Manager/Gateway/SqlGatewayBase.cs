using QuackCalendar.Model;

namespace QuackCalendar.Service.Manager.Gateway
{
    internal abstract class SqlGatewayBase
    {
        internal QCAddEventResponse AddEvent(QCAddEventRequest qcAddEventRequest)
            => AddEventCore(qcAddEventRequest);

        internal QCGetEventsResponse GetEvents(QCGetEventsRequest qcGetEventsRequest)
            => GetEventsCore(qcGetEventsRequest);

        protected abstract QCAddEventResponse AddEventCore(QCAddEventRequest qcAddEventRequest);
        protected abstract QCGetEventsResponse GetEventsCore(QCGetEventsRequest qcGetEventsRequest);
    }
}