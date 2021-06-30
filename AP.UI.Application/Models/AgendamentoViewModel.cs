using AP.Domain.Configuracoes;
using AP.Domain.Models;
using AP.Infrastructure.Factories;
using AP.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AP.UI.Application.Models
{
    public class AgendamentoViewModel
    {
        #region Objetos
        public Agenda Agenda { get; set; }
        public List<Agenda> Agendas { get; set; }
        public List<Medico> Medicos { get; set; }

        #endregion

        #region Interfaces
        private readonly IDbMedico _dbMedico;
        private readonly IDbAgenda _dbAgenda;
        #endregion

        #region Construtores
        public AgendamentoViewModel(Configuracoes configuracoes)
        {
            _dbMedico = FactoryDbMedico.GetDbMedico(configuracoes);
            _dbAgenda = FactoryDbAgenda.GetDbAgenda(configuracoes);
        }
        public AgendamentoViewModel()
        {

        }

        #endregion

        #region Processamento da Página Inicial
        internal AgendamentoViewModel ProcessaPaginaInicial()
        {
            return new AgendamentoViewModel()
            {
                Agenda = new Agenda(),
                Medicos = _dbMedico.GetMedicos(),
                Agendas = _dbAgenda.GetAgendas()
            };
        }

        #endregion

        #region Processamento da Página de Edição
        internal AgendamentoViewModel ProcessaPaginaEdicao(int id)
        {
            return new AgendamentoViewModel()
            {
                Agenda = _dbAgenda.GetAgendas().FirstOrDefault(x => x.Id == id),
                Medicos = _dbMedico.GetMedicos(),
            };
        }

        #endregion

        #region Realiza a Inserção no Banco (INSERT)
        internal bool InsereNovoAgendamento(AgendamentoViewModel solicitacao)
        {
            return _dbAgenda.InsereNovoAgendamento(solicitacao.Agenda);
        }
        #endregion

        #region Realiza a Atualização no Banco (UPDATE)
        internal bool AtualizaAgendamentoSelecionad(AgendamentoViewModel solicitacao)
        {
            return _dbAgenda.AtualizaAgendamentoSelecionad(solicitacao.Agenda);
        }

        #endregion

        #region Realiza a Exclusão no Banco (DELETE)
        internal bool ExcluiAgendamentoSelecionado(int id)
        {
            return _dbAgenda.ExcluiAgendamentoSelecionado(id);
        }

        #endregion
    }
}
