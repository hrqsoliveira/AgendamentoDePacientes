using AP.Domain.Enums;
using System;

namespace AP.Domain.Configuracoes
{
    public class Configuracoes
    {
        #region Objetos
        /// <summary>
        /// String de conexão com o Banco de Dados
        /// </summary>
        public string StringDeConexaoComBancoDeDados { get; set; }
        /// <summary>
        /// Tipo do banco de dados utilizado ex: "MSSQL, ORACLE, Informix..."
        /// </summary>
        public string TipoDoBancoDeDados { get; set; }
        /// <summary>
        /// Enum com o tipo do banco escolhido
        /// </summary>
        public TipoDoBanco TipoDoBanco { get { return GetTipoDoBanco(TipoDoBancoDeDados); } }

        #endregion

        #region Métodos de Negocios

        /// <summary>
        /// Método para realizar a conversão do tipo do banco de dados selecionado no "appsetting.json"
        /// </summary>
        /// <param name="tipoDoBancoDeDados"></param>
        /// <returns></returns>
        private TipoDoBanco GetTipoDoBanco(string tipoDoBancoDeDados)
        {
            Enum.TryParse(tipoDoBancoDeDados, out TipoDoBanco retornoDoTipoDoBanco);
            return retornoDoTipoDoBanco;
        }

        #endregion
    }
}
