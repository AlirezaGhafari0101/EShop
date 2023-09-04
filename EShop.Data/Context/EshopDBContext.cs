﻿using EShop.Domain.Models.Users;
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


        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);





        }


    }
}
