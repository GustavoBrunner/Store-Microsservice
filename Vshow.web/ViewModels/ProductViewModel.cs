namespace VshopWeb.Models {


    public class ProductViewModel {

        public int ProductId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
        
        public string? Image { get; set; }

        public double Price { get; set; }
        
        public string? CategoryName { get; set; }

        public int? CategoryId { get; set; }
    }
}