using CredHubDemoUI.Models;
using Microsoft.EntityFrameworkCore;

namespace CredHubDemoUI
{
    public class CityContext : DbContext
    {
        public CityContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<City> Cities { get; set; }
    }
}