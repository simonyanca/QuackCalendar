using System.Diagnostics.CodeAnalysis;

namespace QuackCalendar.Model
{
    [ExcludeFromCodeCoverage]
    public sealed class QCAddEventResponse : QCResponse
    {
        public QCEvent Event { get; set; } = new QCEvent();
    }
}