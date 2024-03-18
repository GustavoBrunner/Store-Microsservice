using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models;

public class CategoryModelDto {
    
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Attribute {0} is required!")]
    [MaxLength(200)]
    public string? Name { get; set; }

}