using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
namespace EFDbFirstApproach1.Models
{
    public class ComponyDbContext:DbContext
    {
        public ComponyDbContext():base("MyConnectionString")
        {

        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}