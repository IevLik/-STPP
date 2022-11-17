using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Leftovers.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Leftovers.Data.Dtos.Auth;

namespace Leftovers.Data
{
    public class LeftoversContext : IdentityDbContext<LeftoversUser>
    {
        public DbSet<Chain> Chains { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Meal> Meals { get; set; }

        private readonly IConfiguration _configuration;
        public LeftoversContext(IConfiguration configuration)
        {
            _configuration = configuration; 
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=Leftovers");//(_configuration.GetValue<string>("PostgreSQLConnectionString"))
        }
    }
}
