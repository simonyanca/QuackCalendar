using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace QuackCalendar.Model
{
    [ExcludeFromCodeCoverage]
    public sealed class QCGetEventsResponse : QCResponse
    {
        public List<QCEvent> Events { get; set; } = new List<QCEvent>();
    }
}