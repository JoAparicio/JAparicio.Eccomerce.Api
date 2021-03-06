using System;
using System.Text.Json.Serialization;
namespace JAparicio.Ecommerce.Api.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }

    }
}
