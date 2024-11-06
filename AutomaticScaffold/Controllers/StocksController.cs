using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutomaticScaffold.Data;
using AutomaticScaffold.Models;

namespace AutomaticScaffold.Controllers
{
    public class StocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Stocks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Stocks.ToListAsync());
        }

        // GET: Stocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stocks = await _context.Stocks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stocks == null) 
            {
                return NotFound(); // If the selected stock is a found or aavailable value then tell t
            }

            return View(stocks); //Returns to the stocks page within views
        }

        // GET: Stocks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CName,CDescription,Price,PercentageChangeOver12Hours")] Stocks stocks) // Binds the created stock to the information
        {
            if (ModelState.IsValid)
            {
                _context.Add(stocks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stocks);
        }

        // GET: Stocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stocks = await _context.Stocks.FindAsync(id);
            if (stocks == null)
            {
                return NotFound();
            }
            return View(stocks);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CName,CDescription,Price,PercentageChangeOver12Hours")] Stocks stocks)
        {
            if (id != stocks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stocks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StocksExists(stocks.Id))
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
            return View(stocks);
        }

        // GET: Stocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)//If the id of the stock doesnt exist return not found page
            {
                return NotFound();
            }

            var stocks = await _context.Stocks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stocks == null)
            {
                return NotFound();
            }

            return View(stocks);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")] //Calls for the action of deleting to begin
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) //checks if the deletion has been confirmed
        {
            var stocks = await _context.Stocks.FindAsync(id);//Finds the stock and related information
            if (stocks != null)// if the stock isnt deleted
            {
                _context.Stocks.Remove(stocks);//remove and delete the stock and any related infromation frome the entire solution 
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); //Redirects to the index page 
        }

        private bool StocksExists(int id)
        {
            return _context.Stocks.Any(e => e.Id == id);// Checks if the id and the stock its attached to exists
        }
    }
}
