using _23_05_2021.HelpMethods;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq;
using System.Reflection;
using test_23_05_2021.models;

namespace test_23_05_2021
{
    class Context : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }

        public Context() : base()
        {
        }
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //1a fluent api
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                   .Property(x => x.Id).UseIdentityColumn();
            modelBuilder.Entity<Author>()
                .Property(x => x.Id).UseIdentityColumn();
            modelBuilder.Entity<Client>()
                    .Property(x => x.Id).UseIdentityColumn();
            modelBuilder.Entity<Order>()
                .Property(x => x.Id).UseIdentityColumn();

            modelBuilder.Entity<Book>()
                   .Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Author>()
                .Property(x => x.Name).IsRequired();

            //modelBuilder.Properties<string>()
            //.Where(s => s.Name == "Name")
            //.Configure(s => s.HasMaxLength(30).IsRequired());

            //modelBuilder.Conventions.Add(new NameConvention());
            //не в Core

             modelBuilder.Entity<Book>()
                .Property(x => x.Description).IsRequired().HasMaxLength(50); //fl api

            //1б shadow prop
            modelBuilder.Entity<Book>()
            .Property<string>("mark");
            
            //1а naming convention
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName("TEst_" + entity.GetTableName());
            }
            ///1а naming convention обновить обратно
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                var splutedGroup = entity.GetTableName().Split('_').ToList();
                splutedGroup.RemoveAt(0);
                entity.SetTableName(string.Join("_", splutedGroup));
            }

            
            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    foreach (var property in entityType.GetProperties())
            //    {
            //        foreach (var fk in entityType.FindForeignKeys(property))
            //        {
            //            fk.Relational().Name = fk.Relational().Name + "_Test";
            //        }
            //    }
            //}

            //base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddScoped<IOrderService, OrderService>();
        //}
        //public class NameConvention : Convention
        //{
        //    public NameConvention()
        //    {
        //        Properties<string>()
        //            .Where(s => s.Name == "Name")
        //            .Configure(s => s.HasMaxLength(30).IsRequired());
        //    }
        //}
    }
}
