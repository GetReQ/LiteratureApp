namespace Literature.Models
{
  public class Publication
  {
    public int ID { get; set; }
    public string Title { get; set; }
    public string Language { get; set; }
    public string Code { get; set; }

    public override string ToString()
    {
      return $"{Title} ({Code})";
    }
  }
}