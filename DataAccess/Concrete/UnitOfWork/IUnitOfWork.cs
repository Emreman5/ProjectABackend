using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Concrete.Repositories.Abstract;

namespace DataAccess.Concrete.UnitOfWork
{
    public interface IUnitOfWork
    {
        ITestRepository Test { get; }
        IProductRepository ProductRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        ICommentRepository CommentRepository { get; }
        ICampaignRepository CampaignRepository { get; }
        IReservationRepository ReservationRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IAdressRepository AdressRepository { get; }
        IOrderRepository Order { get; }
        IMenuImageRepository MenuImage { get; }
        Task CompleteAsync();
        void Dispose();
    }
}
