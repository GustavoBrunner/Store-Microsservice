using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ProductApi.Models;

public class ProductModelDto{
    

    public int ProductId { get; set; }

    [Required(ErrorMessage = "The {0} is required!")]
    [MaxLength(100)]
    public string? Name { get; set; }

    [Required(ErrorMessage = "The {0} is required")]
    [MaxLength(255, ErrorMessage ="")]
    public string? Description { get; set; }
    [Required(ErrorMessage = "The {0} is required")]
    public string? Image { get; set; }

    [Required(ErrorMessage = "The {0} is required")]
    [Precision(12,2)]
    public double Price { get; set; }
    [JsonIgnore]
    public CategoryModel? Category { get; set; }

    public string? CategoryName { get; set; }

    public int? CategoryId { get; set; }
}