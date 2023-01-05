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
        public string Category { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Title { get; set; }
        public double Price { get; set; }
        public Product CreateEntity(int categoryId)
        {
            return new Product
                { CategoryId =  categoryId ,Name = Name, Description = Description, Title = Title, Price = Price };
        }
    }
}
