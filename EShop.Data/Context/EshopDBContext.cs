using EShop.Domain.Models.Home;
using EShop.Domain.Models.Products;
using EShop.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Data.Context
{
    public class EshopDBContext:DbContext
    {
        public EshopDBContext(DbContextOptions<EshopDBContext> options):base(options)
        {
            
        }


        #region Users
        public DbSet<User> Users { get; set; }
        #endregion

        #region Products
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductGallery> ProductGalleries { get; set; }
        #endregion
        public DbSet<ContactUs> ContactUs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<ContactUs>().HasQueryFilter(c => !c.IsDelete);
            modelBuilder.Entity<Category>().HasQueryFilter(c => !c.IsDelete);
            modelBuilder.Entity<ProductColor>().HasQueryFilter(c => !c.IsDelete);






        }


    }
}
