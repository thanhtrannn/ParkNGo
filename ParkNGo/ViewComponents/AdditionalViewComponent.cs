using Microsoft.AspNetCore.Mvc;
using ParkNGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkNGo.ViewComponents
{
    public class AdditionalViewComponent : ViewComponent
    {

        private readonly ParkNGoContext _context;

        public AdditionalViewComponent(ParkNGoContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string view, string username, string message)
        {
            if (view.Equals("Success"))
            {
                TempData["Success"] = message;
            }else if (view.Equals("Error"))
            {
                TempData["Error"] = message;
            }
            return View(view);
        }
    }
}
