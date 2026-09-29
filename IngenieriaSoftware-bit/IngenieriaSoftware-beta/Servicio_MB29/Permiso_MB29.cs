using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio_MB29
{
    public class Permiso_MB29 : ComponentePermiso_MB29
    {
        public int Id_MB29 { get; set; }

        public override HashSet<int> ObtenerPermisos_MB29()
        {
            return new HashSet<int> { Id_MB29 };
        }
    }
}
