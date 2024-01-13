using AgendamentoHoteis.Business.Models;
using Microsoft.EntityFrameworkCore;


namespace AgendamentoHoteisService.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Agendamento> Agendamento { get; set; }
    }
}
