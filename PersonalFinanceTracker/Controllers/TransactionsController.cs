using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using PersonalFinanceTracker.data;

namespace PersonalFinanceTracker.Controllers
{
    [Authorize] // Ensures only logged-in users can access this controller
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TransactionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Transactions - Show all transactions for the logged-in user
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var transactions = await _context.Transactions
                .Where(t => t.UserId == user.Id)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
            
            return View(transactions);
        }

        // GET: Transactions/Create - Show the form to add a new transaction
        public IActionResult Create()
        {
            return View();
        }

        // POST: Transactions/Create - Save new transaction to database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                transaction.UserId = user.Id;

                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // GET: Transactions/Edit/{id} - Show form to edit a transaction
        public async Task<IActionResult> Edit(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null || transaction.UserId != _userManager.GetUserId(User))
                return NotFound();

            return View(transaction);
        }

        // POST: Transactions/Edit/{id} - Save changes to a transaction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Transaction transaction)
        {
            if (id != transaction.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Transactions.Any(e => e.Id == transaction.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // GET: Transactions/Delete/{id} - Show confirmation page
        public async Task<IActionResult> Delete(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null || transaction.UserId != _userManager.GetUserId(User))
                return NotFound();

            return View(transaction);
        }

        // POST: Transactions/Delete/{id} - Delete transaction
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null || transaction.UserId != _userManager.GetUserId(User))
                return NotFound();

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
