using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace QuackCalendar.Model
{
    [ExcludeFromCodeCoverage]
    public sealed class QCEvent
    {
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime EndDateTime { get; set; } = DateTime.UnixEpoch;

        public int Id { get; set; } = 0;

        [Required, StringLength(80)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime StartDateTime { get; set; } = DateTime.UnixEpoch;
    }
}