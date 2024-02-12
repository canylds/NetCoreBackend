using Core.Entities.Abstract;

namespace Core.Entities.Concrete.Entities;

public class UserOperationClaim : IEntity
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }

    public int UserId { get; set; }
    public int OperationClaimId { get; set; }

    public virtual User? User { get; set; }
    public virtual OperationClaim? OperationClaim { get; set; }

    public UserOperationClaim()
    {

    }

    public UserOperationClaim(int id, int userId, int operationClaimId) : this()
    {
        Id = id;
        UserId = userId;
        OperationClaimId = operationClaimId;
    }
}
