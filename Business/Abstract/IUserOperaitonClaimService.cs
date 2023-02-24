using Core.Utilities.Result.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserOperaitonClaimService
    {
        IResult Add(UserOperationClaim userOperaitonClaim);
        IResult Update(UserOperationClaim userOperaitonClaim);
        IResult Delete(UserOperationClaim userOperaitonClaim);
        IDataResult<List<UserOperationClaim>> GetList();
        IDataResult<UserOperationClaim> GetById(int id);
    }
}
