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
    public class ViewEventModel : PageModel
    {
        public QCEvent SelectedEvent { get; set; } = new QCEvent();

        private readonly IQCDomainFacade domainFacade;

        public ViewEventModel(IQCDomainFacade domainFacade)
        {
            this.domainFacade = domainFacade;
        }

        public async Task<IActionResult> OnGet(int eventId)
        {
            var request = new QCGetEventRequest { EventId = eventId };
            var response = await domainFacade.GetEventAsync(request);
            SelectedEvent = response.Event;

            return Page();
        }
    }
}