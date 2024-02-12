using Core.Entities.Concrete.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.EntityConfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oc => oc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oc => oc.DeletedDate).HasColumnName("DeletedDate");

        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();

        builder.HasIndex(indexExpression: oc => oc.Name, name: "UK_OperationClaims_Name").IsUnique();

        builder.HasMany(oc => oc.UserOperationClaims)
               .WithOne(uoc => uoc.OperationClaim)
               .HasForeignKey(uoc => uoc.OperationClaimId)
               .IsRequired();

        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);
    }
}
