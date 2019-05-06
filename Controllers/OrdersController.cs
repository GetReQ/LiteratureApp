using Literature.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Literature.Controllers
{
  public class OrdersController : Controller
  {
    private readonly MyDatabaseContext _context;

    public OrdersController(MyDatabaseContext context)
    {
      _context = context;
    }

    // GET: Orders
    public async Task<IActionResult> Index()
    {
      return View(await _context.Orders
        .Include(o => o.OrderItems)
        .ThenInclude(i => i.Publication)
        .Include(o => o.Publisher)
        .OrderByDescending(o => o.OrderDate)
        .ToListAsync());
    }

    // GET: Orders/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var orders = await _context.Orders
        .Include(o => o.OrderItems)
        .ThenInclude(i => i.Publication)
        .Include(o => o.Publisher)
        .SingleOrDefaultAsync(o => o.ID == id);
      if (orders == null)
      {
        return NotFound();
      }

      return View(orders);
    }

    // GET: Orders/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Orders/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID,Publisher,OrderItems,OrderPlaced,OrderDelivered")] Order order)
    {
      try
      {
        if (ModelState.IsValid)
        {
          order.OrderDate = DateTime.UtcNow;
          _context.Add(order);
          await _context.SaveChangesAsync();
          return RedirectToAction("Index");
        }
      }
      catch (DataException /* dex */)
      {
        ModelState.AddModelError("", "Unable to add order.");
      }
      return View(order);
    }

    // GET: Orders/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var order = await _context.Orders
        .Include(o => o.OrderItems.Select(i => i.Publication))
        .Include(o => o.Publisher)
        .SingleOrDefaultAsync(o => o.ID == id);
      if (order == null)
      {
        return NotFound();
      }
      return View(order);
    }

    public async Task<IActionResult> Order(int? id)
    {
      if (id == null)
        return NotFound();

      var order = await _context.Orders
        .SingleOrDefaultAsync(o => o.ID == id);

      if (order == null)
        return NotFound();

      //update order status and redirect to index page
      order.OrderPlaced = !order.OrderPlaced;
      await _context.SaveChangesAsync();
      return RedirectToAction("Index");
    }

    // POST: Orders/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,Publisher,OrderItems,OrderPlaced,OrderDelivered")] Order order)
    {
      if (id != order.ID)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(order);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!OrderExists(order.ID))
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
      return View(order);
    }

    // GET: Orders/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var order = await _context.Orders
          .SingleOrDefaultAsync(o => o.ID == id);
      if (order == null)
      {
        return NotFound();
      }

      return View(order);
    }

    // POST: Orders/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var order = await _context.Orders.SingleOrDefaultAsync(o => o.ID == id);
      _context.Orders.Remove(order);
      await _context.SaveChangesAsync();
      return RedirectToAction("Index");
    }

    private bool OrderExists(int id)
    {
      return _context.Orders.Any(o => o.ID == id);
    }
  }
}