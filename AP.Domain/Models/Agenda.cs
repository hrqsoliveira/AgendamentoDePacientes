using System;

namespace AP.Domain.Models
{
    public class Agenda
    {
        public int Id { get; set; }
        public Paciente Paciente { get; set; }
        public Medico Medico { get; set; }
        public DateTime DataHoraDoAgendamento { get; set; } = DateTime.Now.Date;
    }
}
