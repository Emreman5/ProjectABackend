using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;

namespace Model
{
    public class Category : DatabaseEntity
    {
        public string? CategoryName { get; set; }
    }
}
