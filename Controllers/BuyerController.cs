using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SampleCoreApplication.Models;

namespace SampleCoreApplication.Controllers
{
    public class BuyerController : Controller
    {
        private readonly EventContext _context;

        public BuyerController(EventContext context)
        {
            _context = context;
        }

        // GET: Buyer
        public async Task<IActionResult> Index()
        {
            return View(await _context.Buyer.ToListAsync());
        }

        // GET: Buyer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyer = await _context.Buyer
                .SingleOrDefaultAsync(m => m.BuyerId == id);
            if (buyer == null)
            {
                return NotFound();
            }

            return View(buyer);
        }

        // GET: Buyer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Buyer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BuyerId,EventId,TesterKey,BuyerName")] Buyer buyer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buyer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(buyer);
        }

        // GET: Buyer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyer = await _context.Buyer.SingleOrDefaultAsync(m => m.BuyerId == id);
            if (buyer == null)
            {
                return NotFound();
            }
            return View(buyer);
        }

        // POST: Buyer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BuyerId,EventId,TesterKey,BuyerName")] Buyer buyer)
        {
            if (id != buyer.BuyerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buyer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuyerExists(buyer.BuyerId))
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
            return View(buyer);
        }

        // GET: Buyer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyer = await _context.Buyer
                .SingleOrDefaultAsync(m => m.BuyerId == id);
            if (buyer == null)
            {
                return NotFound();
            }

            return View(buyer);
        }

        // POST: Buyer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buyer = await _context.Buyer.SingleOrDefaultAsync(m => m.BuyerId == id);
            _context.Buyer.Remove(buyer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuyerExists(int id)
        {
            return _context.Buyer.Any(e => e.BuyerId == id);
        }

        // GET: Buyer/Create
        public async Task<IActionResult> BuyTicket()
        {
            var testKey = "amijambusaria1386";
            //Get the details for Event, and display the Time value
            var @event = await _context.Event
              .SingleOrDefaultAsync(m => m.EventId == 1);

            if (@event == null)
            {
                return NotFound();
            }

            //check whether somebody has purchased or not
            var @buyer = await _context.Buyer
             .SingleOrDefaultAsync(m => m.EventId == @event.EventId && m.TesterKey==testKey );

            if (@buyer != null)
            {
                ViewBag.successMessage = "Too Late";
                ViewBag.EventTimeOutToDisplay = "0";
            }
            else
            { 
            
            ViewBag.EventTimeOutToDisplay = @event.TimeoutInSeconds.ToString();
            ViewBag.Message = "Hey";
            }
            

            return View();
        }

        // POST: Buyer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyTicket([Bind("BuyerId,EventId,TesterKey,BuyerName")] Buyer buyer)
        {
            if (ModelState.IsValid)
            {
                //check whether somebody has purchased or not
                var @checkBuyerExist = await _context.Buyer
                 .SingleOrDefaultAsync(m => m.EventId == buyer.EventId && m.TesterKey == "amijambusaria1386");

                if (@checkBuyerExist != null)
                {
                    ViewBag.successMessage = "Too Late";
                   // ViewBag.EventTimeOutToDisplay = "0";
                }
                else
                {
                    _context.Add(buyer);
                    await _context.SaveChangesAsync();
                    ViewBag.successMessage = "Congratulations";
                }
                
                ViewBag.EventTimeOutToDisplay = "0";
                //return RedirectToAction(nameof(Index));
            }
            return View(buyer);
        }
    }
}
