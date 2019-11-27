using System.Diagnostics.CodeAnalysis;

namespace QuackCalendar.Model
{
    [ExcludeFromCodeCoverage]
    public class QCResponse
    {
        public string StatusCode { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
}