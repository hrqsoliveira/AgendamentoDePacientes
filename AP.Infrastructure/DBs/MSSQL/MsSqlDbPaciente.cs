using AP.Domain.Configuracoes;
using AP.Domain.Models;
using AP.Infrastructure.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace AP.Infrastructure.DBs.MSSQL
{
    internal class MsSqlDbPaciente : IDbPaciente
    {
        #region Objetos

        private readonly Configuracoes _configuracoes;

        #endregion

        #region Construtores
        public MsSqlDbPaciente(Configuracoes configuracoes)
        {
            _configuracoes = configuracoes;
        }

        #endregion

        #region Obtem Todos Pacientes
        public List<Paciente> GetPacientes()
        {
            using (SqlConnection connection = new SqlConnection(_configuracoes.StringDeConexaoComBancoDeDados))
            {
                string query = @"SELECT [Id]
                                       ,[Nome]
                                   FROM [dbo].[Paciente]";

                return connection.Query<Paciente>(query).ToList();
            }
        }

        #endregion
    }
}
