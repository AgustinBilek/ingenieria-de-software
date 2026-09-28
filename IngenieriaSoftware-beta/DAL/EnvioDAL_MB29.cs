using Servicio_MB29;
using DAL_MB29;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DAL
{
    public class EnvioDAL_MB29
    {
        // ---------- CU1: Registrar Paquete ----------
        public int GuardarPaquete_MB29(Paquete_MB29 paquete)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"INSERT INTO Paquete (TipoPaquete, Contenido)
                             VALUES (@TipoPaquete, @Contenido);
                             SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int idNuevo;
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@TipoPaquete", paquete.TipoPaquete_MB29);
                comando.Parameters.AddWithValue("@Contenido", paquete.Contenido_MB29);
                idNuevo = Convert.ToInt32(comando.ExecuteScalar());
            }

            conectar.Desconectar_MB29();
            return idNuevo;
        }

        // ---------- CU2: Registrar Envío (remitente / destinatario / envío) ----------
        public Remitente_MB29 BuscarRemitentePorDNI_MB29(long dni)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = "SELECT IdRemitente, DNI, Nombre, Apellido, Direccion, Telefono, Email FROM Remitente WHERE DNI = @DNI";
            Remitente_MB29 remitente = null;

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@DNI", dni);
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        remitente = new Remitente_MB29
                        {
                            IdRemitente_MB29 = Convert.ToInt32(reader["IdRemitente"]),
                            DNI_MB29 = Convert.ToInt64(reader["DNI"]),
                            Nombre_MB29 = reader["Nombre"].ToString(),
                            Apellido_MB29 = reader["Apellido"].ToString(),
                            Direccion_MB29 = reader["Direccion"].ToString(),
                            Telefono_MB29 = reader["Telefono"].ToString(),
                            Email_MB29 = reader["Email"] as string
                        };
                    }
                }
            }

            conectar.Desconectar_MB29();
            return remitente;
        }

        public int GuardarRemitente_MB29(Remitente_MB29 remitente)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"INSERT INTO Remitente (DNI, Nombre, Apellido, Direccion, Telefono, Email)
                             VALUES (@DNI, @Nombre, @Apellido, @Direccion, @Telefono, @Email);
                             SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int idNuevo;
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@DNI", remitente.DNI_MB29);
                comando.Parameters.AddWithValue("@Nombre", remitente.Nombre_MB29);
                comando.Parameters.AddWithValue("@Apellido", remitente.Apellido_MB29);
                comando.Parameters.AddWithValue("@Direccion", remitente.Direccion_MB29);
                comando.Parameters.AddWithValue("@Telefono", remitente.Telefono_MB29);
                comando.Parameters.AddWithValue("@Email", (object)remitente.Email_MB29 ?? DBNull.Value);
                idNuevo = Convert.ToInt32(comando.ExecuteScalar());
            }

            conectar.Desconectar_MB29();
            return idNuevo;
        }

        public Destinatario_MB29 BuscarDestinatarioPorDNI_MB29(long dni)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = "SELECT IdDestinatario, DNI, Nombre, Direccion, Telefono FROM Destinatario WHERE DNI = @DNI";
            Destinatario_MB29 destinatario = null;

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@DNI", dni);
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        destinatario = new Destinatario_MB29
                        {
                            IdDestinatario_MB29 = Convert.ToInt32(reader["IdDestinatario"]),
                            DNI_MB29 = Convert.ToInt64(reader["DNI"]),
                            Nombre_MB29 = reader["Nombre"].ToString(),
                            Direccion_MB29 = reader["Direccion"].ToString(),
                            Telefono_MB29 = reader["Telefono"].ToString()
                        };
                    }
                }
            }

            conectar.Desconectar_MB29();
            return destinatario;
        }

        public int GuardarDestinatario_MB29(Destinatario_MB29 destinatario)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"INSERT INTO Destinatario (DNI, Nombre, Direccion, Telefono)
                             VALUES (@DNI, @Nombre, @Direccion, @Telefono);
                             SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int idNuevo;
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@DNI", destinatario.DNI_MB29);
                comando.Parameters.AddWithValue("@Nombre", destinatario.Nombre_MB29);
                comando.Parameters.AddWithValue("@Direccion", destinatario.Direccion_MB29);
                comando.Parameters.AddWithValue("@Telefono", destinatario.Telefono_MB29);
                idNuevo = Convert.ToInt32(comando.ExecuteScalar());
            }

            conectar.Desconectar_MB29();
            return idNuevo;
        }

        public int GuardarEnvio_MB29(Envio_MB29 envio)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"INSERT INTO Envio (IdPaquete, IdRemitente, IdDestinatario, EstadoEnvio, FechaIngreso, HoraIngreso)
                             VALUES (@IdPaquete, @IdRemitente, @IdDestinatario, @EstadoEnvio, @FechaIngreso, @HoraIngreso);
                             SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int idNuevo;
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdPaquete", envio.IdPaquete_MB29);
                comando.Parameters.AddWithValue("@IdRemitente", envio.IdRemitente_MB29);
                comando.Parameters.AddWithValue("@IdDestinatario", envio.IdDestinatario_MB29);
                comando.Parameters.AddWithValue("@EstadoEnvio", envio.EstadoEnvio_MB29);
                comando.Parameters.AddWithValue("@FechaIngreso", envio.FechaIngreso_MB29);
                comando.Parameters.AddWithValue("@HoraIngreso", envio.HoraIngreso_MB29);
                idNuevo = Convert.ToInt32(comando.ExecuteScalar());
            }

            conectar.Desconectar_MB29();
            return idNuevo;
        }

        // ---------- CU3: Condiciones, pago y confirmación ----------
        public void ActualizarCondiciones_MB29(int idEnvio, string prioridad, string tipoEnvio)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"UPDATE Envio SET Prioridad = @Prioridad, TipoEnvio = @TipoEnvio WHERE IdEnvio = @IdEnvio";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdEnvio", idEnvio);
                comando.Parameters.AddWithValue("@Prioridad", prioridad);
                comando.Parameters.AddWithValue("@TipoEnvio", tipoEnvio);
                comando.ExecuteNonQuery();
            }

            conectar.Desconectar_MB29();
        }

        public int GuardarPago_MB29(PagoEnvio_MB29 pago)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"INSERT INTO PagoEnvio (IdEnvio, TipoPago, MontoTotal, FechaPago)
                             VALUES (@IdEnvio, @TipoPago, @MontoTotal, @FechaPago);
                             SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int idNuevo;
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdEnvio", pago.IdEnvio_MB29);
                comando.Parameters.AddWithValue("@TipoPago", pago.TipoPago_MB29);
                comando.Parameters.AddWithValue("@MontoTotal", pago.MontoTotal_MB29);
                comando.Parameters.AddWithValue("@FechaPago", pago.FechaPago_MB29);
                idNuevo = Convert.ToInt32(comando.ExecuteScalar());
            }

            conectar.Desconectar_MB29();
            return idNuevo;
        }

        public void ConfirmarEnvio_MB29(int idEnvio, string nuevoEstado, string codigoSeguimiento, char digitoVerificador)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"UPDATE Envio SET EstadoEnvio = @Estado, CodigoSeguimiento = @Codigo, DigitoVerificador = @DV
                             WHERE IdEnvio = @IdEnvio";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdEnvio", idEnvio);
                comando.Parameters.AddWithValue("@Estado", nuevoEstado);
                comando.Parameters.AddWithValue("@Codigo", codigoSeguimiento);
                comando.Parameters.AddWithValue("@DV", digitoVerificador.ToString());
                comando.ExecuteNonQuery();
            }

            conectar.Desconectar_MB29();
        }

        public string ObtenerEstadoEnvio_MB29(int idEnvio)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = "SELECT EstadoEnvio FROM Envio WHERE IdEnvio = @IdEnvio";
            string estado = null;

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdEnvio", idEnvio);
                var resultado = comando.ExecuteScalar();
                if (resultado != null) estado = resultado.ToString();
            }

            conectar.Desconectar_MB29();
            return estado;
        }

        public List<Paquete_MB29> ObtenerPaquetesSinEnvio_MB29()
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"SELECT p.IdPaquete, p.TipoPaquete, p.Contenido
                      FROM Paquete p
                      WHERE NOT EXISTS (
                          SELECT 1 FROM Envio e WHERE e.IdPaquete = p.IdPaquete
                      )";

            var lista = new List<Paquete_MB29>();

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Paquete_MB29
                        {
                            IdPaquete_MB29 = Convert.ToInt32(reader["IdPaquete"]),
                            TipoPaquete_MB29 = reader["TipoPaquete"].ToString(),
                            Contenido_MB29 = reader["Contenido"].ToString()
                        });
                    }
                }
            }

            conectar.Desconectar_MB29();
            return lista;
        }
    }
}
