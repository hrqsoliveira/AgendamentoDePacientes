using AP.Domain.Configuracoes;
using AP.Infrastructure.DBs.MSSQL;
using AP.Infrastructure.Interfaces;
using System;

namespace AP.Infrastructure.Factories
{
    public static class FactoryDbMedico
    {
        public static IDbMedico GetDbMedico(Configuracoes configuracoes)
        {
            return configuracoes.TipoDoBanco switch
            {
                Domain.Enums.TipoDoBanco.MSSQL => new MsSqlDbMedico(configuracoes),
                Domain.Enums.TipoDoBanco.ORACLE or Domain.Enums.TipoDoBanco.ANOTHER => throw new NotImplementedException("Este banco de dados não foi implementado"),
                _ => throw new InvalidOperationException("Este banco de dados não existe ou está incorreto"),
            };
        }
    }
}
