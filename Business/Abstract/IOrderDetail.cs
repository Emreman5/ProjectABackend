using Core.Utilities.ResponseTypes;
using Model;


namespace Business.Abstract
{
    public interface IOrderDetail
    {
        IDataResult<List<OrderDetail>> GetAll();
        IResult Add(OrderDetail orderDetail);
        IResult Update(OrderDetail orderDetail);
        IDataResult<OrderDetail> GetById(int id);
        IResult Delete(int id);
    }
}
