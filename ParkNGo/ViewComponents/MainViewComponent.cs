using Microsoft.AspNetCore.Mvc;
using ParkNGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkNGo.ViewComponents
{
    public class MainViewComponent : ViewComponent
    {

        private readonly ParkNGoContext _context;

        public MainViewComponent(ParkNGoContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string view, string username)
        {
            var user =  _context.User.FirstOrDefault(x => x.Username == username);  

            if ( view == null )
            {
                view = "Default";
                return View(view, user);
            }
            else if (view.Equals("infoWindow"))
            {
                var property = _context.UserProperty.FirstOrDefault(x => x.PropertyId.Equals(Int32.Parse(username)));
                return View(view, property);
            }
            else if (view.Equals("Review"))
            {
                var review = _context.CommentRating.OrderBy(y => y.Date).Where(x => x.Id.Equals(Int32.Parse(username))).ToList();
                var toReview = new List<CommentRating>();
                for ( int i = 0; i < review.Count; i++)
                {
                    toReview.Add(review[i]);
                    if (i == 2)
                    {
                        break;
                    }
                }          
                    return View(view, toReview);
            }
            else if (view.Equals("GetPayment")){
                var payment = _context.Payment.FirstOrDefault(x => x.Username == ViewData["Username"].ToString());
                return View(view, payment);
            }
            return View();
        }
    }
}
