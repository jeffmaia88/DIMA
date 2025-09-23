using Dima.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mapppings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(c => c.Title).HasMaxLength(100).HasColumnType("VARCHAR").IsRequired();
            builder.Property(c => c.Description).HasMaxLength(255).HasColumnType("VARCHAR").IsRequired(false);
            builder.Property(c => c.UserId).HasMaxLength(160).HasColumnType("VARCHAR").IsRequired();

            builder.HasIndex(c=> c.Id, "IX_Category_Id").IsUnique();


        }
    }
}
