using QuackCalendar.Model;

namespace QuackCalendar.Service.Manager.Gateway
{
    internal sealed class MySqlGateway : SqlGatewayBase
    {
        protected override QCGetEventsResponse GetEventsCore(QCGetEventsRequest qcGetEventsRequest)
        {
            return new QCGetEventsResponse();
        }
    }
}