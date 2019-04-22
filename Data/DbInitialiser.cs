using Literature.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Literature.Data
{
  public static class DbInitialiser
  {
    public static void Initialize(MyDatabaseContext context)
    {

      context.Database.EnsureCreated();

      if (context.Publication.Any())
        return; //Database seeded

      if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
        ProductionSeed(context);
      else
        TestSeed(context);
    }

    private static void TestSeed(MyDatabaseContext context)
    {

      #region Publishers

      var publishers = new Publisher[]
      {
        new Publisher { FirstName = "Teddy", LastName="Heidrick" },
        new Publisher { FirstName = "Rafaela", LastName="Burner" },
        new Publisher { FirstName = "Ehtel", LastName="Mcconnaughey" },
        new Publisher { FirstName = "Leanna", LastName="Bufkin" },
        new Publisher { FirstName = "Krystal", LastName="Lattimore" },
        new Publisher { FirstName = "Lissa", LastName="Sivils" },
        new Publisher { FirstName = "Rana", LastName="Rhines" },
        new Publisher { FirstName = "Letty", LastName="Berryhill" },
        new Publisher { FirstName = "Lashell", LastName="Hoskinson" },
        new Publisher { FirstName = "Giovanni", LastName="Chau" },
        new Publisher { FirstName = "Keven", LastName="Oconnor" },
        new Publisher { FirstName = "Nakia", LastName="Eder" },
        new Publisher { FirstName = "Evalyn", LastName="Stillwell" },
        new Publisher { FirstName = "Isaias", LastName="Nicolai" },
        new Publisher { FirstName = "Nikki", LastName="Knight" },
        new Publisher { FirstName = "Vania", LastName="Rozell" },
        new Publisher { FirstName = "Dodie", LastName="Silvera" },
        new Publisher { FirstName = "Tonia", LastName="Riddles" },
        new Publisher { FirstName = "Lanette", LastName="Schutte" },
        new Publisher { FirstName = "Gladis", LastName="Cost" },
      };

      foreach (Publisher p in publishers)
        context.Publishers.Add(p);
      context.SaveChanges();

      #endregion //Publishers

      #region Publications

      var publications = new Publication[]
      {
        new Publication {Title = "Days Text 2019", Language = "English", Code = "es19-E" },
        new Publication {Title = "Insight on the Scriptures Vol1", Language = "English", Code = "it-1-E" },
        new Publication {Title = "What Can the Bible Teach Us?", Language = "English", Code = "bhs-E" },
        new Publication {Title = "What Does the Bible Really Teach?", Language = "English", Code = "bh-E" },
        new Publication {Title = "Pure Worship of Jehovah - Restored at Last!", Language = "English", Code = "rr-E" }
      };

      foreach (Publication p in publications)
        context.Publication.Add(p);
      context.SaveChanges();

      #endregion //Publications

      #region Orders

      var orders = new Order[]
      {
        new Order
        {
          Publisher = publishers.Single(i => i.LastName == "Burner"),
          OrderItems = new OrderItem[] 
          {
            new OrderItem()
            {
              Publication = publications.Single(i => i.Code == "es19-E"),
              Quantity = 1
            },
            new OrderItem()
            {
              Publication = publications.Single(i => i.Code == "bhs-E"),
              Quantity = 1
            }
          },
          OrderDate = DateTime.UtcNow,
          OrderPlaced = false,
          OrderDelivered = false
        },
        new Order
        {
          Publisher = publishers.Single(i => i.LastName == "Lattimore"),
          OrderItems = new OrderItem[] 
          {
            new OrderItem()
            {
              Publication = publications.Single(i => i.Code == "es19-E"),
              Quantity = 2
            }
          },
          OrderDate = DateTime.UtcNow.AddDays(-20),
          OrderPlaced = false,
          OrderDelivered = false
        },
        new Order
        {
          Publisher = publishers.Single(i => i.LastName == "Knight"),
          OrderItems = new OrderItem[] 
          {
            new OrderItem()
            {
              Publication = publications.Single(i => i.Code == "rr-E"),
              Quantity = 1
            }
          },
          OrderDate = DateTime.UtcNow.AddDays(-17),
          OrderPlaced = false,
          OrderDelivered = false
        },
        new Order
        {
          Publisher = publishers.Single(i => i.LastName == "Schutte"),
          OrderItems = new OrderItem[] 
          {
            new OrderItem()
            {
              Publication = publications.Single(i => i.Code == "bhs-E"),
              Quantity = 3
            },
            new OrderItem()
            {
              Publication = publications.Single(i => i.Code == "es19-E"),
              Quantity = 1
            },
            new OrderItem()
            {
              Publication = publications.Single(i => i.Code == "rr-E"),
              Quantity = 1
            }
          },
          OrderDate = DateTime.UtcNow.AddDays(-32),
          OrderPlaced = true,
          OrderDelivered = false
        },
        new Order
        {
          Publisher = publishers.Single(i => i.LastName == "Sivils"),
          OrderItems = new OrderItem[] 
          {
            new OrderItem()
            {
              Publication = publications.Single(i => i.Code == "rr-E"),
              Quantity = 1
            }
          },
          OrderDate = DateTime.UtcNow.AddDays(-26),
          OrderPlaced = false,
          OrderDelivered = false
        }
      };

      foreach (Order o in orders)
        context.Orders.Add(o);
      context.SaveChanges();

      #endregion //Orders

    }

    private static void ProductionSeed(MyDatabaseContext context)
    {
      throw new NotImplementedException();
    }
  }
}
