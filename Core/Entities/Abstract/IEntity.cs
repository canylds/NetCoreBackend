namespace Core.Entities.Abstract;

public interface IEntity : IEntityTimestamps
{
    int Id { get; set; }
}