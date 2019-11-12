using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkNGo.Models;
using ParkNGo.Tools;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ParkNGo.Controllers
{
    /// <summary>
    /// Set session middleware to controller, Initial Controller for application
    /// </summary>
    [SetSessionVariables]
    public class HomeController : Controller
    {
        private readonly ParkNGoContext _context;

        public HomeController(ParkNGoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Used for call to view component, Use mostly for AJAX call for dynamic views
        /// </summary>
        /// <param name="view">Specify view found in View Component</param>
        /// <param name="username">Identifier for user</param>
        /// <param name="component">Specify name of View Component</param>
        /// <param name="message">Used for Success/Error messages</param>
        /// <returns>View with specified parmeters</returns>
        public IActionResult GetViewComponent(string view, string username, string component, string message)
        {
            component = component == null ? "Registration" : component;     
            return ViewComponent(component, new { view = view, username = username, message = message });
        }
        /// <summary>
        /// Login page set as Index page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            Log.Information("Retrieving Index page...");
            return View();
        }
        /// <summary>
        /// Check login credentials against DB and redirect user to the appropriate view
        /// </summary>
        /// <param name="user">User Object to match against DB</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Index(User user)
        {
            Log.Information("Checking crentials for: " + user.Username);
            var userInDb = await _context.User.FirstOrDefaultAsync(x => x.Username == user.Username && x.Password == user.Password);
            if (userInDb != null)
            {
                Log.Information("Credentials passed for: " + user.Username);
                ViewData["Username"] = user.Username;
                // Set Session variable for successful login
                HttpContext.Session.SetString("Username",  userInDb.Username);
                HttpContext.Session.SetString("Role", userInDb.Role);
                HttpContext.Session.SetString("Credential", "True");
                return RedirectToAction("Main");
            }
            else
            {
                Log.Warning("Credentials did not match DB");
                return View();
            }
        }
        /// <summary>
        /// Direct to main page if credentials checks out, else return to login page
        /// </summary>
        /// <returns>Main page containing greeting, nav bar, footer and main application</returns>
        public IActionResult Main()
        {
            if (HttpContext.Session.GetString("Credential").Equals("True"))
            {
                Log.Information("Create property object for map");
                var userProperty = _context.UserProperty.ToList();
                return View(userProperty);
            }
            else
            {
                Log.Warning("Invalid route access");
                return RedirectToAction("Index");
            }
        }
        /// <summary>
        /// Register page when "Register" link is clicked, calls RegistrationViewComponent for new registrations
        /// </summary>
        /// <returns>Register Page</returns>
        public IActionResult Register()
        {
            Log.Information("Directing to Register Page");
            return View();
        }
        /// <summary>
        /// [POST] - 
        /// </summary>
        /// <param name="user">Store inputs from new user</param>
        /// <param name="userProperty">Store property input from new user</param>
        /// <returns>Login page for successful registration, else registration page for unsuccessful registration</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user, UserProperty userProperty)
        {
            Log.Information("Registering user...");
            // run if modal is valid using data annotation built in model
            if (ModelState.IsValid)
            {
                try
                {
                    // set role to User, admin can only be added through SQL
                    user.Role = "User";
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    if ( userProperty != null)
                    {
                        _context.Add(userProperty);
                        await _context.SaveChangesAsync();
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Log.ForContext<HomeController>().Error(ex, "Problem with registration for {0}", user.Username);
                }
            }
            Log.Warning("Incorrect field inputs for {0}, returng back to Register", user.Username);
            return View(user);
        }
        /// <summary>
        /// Direct app to user account page
        /// </summary>
        /// <returns>Account page for specific user</returns>
        public async Task<IActionResult> Account()
        {
            if ( ViewData["Username"].ToString() != "" )
            {
                Log.Information("Directing to Account page for " + ViewData["Username"]);
                // get user object from DB
                var user = await _context.User.FirstOrDefaultAsync(x => x.Username.Equals(ViewData["Username"].ToString()));
                return View(user);
            }
            else
            {
                Log.Warning("Invalid route access");
                return RedirectToAction("Index");
            }      
        }
        /// <summary>
        /// Direct app to forget password form
        /// </summary>
        /// <returns>Forget password page</returns>
        public IActionResult Forgot()
        {
            Log.Information("Directing to Forgot Password page...");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        /// <summary>
        /// Function to save review to db
        /// </summary>
        /// <param name="comment">Comment input left by user</param>
        /// <param name="id">Username</param>
        /// <param name="rating">Rating input by user</param>
        /// <returns>HTTP status code with message</returns>
        public async Task<IActionResult> SaveReview(string comment, int id, string rating)
        {
            try
            {
                Log.Information("Saving comment to DB...");
                // build comment object and save to db
                var Comment = new CommentRating() { Username = ViewData["Username"].ToString(), Id = id, Comment = comment, Rating = Int32.Parse(rating) };
                _context.Add(Comment);
                await _context.SaveChangesAsync();
                // update property rating
                var propertyComment = _context.CommentRating.Where(x => x.Id == id).ToList();
                // check rating for all comment left for property
                int totalRating = 0;
                var ratingAmount = 0;
                foreach ( var prop in propertyComment)
                {
                    totalRating += (int)prop.Rating;
                    ratingAmount++;
                }
                // average
                float averageRating = totalRating / ratingAmount;
                var context = new ParkNGoContext();
                var property = context.UserProperty.FirstOrDefault(x => x.PropertyId == id);
                property.Rating = averageRating;
                context.Update(property);
                await context.SaveChangesAsync();
                return Ok("Review successfully saved!");
            }
            catch(Exception ex)
            {
                Log.ForContext<HomeController>().Error(ex, "Problem with saving user comment to db");
                return NotFound("Error with saviing");
            }
        }
    }
}
// Thanh Tran
