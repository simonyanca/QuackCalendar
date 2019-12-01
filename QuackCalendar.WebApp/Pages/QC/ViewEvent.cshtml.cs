using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuackCalendar.Service;

namespace QuackCalendar.WebApp.Pages.QC
{
    public class ViewEventModel : PageModel
    {
        private readonly IQCDomainFacade domainFacade;

        public ViewEventModel(IQCDomainFacade domainFacade)
        {
            this.domainFacade = domainFacade;
        }

        public IActionResult OnGet(int eventId)
        {
            return Page();
        }
    }
}