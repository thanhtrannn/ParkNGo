using Microsoft.AspNetCore.Mvc;
using ParkNGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkNGo.ViewComponents
{
    public class TransactionViewComponent : ViewComponent
    {

        private readonly ParkNGoContext _context;

        public TransactionViewComponent(ParkNGoContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
