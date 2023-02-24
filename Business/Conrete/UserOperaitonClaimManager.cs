using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.ValidationAspect;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Conrete
{
    public class UserOperaitonClaimManager : IUserOperaitonClaimService
    {
        private readonly IUserOperationDal? _userOperationDal;
        private readonly IOperationClaimService _operationClaimService;
        private readonly IUserService _userService;

        public UserOperaitonClaimManager(IUserOperationDal userOperationDal, IOperationClaimService operationClaimService, IUserService userService)
        {
            _userOperationDal = userOperationDal;
            _operationClaimService = operationClaimService;
            _userService = userService;
        }

        [ValidationAspect(typeof(UserOperationClaimValidator))]
        public IResult Add(UserOperationClaim userOperaitonClaim)
        {
            var isItDefined = IsOperationSetAvailable(userOperaitonClaim);
            var isItAvailable = IsOperationClaimSetAvailable(userOperaitonClaim.OperationClaimId);
            var userIsDefined = IsUserAvailable(userOperaitonClaim.UserId);

            if (isItDefined.Success && isItAvailable.Success && userIsDefined.Success)
            {
                _userOperationDal.Add(userOperaitonClaim);
                return new SuccessResult(UserOperationClaimMessage.Added);
            }
            return new ErrorResult(isItDefined.Message + isItAvailable.Message + userIsDefined.Message);

        }

        public IResult Delete(UserOperationClaim userOperaitonClaim)
        {
            _userOperationDal.Delete(userOperaitonClaim);
            return new SuccessResult(UserOperationClaimMessage.Deleted);
        }

        public IDataResult<UserOperationClaim> GetById(int id)
        {

            List<UserOperationClaim> claims = _userOperationDal.GetAll();
            for (int i = 0; i < claims.Count; i++)
            {
                if (claims[i].Id == id)
                {
                    return new SuccessDataResult<UserOperationClaim>(claims[i]);
                    
                }

            }
            return new ErrorDataResult<UserOperationClaim>("Tanımlanmış Yetki Bulunamadı");
        }

        public IDataResult<List<UserOperationClaim>> GetList()
        {
            var result = _userOperationDal.GetAll();
            return new SuccessDataResult<List<UserOperationClaim>>(result);
        }

        [ValidationAspect(typeof(UserOperationClaimValidator))]
        public IResult Update(UserOperationClaim userOperaitonClaim)
        {
            _userOperationDal.Update(userOperaitonClaim);
            return new SuccessResult(UserOperationClaimMessage.Updated);
        }


        private IResult IsOperationSetAvailable(UserOperationClaim userOperationClaim)
        {
            // ! kullanıcıya o rol ekliyse hata verir.
            var results = _userOperationDal.GetAll();

            foreach (var res in results)
            {
                if (res.UserId == userOperationClaim.UserId && res.OperationClaimId == userOperationClaim.OperationClaimId)
                {
                    return new ErrorResult(UserOperationClaimMessage.AlreadyAdded);
                }
            }

            return new SuccessResult();

        }

        private IResult IsOperationClaimSetAvailable(int operationClaimId)
        {
            // ! veri tabanında böyle bir yetki var ise true dön.
            var result = _operationClaimService.GetById(operationClaimId);

            if (result.Success)
            {
                return new SuccessResult();
            }
            return new ErrorResult(result.Message);
        }

        private IResult IsUserAvailable(int userId)
        {
            var result = _userService.GetByUserId(userId);

            if (result.Data != null)
                return new SuccessResult();
            return new ErrorResult(result.Message);


        }

    }
}
