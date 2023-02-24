using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Caching;
using Core.Aspect.Transaction;
using Core.Aspect.ValidationAspect;
using Core.Utilities.Hashing;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Conrete
{
    public class UserManager : IUserService
    {

        private readonly IUserDal _dal;
        private readonly IFileService _fileService;

        public UserManager(IUserDal dal, IFileService fileService)
        {
            _dal = dal;
            _fileService = fileService;
        }

        //[RemoveCacheAspect("IUserService.GetList")]
        public async void Add(RegisterAuthDto authDto)
        {
            string fileName = _fileService.FileSave(authDto.image, "./Content/img/");

            var user = CreateUser(authDto, fileName);

            _dal.Add(user);
        }

        private User CreateUser(RegisterAuthDto authDto, string fileName)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(authDto.password, out passwordHash, out passwordSalt);

            User user = new User
            {
                Name = authDto.name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                ImageUrl = fileName,
            };
            return user;
        }

        public User GetByUserName(string username)
        {

            List<User> users = _dal.GetAll();

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Name == username)
                {
                    return users[i];
                }

            }

            return new User { };

        }

        [CacheAspect(60)]
        public List<User> GetList()
        {
            return _dal.GetAll();
        }

        [ValidationAspect(typeof(UserValidator))]
        [TransactionAspect]
        public IResult Update(User user)
        {
            _dal.Update(user);
            return new SuccessResult(UserMessage.UpdatedUser);
        }

        public IResult Delete(User user)
        {
            _dal.Delete(user);
            return new SuccessResult(UserMessage.DeletedUser);
        }

        IDataResult<List<User>> IUserService.GetList()
        {
            var res = _dal.GetAll();
            return new SuccessDataResult<List<User>>(res);
        }

        public IDataResult<User> GetByUserId(int id)
        {
            List<User> users = _dal.GetAll();


            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Id == id)
                {
                    return new SuccessDataResult<User>(users[i]);
                }

            }
            return new ErrorDataResult<User>("Kullanıcı Bulunamadı");
        }

        public IResult ChangePassword(UserChangePassDto userChangePassDto)
        {
            var user = _dal.Get(p => p.Id == userChangePassDto.UserId);

            bool result = HashingHelper.VerifyPasswordHash(userChangePassDto.CurrentPassword, user.PasswordHash, user.PasswordSalt);
            if (!result)
            {
                return new ErrorResult(UserMessage.CurrentPassIsWrong);
            }
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(userChangePassDto.NewPassword, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _dal.Update(user);
            return new SuccessResult(UserMessage.UpdatedPassword);
        }

        public List<OperationClaim> GetUserOperationClaims(int userId)
        {
            return _dal.GetUserOperationClaims(userId);
        }
    }
}
