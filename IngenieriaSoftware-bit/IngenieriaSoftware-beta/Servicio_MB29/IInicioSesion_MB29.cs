using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio_MB29
{
    public interface IInicioSesion_MB29
    {
        bool IniciarSesion_MB29(string usuario, string passhash);
    }
}
