using AP.Domain.Configuracoes;
using AP.Domain.Models;
using AP.Infrastructure.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace AP.Infrastructure.DBs.MSSQL
{
    internal class MsSqlDbMedico : IDbMedico
    {
        #region Objetos

        private readonly Configuracoes _configuracoes;

        #endregion

        #region Construtores
        public MsSqlDbMedico(Configuracoes configuracoes)
        {
            _configuracoes = configuracoes;
        }

        #endregion

        #region Obtendo todos os Médicos
        public List<Medico> GetMedicos()
        {
            using (SqlConnection connection = new SqlConnection(_configuracoes.StringDeConexaoComBancoDeDados))
            {
                string query = @"SELECT [Id]
                                       ,[Nome]
                                   FROM [dbo].[Medico]";

                return connection.Query<Medico>(query).ToList();
            }
        }
        #endregion
    }
}
