using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;

namespace Model
{
    public class Campaign : DatabaseEntity
    {
        public int ProductId { get; set; }
        public float Discount { get; set; }
        public string? Description { get; set; }
        public int Count { get; set; }

    }
}
