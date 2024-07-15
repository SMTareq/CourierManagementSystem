using GAMEPORTALCMS.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace GAMEPORTALCMS.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<tbl_User> Users { get; set; }
        public DbSet<tbl_Order> tbl_Orders { get; set; }
        public DbSet<tbl_TrackInfo> tbl_Tracks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<tbl_User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<tbl_Order>()
               .HasKey(u => u.Id);

            modelBuilder.Entity<tbl_TrackInfo>()
               .HasKey(u => u.Id);

        }
    }
}
