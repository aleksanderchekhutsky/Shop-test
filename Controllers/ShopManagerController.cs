using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Data.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Shop.Controllers
{
    public class ShopManagerController : Controller
    {
        private readonly AppDBContent _context;

        public ShopManagerController(AppDBContent context)
        {
            _context = context;
        }

        // GET: ShopManager
        public async Task<IActionResult> Index()
        {
            var appDBContent = _context.Car.Include(c => c.Category);
            return View(await appDBContent.ToListAsync());
        }

        // GET: ShopManager/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: ShopManager/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Category, "Id", "Id");
            return View();
        }

        // POST: ShopManager/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ShortDesc,LongDesc,Img,Price,IsFavourite,Available,CategoryID")] Car car)
        {
            if (ModelState.IsValid)
            {
                
                    
                car.Img = "/img/" + car.Img;
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "Id", "Id", car.CategoryID);
            return View(car);
        }

        // GET: ShopManager/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "Id", "Id", car.CategoryID);
            
            return View(car);
        }

        // POST: ShopManager/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ShortDesc,LongDesc,Img,Price,IsFavourite,Available,CategoryID")] Car car) ///editet
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                try
                {
                   
                    
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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
            ViewData["CategoryID"] = new SelectList(_context.Category, "Id", "Id", car.CategoryID);
            return View(car);
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                string content_type = file.ContentType;
            }
            //editt
            //_context.Update();
            await _context.SaveChangesAsync();

            //edit


            return RedirectToAction("Index");
        }


        // GET: ShopManager/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: ShopManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Car.FindAsync(id);
            _context.Car.Remove(car);
           
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Car.Any(e => e.Id == id);
        }
    }
}
