using AP.Domain.Models;
using System.Collections.Generic;

namespace AP.Infrastructure.Interfaces
{
    public interface IDbMedico
    {
        List<Medico> GetMedicos();
    }
}
