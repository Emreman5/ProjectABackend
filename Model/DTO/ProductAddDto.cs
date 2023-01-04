using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Model.DTO
{
    public class ProductPostDto
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Title { get; set; }
        public double Price { get; set; }
        public Product CreateEntity()
        {
            return new Product
                { CategoryId = CategoryId, Name = Name, Description = Description, Title = Title, Price = Price };
        }
    }
}
