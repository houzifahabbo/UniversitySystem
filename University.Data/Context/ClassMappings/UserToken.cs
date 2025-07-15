using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace University.Data.Context.ClassMappings
{
    public class UserTokenMapping : IEntityTypeConfiguration<IdentityUserToken<int>>
    {
        void IEntityTypeConfiguration<IdentityUserToken<int>>.Configure(EntityTypeBuilder<IdentityUserToken<int>> builder)
        {
            builder.ToTable("UserToken");
            builder.HasKey(u => u.UserId);
        }
    }
}