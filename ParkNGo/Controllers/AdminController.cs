using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParkNGo.Middleware;
using ParkNGo.Models;
using ParkNGo.Tools;

namespace ParkNGo.Controllers
{
    [SetSessionVariables]
    [AdminMiddleware]
    public class AdminController : Controller
    {
        private readonly ParkNGoContext _context;

        public AdminController(ParkNGoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Users()
        {
            return View(await _context.User.ToListAsync());
        }
        // GET: Admin
        public async Task<IActionResult> Index()
        {
            var admin =  _context.User.FirstOrDefault(x => x.Username.Equals(ViewData["Username"].ToString()));
            return View(admin);    
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Username == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Username,Password,FirstName,LastName,Email,Gender,BirthDate,Address,City,ProvinceId,PostalCode,SecretQuestion1,SecretAnswer1,SecretQuestion2,SecretAnswer2,DisplayUrl,Role,PhoneNumber,PaymentId,PropertyId")] User user)
        {
            if (id != user.Username)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Username))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Username == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.Username == id);
        }

        public IActionResult Parking()
        {
            var parkings = _context.Parking.ToList();
            return View(parkings);
        }
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Parking parking)
        {
            return View();
        }
        public async Task<IActionResult> ParkingEdit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parking = await _context.Parking
                .FirstOrDefaultAsync(m => m.ParkingId.Equals(id));
            if (parking == null)
            {
                return NotFound();
            }

            return View(parking);
        }

    }
}
