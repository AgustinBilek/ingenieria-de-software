using DAL_MB29;
using Servicio_MB29;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
  public  class PermisoDAL_MB29
    {
        public List<Permiso_MB29> ObtenerTodosPermisos_MB29()
        {
            var permisos = new List<Permiso_MB29>();
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = "SELECT IdPermiso, Nombre FROM Permiso";

            using (SqlCommand cmd = new SqlCommand(query, conexion))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    permisos.Add(new Permiso_MB29
                    {
                        Id_MB29 = Convert.ToInt32(reader["IdPermiso"]),
                        Nombre = reader["Nombre"].ToString()
                    });
                }
            }

            conectar.Desconectar_MB29();
            return permisos;
        }
    }
}
