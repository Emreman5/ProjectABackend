using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.Repository;
using Model;

namespace DataAccess.Concrete.Repositories.Abstract
{
    public interface IOrderRepository : IRepository<Order>
    {
    }
}
