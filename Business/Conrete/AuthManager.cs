using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.ValidationAspect;
using Core.Utilities.Business;
using Core.Utilities.Hashing;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Conrete
{
    public class AuthManager : IAuthService
    {

        private readonly IUserService? _userService;
        private readonly ITokenHandler? _tokenHandler;

        public AuthManager(IUserService? userService, ITokenHandler? tokenHandler)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
        }

        public IDataResult<Token> Login(LoginAuthDto loginAuthDto)
        {
            var user = _userService.GetByUserName(loginAuthDto.Name);
            
            if(user.Name != loginAuthDto.Name)
            {
                return new ErrorDataResult<Token>("Kullanıcı Bilgisi Bulunamadı");
            }
            
            var result = HashingHelper.VerifyPasswordHash(loginAuthDto.Password, user.PasswordHash, user.PasswordSalt);
            List<OperationClaim> claims = _userService.GetUserOperationClaims(user.Id.Value);
            if (result)
            {
                Token token = new Token();
                //token = _tokenHandler.CreateToken(user, claims);
                return new SuccessDataResult<Token>(token);
            }
            return new ErrorDataResult<Token>("Kullanıcı adı veya parola hatalı!!!");
        }

        [ValidationAspect(typeof(AuthValidator))]
        public IResult Register(RegisterAuthDto authDto)
        {
            IResult result = BusinessRules.Run(
                 UsernameExist(authDto.name),
                 CheckImageExtensions(authDto.image.FileName),
                 CheckImageSize(authDto.image.Length)
             );

            if (result != null)
            {
                return result;
            }




            _userService.Add(authDto);
            return new SuccessResult("Kullanıcı Başarıyla Kayıt Oldu.");


        }


        private IResult UsernameExist(string username)
        {
            var res = _userService.GetByUserName(username);
            if (res.Name == username)
            {
                return new ErrorResult("Bu kullanıcı adı alınmış.");
            }
            else
            {
                return new SuccessResult();
            }
        }

        private IResult CheckImageSize(long size)
        {
            decimal checkSize = Convert.ToDecimal(size * 0.000001);

            if (checkSize > 3)
            {
                return new ErrorResult("Resim boyutu 1mb'den fazla olamaz.");
            }
            return new SuccessResult();
        }

        private IResult CheckImageExtensions(string fileName)
        {

            var ext = fileName.Substring(fileName.LastIndexOf('.'));
            var extension = ext.ToLower();
            List<string> AllowFileExtensions = new List<string> { ".jpg", ".jpeg", ".gif", ".png" };
            if (!AllowFileExtensions.Contains(extension))
            {
                return new ErrorResult("Doğru Formatta Resim Ekleyiniz!!");
            }
            return new SuccessResult();
        }

    }
}
