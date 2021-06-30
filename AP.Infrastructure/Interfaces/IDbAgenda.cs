using AP.Domain.Models;
using System.Collections.Generic;

namespace AP.Infrastructure.Interfaces
{
    public interface IDbAgenda
    {
        List<Agenda> GetAgendas();
        bool InsereNovoAgendamento(Agenda agenda);
        bool ExcluiAgendamentoSelecionado(int id);
        bool AtualizaAgendamentoSelecionad(Agenda agenda);
    }
}
