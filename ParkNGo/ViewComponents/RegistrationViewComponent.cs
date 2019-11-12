using Microsoft.AspNetCore.Mvc;
using ParkNGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkNGo.ViewComponents
{
    public class RegistrationViewComponent : ViewComponent
    {

        private readonly ParkNGoContext _context;

        public RegistrationViewComponent(ParkNGoContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string view, string username)
        {
            var user =  _context.User.FirstOrDefault(x => x.Username == username);  
            if ( view == null )
            {
                view = "Default";
            }
            return View(view, user);
        }
    }
}
