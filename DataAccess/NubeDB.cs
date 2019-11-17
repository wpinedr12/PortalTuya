using DataAccess.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class NubeDB : DbContext
    {

        public NubeDB(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().HasKey(p => p.Id);
        }

    }
    
}
