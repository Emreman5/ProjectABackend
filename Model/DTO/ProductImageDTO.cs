using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Model.DTO
{
    public class ProductImageDto
    {
        public int ProductId { get; set; }
        public IFormFile File { get; set; }
    }
}
