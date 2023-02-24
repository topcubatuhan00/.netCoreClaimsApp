using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, SimpleContextDb>, IUserDal
    {
        public List<OperationClaim> GetUserOperationClaims(int id)
        {
            using (var context = new SimpleContextDb())
            {
                var result = from userOperationClaim in context.UserOperationClaims.Where(claim => claim.Id == id)
                             join operationClaim in context.OperationClaims on userOperationClaim.OperationClaimId equals operationClaim.Id
                             select new OperationClaim
                             {
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name,
                             };
                return result.OrderBy(p => p.Name).ToList();


            }
        }
    }
}
