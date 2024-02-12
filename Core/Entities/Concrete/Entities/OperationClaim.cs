using Core.Entities.Abstract;

namespace Core.Entities.Concrete.Entities;

public class OperationClaim : IEntity
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }

    public string Name { get; set; }

    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }

    public OperationClaim()
    {
        UserOperationClaims = new HashSet<UserOperationClaim>();
    }

    public OperationClaim(int id, string name) : this()
    {
        Id = id;
        Name = name;
    }
}
