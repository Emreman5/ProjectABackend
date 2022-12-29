using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.Repository;
using DataAccess.Concrete.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess.Concrete.Repositories.Concrete
{
    public class MenuImageRepository : Repository<MenuImage>, IMenuImageRepository
    {
        public MenuImageRepository(DbContext context) : base(context)
        {
        }
    }
}
