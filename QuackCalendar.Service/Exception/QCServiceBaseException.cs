using System.Runtime.Serialization;

namespace QuackCalendar.Service.Exception
{
    public abstract class QCServiceBaseException : System.Exception
    {
        public QCServiceBaseException()
        {
        }

        public QCServiceBaseException(string message) : base(message)
        {
        }

        public QCServiceBaseException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected QCServiceBaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}