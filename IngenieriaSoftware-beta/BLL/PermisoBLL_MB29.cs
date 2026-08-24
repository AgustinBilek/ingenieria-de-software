using DAL;
using Servicio_MB29;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PermisoBLL_MB29
    {
        private readonly PermisoDAL_MB29 permisoDAL = new PermisoDAL_MB29();

        public List<Permiso_MB29> ObtenerPermisos_MB29()
        {
            return permisoDAL.ObtenerTodosPermisos_MB29();
        }
    }
}
