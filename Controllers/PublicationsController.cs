using Literature.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Literature.Controllers
{
  public class PublicationsController : Controller
  {
    private readonly MyDatabaseContext _context;

    public PublicationsController(MyDatabaseContext context)
    {
      _context = context;
    }

    // GET: Publications
    public async Task<IActionResult> Index()
    {
      return View(await _context.Publication.ToListAsync());
    }

    // GET: Publications/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var publication = await _context.Publication
          .SingleOrDefaultAsync(m => m.ID == id);
      if (publication == null)
      {
        return NotFound();
      }

      return View(publication);
    }

    // GET: Publications/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Publications/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID,Title,Language,Code")] Publication publication)
    {
      try
      {
        if (ModelState.IsValid)
        {
          _context.Add(publication);
          await _context.SaveChangesAsync();
          return RedirectToAction("Index");
        }
      }
      catch (DataException /* dex */)
      {
        ModelState.AddModelError("", "Unable to add publication.");
      }
        return View(publication);
    }

    // GET: Publications/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var publication = await _context.Publication.SingleOrDefaultAsync(m => m.ID == id);
      if (publication == null)
      {
        return NotFound();
      }
      return View(publication);
    }

    // POST: Publications/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Language,Code")] Publication publication)
    {
      if (id != publication.ID)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(publication);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!PublicationExists(publication.ID))
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
      return View(publication);
    }

    // GET: Publications/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var publication = await _context.Publication
          .SingleOrDefaultAsync(m => m.ID == id);
      if (publication == null)
      {
        return NotFound();
      }

      return View(publication);
    }

    // POST: Publications/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var publication = await _context.Publication.SingleOrDefaultAsync(m => m.ID == id);
      _context.Publication.Remove(publication);
      await _context.SaveChangesAsync();
      return RedirectToAction("Index");
    }

    private bool PublicationExists(int id)
    {
      return _context.Publication.Any(p => p.ID == id);
    }
  }
}