using Literature.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Literature.Controllers
{
  public class PublishersController : Controller
  {
    private readonly MyDatabaseContext _context;

    public PublishersController(MyDatabaseContext context)
    {
      _context = context;
    }

    // GET: Publishers
    public async Task<IActionResult> Index()
    {
      return View(await _context.Publishers.ToListAsync());
    }

    // GET: Publishers/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var publisher = await _context.Publishers
          .SingleOrDefaultAsync(m => m.ID == id);
      if (publisher == null)
      {
        return NotFound();
      }

      return View(publisher);
    }

    // GET: Publishers/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Publishers/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID,FirstName,LastName")] Publisher publisher)
    {
      try
      {
        if (ModelState.IsValid)
        {
          _context.Add(publisher);
          await _context.SaveChangesAsync();
          return RedirectToAction("Index");
        }
      }
      catch (DataException /* dex */)
      {
        ModelState.AddModelError("", "Unable to add publisher.");
      }
        return View(publisher);
    }

    // GET: Publishers/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var publisher = await _context.Publishers.SingleOrDefaultAsync(p => p.ID == id);
      if (publisher == null)
      {
        return NotFound();
      }
      return View(publisher);
    }

    // POST: Publishers/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName")] Publisher publisher)
    {
      if (id != publisher.ID)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(publisher);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!PublisherExists(publisher.ID))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction("Index");
      }
      return View(publisher);
    }

    // GET: Publishers/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var publisher = await _context.Publishers
          .SingleOrDefaultAsync(p => p.ID == id);
      if (publisher == null)
      {
        return NotFound();
      }

      return View(publisher);
    }

    // POST: Publishers/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var publisher = await _context.Publishers.SingleOrDefaultAsync(p => p.ID == id);
      _context.Publishers.Remove(publisher);
      await _context.SaveChangesAsync();
      return RedirectToAction("Index");
    }

    private bool PublisherExists(int id)
    {
      return _context.Publishers.Any(p => p.ID == id);
    }
  }
}