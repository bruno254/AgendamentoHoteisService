using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AgendamentoHoteis.Business.Models
{
    public class Agendamento : Entity
    {
        public DateTime DataAgendamento { get; set; }
        public long IdUsuario { get; set; }
        public long NroQuarto { get; set; }
        public bool Cancelado { get; set; }
        public string? Msg { get; set; }
    }
}
