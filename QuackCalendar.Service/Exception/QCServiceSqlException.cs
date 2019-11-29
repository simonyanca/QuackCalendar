using System.Runtime.Serialization;

namespace QuackCalendar.Service.Exception
{
    public sealed class QCServiceSqlException : QCServiceBaseException
    {
        public QCServiceSqlException()
        {
        }

        public QCServiceSqlException(string message) : base(message)
        {
        }

        public QCServiceSqlException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        public QCServiceSqlException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}