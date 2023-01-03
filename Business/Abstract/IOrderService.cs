using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.ResponseTypes;
using Model;
using Model.DTO;

namespace Business.Abstract
{
    public interface IOrderService
    {
        public Task<IResult> CreateOrder(OrderDto order);
        public IResult DeleteOrder(int orderId);
        public Task<IDataResult<List<Order>>> GetOrdersByCustomerId(int customerId);
        public Task<IDataResult<List<Order>>> GetAllOrders();
        public Task<IDataResult<List<OrderDetail>>> GetOrderDetailsById(int orderId);



    }
}
