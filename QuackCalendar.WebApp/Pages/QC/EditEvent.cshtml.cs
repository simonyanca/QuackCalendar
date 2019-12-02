using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuackCalendar.Model;
using QuackCalendar.Service;

namespace QuackCalendar.WebApp.Pages.QC
{
    public class EditEventModel : PageModel
    {
        [BindProperty]
        public QCEvent SelectedEvent { get; set; } = new QCEvent();

        private readonly IQCDomainFacade domainFacade;

        public EditEventModel(IQCDomainFacade domainFacade)
        {
            this.domainFacade = domainFacade;
        }

        public async Task<IActionResult> OnGet(int? eventId, int? selectedYear, int? selectedMonth, int? selectedDay)
        {
            if (eventId.HasValue && eventId.Value > 0)
            {
                var request = new QCGetEventRequest { EventId = eventId.Value };
                var response = await domainFacade.GetEventAsync(request);
                SelectedEvent = response.Event;
            }
            else
            {
                var eventDate = new DateTime(
                (selectedYear.HasValue) ? selectedYear.Value : DateTime.Today.Year,
                (selectedMonth.HasValue) ? selectedMonth.Value : DateTime.Today.Month,
                (selectedDay.HasValue) ? selectedDay.Value : DateTime.Today.Day,
                12, 0, 0);
                SelectedEvent.StartDateTime = eventDate;
                SelectedEvent.EndDateTime = eventDate;
            }

            if (eventId.HasValue && eventId.Value > 0 && string.IsNullOrEmpty(SelectedEvent.Name))
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (SelectedEvent.Id > 0)
            {
                var updateRequest = new QCUpdateEventRequest { Event = SelectedEvent, UserId = 1 };
                var updateResponse = await domainFacade.UpdateEventAsync(updateRequest);
                SelectedEvent = updateResponse.Event;
            }
            else
            {
                var request = new QCAddEventRequest { Event = SelectedEvent, UserId = 1 };
                var response = await domainFacade.AddEventAsync(request);
                SelectedEvent.Id = response.Event.Id;
            }

            return RedirectToPage("./ViewEvent", new { eventId = SelectedEvent.Id });
        }
    }
}