using DAL;
using DAL_MB29;
using Servicio_MB29;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DigitoVerificadorBLL_MB29
    {
        private readonly DigitoVerificadorDAL_MB29 _dal = new DigitoVerificadorDAL_MB29();
        private readonly UsuarioDAL_MB29 _usuDAL = new UsuarioDAL_MB29();
        private readonly RolDAL_MB29 _rolDAL = new RolDAL_MB29();
        private readonly FamiliaDAL_MB29 _famDAL = new FamiliaDAL_MB29();
        private readonly PermisoDAL_MB29 _perDAL = new PermisoDAL_MB29();

        public void GuardarDVPersona_MB29()
        {
            var usuarios = _usuDAL.CargarUsuarios_MB29();
            _dal.Guardar_MB29("Persona",
                DigitoVerificador_MB29.CalcularDVH_MB29(usuarios),
                DigitoVerificador_MB29.CalcularDVV_MB29(usuarios));
        }

        public void GuardarDVRol_MB29()
        {
            var roles = _rolDAL.ObtenerTodosRoles_MB29();
            _dal.Guardar_MB29("Rol",
                DigitoVerificador_MB29.CalcularDVH_MB29(roles),
                DigitoVerificador_MB29.CalcularDVV_MB29(roles));
        }

        public void GuardarDVFamilia_MB29()
        {
            var familias = _famDAL.ObtenerTodasFamilias_MB29();
            _dal.Guardar_MB29("Familia",
                DigitoVerificador_MB29.CalcularDVH_MB29(familias),
                DigitoVerificador_MB29.CalcularDVV_MB29(familias));
        }

        public void GuardarDVPermiso_MB29()
        {
            var permisos = _perDAL.ObtenerTodosPermisos_MB29();
            _dal.Guardar_MB29("Permiso",
                DigitoVerificador_MB29.CalcularDVH_MB29(permisos),
                DigitoVerificador_MB29.CalcularDVV_MB29(permisos));
        }

        public List<string> VerificarDVCompleto_MB29()
        {
            var irregularidades = new List<string>();

            var usuarios = _usuDAL.CargarUsuarios_MB29();
            Verificar_MB29("Persona",
                DigitoVerificador_MB29.CalcularDVH_MB29(usuarios),
                DigitoVerificador_MB29.CalcularDVV_MB29(usuarios),
                irregularidades);

            var roles = _rolDAL.ObtenerTodosRoles_MB29();
            Verificar_MB29("Rol",
                DigitoVerificador_MB29.CalcularDVH_MB29(roles),
                DigitoVerificador_MB29.CalcularDVV_MB29(roles),
                irregularidades);

            var familias = _famDAL.ObtenerTodasFamilias_MB29();
            Verificar_MB29("Familia",
                DigitoVerificador_MB29.CalcularDVH_MB29(familias),
                DigitoVerificador_MB29.CalcularDVV_MB29(familias),
                irregularidades);

            var permisos = _perDAL.ObtenerTodosPermisos_MB29();
            Verificar_MB29("Permiso",
                DigitoVerificador_MB29.CalcularDVH_MB29(permisos),
                DigitoVerificador_MB29.CalcularDVV_MB29(permisos),
                irregularidades);

            return irregularidades;
        }

        private void Verificar_MB29(string tabla, string dvhActual, string dvvActual,
                                     List<string> irregularidades)
        {
            var guardado = _dal.Obtener_MB29(tabla);
            if (guardado == null) return;

            if (guardado.Value.DVH != dvhActual)
                irregularidades.Add($"{tabla}: DVH almacenado no coincide.");

            if (guardado.Value.DVV != dvvActual)
                irregularidades.Add($"{tabla}: DVV almacenado no coincide.");
        }
    }
}
