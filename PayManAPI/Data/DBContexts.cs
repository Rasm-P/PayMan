using Microsoft.EntityFrameworkCore;
using PayManAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayManAPI.Data
{
    public class MyDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    string mySqlConnectionStr = "server=localhost; port=3307; database=PayMan; user=dev; password=ax2; Persist Security Info=True; Connect Timeout=300";
        //    optionsBuilder.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr));
        //}
    }
}
