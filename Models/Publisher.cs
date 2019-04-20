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

  }
}
