using AP.Domain.Configuracoes;
using AP.Domain.Models;
using AP.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System;

namespace AP.Infrastructure.DBs.MSSQL
{
    internal class MsSqlDbAgenda : IDbAgenda
    {
        #region Objetos

        private readonly Configuracoes _configuracoes;

        #endregion

        #region Construtores
        public MsSqlDbAgenda(Configuracoes configuracoes)
        {
            _configuracoes = configuracoes;
        }

        #endregion

        #region Obtem todas as Agendas
        public List<Agenda> GetAgendas()
        {
            using (SqlConnection connection = new SqlConnection(_configuracoes.StringDeConexaoComBancoDeDados))
            {
                string query = @"SELECT agenda.[Id]
                                       ,agenda.[DataHoraDoAgendamento]
                                 	   ,medico.[Id]
                                 	   ,medico.[Nome]
                                 	   ,paciente.[Id]
                                 	   ,paciente.[Nome]
                                    FROM [dbo].[Agenda] agenda
                                  INNER JOIN Medico medico
                                  ON  agenda.IdMedico = medico.Id
                                  INNER JOIN Paciente paciente
                                  ON agenda.IdPaciente = paciente.Id";

                return connection.Query<Agenda, Medico, Paciente, Agenda>(query,
                    (Agenda, Medico, Paciente) =>
                    {
                        Agenda.Paciente = Paciente;
                        Agenda.Medico = Medico;

                        return Agenda;
                    }).ToList();
            }
        }

        #endregion

        #region Insere Novo Agendamento
        public bool InsereNovoAgendamento(Agenda agenda)
        {
            using (SqlConnection connection = new SqlConnection(_configuracoes.StringDeConexaoComBancoDeDados))
            {
                int respostaIdPaciente = InsereNovoPaciente(connection, agenda.Paciente);

                if (respostaIdPaciente != 0)
                {
                    string query = @"INSERT INTO [dbo].[Agenda]
                                                  ([IdPaciente]
                                                  ,[IdMedico]
                                                  ,[DataHoraDoAgendamento])
                                            VALUES
                                                  (@IdPaciente
                                                  ,@IdMedico
                                                  ,@DataHoraDoAgendamento)";

                    return Convert.ToBoolean(connection.Execute(query,
                        new
                        {
                            agenda.DataHoraDoAgendamento,
                            IdPaciente = respostaIdPaciente,
                            IdMedico = agenda.Medico.Id
                        }));
                }
                else
                {
                    return false;
                }
            }
        }

        private int InsereNovoPaciente(SqlConnection connection, Paciente paciente)
        {
            string query = @"INSERT INTO [dbo].[Paciente]
                                              ([Nome])
                                        VALUES
                                              (@Nome);
                            SELECT SCOPE_IDENTITY()";

            return connection.Query<int>(query, new { paciente.Nome }).FirstOrDefault();
        }

        #endregion

        #region Atualia Agendamento Selecionado
        public bool AtualizaAgendamentoSelecionad(Agenda agenda)
        {
            using (SqlConnection connection = new SqlConnection(_configuracoes.StringDeConexaoComBancoDeDados))
            {
                string query = @"UPDATE [dbo].[Agenda]
                                          SET [IdPaciente] = @IdPaciente
                                             ,[IdMedico] = @IdMedico
                                             ,[DataHoraDoAgendamento] = @DataHoraDoAgendamento
                                        WHERE Id = @Id";

                return Convert.ToBoolean(connection.Execute(query, 
                    new 
                    { 
                        agenda.Id,
                        agenda.DataHoraDoAgendamento,
                        IdPaciente = agenda.Paciente.Id,
                        IdMedico = agenda.Medico.Id
                    }));
            }
        }
        #endregion

        #region Exclui Agendamento Selecionado
        public bool ExcluiAgendamentoSelecionado(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuracoes.StringDeConexaoComBancoDeDados))
            {
                string query = @"DELETE FROM [dbo].[Agenda]
                                      WHERE Id = @Id";

                return Convert.ToBoolean(connection.Execute(query, new { Id = id }));
            }
        }
        #endregion
    }
}
