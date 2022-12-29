using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Abstract;

namespace Model.DTO
{
    public class MenuDto : IDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Title { get; set; }
        public string? IconBase64 { get; set; }
        public double Price { get; set; }
    }
}
