using Core.Utilities.ResponseTypes;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICampaignService
    {
        IDataResult<List<Campaign>> GetAll();
        IResult Add(Campaign campaign);
        IResult Update(Campaign campaign);
        IDataResult<Campaign> GetById(int id);
        IResult Delete(int id);
    }
}
