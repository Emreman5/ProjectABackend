using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;

namespace Model
{
    public class Product : DatabaseEntity
    {
        [ForeignKey("Id")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Title { get; set; }
        public double Price { get; set; }
    }
}
