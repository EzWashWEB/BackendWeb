using EzWash.API.Domain.Models;
using EzWash.API.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EzWash.API.Domain.Persistence.Contexts
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        //TODO: Definir las entidades que pasarian a tablas a la BD
        //HINT: public DbSet<Category> Categories { get; set; }
        
        //GEOGRAPHIC
        public DbSet<Department> Deparments { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Wallet> Wallets { get; set; }




        //INTERACTIONS
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Benefit> Benefits { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            //TODO: Definir los campos de las tablas
            
            //TODO: Inicializar las tablas segun las entidades
            //HINT: builder.Entity<Category>().ToTable("Categories");
            
            builder.Entity<Department>().ToTable("Departments");
            builder.Entity<Province>().ToTable("Provinces");
            builder.Entity<District>().ToTable("Districts");
            builder.Entity<Wallet>().ToTable("Wallets");



            builder.Entity<Plan>().ToTable("Plans");


            //TODO: Establecer constraints como PK, IsRequired, etc.
            //HINT: builder.Entity<Category>().HasKey(p => p.Id);
            //HINT: builder.Entity<Category>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            //HINT: builder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(30);

            builder.Entity<Department>().HasKey(p => p.Id);
            builder.Entity<Department>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Department>().Property(p => p.Name).IsRequired().HasMaxLength(30);

            builder.Entity<Province>().HasKey(p => p.Id);
            builder.Entity<Province>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Province>().Property(p => p.Name).IsRequired().HasMaxLength(30);

            builder.Entity<District>().HasKey(p => p.Id);
            builder.Entity<District>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<District>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            
            builder.Entity<Wallet>().HasKey(p => p.Id);
            builder.Entity<Wallet>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Wallet>().Property(p => p.Amount).IsRequired();
            builder.Entity<Wallet>().Property(p => p.Currency).IsRequired().HasMaxLength(30);







            builder.Entity<Plan>().HasKey(p => p.Id);
            builder.Entity<Plan>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Plan>().Property(p => p.Name).IsRequired().HasMaxLength(30);


            builder.Entity<Benefit>().HasKey(p => p.Id);
            builder.Entity<Benefit>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Benefit>().Property(p => p.Description).IsRequired().HasMaxLength(30);

            //TODO: Establecer relaciones con otras tablas
            /*
             Uno a muchos
             
             builder.Entity<Category>()
                .HasMany(p => p.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);
                
             Muchos a muchos -> Se crea la tabla combinada (ej. ProductTag), se crea una PK compuesta
             y se establecen dos relaciones de uno a muchos
             
            builder.Entity<ProductTag>().HasKey(pt => new {pt.ProductId, pt.TagId});
            
            builder.Entity<ProductTag>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.ProductTags)
                .HasForeignKey(pt => pt.ProductId);
            builder.Entity<ProductTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(p => p.ProductTags)
                .HasForeignKey(pt => pt.TagId);
             */


             builder.Entity<Department>()
                .HasMany(p => p.Provinces)
                .WithOne(p => p.Department)
                .HasForeignKey(p => p.DepartmentId);
             

             builder.Entity<Province>()
                .HasMany(p => p.Districts)
                .WithOne(p => p.Province)
                .HasForeignKey(p => p.ProvinceId);

              //TODO: Activar cuando Locations este implementado
             /*
              builder.Entity<District>()
                .HasMany(p => p.Locations)
                .WithOne(p => p.District)
                .HasForeignKey(p => p.DistrictId);
              */

            //TODO: Crear seed data o data inicial para no tener que crearlo en los endpoints a cada rato
            /*
            builder.Entity<Product>().HasData
            (
                new Product
                {
                    Id = 100, Name = "Apple", QuantityInPackage = 1, UnitOfMeasurement = EUnitOfMeasurement.Unity,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 101, Name = "Milk", QuantityInPackage = 1, UnitOfMeasurement = EUnitOfMeasurement.Liter,
                    CategoryId = 2
                }
            );
             */

            builder.Entity<Department>().HasData(
                new Department
                {
                    Id=1, Name="Lima"
                },
                new Department
                {
                    Id=2, Name="Piura"
                },
                new Department
                {
                    Id=3, Name="Tacna"
                }
            );
            builder.Entity<Province>().HasData(
                new Province
                {
                    Id=1, Name="Lima", DepartmentId = 1
                },
                new Province
                {
                    Id=2, Name="Ayabaca", DepartmentId = 2
                },
                new Province
                {
                    Id=3, Name="Candarave", DepartmentId = 3
                }
            );
            
            builder.Entity<District>().HasData(
                new District
                {
                    Id=1, Name="La Molina", ProvinceId = 1
                },
                new District
                {
                    Id=2, Name="Lagunas", ProvinceId = 2
                },
                new District
                {
                    Id=3, Name="Camilaca", ProvinceId = 3
                }
            );

            builder.Entity<Wallet>().HasData(
                new Wallet
                {
                    Id = 1,
                    Amount = 0,
                    Currency = "Soles"
                },
                new Wallet
                {
                    Id = 2,
                    Amount = 50,
                    Currency = "Soles"
                },
                new Wallet
                {
                    Id = 3,
                    Amount = 10,
                    Currency = "Soles"
                }
            );


            builder.Entity<Plan>().HasData(
                new Plan
                {
                    Id = 1, Name = "Free"
                },
                new Plan
                {
                    Id = 2, Name = "Premium"
                }
                
            );

            builder.Entity<Benefit>().HasData(
               new Benefit
               {
                   Id = 1, Description = "Quick Wash"
               },
               new Benefit
               {
                   Id = 2, Description = "Deliver Free"
               },

               new Benefit
               {
                   Id = 3,
                   Description = "free scented"
               }

           );

            /*
               builder.Entity<Benefit>()
               .HasMany(p => p.Benefit)
               .WithOne(p => p.Plan)
               .HasForeignKey(p => p.PlanId);
            */


            builder.ApplySnakeCaseNamingConvention();
        }
    }
}