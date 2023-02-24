using Business.Abstract;
using Business.Aspects.Secured;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.ValidationAspect;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Conrete
{
    public class OperationClaimManager : IOperationClaimService
    {

        private readonly IOperationClaimDal? _operationClaimDal;

        public OperationClaimManager(IOperationClaimDal? operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }

        [ValidationAspect(typeof(OperationClaimValidator))]
        public IResult Add(OperationClaim operationClaim)
        {
            var isNameAvailable = IsNameAvailable(operationClaim.Name);

            if (isNameAvailable.Success)
            {
                _operationClaimDal.Add(operationClaim);
                return new SuccessResult(OperationClaimMessage.Added);
            }
            return new ErrorResult(isNameAvailable.Message);
        }

        public IResult Delete(OperationClaim operationClaim)
        {
            _operationClaimDal.Delete(operationClaim);
            return new SuccessResult(OperationClaimMessage.Deleted);
        }

        public IDataResult<OperationClaim> GetById(int id)
        {
            List<OperationClaim> claims = _operationClaimDal.GetAll();

            for (int i = 0; i < claims.Count; i++)
            {
                if (claims[i].Id == id)
                {
                    return new SuccessDataResult<OperationClaim>(claims[i]);
                }

            }

            return new ErrorDataResult<OperationClaim>("Böyle bir yetki yok.");


        }

        [SecuredAspect("ADMIN")]
        public IDataResult<List<OperationClaim>> GetList()
        {
            return new SuccessDataResult<List<OperationClaim>>(_operationClaimDal.GetAll());
        }


        [ValidationAspect(typeof(OperationClaimValidator))]
        public IResult Update(OperationClaim operationClaim)
        {
            _operationClaimDal.Update(operationClaim);
            return new SuccessResult(OperationClaimMessage.Updated);
        }

        private IResult IsNameAvailable(string name)
        {
            var result = _operationClaimDal.Get(p => p.Name == name);
            if (result.Name == name)
            {
                return new ErrorResult(OperationClaimMessage.NotExist);
            }
            return new SuccessResult();
        }

    }
}
