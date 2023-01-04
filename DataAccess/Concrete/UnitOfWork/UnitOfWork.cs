using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Concrete.Context;
using DataAccess.Concrete.Repositories.Abstract;
using DataAccess.Concrete.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess.Concrete.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        public ITestRepository Test { get; private set; }
        public IProductRepository ProductRepository { get; }
        public IOrderDetailRepository OrderDetailRepository { get; }
        public ICommentRepository CommentRepository { get; }
        public ICampaignRepository CampaignRepository { get; }
        public IReservationRepository ReservationRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IAddressRepository AdressRepository { get; }

        public IOrderRepository Order { get; }
        public IMenuImageRepository MenuImage { get; }

        public UnitOfWork(DbContext context)
        {
            _dbContext =  context;
            ProductRepository = new ProductRepository(_dbContext);
            Test = new TestRepository(_dbContext);
            Order = new OrderRepository(_dbContext);
            MenuImage = new MenuImageRepository(_dbContext);
            OrderDetailRepository = new OrderDetailRepository(_dbContext);
            CommentRepository = new CommentRepository(_dbContext);
            CategoryRepository = new CategoryRepository(_dbContext);
            ReservationRepository = new ReservationRepository(_dbContext);
            AdressRepository = new AddressRepository(_dbContext);
            CampaignRepository = new CampaignRepository(_dbContext);
        }
        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public CustomUser GetUserByToken(string token)
        {
            var userId = _dbContext.Set<ApplicationUserToken>().FirstOrDefault(u => u.Value == token).UserId;
            var user = _dbContext.Set<CustomUser>().FirstOrDefault(u => u.Id == userId);
            return user;
        }

        public ApplicationUserToken FindToken(string token)
        {
            var userToken = _dbContext.Set<ApplicationUserToken>().FirstOrDefault(t => t.Value == token);
            return userToken;
        }

        public MsSqlDbContext GetContext()
        {
            return (MsSqlDbContext)_dbContext;
        }
    }
}
