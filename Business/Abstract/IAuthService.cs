using Core.Utilities.Result.Abstract;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IResult Register(RegisterAuthDto authDto);
        IDataResult<Token> Login(LoginAuthDto loginAuthDto);
    }
}
