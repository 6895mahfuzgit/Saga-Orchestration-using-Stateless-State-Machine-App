using TransportServiceApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace TransportServiceApp.Infrastructure.Data
{
    public class TranspostDBContext : DbContext
    {
        public TranspostDBContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Transpost> Transposts { get; set; }
    }
}
