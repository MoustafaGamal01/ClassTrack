using Microsoft.EntityFrameworkCore;

namespace WeladSanad.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Attend> Attends { get; set; }
        public DbSet<StudentAttend> StudentAttends { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
            .Property(s => s.Name)
            .IsUnicode(true);

            modelBuilder.Entity<Group>()
                .Property(g => g.Name)
                .IsUnicode(true);

            modelBuilder.Entity<Attend>()
                .Property(a => a.Type)
                .IsUnicode(true);

            modelBuilder.Entity<StudentAttend>()
                .Property(sa => sa.Description)
                .IsUnicode(true);
        }
    }
}
