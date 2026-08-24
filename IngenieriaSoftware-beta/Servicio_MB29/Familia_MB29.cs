using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio_MB29
{
    public class Familia_MB29 : ComponentePermiso_MB29
    {
        public int IdFamilia_MB29 { get; set; }
        public List<ComponentePermiso_MB29> Hijos_MB29 { get; set; } = new List<ComponentePermiso_MB29>();

        public override HashSet<int> ObtenerPermisos_MB29()
        {
            HashSet<int> permisos = new HashSet<int>();

            foreach (var hijo in Hijos_MB29)
            {
                permisos.UnionWith(hijo.ObtenerPermisos_MB29());
            }

            return permisos;
        }

        public void AgregarComponente_MB29(ComponentePermiso_MB29 componente)
        {
            if (componente == this)
                throw new Exception("Una familia no puede contenerse a sí misma.");

            if (componente is Familia_MB29 familia &&
                ContieneFamilia_MB29(familia, this))
            {
                throw new Exception("Se produciría una referencia circular.");
            }

            if (ObtenerPermisos_MB29().Overlaps(componente.ObtenerPermisos_MB29()))
            {
                throw new Exception("Existen permisos repetidos.");
            }

            Hijos_MB29.Add(componente);
        }

        private bool ContieneFamilia_MB29(Familia_MB29 origen, Familia_MB29 buscada)
        {
            foreach (var hijo in origen.Hijos_MB29)
            {
                if (hijo == buscada)
                    return true;

                if (hijo is Familia_MB29 familia &&
                    ContieneFamilia_MB29(familia, buscada))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
