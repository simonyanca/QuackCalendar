using System;
using System.Collections.Generic;
using System.Text;

namespace QuackCalendar.Model
{
    public sealed class QCUpdateEventResponse : QCResponse
    {
        public QCEvent Event { get; set; } = new QCEvent();
    }
}