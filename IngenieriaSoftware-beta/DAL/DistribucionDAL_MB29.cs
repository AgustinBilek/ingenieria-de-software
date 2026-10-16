using Servicio_MB29;
using DAL_MB29;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DAL
{
    public class DistribucionDAL_MB29
    {
        // Reutilizado por los 3 CU para actualizar el estado del envío
        public void ActualizarEstadoEnvio_MB29(int idEnvio, string nuevoEstado)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = "UPDATE Envio SET EstadoEnvio = @Estado WHERE IdEnvio = @IdEnvio";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdEnvio", idEnvio);
                comando.Parameters.AddWithValue("@Estado", nuevoEstado);
                comando.ExecuteNonQuery();
            }

            conectar.Desconectar_MB29();
        }

        // ---------- CU1 PN2: Asignar Envío a Repartidor ----------
        public int GuardarAsignacion_MB29(AsignacionRepartidor_MB29 asignacion)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"INSERT INTO AsignacionRepartidor (IdEnvio, IdRepartidor, FechaAsignacion)
                             VALUES (@IdEnvio, @IdRepartidor, @FechaAsignacion);
                             SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int idNuevo;
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdEnvio", asignacion.IdEnvio_MB29);
                comando.Parameters.AddWithValue("@IdRepartidor", asignacion.IdRepartidor_MB29);
                comando.Parameters.AddWithValue("@FechaAsignacion", asignacion.FechaAsignacion_MB29);
                idNuevo = Convert.ToInt32(comando.ExecuteScalar());
            }

            conectar.Desconectar_MB29();
            return idNuevo;
        }

        // ---------- CU2 PN2: Cargar Paquete en Vehículo ----------
        public int GuardarCarga_MB29(CargaVehiculo_MB29 carga)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"INSERT INTO CargaVehiculo (IdEnvio, IdVehiculo, IdOperarioCarga, FechaHoraCarga)
                             VALUES (@IdEnvio, @IdVehiculo, @IdOperarioCarga, @FechaHoraCarga);
                             SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int idNuevo;
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdEnvio", carga.IdEnvio_MB29);
                comando.Parameters.AddWithValue("@IdVehiculo", carga.IdVehiculo_MB29);
                comando.Parameters.AddWithValue("@IdOperarioCarga", carga.IdOperarioCarga_MB29);
                comando.Parameters.AddWithValue("@FechaHoraCarga", carga.FechaHoraCarga_MB29);
                idNuevo = Convert.ToInt32(comando.ExecuteScalar());
            }

            conectar.Desconectar_MB29();
            return idNuevo;
        }

        // ---------- CU3 PN2: Realizar Entrega (recepción / inicio / entrega final) ----------
        public int GuardarEntrega_MB29(EntregaEnvio_MB29 entrega)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"INSERT INTO EntregaEnvio (IdEnvio, IdRepartidor, FechaHoraRecepcion)
                             VALUES (@IdEnvio, @IdRepartidor, @FechaHoraRecepcion);
                             SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int idNuevo;
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdEnvio", entrega.IdEnvio_MB29);
                comando.Parameters.AddWithValue("@IdRepartidor", entrega.IdRepartidor_MB29);
                comando.Parameters.AddWithValue("@FechaHoraRecepcion", entrega.FechaHoraRecepcion_MB29);
                idNuevo = Convert.ToInt32(comando.ExecuteScalar());
            }

            conectar.Desconectar_MB29();
            return idNuevo;
        }

        public void RegistrarInicioEntrega_MB29(int idEntrega, DateTime fechaSalida, TimeSpan horaSalida)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"UPDATE EntregaEnvio SET FechaSalida = @FechaSalida, HoraSalida = @HoraSalida
                             WHERE IdEntrega = @IdEntrega";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdEntrega", idEntrega);
                comando.Parameters.AddWithValue("@FechaSalida", fechaSalida);
                comando.Parameters.AddWithValue("@HoraSalida", horaSalida);
                comando.ExecuteNonQuery();
            }

            conectar.Desconectar_MB29();
        }

        public void RegistrarEntregaFinal_MB29(int idEntrega, DateTime fechaEntrega, TimeSpan horaEntrega, string firma)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"UPDATE EntregaEnvio SET FechaEntrega = @FechaEntrega, HoraEntrega = @HoraEntrega, FirmaDestinatario = @Firma
                             WHERE IdEntrega = @IdEntrega";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdEntrega", idEntrega);
                comando.Parameters.AddWithValue("@FechaEntrega", fechaEntrega);
                comando.Parameters.AddWithValue("@HoraEntrega", horaEntrega);
                comando.Parameters.AddWithValue("@Firma", firma);
                comando.ExecuteNonQuery();
            }

            conectar.Desconectar_MB29();
        }


        public string ObtenerEstadoEnvio_AB29(int idEnvio)
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

        public List<Envio_MB29> ObtenerEnviosListosParaDespacho_AB29()
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"SELECT IdEnvio, IdPaquete, EstadoEnvio, FechaIngreso
                     FROM Envio
                     WHERE EstadoEnvio = 'Listo para despacho'";

            var lista = new List<Envio_MB29>();

            using (SqlCommand comando = new SqlCommand(query, conexion))
            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new Envio_MB29
                    {
                        IdEnvio_MB29 = Convert.ToInt32(reader["IdEnvio"]),
                        IdPaquete_MB29 = Convert.ToInt32(reader["IdPaquete"]),
                        EstadoEnvio_MB29 = reader["EstadoEnvio"].ToString(),
                        FechaIngreso_MB29 = Convert.ToDateTime(reader["FechaIngreso"])
                    });
                }
            }

            conectar.Desconectar_MB29();
            return lista;
        }

        public List<RepartidorDisponible_AB29> ObtenerRepartidoresDisponibles_AB29()
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"SELECT p.IdPersona, p.Nombre, p.Apellido
                     FROM Persona p
                     INNER JOIN Rol r ON r.IdRol = p.IdRol
                     WHERE r.Nombre = 'Repartidor'
                       AND p.Estado = 'Habilitado'
                       AND p.Bloqueado = 0";

            var lista = new List<RepartidorDisponible_AB29>();

            using (SqlCommand comando = new SqlCommand(query, conexion))
            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new RepartidorDisponible_AB29
                    {
                        IdPersona_AB29 = Convert.ToInt32(reader["IdPersona"]),
                        Nombre_AB29 = reader["Nombre"].ToString(),
                        Apellido_AB29 = reader["Apellido"].ToString()
                    });
                }
            }

            conectar.Desconectar_MB29();
            return lista;
        }

        public List<Envio_MB29> ObtenerEnviosPorEstado_AB29(string estado)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();
            var lista = new List<Envio_MB29>();

            string query = @"SELECT IdEnvio, IdPaquete, EstadoEnvio, FechaIngreso
                     FROM Envio WHERE EstadoEnvio = @Estado";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@Estado", estado);
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Envio_MB29
                        {
                            IdEnvio_MB29 = Convert.ToInt32(reader["IdEnvio"]),
                            IdPaquete_MB29 = Convert.ToInt32(reader["IdPaquete"]),
                            EstadoEnvio_MB29 = reader["EstadoEnvio"].ToString(),
                            FechaIngreso_MB29 = Convert.ToDateTime(reader["FechaIngreso"])
                        });
                    }
                }
            }

            conectar.Desconectar_MB29();
            return lista;
        }

        public List<Vehiculo_MB29> ObtenerVehiculosDisponibles_AB29()
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();
            var lista = new List<Vehiculo_MB29>();

            string query = "SELECT IdVehiculo, Patente, Tipo, Estado FROM Vehiculo WHERE Estado = 'Disponible'";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new Vehiculo_MB29
                    {
                        IdVehiculo_MB29 = Convert.ToInt32(reader["IdVehiculo"]),
                        Patente_MB29 = reader["Patente"].ToString(),
                        Tipo_MB29 = reader["Tipo"].ToString(),
                        Estado_MB29 = reader["Estado"].ToString()
                    });
                }
            }

            conectar.Desconectar_MB29();
            return lista;
        }

        public List<EnvioParaEntrega_AB29> ObtenerEnviosDeRepartidor_AB29(int idRepartidor, string estado)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();
            var lista = new List<EnvioParaEntrega_AB29>();

            string query = @"SELECT e.IdEnvio, d.Direccion, e.EstadoEnvio
                     FROM Envio e
                     INNER JOIN AsignacionRepartidor a ON a.IdEnvio = e.IdEnvio
                     INNER JOIN Destinatario d ON d.IdDestinatario = e.IdDestinatario
                     WHERE a.IdRepartidor = @IdRepartidor AND e.EstadoEnvio = @Estado";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdRepartidor", idRepartidor);
                comando.Parameters.AddWithValue("@Estado", estado);
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new EnvioParaEntrega_AB29
                        {
                            IdEnvio_AB29 = Convert.ToInt32(reader["IdEnvio"]),
                            Direccion_AB29 = reader["Direccion"].ToString(),
                            Estado_AB29 = reader["EstadoEnvio"].ToString()
                        });
                    }
                }
            }

            conectar.Desconectar_MB29();
            return lista;
        }

        // Devuelve 0 si el envío todavía no tiene recepción registrada
        public int ObtenerIdEntregaPorEnvio_AB29(int idEnvio)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();
            int id = 0;

            string query = "SELECT TOP 1 IdEntrega FROM EntregaEnvio WHERE IdEnvio = @IdEnvio ORDER BY IdEntrega DESC";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdEnvio", idEnvio);
                var resultado = comando.ExecuteScalar();
                if (resultado != null) id = Convert.ToInt32(resultado);
            }

            conectar.Desconectar_MB29();
            return id;
        }
    }
}
