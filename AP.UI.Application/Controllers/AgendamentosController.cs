using AP.Domain.Configuracoes;
using AP.UI.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace AP.UI.Application.Controllers
{
    public class AgendamentosController : Controller
    {
        #region Objetos
        private readonly AgendamentoViewModel _agendamentoViewModel;
        #endregion

        #region Construtores
        public AgendamentosController(Configuracoes configuracoes)
        {
            _agendamentoViewModel = new AgendamentoViewModel(configuracoes);
        }
        #endregion

        #region Página Inicial
        public IActionResult Index()
        {
            return View(_agendamentoViewModel.ProcessaPaginaInicial());
        }

        #endregion

        #region Página de Edição
        public IActionResult Edicao(int id)
        {
            return View("EdicaoDeAgendamentos", _agendamentoViewModel.ProcessaPaginaEdicao(id));
        }
        #endregion

        #region Ação de Adição (INSERT)
        public IActionResult Salvar(AgendamentoViewModel solicitacao)
        {
            bool resposta = _agendamentoViewModel.InsereNovoAgendamento(solicitacao);

            if (resposta)
            {
                TempData["success"] = "Agendamento realizado com sucesso!";
            }
            else
            {
                TempData["error"] = "Não foi possível realizar o agendamento!";
            }

            return Redirect("Index");
        }

        #endregion

        #region Ação de Atualização (UPDATE)
        public IActionResult Editar(AgendamentoViewModel solicitacao)
        {
            bool resposta = _agendamentoViewModel.AtualizaAgendamentoSelecionad(solicitacao);
            
            if (resposta)
            {
                TempData["success"] = "Agendamento atualizado com sucesso!";
            }
            else
            {
                TempData["error"] = "Não foi possível atualizar o agendamento!";
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Ação de Exclusão (DELETE)
        public IActionResult Excluir(int id)
        {
            bool resposta = _agendamentoViewModel.ExcluiAgendamentoSelecionado(id);

            if (resposta)
            {
                TempData["success"] = "Agendamento excluído com sucesso!";
            }
            else
            {
                TempData["error"] = "Não foi possível excluir o agendamento!";
            }

            return RedirectToAction("Index");
        }
        #endregion
    }
}
