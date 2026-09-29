using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio_MB29
{


    public class Paquete_MB29
    {
        public int IdPaquete_MB29 { get; set; }
        public string TipoPaquete_MB29 { get; set; }
        public string Contenido_MB29 { get; set; }

        public Paquete_MB29() { }
        public Paquete_MB29(string tipoPaquete, string contenido)
        {
            TipoPaquete_MB29 = tipoPaquete;
            Contenido_MB29 = contenido;
        }

        public string Descripcion_MB29
        {
            get { return $"#{IdPaquete_MB29} - {TipoPaquete_MB29} ({Contenido_MB29})"; }
        }
    }

    public class Remitente_MB29
    {
        public int IdRemitente_MB29 { get; set; }
        public long DNI_MB29 { get; set; }
        public string Nombre_MB29 { get; set; }
        public string Apellido_MB29 { get; set; }
        public string Direccion_MB29 { get; set; }
        public string Telefono_MB29 { get; set; }
        public string Email_MB29 { get; set; }
    }

    public class Destinatario_MB29
    {
        public int IdDestinatario_MB29 { get; set; }
        public long DNI_MB29 { get; set; }
        public string Nombre_MB29 { get; set; }
        public string Direccion_MB29 { get; set; }
        public string Telefono_MB29 { get; set; }
    }

    public class Vehiculo_MB29
    {
        public int IdVehiculo_MB29 { get; set; }
        public string Patente_MB29 { get; set; }
        public string Tipo_MB29 { get; set; }
        public string Estado_MB29 { get; set; }
    }

    public class Envio_MB29
    {
        public int IdEnvio_MB29 { get; set; }
        public int IdPaquete_MB29 { get; set; }
        public int IdRemitente_MB29 { get; set; }
        public int IdDestinatario_MB29 { get; set; }
        public string EstadoEnvio_MB29 { get; set; }
        public DateTime FechaIngreso_MB29 { get; set; }
        public TimeSpan HoraIngreso_MB29 { get; set; }
        public string Prioridad_MB29 { get; set; }
        public string TipoEnvio_MB29 { get; set; }
        public string CodigoSeguimiento_MB29 { get; set; }
        public char DigitoVerificador_MB29 { get; set; }

        public string Descripcion_MB29
        {
            get { return $"#{IdEnvio_MB29} - Paquete #{IdPaquete_MB29} ({FechaIngreso_MB29:dd/MM/yyyy})"; }
        }
    }

    public class PagoEnvio_MB29
    {
        public int IdPago_MB29 { get; set; }
        public int IdEnvio_MB29 { get; set; }
        public string TipoPago_MB29 { get; set; }
        public decimal MontoTotal_MB29 { get; set; }
        public DateTime FechaPago_MB29 { get; set; }
    }

    public class AsignacionRepartidor_MB29
    {
        public int IdAsignacion_MB29 { get; set; }
        public int IdEnvio_MB29 { get; set; }
        public int IdRepartidor_MB29 { get; set; }
        public DateTime FechaAsignacion_MB29 { get; set; }
    }

    public class CargaVehiculo_MB29
    {
        public int IdCarga_MB29 { get; set; }
        public int IdEnvio_MB29 { get; set; }
        public int IdVehiculo_MB29 { get; set; }
        public int IdOperarioCarga_MB29 { get; set; }
        public DateTime FechaHoraCarga_MB29 { get; set; }
    }

    public class EntregaEnvio_MB29
    {
        public int IdEntrega_MB29 { get; set; }
        public int IdEnvio_MB29 { get; set; }
        public int IdRepartidor_MB29 { get; set; }
        public DateTime? FechaHoraRecepcion_MB29 { get; set; }
        public DateTime? FechaSalida_MB29 { get; set; }
        public TimeSpan? HoraSalida_MB29 { get; set; }
        public DateTime? FechaEntrega_MB29 { get; set; }
        public TimeSpan? HoraEntrega_MB29 { get; set; }
        public string FirmaDestinatario_MB29 { get; set; }
    }

    // Registro de la Bitácora de Cambios (solo ABM, distinta de BitacoraServicio_MB29)
    public class Cambioservicio_MB29
    {
        public int IdCambio_MB29 { get; set; }
        public string Usuario_MB29 { get; set; }
        public string Tabla_MB29 { get; set; }
        public string Operacion_MB29 { get; set; } // "Alta" | "Baja" | "Modificacion"
        public int IdRegistro_MB29 { get; set; }
        public DateTime FechaHora_MB29 { get; set; }
        public string DetalleAnterior_MB29 { get; set; }
        public string DetalleNuevo_MB29 { get; set; }

        public Cambioservicio_MB29() { }

        public Cambioservicio_MB29(string usuario, string tabla, string operacion, int idRegistro,
            string detalleAnterior, string detalleNuevo)
        {
            Usuario_MB29 = usuario;
            Tabla_MB29 = tabla;
            Operacion_MB29 = operacion;
            IdRegistro_MB29 = idRegistro;
            FechaHora_MB29 = DateTime.Now;
            DetalleAnterior_MB29 = detalleAnterior;
            DetalleNuevo_MB29 = detalleNuevo;
        }



    }

}

