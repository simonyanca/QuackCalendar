using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuackCalendar.Model;
using QuackCalendar.Service;

namespace QuackCalendar.WebApp.Pages.QC
{
    public class ViewEventModel : PageModel
    {
        public QCEvent SelectedEvent { get; set; } = new QCEvent();

        private readonly IQCDomainFacade domainFacade;

        public ViewEventModel(IQCDomainFacade domainFacade)
        {
            this.domainFacade = domainFacade;
        }

        public async Task<IActionResult> OnGet(int eventId, bool? deleteEvent)
        {
            var request = new QCGetEventRequest { EventId = eventId };
            var response = await domainFacade.GetEventAsync(request);
            SelectedEvent = response.Event;

            if (deleteEvent.HasValue && deleteEvent.Value == true)
            {
                var deleteRequest = new QCDeleteEventRequest { EventId = eventId };
                await domainFacade.DeleteEventAsync(deleteRequest);
                TempData["Message"] = $"Event \"{SelectedEvent.Name}\" was deleted.";
                return RedirectToPage("./ViewMonth", new { selectedYear = SelectedEvent.StartDateTime.Year, selectedMonth = SelectedEvent.StartDateTime.Month });
            }

            return Page();
        }
    }
}