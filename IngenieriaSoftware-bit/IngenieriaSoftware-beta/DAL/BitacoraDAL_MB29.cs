using Servicio_MB29;
using DAL_MB29;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class BitacoraDAL_MB29
    {
       
        
            public void Guardar_MB29(BitacoraServicio_MB29 bitacora)
            {
                var conectar = new ConexionDB_MB29();
                var conexion = conectar.Conectar_MB29();

                string query = @"INSERT INTO Bitacora (usuario, accion, modulo, fecha, descripcion,criticidad)
                         VALUES (@usuario, @accion, @modulo, @fecha, @descripcion,@criticidad)";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@Usuario", bitacora.usuario_MB29);
                    comando.Parameters.AddWithValue("@Accion", bitacora.accion_MB29);
                    comando.Parameters.AddWithValue("@Modulo", bitacora.modulo_MB29);
                    comando.Parameters.AddWithValue("@Fecha", bitacora.fecha_MB29);
                    comando.Parameters.AddWithValue("@Descripcion", bitacora.Descripcion_MB29);
                    comando.Parameters.AddWithValue("@Criticidad", bitacora.Criticidad_MB29);
                    comando.ExecuteNonQuery();
                }

                conectar.Desconectar_MB29();
            }

        public List<BitacoraServicio_MB29> CargarBitacora_MB29()
        {
            List<BitacoraServicio_MB29> bitacora = new List<BitacoraServicio_MB29>();

            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = "SELECT Id, Usuario, Accion, Modulo, Fecha, Descripcion, Criticidad FROM Bitacora";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["Id"].ToString());
                        string usuario = reader["Usuario"].ToString();
                        string accion = reader["Accion"].ToString();
                        string modulo = reader["Modulo"].ToString();
                        DateTime fecha = Convert.ToDateTime(reader["Fecha"].ToString());
                        string descripcion = reader["Descripcion"].ToString();
                        int criticidad = Convert.ToInt32(reader["Criticidad"].ToString());

                        BitacoraServicio_MB29 bitac = new BitacoraServicio_MB29(id, usuario, accion, modulo, fecha, descripcion, criticidad);

                        bitacora.Add(bitac);
                    }
                }
            }
            conectar.Desconectar_MB29();

            return bitacora;
        }

        public int ObtenerIntentosFallidos_MB29(string usuario)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"
        SELECT COUNT(*) FROM Bitacora
        WHERE Usuario = @usuario
          AND Accion  = 'Login Fallido'
          AND Fecha   > ISNULL(
              (SELECT MAX(Fecha) FROM Bitacora
               WHERE Usuario = @usuario
                 AND Accion IN ('Login OK', 'Desbloqueo')),
              '1900-01-01')";

            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                cmd.Parameters.AddWithValue("@usuario", usuario);
                int resultado = (int)cmd.ExecuteScalar();
                conectar.Desconectar_MB29();
                return resultado;
            }
        }

    }
}
