using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Literature.Models
{
  public class Order
  {
    public int ID { get; set; }

    public Publisher Publisher { get; set; }

    [Display(Name = "Items")]
    public ICollection<OrderItem> OrderItems { get; set; }

    [Display(Name = "# Items")]
    public int NumberOfItems
    {
      get { return (OrderItems == null ? 0 : OrderItems.Count); }
    }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    [Display(Name = "Date Placed")]
    public DateTime OrderDate { get; set; }

    [Display(Name = "Order Placed")]
    public bool OrderPlaced { get; set; }

    [Display(Name = "Order Delivered")]
    public bool OrderDelivered { get; set; }
  }
}