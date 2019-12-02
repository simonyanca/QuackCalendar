using System;
using System.Collections.Generic;
using System.Text;

namespace QuackCalendar.Model
{
    public sealed class QCUpdateEventRequest : QCAuthenticatedRequest
    {
        public QCEvent Event { get; set; } = new QCEvent();
    }
}