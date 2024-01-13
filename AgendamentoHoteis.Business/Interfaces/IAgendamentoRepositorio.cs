using AgendamentoHoteis.Business.Models;

namespace AgendamentoHoteis.Business.Interfaces
{
    public interface IAgendamentoRepositorio : IRepositorioBase<Agendamento>
    {
        Task AdicionarAgendamento(Agendamento ag);
    }
}
