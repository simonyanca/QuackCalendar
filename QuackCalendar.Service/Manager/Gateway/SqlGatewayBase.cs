using QuackCalendar.Model;

namespace QuackCalendar.Service.Manager.Gateway
{
    internal abstract class SqlGatewayBase
    {
        internal QCGetEventsResponse GetEvents(QCGetEventsRequest qcGetEventsRequest)
            => GetEventsCore(qcGetEventsRequest);

        protected abstract QCGetEventsResponse GetEventsCore(QCGetEventsRequest qcGetEventsRequest);
    }
}