using System;
using System.Collections.Generic;
using System.Text;

namespace QuackCalendar.Model
{
    public sealed class QCDeleteEventRequest : QCAuthenticatedRequest
    {
        public int EventId { get; set; } = 0;
    }
}