using AgendamentoHoteis.Business.Interfaces;
using AgendamentoHoteis.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendamentoHoteis.Business.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepositorio _agendamentoRepository;

        public AgendamentoService(IAgendamentoRepositorio agendamentoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
        }
        public async Task AdicionarAgendamento(Agendamento agendamento)
        {
            await _agendamentoRepository.AdicionarAgendamento(agendamento);
        }
    }
}
