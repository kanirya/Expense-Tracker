using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expense_Tracker.Models;

namespace Expense_Tracker.Controllers
{
    public class hostelRoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public hostelRoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: hostelRooms
        public async Task<IActionResult> Index()
        {
            var Rooms = await _context.hostelRoom.OrderBy(i => i.floor).ToListAsync();
            return View(Rooms);
        }

        // GET: hostelRooms/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hostelRoom = await _context.hostelRoom
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hostelRoom == null)
            {
                return NotFound();
            }

            return View(hostelRoom);
        }

        // GET: hostelRooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: hostelRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,roomNo,floor,seats")] hostelRoom hostelRoom)
        {
            if (ModelState.IsValid)
            {
                hostelRoom.Id = Guid.NewGuid();
                _context.Add(hostelRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hostelRoom);
        }

        // GET: hostelRooms/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hostelRoom = await _context.hostelRoom.FindAsync(id);
            if (hostelRoom == null)
            {
                return NotFound();
            }
            return View(hostelRoom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,roomNo,floor,seats")] hostelRoom hostelRoom)
        {
            if (id != hostelRoom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hostelRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!hostelRoomExists(hostelRoom.Id))
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
            return View(hostelRoom);
        }

        // GET: hostelRooms/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hostelRoom = await _context.hostelRoom
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hostelRoom == null)
            {
                return NotFound();
            }

            return View(hostelRoom);
        }

        // POST: hostelRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var hostelRoom = await _context.hostelRoom.FindAsync(id);
            if (hostelRoom != null)
            {
                _context.hostelRoom.Remove(hostelRoom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool hostelRoomExists(Guid id)
        {
            return _context.hostelRoom.Any(e => e.Id == id);
        }
    }
}
