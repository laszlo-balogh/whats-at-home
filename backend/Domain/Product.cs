using System.ComponentModel.DataAnnotations;

namespace Domain
{ 
    public class Product
    {
        [Key] 
        public required string Barcode { get; set; }

        public required string Name { get; set; }
        public required string Unit { get; set; }
        public required string? Category { get; set; }
    }
}