using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Abstract;

namespace Model.DTO
{
    public class RestaurantMenuDto : IDto
    {
        public string? RestaurantName { get; set; }
        public List<Product>? Menus { get; set; }

    }
}
