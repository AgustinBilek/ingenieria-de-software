using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Servicio_MB29;

namespace DAL_MB29
{
    public class UsuarioDAL_MB29
    {
         public List<UsuarioServicio_MB29> CargarUsuarios_MB29()
         {
            List<UsuarioServicio_MB29> usuarios = new List<UsuarioServicio_MB29>();

            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = "SELECT IdPersona, DNI, IdRol, Usuario, PasswordHash, Nombre, Apellido, Telefono, Email, Estado, Bloqueado, PrimerLogin, UltimoIdioma FROM Persona";
            
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["IdPersona"].ToString());
                        double dni = Convert.ToDouble(reader["DNI"].ToString());
                        int idrol = Convert.ToInt32(reader["IdRol"].ToString());
                        string usuario = reader["Usuario"].ToString();
                        string passwordHash = reader["PasswordHash"].ToString();
                        string telefono = reader["Telefono"].ToString();
                        string nombre = reader["Nombre"].ToString();
                        string apellido = reader["Apellido"].ToString();
                        string email = reader["Email"].ToString();
                        string estado = reader["Estado"].ToString();
                        bool bloqueado = Convert.ToBoolean(reader["Bloqueado"]);
                        bool primerlogin = Convert.ToBoolean(reader["PrimerLogin"]);
                        string ultimoidioma = reader["UltimoIdioma"].ToString();

                        UsuarioServicio_MB29 user = new UsuarioServicio_MB29(id, usuario, passwordHash, true, nombre, apellido, dni, idrol, email, telefono, bloqueado);
                        user.Estado_MB29 = estado;
                        user.PrimerLogin_MB29 = primerlogin;
                        user.UltimoIdioma_MB29 = ultimoidioma;

                        usuarios.Add(user);
                    }
                }
            }
            conectar.Desconectar_MB29();

            return usuarios;
         }

        public void GuardarUsuario_MB29(UsuarioServicio_MB29 usuario)
        {
            var conectar = new ConexionDB_MB29();

            SqlConnection conexion = conectar.Conectar_MB29();

            string query = @"INSERT INTO Persona (DNI, IdRol, Usuario, Nombre, Apellido, Telefono, Email, PasswordHash, Bloqueado, Estado, PrimerLogin)
                            VALUES (@DNI, @IdRol, @Usuario, @Nombre, @Apellido, @Telefono, @Email, @PasswordHash, @Bloqueado, @Estado, 1);
                            SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@DNI", usuario.DNI_MB29);
                comando.Parameters.AddWithValue("@IdRol", usuario.IdRol_MB29);
                comando.Parameters.AddWithValue("@Usuario", usuario.Usuario_MB29);
                comando.Parameters.AddWithValue("@PasswordHash", usuario.PassHash_MB29);
                comando.Parameters.AddWithValue("@Nombre", usuario.Nombre_MB29);
                comando.Parameters.AddWithValue("@Apellido", usuario.Apellido_MB29);
                comando.Parameters.AddWithValue("@Telefono", usuario.Telefono_MB29);
                comando.Parameters.AddWithValue("@Email", usuario.Email_MB29);
                comando.Parameters.AddWithValue("@Bloqueado", false);
                comando.Parameters.AddWithValue("@Estado", "Habilitado");

                int nuevoId = Convert.ToInt32(comando.ExecuteScalar());
                usuario.IdPersona_MB29 = nuevoId;
            }
            conectar.Desconectar_MB29();
        }

        public void ModificarUsuario_MB29(UsuarioServicio_MB29 usuario)
        {
            var conectar = new ConexionDB_MB29();

            SqlConnection conexion = conectar.Conectar_MB29();

            string query = @"UPDATE Persona SET Email = @Email, IdRol = @IdRol, Bloqueado = @Bloqueado
                             WHERE IdPersona = @IdPersona";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdPersona", usuario.IdPersona_MB29);
                comando.Parameters.AddWithValue("@Email", usuario.Email_MB29);
                comando.Parameters.AddWithValue("@IdRol", usuario.IdRol_MB29);
                comando.Parameters.AddWithValue("@Bloqueado", usuario.Bloqueado_MB29);

                comando.ExecuteNonQuery();
            }
            conectar.Desconectar_MB29();
        }

        public void DeshabilitarUsuario_MB29(UsuarioServicio_MB29 usuario)
        {
            var conectar = new ConexionDB_MB29();
            SqlConnection conexion = conectar.Conectar_MB29();

            string query = @"UPDATE Persona SET Estado = @Estado 
                             WHERE IdPersona = @IdPersona";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdPersona", usuario.IdPersona_MB29);
                comando.Parameters.AddWithValue("@Estado", usuario.Estado_MB29);

                comando.ExecuteNonQuery();
            }
            conectar.Desconectar_MB29();
        }

        public void DesbloquearUsuario_MB29(UsuarioServicio_MB29 usuario)
        {
            var conectar = new ConexionDB_MB29();
            SqlConnection conexion = conectar.Conectar_MB29();

            string query = @"UPDATE Persona SET Bloqueado = @Bloqueado, primerlogin = @PrimerLogin
                     WHERE IdPersona = @IdPersona";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdPersona", usuario.IdPersona_MB29);
                comando.Parameters.AddWithValue("@Bloqueado", usuario.Bloqueado_MB29);
                comando.Parameters.AddWithValue("@PrimerLogin", usuario.PrimerLogin_MB29);

                comando.ExecuteNonQuery();
            }
            conectar.Desconectar_MB29();
        }

        public void CambiarContraseña_MB29(UsuarioServicio_MB29 usuario)
        {
            var conectar = new ConexionDB_MB29();

            SqlConnection conexion = conectar.Conectar_MB29();

            string query = @"UPDATE Persona SET PasswordHash = @PasswordHash
                             WHERE IdPersona = @IdPersona";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdPersona", usuario.IdPersona_MB29);
                comando.Parameters.AddWithValue("@PasswordHash", usuario.PassHash_MB29);

                comando.ExecuteNonQuery();
            }
            conectar.Desconectar_MB29();
        }

        public void MarcarPrimerLoginUsado_MB29(UsuarioServicio_MB29 usuario)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = "UPDATE Persona SET PrimerLogin = 0 WHERE IdPersona = @IdPersona";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdPersona", usuario.IdPersona_MB29);
                comando.ExecuteNonQuery();
            }

            conectar.Desconectar_MB29();
        }

        public void GuardarIdioma_MB29(UsuarioServicio_MB29 usuario)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"UPDATE Persona SET UltimoIdioma = @UltimoIdioma
                            WHERE IdPersona = @IdPersona";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdPersona", usuario.IdPersona_MB29);
                comando.Parameters.AddWithValue("@UltimoIdioma", usuario.UltimoIdioma_MB29);
                comando.ExecuteNonQuery();
            }

            conectar.Desconectar_MB29();
        }
    }
}
