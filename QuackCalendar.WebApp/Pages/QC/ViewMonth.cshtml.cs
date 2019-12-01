using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuackCalendar.Model;
using QuackCalendar.Service;

namespace QuackCalendar.WebApp.Pages.QC
{
    public class ViewMonthModel : PageModel
    {
        public List<QCEvent>[] EventsPerDayOfTheMonth { get; set; } = new List<QCEvent>[31];
        public int[,] DaysOfTheMonth { get; set; } = new int[7, 6];
        public int SelectedMonth { get; set; }
        public string SelectedMonthWord => new DateTime(2000, SelectedMonth, 1).ToString("MMMM");
        public int SelectedYear { get; set; }

        private readonly IQCDomainFacade domainFacade;
        private List<QCEvent> qcEvents;

        public ViewMonthModel(IQCDomainFacade domainFacade)
        {
            this.domainFacade = domainFacade;
        }

        public async Task<IActionResult> OnGet(int? selectedYear, int? selectedMonth)
        {
            SelectedYear = (selectedYear == null) ? DateTime.Now.Year : (int)selectedYear;
            SelectedMonth = (selectedMonth == null) ? DateTime.Now.Month : (int)selectedMonth;

            await PopulateQCEvents();
            PopulateEventsPerDayOfTheMonthFromQCEvents();
            PopulateDaysOfTheMonth();

            return Page();
        }

        private void PopulateEventsPerDayOfTheMonthFromQCEvents()
        {
            var lastDayOfTheMonth = DateTime.DaysInMonth(SelectedYear, SelectedMonth);

            for (int day = 1; day < lastDayOfTheMonth + 1; day++)
            {
                EventsPerDayOfTheMonth[day - 1] = new List<QCEvent>();

                var matchingEvents = qcEvents.Where(x => x.StartDateTime.Day <= day && x.EndDateTime.Day >= day);
                EventsPerDayOfTheMonth[day - 1].AddRange(matchingEvents);

                //foreach (var e in matchingEvents)
                //{
                //    string displayHour = "x";

                //    if (e.StartDateTime.Hour == 0)
                //    {
                //        displayHour = "12";
                //    }
                //    else if (e.StartDateTime.Hour > 12)
                //    {
                //        displayHour = (e.StartDateTime.Hour - 12).ToString();
                //    }

                //    displayHour += (e.StartDateTime.Hour < 12) ? "am" : "pm";
                //    EventsPerDayOfTheMonth[day - 1].Add($"{displayHour} {e.Name.Substring(0, 8)}...");
                //}
            }
        }

        private void PopulateDaysOfTheMonth()
        {
            var firstDayOfTheMonth = (int)new DateTime(SelectedYear, SelectedMonth, 1).DayOfWeek;
            var daysInTheMonth = DateTime.DaysInMonth(SelectedYear, SelectedMonth);

            for (int day = firstDayOfTheMonth; day < daysInTheMonth + firstDayOfTheMonth; day++)
            {
                var x = (day % 7);
                var y = (int)Math.Floor(day / 7d);
                DaysOfTheMonth[x, y] = day + 1 - firstDayOfTheMonth;
            }
        }

        private async Task PopulateQCEvents()
        {
            var lastDayOfTheMonth = DateTime.DaysInMonth(SelectedYear, SelectedMonth);
            var qcRequest = new QCGetEventsRequest
            {
                EndDate = new DateTime(SelectedYear, SelectedMonth, lastDayOfTheMonth, 23, 59, 59),
                StartDate = new DateTime(SelectedYear, SelectedMonth, 1, 0, 0, 0),
                UserId = 1
            };

            var response = await domainFacade.GetEventsAsync(qcRequest);
            qcEvents = response.Events;
        }
    }
}