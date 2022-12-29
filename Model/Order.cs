using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;

namespace Model
{
    public class Order : DatabaseEntity
    {
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool OrderStatus { get; set; }
    }
}
