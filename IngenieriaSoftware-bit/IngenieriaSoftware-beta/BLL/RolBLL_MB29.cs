using DAL;
using Servicio_MB29;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RolBLL_MB29
    {
        private readonly RolDAL_MB29 rolDAL = new RolDAL_MB29();

        public List<Rol_MB29> ObtenerRoles_MB29()
        {
            return rolDAL.ObtenerTodosRoles_MB29();
        }

        public void GuardarRol_MB29(Rol_MB29 rol)
        {
            if (string.IsNullOrWhiteSpace(rol.Nombre))
                throw new Exception("El nombre del rol no puede estar vacío.");

            rolDAL.GuardarRol_MB29(rol);
        }

        public void EliminarRol_MB29(Rol_MB29 rol)
        {
            if (rolDAL.EstaEnUso_MB29(rol))
                throw new Exception("El rol está asignado a uno o más usuarios y no puede eliminarse.");

            rolDAL.EliminarRol_MB29(rol);
        }

        public void AgregarComponente_MB29(Rol_MB29 rol, ComponentePermiso_MB29 componente)
        {
            //Valida permisos repetidos usando composite
            rol.AgregarComponenteARol_MB29(componente);

            if (componente is Familia_MB29 familia)
                rolDAL.AgregarFamiliaARol_MB29(rol, familia);
            else if (componente is Permiso_MB29 permiso)
                rolDAL.AgregarPermisoARol_MB29(rol, permiso);
        }

        public void EliminarFamilia_MB29(Rol_MB29 rol, Familia_MB29 familia)
        {
            rolDAL.EliminarFamiliaDeRol_MB29(rol, familia);
            //Actualiza en memoria sin recargar todo
            var enMemoria = rol.Componentes_MB29.OfType<Familia_MB29>()
                               .FirstOrDefault(f => f.IdFamilia_MB29 == familia.IdFamilia_MB29);
            if (enMemoria != null) rol.Componentes_MB29.Remove(enMemoria);
        }

        public void EliminarPermiso_MB29(Rol_MB29 rol, Permiso_MB29 permiso)
        {
            rolDAL.EliminarPermisoDeRol_MB29(rol, permiso);
            //Actualiza en memoria sin recargar todo
            var enMemoria = rol.Componentes_MB29.OfType<Permiso_MB29>()
                               .FirstOrDefault(p => p.Id_MB29 == permiso.Id_MB29);
            if (enMemoria != null) rol.Componentes_MB29.Remove(enMemoria);
        }
    }
}
