using PayManWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayManWebAPI.DBContexts
{
    public class MyDBContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Use Fluent API to configure  

            // Map entities to tables  
            //modelBuilder.Entity<TodoItem>().ToTable("TodoItems");

            // Configure Primary Keys  
            //modelBuilder.Entity<TodoItem>().HasKey(u => u.Id).HasName("PK_TodoItems");

            // Configure indexes  
            //modelBuilder.Entity<TodoItem>().HasIndex(u => u.Name).HasDatabaseName("Idx_Name");

            // Configure columns  
            //modelBuilder.Entity<TodoItem>().Property(u => u.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            //modelBuilder.Entity<TodoItem>().Property(u => u.Name).HasColumnType("varchar(50)").IsRequired();
            //modelBuilder.Entity<TodoItem>().Property(u => u.IsComplete).HasColumnType("tinyint").HasDefaultValue(false);

            // Configure relationships
        }
    }
}