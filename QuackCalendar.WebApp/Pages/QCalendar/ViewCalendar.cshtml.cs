using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using QuackCalendar.Model;
using QuackCalendar.Service;

namespace QuackCalendar.WebApp.Pages.QCalendar
{
    public class ViewCalendarModel : PageModel
    {
        public string Message { get; set; } = string.Empty;
        public IEnumerable<QCEvent> Events { get; set; } = new List<QCEvent>();
        public int[,] DaysOfTheMonth = new int[6, 7];
        public DateTime MonthToView = new DateTime(2019, 12, 1);

        private readonly IConfiguration configuration;
        private readonly IQCDomainFacade qcDomainFacade;

        public ViewCalendarModel(IConfiguration configuration, IQCDomainFacade qcDomainFacade)
        {
            this.configuration = configuration;
            this.qcDomainFacade = qcDomainFacade;
        }

        public async Task OnGet()
        {
            var request = new QCGetEventsRequest
            {
                EndDate = new DateTime(2019, 12, 31, 23, 59, 59),
                StartDate = new DateTime(2019, 1, 1, 0, 0, 0),
                UserId = 1
            };

            Events = (await qcDomainFacade.GetEventsAsync(request)).Events;
            Message = configuration["Message"];

            SetupMonthToView();
        }

        public string GetDayEventsHtml(int dayOfTheMonth)
        {
            var eventHtml = string.Empty;

            foreach (var calendarEvent in Events)
            {
                if (calendarEvent.StartDateTime.Year == 2019
                    && calendarEvent.StartDateTime.Month == 12
                    && calendarEvent.StartDateTime.Day == dayOfTheMonth)
                {
                    string hour = string.Empty;
                    if (calendarEvent.StartDateTime.Hour == 0) { hour = "12a "; }
                    else if (calendarEvent.StartDateTime.Hour <= 12) { hour = $"{calendarEvent.StartDateTime.Hour}a "; }
                    else { hour = $"{calendarEvent.StartDateTime.Hour - 12}p "; }


                    eventHtml += $"{hour}{calendarEvent.Name}";
                }
            }

            return eventHtml;
        }

        private void SetupMonthToView()
        {
            var daysInMonth = DateTime.DaysInMonth(MonthToView.Year, MonthToView.Month);
            var firstOfTheMonth = (int)MonthToView.DayOfWeek;

            DaysOfTheMonth[0, firstOfTheMonth] = 1;

            for (int i = firstOfTheMonth; i < firstOfTheMonth + daysInMonth; i++)
            {
                var currentWeek = (int)Math.Floor((i) / 7d);
                var currentDay = (i % 7);

                DaysOfTheMonth[currentWeek, currentDay] = i + 1;
            }
        }
    }
}