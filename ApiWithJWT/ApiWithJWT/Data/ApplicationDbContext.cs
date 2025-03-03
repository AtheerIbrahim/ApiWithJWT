﻿using ApiWithJWT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ApiWithJWT.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
	:IdentityDbContext<Account,Role,Guid>(options)
{


	public DbSet<Customer> Customers { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Subcategory> Subcategories { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<OrderItem> OrderItems { get; set; }
	public DbSet<Cart> Carts { get; set; }


	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<Account>(entity =>
		{
			entity.ToTable("Acounts");
		});

		builder.Entity<Customer>(entity =>
		{
			entity.ToTable("Customers");
		});

		builder.Entity<Role>(entity =>
		{
			entity.ToTable("Roles");
		});


		builder.Entity<IdentityUserRole<Guid>>(entity =>
		{
			entity.ToTable("UserRoles");
		});

		builder.Entity<IdentityUserClaim<Guid>>(entity =>
		{
			entity.ToTable("UserClaims");
		});

		builder.Entity<IdentityUserLogin<Guid>>(entity =>
		{
			entity.ToTable("UserLogins");
		});

		builder.Entity<IdentityRoleClaim<Guid>>(entity =>
		{
			entity.ToTable("RoleClaims");

		});

		builder.Entity<IdentityUserToken<Guid>>(entity =>
		{
			entity.ToTable("UserTokens");
		});




		/////////////////////////////
		///

		builder
			 .Entity<Product>()
			 .HasOne(p => p.Subcategory)
			 .WithMany(s => s.Products)
			 .HasForeignKey(p => p.SubcategoryId);
		// Configure one-to-many relationship between Category and Subcategory
		builder
			.Entity<Subcategory>()
			.HasOne(s => s.Category) // Each Subcategory belongs to one Category
			.WithMany(c => c.Subcategories) // Each Category can have multiple Subcategories
			.HasForeignKey(s => s.CategoryId); // Foreign key property in Subcategory

		// Configure one-to-many relationship between Category and Product
		builder
			.Entity<Product>()
			.HasOne(p => p.Category) // Each product has one category
			.WithMany(c => c.Products) // Each category has many products
			.HasForeignKey(p => p.CategoryId); // Foreign key property in Product class

		// Configure one-to-many relationship between Subcategory and Product
		builder
			.Entity<Product>()
			.HasOne(p => p.Subcategory) // Each product has one Subcategory
			.WithMany(s => s.Products) //Each SubCetogory has many Product
			.HasForeignKey(p => p.SubcategoryId); // Foreign Key property in Product
												  // Configure one-to-many relationship between Category and Subcategory
		builder
			.Entity<Subcategory>()
			.HasOne(s => s.Category) // Each Subcategory belongs to one Category
			.WithMany(c => c.Subcategories) // Each Category can have multiple Subcategories
			.HasForeignKey(s => s.CategoryId); // Foreign key property in Subcategory

		//cart:
		builder.Entity<Cart>().HasKey(c => c.CartId);
		//one to one relationship between cart&user
		builder
			.Entity<Cart>()
			.HasOne(u => u.Customer)
			.WithOne(c => c.Cart)
			.HasForeignKey<Cart>(c => c.CustomerId);
		//one to many relationship between cart&Product
		builder
			.Entity<Product>()
			.HasOne(c => c.Cart)
			.WithMany(p => p.Products)
			.HasForeignKey(c => c.CartId);

		// relation between OrderItem and product

		builder.Entity<OrderItem>()
		  .HasOne(o => o.Product)
		  .WithMany(p => p.OrderItem)
		  .HasForeignKey(o => o.ProductId);
	}
}
