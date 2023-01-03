using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class CategoryDto
    {
        public string? CategoryName { get; set; }

        public Category CreateEntity()
        {
            return new Category { CategoryName = CategoryName };
        }
    }
}
