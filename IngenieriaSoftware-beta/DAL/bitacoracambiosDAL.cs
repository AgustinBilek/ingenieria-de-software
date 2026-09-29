using DAL_MB29;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio_MB29;

namespace DAL
{
   public class bitacoracambiosDAL
    {
        public void Guardar_MB29(Cambioservicio_MB29 cambio)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"INSERT INTO BitacoraCambios (Usuario, Tabla, Operacion, IdRegistro, FechaHora, DetalleAnterior, DetalleNuevo)
                             VALUES (@Usuario, @Tabla, @Operacion, @IdRegistro, @FechaHora, @DetalleAnterior, @DetalleNuevo)";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@Usuario", cambio.Usuario_MB29);
                comando.Parameters.AddWithValue("@Tabla", cambio.Tabla_MB29);
                comando.Parameters.AddWithValue("@Operacion", cambio.Operacion_MB29);
                comando.Parameters.AddWithValue("@IdRegistro", cambio.IdRegistro_MB29);
                comando.Parameters.AddWithValue("@FechaHora", cambio.FechaHora_MB29);
                comando.Parameters.AddWithValue("@DetalleAnterior", (object)cambio.DetalleAnterior_MB29 ?? DBNull.Value);
                comando.Parameters.AddWithValue("@DetalleNuevo", (object)cambio.DetalleNuevo_MB29 ?? DBNull.Value);
                comando.ExecuteNonQuery();
            }

            conectar.Desconectar_MB29();
        }

        public List<Cambioservicio_MB29> CargarPorTabla_MB29(string tabla)
        {
            List<Cambioservicio_MB29> lista = new List<Cambioservicio_MB29>();

            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"SELECT IdCambio, Usuario, Tabla, Operacion, IdRegistro, FechaHora, DetalleAnterior, DetalleNuevo
                              FROM BitacoraCambios WHERE Tabla = @Tabla ORDER BY FechaHora DESC";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@Tabla", tabla);
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Cambioservicio_MB29
                        {
                            IdCambio_MB29 = Convert.ToInt32(reader["IdCambio"]),
                            Usuario_MB29 = reader["Usuario"].ToString(),
                            Tabla_MB29 = reader["Tabla"].ToString(),
                            Operacion_MB29 = reader["Operacion"].ToString(),
                            IdRegistro_MB29 = Convert.ToInt32(reader["IdRegistro"]),
                            FechaHora_MB29 = Convert.ToDateTime(reader["FechaHora"]),
                            DetalleAnterior_MB29 = reader["DetalleAnterior"] as string,
                            DetalleNuevo_MB29 = reader["DetalleNuevo"] as string
                        });
                    }
                }
            }

            conectar.Desconectar_MB29();
            return lista;
        }
    }
}

