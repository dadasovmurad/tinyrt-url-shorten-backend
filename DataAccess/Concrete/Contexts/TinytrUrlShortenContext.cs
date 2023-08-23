using Entitites.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Contexts
{
    public class TinytrUrlShortenContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=MURAD;Database=TinytrUrlShorten;TrustServerCertificate=True;Trusted_Connection=true");
        }
        public DbSet<ShortenUrl> ShortenUrls { get; set; }
    }
}
