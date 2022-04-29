using Microsoft.EntityFrameworkCore;

namespace database
{
    public class EfDbContext : DbContext
    {
        public EfDbContext() : base() { }

        public DbSet<A> ARows { get; set; }
        public DbSet<B> BRows { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        public const string ConnectionString = "Data Source=.\\SQLEXPRESS; Initial Catalog=EfVsAdo; Integrated Security=True; MultipleActiveResultSets=True;Max Pool Size=250;";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<A>(a =>
            {
                a.HasKey(a => a.Id);
                a.Property(e => e.Id).UseIdentityColumn();
            });
            modelBuilder.Entity<B>(b =>
            {
                b.HasKey(b => b.Id);
                b.Property(b => b.Id).UseIdentityColumn();
            });
        }
    }
}