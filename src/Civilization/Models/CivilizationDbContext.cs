using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Civilization.Models
{
    public class CivilizationDbContext : IdentityDbContext<User>
    {
        public CivilizationDbContext(DbContextOptions options) : base(options)
        {

        }
        

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Civilization;integrated security=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<PlayerGamePiece>().HasKey(x => new { x.PlayerId, x.GamePieceId });
        }
    }
}
