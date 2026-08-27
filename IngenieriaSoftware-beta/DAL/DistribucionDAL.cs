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
   public class DistribucionDAL
    {
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
    }
}
