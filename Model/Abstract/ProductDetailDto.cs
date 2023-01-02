using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Model.Abstract
{
    public class ProductDetailDto
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? CategoryName { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public List<System.Uri>? Images { get; set; }
    }
}
