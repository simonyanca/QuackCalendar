using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace QuackCalendar.Model
{
    [ExcludeFromCodeCoverage]
    public class QCAuthenticatedRequest : QCRequest
    {
        public string SessionToken { get; set; } = string.Empty;

        [IgnoreDataMember]
        public int UserId { get; set; } = 0;
    }
}