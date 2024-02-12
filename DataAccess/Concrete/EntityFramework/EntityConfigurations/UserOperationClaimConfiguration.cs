using Core.Entities.Concrete.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.EntityConfigurations;

public class UserOperationClaimConfiguration : IEntityTypeConfiguration<UserOperationClaim>
{
    public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
    {
        builder.ToTable("UserOperationClaims").HasKey(uoc => uoc.Id);

        builder.Property(uoc => uoc.Id).HasColumnName("Id").IsRequired();
        builder.Property(uoc => uoc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(uoc => uoc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(uoc => uoc.DeletedDate).HasColumnName("DeletedDate");

        builder.Property(uoc => uoc.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(uoc => uoc.OperationClaimId).HasColumnName("OperationClaimId").IsRequired();

        builder.HasOne(uoc => uoc.User)
               .WithMany(u => u.UserOperationClaims)
               .HasForeignKey(uoc => uoc.UserId);

        builder.HasOne(uoc => uoc.OperationClaim)
               .WithMany(oc => oc.UserOperationClaims)
               .HasForeignKey(uoc => uoc.OperationClaimId);

        builder.HasQueryFilter(uoc => !uoc.DeletedDate.HasValue);
    }
}