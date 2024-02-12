using Core.DataAccess;
using Core.Entities.Concrete.Entities;

namespace DataAccess.Abstract;

public interface IUserOperationClaimRepository : IRepository<UserOperationClaim>
{
    public IList<OperationClaim> GetOperationClaimsByUserId(int userId);
}
