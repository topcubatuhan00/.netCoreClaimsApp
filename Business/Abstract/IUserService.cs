using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IUserService
    {
        void Add(RegisterAuthDto authDto);
        IResult Update(User user);
        IResult ChangePassword(UserChangePassDto userChangePassDto);
        IResult Delete(User user);
        IDataResult<List<User>> GetList();
        User GetByUserName(string username);
        IDataResult<User> GetByUserId(int id);
        List<OperationClaim> GetUserOperationClaims(int userId);
    }
}
