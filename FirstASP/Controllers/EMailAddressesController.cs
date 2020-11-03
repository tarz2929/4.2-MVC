using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstASP.Models;

namespace FirstASP.Controllers
{
    public class EMailAddressesController : Controller
    {
        private readonly PersonContext _context;

        public EMailAddressesController(PersonContext context)
        {
            _context = context;
        }

        // GET: EMailAddresses
        public async Task<IActionResult> Index()
        {
            var personContext = _context.EMailAddresses.Include(e => e.Person);
            return View(await personContext.ToListAsync());
        }

        // GET: EMailAddresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eMailAddress = await _context.EMailAddresses
                .Include(e => e.Person)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (eMailAddress == null)
            {
                return NotFound();
            }

            return View(eMailAddress);
        }

        // GET: EMailAddresses/Create
        public IActionResult Create()
        {
            ViewData["PersonID"] = new SelectList(_context.People, "ID", "ID");
            return View();
        }

        // POST: EMailAddresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PersonID,Address")] EMailAddress eMailAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eMailAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonID"] = new SelectList(_context.People, "ID", "ID", eMailAddress.PersonID);
            return View(eMailAddress);
        }

        // GET: EMailAddresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eMailAddress = await _context.EMailAddresses.FindAsync(id);
            if (eMailAddress == null)
            {
                return NotFound();
            }
            ViewData["PersonID"] = new SelectList(_context.People, "ID", "ID", eMailAddress.PersonID);
            return View(eMailAddress);
        }

        // POST: EMailAddresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PersonID,Address")] EMailAddress eMailAddress)
        {
            if (id != eMailAddress.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eMailAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EMailAddressExists(eMailAddress.ID))
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
            ViewData["PersonID"] = new SelectList(_context.People, "ID", "ID", eMailAddress.PersonID);
            return View(eMailAddress);
        }

        // GET: EMailAddresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eMailAddress = await _context.EMailAddresses
                .Include(e => e.Person)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (eMailAddress == null)
            {
                return NotFound();
            }

            return View(eMailAddress);
        }

        // POST: EMailAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eMailAddress = await _context.EMailAddresses.FindAsync(id);
            _context.EMailAddresses.Remove(eMailAddress);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EMailAddressExists(int id)
        {
            return _context.EMailAddresses.Any(e => e.ID == id);
        }
    }
}
