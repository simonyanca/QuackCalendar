using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QuackCalendar.WebApp.Pages.QC
{
    public class ViewMonthModel : PageModel
    {
        public int[,] DaysOfTheMonth { get; set; } = new int[7, 6];
        public int SelectedMonth { get; set; }
        public string SelectedMonthWord => new DateTime(2000, SelectedMonth, 1).ToString("MMMM");
        public int SelectedYear { get; set; }

        public ViewMonthModel()
        {

        }

        public void OnGet(int? selectedYear, int? selectedMonth)
        {
            SelectedYear = (selectedYear == null) ? DateTime.Now.Year : (int)selectedYear;
            SelectedMonth = (selectedMonth == null) ? DateTime.Now.Month : (int)selectedMonth;

            PopulateDaysOfTheMonth();
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
    }
}