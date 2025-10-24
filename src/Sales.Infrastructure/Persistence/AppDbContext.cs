
using Microsoft.EntityFrameworkCore;
using Sales.Domain.Entities;

namespace Sales.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Sale> Sales => Set<Sale>();
        public DbSet<SaleItem> SaleItems => Set<SaleItem>();

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("Sales");

                entity.HasKey(s => s.Id);

                entity.Property(s => s.SaleNumber)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(s => s.Date)
                      .IsRequired();

                entity.Property(s => s.Customer)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(s => s.Branch)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(s => s.IsCancelled)
                      .IsRequired()
                      .HasDefaultValue(false);

                entity.Ignore(s => s.TotalAmount);

                entity.HasMany(s => s.Items)
                      .WithOne()
                      .HasForeignKey("SaleId")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<SaleItem>(entity =>
            {
                entity.ToTable("SaleItems");

                entity.HasKey(i => i.Id);

                entity.Property(i => i.Product)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(i => i.Quantity)
                      .IsRequired();

                entity.Property(i => i.UnitPrice)
                      .HasColumnType("decimal(18,2)");

                entity.Property(i => i.Discount)
                      .HasColumnType("decimal(5,2)");

                entity.Ignore(i => i.Total);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
