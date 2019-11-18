using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ParkNGo.Models;
using ParkNGo.Tools;
using Serilog;

namespace ParkNGo.Controllers
{
    /// <summary>
    /// Set session middleware to controller, Controller for user page features
    /// </summary>
    [SetSessionVariables]
    public class UserController : Controller
    {
        private readonly ParkNGoContext _context;

        public UserController(ParkNGoContext context)
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
            Log.Information("Direct to View Component: {0}, with view: {1}", component, view);
            component = component == null ? "Registration" : component;
            return ViewComponent(component, new { view = view, username = username, message= message });
        }
        /// <summary>
        /// Edit user personal information
        /// </summary>
        /// <returns>Edit form</returns>
        public async Task<IActionResult> Edit()
        {
            if (ViewData["Username"].ToString() != "")
            {
                Log.Information("Directing to Edit page...");
                var user = await _context.User.FirstOrDefaultAsync(x => x.Username.Equals(ViewData["Username"]));
                return View(user);
            }
            else
            {
                Log.Warning("Invalid route access");
                return View("Index", "Home");
            }

        }
        /// <summary>
        /// [POST] - Change user's information 
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user)
        {
            // check valition from model data annotation
            if (ModelState.IsValid)
            {
                try
                {
                    Log.Information("Save user edit to DB");
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Success";
                    return RedirectToAction("Account", "Home");
                }
                catch (Exception ex)
                {
                    Log.ForContext<UserController>().Error(ex, "Error with saving to DB");
                    TempData["Error"] = "Error with server, please contact customer support";
                    return View("Edit", user);
                }
            }
            else
            {
                Log.Warning("Model not valid");
                TempData["Error"] = "Please check fields";
                return View("Edit", user);
            }

        }
        /// <summary>
        /// Add/edit property
        /// </summary>
        /// <returns>Property page</returns>
        public async Task<IActionResult> Property()
        {
            if (ViewData["Username"].ToString() != "")
            {
                Log.Information("Property page to edit/add property");
                var property = await _context.UserProperty.FirstOrDefaultAsync(x => x.Username.Equals(ViewData["Username"]));
                return View(property);
            }
            else
            {
                Log.Warning("Invalid route access");
                return View("Index", "Home");
            }
        }
        /// <summary>
        /// [POST] - Submit edit to previous property, or add property
        /// </summary>
        /// <param name="property">Property information</param>
        /// <param name="file">Picture of property, uploaded by user</param>
        /// <returns>Property page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Property(UserProperty property, IFormFile file)
        {
            try
            {
                property.Username = ViewData["Username"].ToString();
                // convert image to be stored in DB
                if (file != null)
                {
                    Log.Information("Convert image to format for DB");
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        property.ImageUrl = ms.ToArray();
                    }

                }
                // If property exist, update to DB
                if ( property.PropertyId > 0 )
                {
                    Log.Information("Updating property");
                    _context.Update(property);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    Log.Information("Adding property");
                    _context.Add(property);
                    await _context.SaveChangesAsync();
                }
                // check newly added property and assign property value to user db
                Log.Information("Updating added property value to user db");
                var prop = _context.UserProperty.FirstOrDefault(x => x.Username.Equals(ViewData["Username"].ToString()));
                var user = _context.User.FirstOrDefault(x => x.Username.Equals(ViewData["Username"].ToString()));
                user.PropertyId = prop.PropertyId;
                _context.Update(user);
                await _context.SaveChangesAsync();
                Log.Information("Success to adding/updating property information");
                TempData["Success"] = "Success";
                return RedirectToAction("Account", "Home");
            }
            catch (Exception ex)
            {
                Log.ForContext<UserController>().Error(ex, "Error to adding/updating property information");
                TempData["Error"] = "Error with server, please contact customer support";
                return View(property);
            }
        }
        /// <summary>
        /// Add/update payment page
        /// </summary>
        /// <returns>Payment page</returns>
        public async Task<IActionResult> Payment()
        {
            if (ViewData["Username"].ToString() != "")
            {
                Log.Information("Directing to Payment page...");
                var payment = await _context.Payment.FirstOrDefaultAsync(x => x.Username.Equals(ViewData["Username"]));
                return View(payment);
            }
            else
            {
                Log.Warning("Invalid route access");
                return View("Index", "Home");
            }
        }
        /// <summary>
        /// [POST] - Update/add payment information
        /// </summary>
        /// <param name="payment">Payment information</param>
        /// <returns>Payment page with update/added payment information</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Payment(Payment payment)
        {
            try
            {
                payment.Username = ViewData["Username"].ToString();
                // update payment information if existed before
                if (payment.PaymentId > 0)
                {
                    Log.Information("Updating payment information");
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    Log.Information("Adding payment information");
                    _context.Add(payment);
                    await _context.SaveChangesAsync();
                }
                Log.Information("Retrieving new/updated payment id and updating user db");
                var pay = _context.Payment.FirstOrDefault(x => x.Username.Equals(ViewData["Username"].ToString()));
                var user = _context.User.FirstOrDefault(x => x.Username.Equals(ViewData["Username"].ToString()));
                user.PropertyId = pay.PaymentId;
                _context.Update(user);
                Log.Information("Success to adding/updating payment information");
                TempData["Success"] = "Success";
                return RedirectToAction("Account", "Home");
            }
            catch(Exception ex)
            {
                Log.ForContext<UserController>().Error(ex, "Error to adding/updating payment information");
                TempData["Error"] = "Error with server, please contact customer support";
                return View(payment);
            }
        }
        /// <summary>
        /// Inbox page to allow for messaging Customer service and/or other users
        /// </summary>
        /// <returns>Inbox page</returns>
        public async Task<IActionResult> Inbox()
        {
            if (ViewData["Username"].ToString() != "")
            {
                Log.Information("Directing to Inbox page...");
                var messages = await _context.Messages.Where(x => x.To.Equals(ViewData["Username"]) || x.From.Equals(ViewData["Username"])).OrderByDescending(x => x.Time).ToListAsync();
                return View(messages);
            }
            else
            {
                Log.Warning("Invalid route access");
                return View("Index", "Home");
            }
        }
        /// <summary>
        /// Function to call expand message when user clicks on a message item found in their inbox
        /// </summary>
        /// <param name="id">Div id to identify Primary Key</param>
        /// <returns></returns>
        public async Task<IActionResult> Message(int id)
        {
            Log.Information("Retrieve message with ID: " + id);
            // instantiate new message object
            var message = new Messages();
            if ( id > 0)
            {
                message = await _context.Messages.FirstOrDefaultAsync(x => x.MessageId == id);
            }
            return PartialView(message);
        }
        /// <summary>
        /// [POST] - Reply/send message to user
        /// </summary>
        /// <param name="message">Information related to the message</param>
        /// <returns>Inbox page</returns>
        [HttpPost]
        public async Task<IActionResult> Message(Messages message)
        {
            try
            {
                message.From = ViewData["Username"].ToString();
                message.Time = DateTime.Now;
                if (message.MessageId == 0)
                {
                    Log.Information("Saving message to DB");
                    _context.Add(message);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Message successfully sent";
                }
                else
                {
                    Log.Information("Updating message to DB");
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Message successfully sent";
                }
                Log.Information("Success in updating/adding message");
                return RedirectToAction("Inbox");
            }
            catch(Exception ex)
            {
                Log.ForContext<UserController>().Error(ex, "Error in updating/adding message");
                TempData["Error"] = "Message was unsuccessful";
                return RedirectToAction("Inbox");
            }
        }
        /// <summary>
        /// Set schedule for property
        /// </summary>
        /// <returns>Schedule page using schedule.js</returns>
        public async Task<IActionResult> Schedule()
        {
            if (ViewData["Username"].ToString() != "")
            {
                Log.Information("Directing to Schedule page...");
                var property = await _context.UserProperty.FirstOrDefaultAsync(x => x.Username.Equals(ViewData["Username"]));
                return View(property);
            }
            else
            {
                Log.Warning("Invalid route access");
                return View("Index", "Home");
            }
        }
        /// <summary>
        /// Save/update property schedule to DB
        /// </summary>
        /// <param name="id">Property Id, retrieved from DB</param>
        /// <param name="scheduleJson">JSON formatted date and time, used by schedule.js</param>
        /// <returns>Updated schedule for property</returns>
        public async Task<IActionResult> SetSchedule(int id, string scheduleJson)
        {
            try
            {
                Log.Information("Saving schedule to Property DB");
                var context = new ParkNGoContext();
                var property = await context.UserProperty.FirstOrDefaultAsync(x => x.PropertyId == id);
                property.AvailabilityId = scheduleJson;
                context.Update(property);
                await context.SaveChangesAsync();
                Log.Information("Successful update to property DB with schedule");
                return Ok("Schedule update successful!");
            }
            catch( Exception ex)
            {
                Log.ForContext<UserController>().Error(ex, "Unsuccessful update to schedule for property: " + id);
                return NotFound("Schedule update failed!");
            }

        }
        /// <summary>
        /// List of recent transactions done
        /// </summary>
        /// <returns>Transaction page</returns>
        public async Task<IActionResult> Transactions()
        {
            if (ViewData["Username"].ToString() != "")
            {
                Log.Information("Directing to Transaction page...");
                var transactions = _context.Transaction.Where(x => x.PurchaserId.Equals(ViewData["Username"].ToString()) || x.PropertyOwner.Equals(ViewData["Username"].ToString())).ToList();
                return View(transactions);
            }
            else
            {
                Log.Warning("Invalid route access");
                return View("Index", "Home");
            }
        }
        /// <summary>
        /// Schedule of the selected property
        /// </summary>
        /// <param name="propertyId">Property Id, selected from maps</param>
        /// <returns>Property schedule page</returns>
        public async Task<IActionResult> BookProperty(int propertyId)
        {
            Log.Information("Get Property schedule...");
            var property = await _context.UserProperty.FirstOrDefaultAsync(x => x.PropertyId == propertyId);
            return View(property);
        }
        /// <summary>
        /// Confirmation of payment, and to succesfully book parking
        /// </summary>
        /// <param name="propertyId">Property Id</param>
        /// <param name="details">Selected schedule</param>
        /// <param name="propertyOwner">Owner of property</param>
        /// <param name="paymentId">User selected payment information</param>
        /// <param name="amount">Amount to purchase time slot</param>
        /// <returns>Booking Property page with status</returns>
        public async Task<IActionResult> ContinuePayment(int propertyId, string details, string propertyOwner, int paymentId, float amount)
        {
            try { 
                // instantate new transaction object
                var Transaction = new Transaction() { PropertyId = propertyId, Amount = amount, PaymentId = paymentId, PropertyOwner= propertyOwner, PurchaserId = ViewData["Username"].ToString(), TimeSlot = details, TransactionDate = DateTime.Now, Status = "Processing" };
                Log.Information("Adding transaction to DB");
                _context.Add(Transaction);
                await _context.SaveChangesAsync();
                AdjustmentOfSchedule(Transaction);
                Log.Information("Successful adding to transaction DB");
                return Ok("Please wait for payment processing");
            }
            catch ( Exception ex)
            {
                Log.ForContext<UserController>().Error(ex, "Error adding to transaction DB");
                return NotFound("Error please try again");
            }
        }
        /// <summary>
        /// Check transaction against property availability
        /// </summary>
        public async void AdjustmentOfSchedule(Transaction transaction)
        {
            try
            {
                var booked = false;
                var property = _context.UserProperty.FirstOrDefault(x => x.PropertyId == transaction.PropertyId);
                var book = JArray.Parse(transaction.TimeSlot);
                for ( var i = 0; i < book.Count(); i++)
                {
                    if (book[i]["periods"].Count() > 0)
                    {
                        for( var index = 0; index < book[i]["periods"].Count(); index++)
                        {
                            var checkColor = book[i]["periods"][index]["backgroundColor"];
                            if (checkColor.ToString().Equals("rgba(0, 200, 0, 0.5)"))
                            {
                                book[i]["periods"][index]["backgroundColor"] = "rgba(255, 0, 0, 0.8)";
                                booked = true;
                            }
                        }
                    }
                }
                // if time slow wasavailable
                if ( booked == true)
                {
                    property.AvailabilityId = book.ToString();
                    _context.Update(property);
                    await _context.SaveChangesAsync();
                    var context = new ParkNGoContext();
                    var tran = context.Transaction.FirstOrDefault(x => x.TransactionId == transaction.TransactionId);
                    tran.Status = "Booked";
                    context.Update(tran);
                    await context.SaveChangesAsync();
                    Log.Information("Property availability updated and booked");
                }
                else
                {
                    Log.Warning("Property availability unable to book");
                }
            }
          
            catch (Exception ex)
            {
                Log.Error(ex, "Error with transaction");
            }
        }
    }
}
