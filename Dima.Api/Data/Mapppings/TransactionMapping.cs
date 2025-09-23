using Dima.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mapppings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(t => t.Title).HasMaxLength(100).HasColumnType("VARCHAR").IsRequired();
            builder.Property(t => t.CreatedAt).IsRequired();
            builder.Property(t => t.PaidOrReceivedAt).IsRequired(false);
            builder.Property(t => t.Type).HasColumnType("SMALLINT").IsRequired();
            builder.Property(t => t.Amount).HasColumnType("MONEY").IsRequired();
            builder.Property(t => t.UserId).IsRequired().HasColumnType("VARCHAR").HasMaxLength(160);
           
            builder.HasIndex(t => t.Id, "IX_Transaction_Id").IsUnique();
        }
    }
}
