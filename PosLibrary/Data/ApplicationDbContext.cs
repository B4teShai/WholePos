using Microsoft.EntityFrameworkCore;
using PosLibrary.Models;

namespace PosLibrary.Data
{
    /// <summary>
    /// POS системд өгөгдлийн сангийн контекст үүсгэнэ.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// ApplicationDbContext классын шинэ жишээг эхлүүлнэ.
        /// </summary>
        /// <param name="options">Контекстыг тогтмол болгохын тулд ашиглагдана.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Хэрэглэгчдийн жагсаалтыг авах.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Бүтээгдэхүүнүүддийн жагсаалтыг авах.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Ангиллын жагсаалтыг авах.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Борлуулалтын жагсаалтыг авах.
        /// </summary>
        public DbSet<Sale> Sales { get; set; }

        /// <summary>
        /// Борлуулалтын бүтээгдэхүүнүүддийн жагсаалтыг авах.
        /// </summary>
        public DbSet<SaleItem> SaleItems { get; set; }

        /// <summary>
        /// Тохируулах модель үүсгэнэ.
        /// </summary>
        /// <param name="modelBuilder">Тохируулах модель үүсгэнэ.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasDiscriminator(u => u.Role)
                .HasValue<Manager>("Manager")
                .HasValue<Cashier>("Cashier");

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<SaleItem>()
                .HasOne(si => si.Sale)
                .WithMany(s => s.Items)
                .HasForeignKey(si => si.SaleId);

            modelBuilder.Entity<SaleItem>()
                .HasOne(si => si.Product)
                .WithMany()
                .HasForeignKey(si => si.ProductId);
        }
    }
} 