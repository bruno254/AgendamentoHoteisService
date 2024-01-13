using AgendamentoHoteis.Business.Interfaces;
using AgendamentoHoteis.Business.Models;
using AgendamentoHoteisService.Data.Context;
using Microsoft.Extensions.Configuration;   

namespace AgendamentoHoteisService.Data.Repositorio
{
    public class AgendamentoRepositorio : RepositorioBase<Agendamento>, IAgendamentoRepositorio
    {
        private object agendamento;

        public AgendamentoRepositorio(AppDbContext context, IConfiguration configuration) : base(context, configuration)
        {

        }
            
        public async Task AdicionarAgendamento(Agendamento ag)
        {
            var count = Db.Agendamento.Where(x => x.DataAgendamento == ag.DataAgendamento && x.NroQuarto == ag.NroQuarto && x.Cancelado == false).Count();
            if(count == 0)
            {
                ag.Cancelado = false;
                ag.Msg = "Agendamento Realizado";
            }
            else
            {
                ag.Cancelado = true;
                ag.Msg = "Agendamento não realizado, quarto indisponivel para a data selecionada";
            }

            await Adicionar(ag);
        }

    }
}
