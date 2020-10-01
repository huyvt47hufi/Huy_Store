using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HUY_Store.Models;

namespace HUY_Store.Data
{
    public class HUY_StoreContext : DbContext
    {
        public HUY_StoreContext (DbContextOptions<HUY_StoreContext> options)
            : base(options)
        {
        }

        public DbSet<HUY_Store.Models.Sneaker> Sneaker { get; set; }

        public DbSet<HUY_Store.Models.Admin> Admin { get; set; }
    }
}
