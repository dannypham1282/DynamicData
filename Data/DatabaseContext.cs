using DynamicData.Models;
using Microsoft.EntityFrameworkCore;

namespace DynamicData.Data
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Authorization> Authorization { get; set; }
        public DbSet<Field> Field { get; set; }
        public DbSet<FieldType> FieldType { get; set; }
        public DbSet<FieldValue> FieldValue { get; set; }
        public DbSet<FieldLog> FieldLog { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Library> Library { get; set; }
        public DbSet<LibraryLog> LibraryLog { get; set; }
        public DbSet<LibraryType> LibraryType { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<SecurityGroup> SecurityGroup { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<ItemFile> ItemFile { get; set; }
        public DbSet<ItemLog> ItemLog { get; set; }
        public DbSet<DefaultField> DefaultField { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<UserOrganization> UserOrganization { get; set; }
        public DbSet<OrganizationLibrary> OrganizationLibrary { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //if (!optionsBuilder.IsConfigured)
            //{
            //    IConfigurationRoot configuration = new ConfigurationBuilder()
            //       .SetBasePath(Directory.GetCurrentDirectory())
            //       .AddJsonFile("appsettings.json")
            //       .Build();
            //    var connectionString = configuration.GetConnectionString("appConn");
            //    optionsBuilder.UseSqlServer(connectionString);
            //}
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Authorization>().Property(b => b.GUID).HasDefaultValueSql("newid()");

            //modelBuilder.Entity<Field>().Property(b => b.GUID).HasDefaultValueSql("newid()");

            //modelBuilder.Entity<FieldType>().Property(b => b.GUID).HasDefaultValueSql("newid()");

            //modelBuilder.Entity<FieldValue>().Property(b => b.GUID).HasDefaultValueSql("newid()");

            //modelBuilder.Entity<FieldLog>().Property(b => b.GUID).HasDefaultValueSql("newid()");

            //modelBuilder.Entity<Item>().Property(b => b.GUID).HasDefaultValueSql("newid()");

            //modelBuilder.Entity<Library>().Property(b => b.GUID).HasDefaultValueSql("newid()");

            //modelBuilder.Entity<LibraryLog>().Property(b => b.GUID).HasDefaultValueSql("newid()");

            //modelBuilder.Entity<LibraryType>().Property(b => b.GUID).HasDefaultValueSql("newid()");

            //modelBuilder.Entity<Permission>().Property(b => b.GUID).HasDefaultValueSql("newid()");

            //modelBuilder.Entity<Role>().Property(b => b.GUID).HasDefaultValueSql("newid()");

            //modelBuilder.Entity<SecurityGroup>().Property(b => b.GUID).HasDefaultValueSql("newid()");

            //modelBuilder.Entity<User>().Property(b => b.GUID).HasDefaultValueSql("newid()");

            //modelBuilder.Entity<UserRole>().Property(b => b.GUID).HasDefaultValueSql("newid()");

            //modelBuilder.Entity<ItemFile>().Property(b => b.GUID).HasDefaultValueSql("newid()");

            //modelBuilder.Entity<ItemLog>().Property(b => b.GUID).HasDefaultValueSql("newid()");

        }
        public DbSet<DynamicData.Models.LinkLibrary> LinkLibrary { get; set; }
    }
}
