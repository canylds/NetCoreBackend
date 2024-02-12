using Core.Entities.Abstract;

namespace Core.Entities.Concrete.Entities;

public class User : IEntity
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }

    public bool Status { get; set; }

    public string Email { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }

    public User()
    {
        UserOperationClaims = new HashSet<UserOperationClaim>();
    }

    public User(int id, bool status, string email, byte[] passwordSalt, byte[] passwordHash, string firstName, string lastName) : this()
    {
        Id = id;
        Status = status;
        Email = email;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
        FirstName = firstName;
        LastName = lastName;
    }
}
