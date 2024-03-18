using System.Text.Json.Serialization;

namespace ProductApi.Models;

public class ProductModel {

    public int ProductId { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public double Price { get; set; }
    [JsonIgnore]
    public CategoryModel? Category { get; set; }

    public int? CategoryId { get; set; }
}