using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkNGo.ViewComponents
{
    public class RegistrationViewComponent : ViewComponent
    {

        public RegistrationViewComponent()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync(string view)
        {
            if ( view == null )
            {
                view = "Default";
            }
            return View(view);
        }
    }
}
