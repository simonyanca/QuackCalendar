namespace QuackCalendar.Model
{
    public sealed class QCGetEventResponse : QCResponse
    {
        public QCEvent Event { get; set; } = new QCEvent();
    }
}