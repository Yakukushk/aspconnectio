#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication2.Controllers
{
   // [Authorize(Roles = "admin, user")]
    public class ToursController : Controller
    {
       

        private readonly DBLibraryContext _context;
        private readonly IWebHostEnvironment host;

        public ToursController(DBLibraryContext context, IWebHostEnvironment host)
        {
            _context = context;
            this.host = host;
        }
        public static int Print(string str) {
            Console.WriteLine(str);
            return 0;
        }

        // GET: Tours
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tour.ToListAsync());
        }

        // GET: Tours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tour
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        // GET: Tours/Create
        public IActionResult Create()
        {
            return View();
        }
        //GET: Tours/Excel
        public IActionResult Excel() {
        return View();
        }

        // POST: Tours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Price,Name,Info,ImageFile")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = this.host.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(tour.ImageFile.FileName);
                string ExtensionFile = Path.GetExtension(tour.ImageFile.FileName);

                tour.Title = fileName = fileName + DateTime.Now.ToString("yymmssfff") + ExtensionFile;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await tour.ImageFile.CopyToAsync(fileStream);
                }
                _context.Add(tour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tour);
          
        }

        // GET: Tours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tour.FindAsync(id);

            if (tour == null)
            {
                return NotFound();
            }
            return View(tour);
        }

        // POST: Tours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price, Info")] Tour tour)
        {
            if (id != tour.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourExists(tour.Id))
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
            return View(tour);
        }

        // GET: Tours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tour
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        // POST: Tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tour = await _context.Tour.FindAsync(id);
            
            _context.Tour.Remove(tour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourExists(int id)
        {
            return _context.Tour.Any(e => e.Id == id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel)
        {
            if (ModelState.IsValid)
            {
                if (fileExcel != null)
                {
                    using (var stream = new FileStream(fileExcel.FileName, FileMode.Create))
                    {
                        await fileExcel.CopyToAsync(stream);
                        using (XLWorkbook workbook = new XLWorkbook(stream, XLEventTracking.Disabled))
                        {
                            foreach (IXLWorksheet worksheet in workbook.Worksheets)
                            {
                                Tour newlist;
                                var c = (from list in _context.Tour
                                         where list.Name.Contains(worksheet.Name)
                                         select list).ToList();
                                if (c.Count > 0)
                                {
                                    newlist = c[0];
                                }
                                else
                                {
                                    newlist = new Tour();
                                    newlist.Name = worksheet.Name;

                                    newlist.Info = "from EXCEl";
                                    _context.Tour.Add(newlist);
                                }
                                foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
                                {
                                    try
                                    {
                                        Tour tour = new Tour();
                                        tour.Name = row.Cell(1).Value.ToString();
                                        tour.Info = row.Cell(6).Value.ToString();
                                        tour = newlist;
                                        _context.Tour.Add(tour);


                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Export()
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                try
                {
                    var lists = _context.Tour.ToList();
                    foreach (var c in lists)
                    {
                        var worksheet = workbook.Worksheets.Add(c.Name);
                        worksheet.Cell("A1").Value = "Name";
                        worksheet.Cell("B1").Value = "City";
                        worksheet.Cell("C1").Value = "City";
                        worksheet.Cell("D1").Value = "Price";
                        worksheet.Cell("E1").Value = "Price";
                        worksheet.Cell("F1").Value = "Info";
                        worksheet.Row(1).Style.Font.Bold = true;
                        var tours = c.Name.ToList();

                    }
                }
                catch (Exception ex) {
                    Print(ex.ToString());
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();
                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"library_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }
    }
}
    

