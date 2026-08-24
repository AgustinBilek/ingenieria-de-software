using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio_MB29
{
    public abstract class ComponentePermiso_MB29
    {
        public string Nombre { get; set; }

        public abstract HashSet<int> ObtenerPermisos_MB29();
    }
}
