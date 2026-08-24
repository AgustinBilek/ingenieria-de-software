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
   public class RolDAL_MB29
    {
        public List<Rol_MB29> ObtenerTodosRoles_MB29()
        {
            var roles = new List<Rol_MB29>();
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            //Traemos roles base
            using (SqlCommand cmd = new SqlCommand("SELECT IdRol, Nombre FROM Rol", conexion))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    roles.Add(new Rol_MB29
                    {
                        IdRol_MB29 = Convert.ToInt32(reader["IdRol"]),
                        Nombre = reader["Nombre"].ToString()
                    });
            }

            //Para cada rol, carga familias y permisos sueltos
            var familiaDAL = new FamiliaDAL_MB29();
            var todasFamilias = familiaDAL.ObtenerTodasFamilias_MB29();
            var familiasDict = todasFamilias.ToDictionary(f => f.IdFamilia_MB29);

            var permisoDAL = new PermisoDAL_MB29();
            var todosPermisos = permisoDAL.ObtenerTodosPermisos_MB29();
            var permisosDict = todosPermisos.ToDictionary(p => p.Id_MB29);

            foreach (var rol in roles)
            {
                //Familias del rol
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT IdFamilia FROM RolFamilia WHERE IdRol = @IdRol", conexion))
                {
                    cmd.Parameters.AddWithValue("@IdRol", rol.IdRol_MB29);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            int idFam = Convert.ToInt32(reader["IdFamilia"]);
                            if (familiasDict.TryGetValue(idFam, out var f))
                                rol.Componentes_MB29.Add(f);
                        }
                }

                //Permisos sueltos del rol
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT IdPermiso FROM RolPermiso WHERE IdRol = @IdRol", conexion))
                {
                    cmd.Parameters.AddWithValue("@IdRol", rol.IdRol_MB29);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            int idPerm = Convert.ToInt32(reader["IdPermiso"]);
                            if (permisosDict.TryGetValue(idPerm, out var p))
                                rol.Componentes_MB29.Add(p);
                        }
                }
            }

            conectar.Desconectar_MB29();
            return roles;
        }

        public void GuardarRol_MB29(Rol_MB29 rol)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"INSERT INTO Rol (Nombre)
                             VALUES (@Nombre);
                             SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                cmd.Parameters.AddWithValue("@Nombre", rol.Nombre);
                rol.IdRol_MB29 = Convert.ToInt32(cmd.ExecuteScalar());
            }

            conectar.Desconectar_MB29();
        }

        public void EliminarRol_MB29(Rol_MB29 rol)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM RolFamilia WHERE IdRol = @IdRol", conexion))
            {
                cmd.Parameters.AddWithValue("@IdRol", rol.IdRol_MB29);
                cmd.ExecuteNonQuery();
            }

            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM RolPermiso WHERE IdRol = @IdRol", conexion))
            {
                cmd.Parameters.AddWithValue("@IdRol", rol.IdRol_MB29);
                cmd.ExecuteNonQuery();
            }

            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM Rol WHERE IdRol = @IdRol", conexion))
            {
                cmd.Parameters.AddWithValue("@IdRol", rol.IdRol_MB29);
                cmd.ExecuteNonQuery();
            }

            conectar.Desconectar_MB29();
        }

        public void AgregarFamiliaARol_MB29(Rol_MB29 rol, Familia_MB29 familia)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = "INSERT INTO RolFamilia (IdRol, IdFamilia) VALUES (@IdRol, @IdFamilia)";

            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                cmd.Parameters.AddWithValue("@IdRol", rol.IdRol_MB29);
                cmd.Parameters.AddWithValue("@IdFamilia", familia.IdFamilia_MB29);
                cmd.ExecuteNonQuery();
            }

            conectar.Desconectar_MB29();
        }

        public void AgregarPermisoARol_MB29(Rol_MB29 rol, Permiso_MB29 permiso)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = "INSERT INTO RolPermiso (IdRol, IdPermiso) VALUES (@IdRol, @IdPermiso)";

            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                cmd.Parameters.AddWithValue("@IdRol", rol.IdRol_MB29);
                cmd.Parameters.AddWithValue("@IdPermiso", permiso.Id_MB29);
                cmd.ExecuteNonQuery();
            }

            conectar.Desconectar_MB29();
        }

        public bool EstaEnUso_MB29(Rol_MB29 rol)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = "SELECT COUNT(*) FROM Persona WHERE IdRol = @IdRol";

            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                cmd.Parameters.AddWithValue("@IdRol", rol.IdRol_MB29);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                conectar.Desconectar_MB29();
                return count > 0;
            }
        }

        public void EliminarFamiliaDeRol_MB29(Rol_MB29 rol, Familia_MB29 familia)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"DELETE FROM RolFamilia 
                            WHERE IdRol = @IdRol AND IdFamilia = @IdFamilia";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdRol", rol.IdRol_MB29);
                comando.Parameters.AddWithValue("@IdFamilia", familia.IdFamilia_MB29);
                comando.ExecuteNonQuery();
            }

            conectar.Desconectar_MB29();
        }

        public void EliminarPermisoDeRol_MB29(Rol_MB29 rol, Permiso_MB29 permiso)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"DELETE FROM RolPermiso 
                            WHERE IdRol = @IdRol AND IdPermiso = @IdPermiso";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdRol", rol.IdRol_MB29);
                comando.Parameters.AddWithValue("@IdPermiso", permiso.Id_MB29);
                comando.ExecuteNonQuery();
            }

            conectar.Desconectar_MB29();
        }
    }
}
