using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class OrderDto
    {
        public int CustomerId { get; set; }
        public bool OrderStatus { get; set; }
        public List<OrderDetailDto>? Orders { get; set; }
        public DateTime OrderDate { get; set; }

        public Order CreateEntity()
        {
            return new Order { CustomerId = CustomerId, OrderStatus = OrderStatus, OrderDate = OrderDate };
        }
    }
}
