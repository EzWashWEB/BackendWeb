﻿using Microsoft.EntityFrameworkCore;

namespace EzWash.API.Domain.Persistence.Contexts
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        //TODO: Definir las entidades que pasarian a tablas a la BD
        //HINT: public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            //TODO: Definir los campos de las tablas
            
            //TODO: Inicializar las tablas segun las entidades
            //HINT: builder.Entity<Category>().ToTable("Categories");
            
            //TODO: Establecer constraints como PK, IsRequired, etc.
            //HINT: builder.Entity<Category>().HasKey(p => p.Id);
            //HINT: builder.Entity<Category>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            //HINT: builder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            
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
            
            //TODO: Realizar extension para transformar los nombres de las tablas a la convencion snakecase
            builder.ApplySnakeCaseNamingConvention();
        }
    }
}