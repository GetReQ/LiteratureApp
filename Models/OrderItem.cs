namespace Literature.Models
{
  public class OrderItem
  {
    public int ID { get; set; }

    public Publication Publication { get; set; }

    public int Quantity { get; set; }
  }
}