
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FreerunZufuss.Web.Models;
using FreerunZufuss.Web.Data;


public class TripsController : Controller
{
    private readonly ApplicationDbContext _context;

    public TripsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: TRIPS
    public async Task<IActionResult> Index()
    {
        var trips = await _context.Trips
            .Include(t => t.Photos)
            .ToListAsync();

        return View(trips);
    }

    // GET: TRIPS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var trip = await _context.Trips
            .FirstOrDefaultAsync(m => m.Id == id);
        if (trip == null)
        {
            return NotFound();
        }

        return View(trip);
    }

    // GET: TRIPS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: TRIPS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Trip trip)
    {
        if (ModelState.IsValid)
        {
            // save trip first
            _context.Add(trip);
            await _context.SaveChangesAsync();

            // save uploaded images
            if (trip.UploadedImages != null)
            {
                foreach (var file in trip.UploadedImages)
                {
                    if (file.Length > 0)
                    {
                        var fileName =
                            Guid.NewGuid().ToString() +
                            Path.GetExtension(file.FileName);

                        var savePath = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot/images/trips",
                            fileName);

                        using (var stream =
                            new FileStream(savePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        // save into DB
                        var photo = new TripPhoto
                        {
                            TripId = trip.Id,
                            ImagePath = "/images/trips/" + fileName
                        };

                        _context.TripPhotos.Add(photo);
                    }
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        return View(trip);
    }

    // GET: TRIPS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var trip = await _context.Trips.FindAsync(id);
        if (trip == null)
        {
            return NotFound();
        }
        return View(trip);
    }

    // POST: TRIPS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,Content,Description,Photos")] Trip trip)
    {
        if (id != trip.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(trip);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripExists(trip.Id))
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
        return View(trip);
    }

    // GET: TRIPS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var trip = await _context.Trips
            .FirstOrDefaultAsync(m => m.Id == id);
        if (trip == null)
        {
            return NotFound();
        }

        return View(trip);
    }

    // POST: TRIPS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var trip = await _context.Trips.FindAsync(id);
        if (trip != null)
        {
            _context.Trips.Remove(trip);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TripExists(int? id)
    {
        return _context.Trips.Any(e => e.Id == id);
    }
}
