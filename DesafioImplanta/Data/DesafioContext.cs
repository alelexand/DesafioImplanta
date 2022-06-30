using DesafioImplanta.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioImplanta.Data
{
    public class DesafioContext : DbContext
    {
        public DesafioContext(DbContextOptions<DesafioContext> opitions) : base(opitions)
        {
        }
        public DbSet<Profissional> Profissionals { get; set; }
    }
}
