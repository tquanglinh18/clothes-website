using System;
using ClothesWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothesWebAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Products> Products { get; set; }
    }
}

