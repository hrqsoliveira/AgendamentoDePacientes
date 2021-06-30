using AP.Domain.Models;
using System.Collections.Generic;

namespace AP.Infrastructure.Interfaces
{
    public interface IDbPaciente
    {
        List<Paciente> GetPacientes();
    }
}
