using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mapppings.Identity
{
    public class IdentityRoleClaimMapping : IEntityTypeConfiguration<IdentityRoleClaim<long>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<long>> builder)
        {
            builder.ToTable("IdentityRoleClaim");
            builder.HasKey(rc => rc.Id);
            builder.Property(u => u.ClaimType).HasMaxLength(256);
            builder.Property(u => u.ClaimValue).HasMaxLength(256);
        }
    }
}
