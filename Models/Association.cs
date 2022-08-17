#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace Categoris.Models;

public class Association
{
    [Key]
    public int AssociationId { get; set; }
    [Display(Name ="Category")]
    public int CategoryId { get; set; }
    public int ProductId { get; set; }
    
    public Product? Product { get; set; }
    public Category? Category  { get; set; }

}

