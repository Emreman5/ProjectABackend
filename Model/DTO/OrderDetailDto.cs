using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class OrderDetailDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public OrderDetail CreateEntity(int orderId)
        {
            return new OrderDetail()
            {
                OrderId = orderId,
                Price = Price,
                ProductId = ProductId,
                Quantity = Quantity
            };
        }

    }
}
