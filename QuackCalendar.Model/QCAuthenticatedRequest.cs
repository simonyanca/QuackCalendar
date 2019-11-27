using System.Diagnostics.CodeAnalysis;

namespace QuackCalendar.Model
{
    [ExcludeFromCodeCoverage]
    public class QCAuthenticatedRequest : QCRequest
    {
        public string SessionToken { get; set; } = string.Empty;
    }
}