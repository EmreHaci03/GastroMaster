using GastroMaster.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GastroMaster.DataAccessLayer.Concrete
{
    public class GastroMasterContext:IdentityDbContext<AppUser,AppRole,string>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-1G6PJJL\\SQLEXPRESS;Database=GastroMasterDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<CashRegister> CashRegisters { get; set; }
        public DbSet<MenuTable> MenuTables { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<AboutItem> AboutItems { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<Timeline> Timelines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>()
                .HasOne(x => x.Product)
                .WithMany(x => x.OrderDetails)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Basket>()
                .HasOne(x => x.Product)
                .WithMany(x => x.Baskets)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Basket>()
                .HasOne(x => x.MenuTable)
                .WithMany(x => x.Baskets)
                .HasForeignKey(x => x.MenuTableId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>().Property(x => x.Price).HasPrecision(18, 2);
            modelBuilder.Entity<OrderDetail>().Property(x => x.UnitPrice).HasPrecision(18, 2);
            modelBuilder.Entity<OrderDetail>().Property(x => x.TotalPrice).HasPrecision(18, 2);
            modelBuilder.Entity<Order>().Property(x => x.TotalPrice).HasPrecision(18, 2);
            modelBuilder.Entity<Newsletter>().HasIndex(x => x.Email).IsUnique(); // Kaydedilen Mail bir Daha Kaydedilmesin Diye

            base.OnModelCreating(modelBuilder);
        }
    }
}
