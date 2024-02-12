using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.Entities;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Repositories;

public class UserOperationClaimRepository : EfEntityRepositoryBase<UserOperationClaim, BaseDbContext>, IUserOperationClaimRepository
{
    public UserOperationClaimRepository(BaseDbContext context) : base(context)
    {

    }

    public IList<OperationClaim> GetOperationClaimsByUserId(int userId)
    {
        return Query().AsNoTracking()
                            .Where(uoc => uoc.UserId == userId)
                            .Include(uoc => uoc.OperationClaim)
                            .Select(uoc => new OperationClaim
                            {
                                Id = uoc.OperationClaimId,
                                Name = uoc.OperationClaim.Name
                            }).ToList();
    }
}
