using Servicio_MB29;
using System;
using DAL_MB29;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class FamiliaBLL_MB29
    {
        private readonly FamiliaDAL_MB29 familiaDAL = new FamiliaDAL_MB29();

        public bool FamiliaEstaEnUso_MB29(
        Familia_MB29 familia,
        List<Familia_MB29> familias,
        List<Rol_MB29> roles)
        {
            bool usadaPorFamilia =
                familias.Any(f => f.Hijos_MB29.Contains(familia));

            bool usadaPorRol =
                roles.Any(r => r.Componentes_MB29.Contains(familia));

            return usadaPorFamilia || usadaPorRol;
        }

        public void EliminarPermiso_MB29(Familia_MB29 familia, Permiso_MB29 permiso)
        {
            familiaDAL.EliminarPermiso_MB29(familia, permiso);
            //Actualizo los hijos en memoria
            var hijo = familia.Hijos_MB29.OfType<Permiso_MB29>().FirstOrDefault(p => p.Id_MB29 == permiso.Id_MB29);
            if (hijo != null) familia.Hijos_MB29.Remove(hijo);
        }

        public List<Familia_MB29> ObtenerFamilias_MB29()
        {
            return familiaDAL.ObtenerTodasFamilias_MB29();
        }

        public void GuardarFamilia_MB29(Familia_MB29 familia)
        {
            if (string.IsNullOrWhiteSpace(familia.Nombre))
                throw new Exception("El nombre de la familia no puede estar vacío.");

            familiaDAL.GuardarFamilia_MB29(familia);

        }

        public List<Permiso_MB29> PermisosFamilia_MB29(Familia_MB29 familia)
        {
            List<Permiso_MB29> permisos = new List<Permiso_MB29>();
            permisos = familiaDAL.PermisosFamilia_MB29(familia);
            return permisos;
        }

        public void EliminarFamilia_MB29(Familia_MB29 familia)
        {
            if (familia.Hijos_MB29.Count > 0)
                throw new Exception("La familia tiene permisos o subfamilias dentro y no puede eliminarse.");

            var familias = familiaDAL.ObtenerTodasFamilias_MB29();

            bool usadaPorOtraFamilia = familias.Any(f =>
                f.IdFamilia_MB29 != familia.IdFamilia_MB29 &&
                ContieneComponente_MB29(f, familia));

            if (usadaPorOtraFamilia)
                throw new Exception("La familia está siendo utilizada por otra familia.");

            var roles = familiaDAL.ObtenerRolesQueUsanFamilia_MB29(familia.IdFamilia_MB29);

            if (roles.Count > 0)
                throw new Exception("La familia está asignada a uno o más roles.");

            familiaDAL.EliminarFamilia_MB29(familia);
        }
        public void AgregarComponente_MB29(Familia_MB29 familia, ComponentePermiso_MB29 componente)
        {
            familia.AgregarComponente_MB29(componente);

            if (componente is Permiso_MB29 permiso)
                familiaDAL.AgregarPermiso_MB29(familia, permiso);
            else if (componente is Familia_MB29 subfamilia)
                familiaDAL.AgregarSubfamilia_MB29(familia, subfamilia);
            else
                throw new Exception("Tipo de componente no reconocido.");
        }


        private bool ContieneComponente_MB29(Familia_MB29 familia, ComponentePermiso_MB29 buscado)
        {
            foreach (var hijo in familia.Hijos_MB29)
            {
                if (hijo == buscado) return true;
                if (hijo is Familia_MB29 subfamilia && ContieneComponente_MB29(subfamilia, buscado))
                    return true;
            }
            return false;
        }

    }
}
