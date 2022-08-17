#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Categoris.Models;


public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required(ErrorMessage ="is required")]
    [Display(Name ="Name")]
    public string Name { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public List<Association> CategoryList { get; set; } = new List<Association>(); // is a list of association object
}