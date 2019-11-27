using System;
using System.Diagnostics.CodeAnalysis;

namespace QuackCalendar.Model
{
    [ExcludeFromCodeCoverage]
    public sealed class QCEvent
    {
        public string Description { get; set; } = string.Empty;
        public DateTime EndDateTime { get; set; } = DateTime.UnixEpoch;
        public string Name { get; set; } = string.Empty;
        public DateTime StartDateTime { get; set; } = DateTime.UnixEpoch;
    }
}