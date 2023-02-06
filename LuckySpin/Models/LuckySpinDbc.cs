using System;
using Microsoft.EntityFrameworkCore;
namespace LuckySpin.Models
{
    public class LuckySpinDbc : DbContext
    {
        //Constructor
        public LuckySpinDbc(DbContextOptions<LuckySpinDbc> options) : base(options)
        {
            Database.EnsureCreated();
        }

        // Entity Properties
        public DbSet<Player> Players { get; set; }
        public DbSet<Spin> Spins { get; set; }
    }
}

