namespace QuackCalendar.Model
{
    public sealed class QCGetEventRequest : QCAuthenticatedRequest
    {
        public int EventId { get; set; } = 0;
    }
}