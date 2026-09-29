using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio_MB29
{
    public class Rol_MB29 : ComponentePermiso_MB29
    {
        public int IdRol_MB29 { get; set; }
        public List<ComponentePermiso_MB29> Componentes_MB29 { get; set; } = new List<ComponentePermiso_MB29>();

        public override HashSet<int> ObtenerPermisos_MB29()
        {
            HashSet<int> permisos = new HashSet<int>();

            foreach (var componente in Componentes_MB29)
            {
                permisos.UnionWith(componente.ObtenerPermisos_MB29());
            }

            return permisos;
        }

        public void AgregarComponenteARol_MB29(ComponentePermiso_MB29 componente)
        {
            if (ObtenerPermisos_MB29().Overlaps(componente.ObtenerPermisos_MB29()))
            {
                throw new Exception("El rol contiene permisos repetidos.");
            }

            Componentes_MB29.Add(componente);
        }
    }
}
