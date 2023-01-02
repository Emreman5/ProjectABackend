using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.ResponseTypes;
using DataAccess.Concrete.Repositories.Abstract;
using DataAccess.Concrete.UnitOfWork;
using Model;

namespace Business.Concrete
{
    public class CampaignManager : ICampaignService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICampaignRepository _campaign;
        public CampaignManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _campaign = _unitOfWork.CampaignRepository;
        }

        public IDataResult<List<Campaign>> GetAll()
        {
            var result = _campaign.Query().ToList();
            return new SuccessDataResult<List<Campaign>>(result);
        }

        public IResult Add(Campaign campaign)
        {
            _campaign.AddAsync(campaign);
            return new SuccessResult();
        }

        public IResult Update(Campaign campaign)
        {
            _campaign.UpdateAsync(campaign);
            return new SuccessResult();
        }

        public IDataResult<Campaign> GetById(int id)
        {
            var result = _campaign.GetByIdAsync(id).Result;
            return new SuccessDataResult<Campaign>(result);
        }

        public IResult Delete(int id)
        {
            var selectedCampaign = _campaign.GetByIdAsync(id).Result;
            return new SuccessDataResult<Campaign>(selectedCampaign);
        }
    }
}
