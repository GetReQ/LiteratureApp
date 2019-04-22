using System.ComponentModel.DataAnnotations;

namespace Literature.Models
{
  public class Publisher
  {
    public int ID { get; set; }

    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Display(Name = "Publisher")]
    public string Name
    {
      get { return ToString(); }
    }
                         
    public override string ToString()
    {
      return $"{FirstName} {LastName}";
    }
  }
}