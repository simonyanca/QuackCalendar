using System.Diagnostics.CodeAnalysis;

namespace QuackCalendar.Model
{
    [ExcludeFromCodeCoverage]
    public sealed class QCAddEventRequest : QCAuthenticatedRequest
    {
        public QCEvent Event { get; set; } = new QCEvent();
    }
}