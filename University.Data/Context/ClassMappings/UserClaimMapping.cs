using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace University.Data.Context.ClassMappings
{
    public class UserClaimMapping : IEntityTypeConfiguration<IdentityUserClaim<int>>
    {
        void IEntityTypeConfiguration<IdentityUserClaim<int>>.Configure(EntityTypeBuilder<IdentityUserClaim<int>> builder)
        {
            builder.ToTable("UserClaim");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("UserClaimId");
        }
    }
}