using System;
using System.Diagnostics.CodeAnalysis;

namespace QuackCalendar.Model
{
    [ExcludeFromCodeCoverage]
    public sealed class QCGetEventsRequest : QCAuthenticatedRequest
    {
        public DateTime EndDate { get; set; } = DateTime.UnixEpoch;
        public DateTime StartDate { get; set; } = DateTime.UnixEpoch;
    }
}