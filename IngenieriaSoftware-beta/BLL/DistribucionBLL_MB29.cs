using Servicio_MB29;
using DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class DistribucionBLL_MB29
    {
        private readonly DistribucionDAL_MB29 _dal = new DistribucionDAL_MB29();

       
        public List<Envio_MB29> ObtenerEnviosAsignados_AB29()
        {
            return _dal.ObtenerEnviosPorEstado_AB29("Asignado");
        }

        public List<Vehiculo_MB29> ObtenerVehiculosDisponibles_AB29()
        {
            return _dal.ObtenerVehiculosDisponibles_AB29();
        }

        public List<EnvioParaEntrega_AB29> ObtenerEnviosCargadosDeRepartidor_AB29(int idRepartidor)
        {
            return _dal.ObtenerEnviosDeRepartidor_AB29(idRepartidor, "Cargado");
        }

        public List<EnvioParaEntrega_AB29> ObtenerEnviosEnCaminoDeRepartidor_AB29(int idRepartidor)
        {
            return _dal.ObtenerEnviosDeRepartidor_AB29(idRepartidor, "En camino");
        }

        public int ObtenerIdEntrega_AB29(int idEnvio)
        {
            return _dal.ObtenerIdEntregaPorEnvio_AB29(idEnvio);
        }

        public AsignacionRepartidor_MB29 AsignarEnvioARepartidor_MB29(string usuarioLogueado, int idEnvio, int idRepartidor)
        {
            string estadoAnterior = _dal.ObtenerEstadoEnvio_AB29(idEnvio);
            if (estadoAnterior != "Listo para despacho")
                throw new Exception("El envío no está en condiciones de ser asignado.");

            var asignacion = new AsignacionRepartidor_MB29
            {
                IdEnvio_MB29 = idEnvio,
                IdRepartidor_MB29 = idRepartidor,
                FechaAsignacion_MB29 = DateTime.Now
            };
            asignacion.IdAsignacion_MB29 = _dal.GuardarAsignacion_MB29(asignacion);
            _dal.ActualizarEstadoEnvio_MB29(idEnvio, "Asignado");

            bitacoracambiosBLL.instancia.RegistrarModificacion_MB29(usuarioLogueado, "Envio", idEnvio,
                $"Estado={estadoAnterior}", $"Estado=Asignado; Repartidor={idRepartidor}");

            return asignacion;
        }

        
        public CargaVehiculo_MB29 CargarPaqueteEnVehiculo_MB29(string usuarioLogueado, int idEnvio, int idVehiculo, int idOperarioCarga)
        {
            string estadoAnterior = _dal.ObtenerEstadoEnvio_AB29(idEnvio);
            if (estadoAnterior != "Asignado")
                throw new Exception("El envío no está asignado a un repartidor.");

            var carga = new CargaVehiculo_MB29
            {
                IdEnvio_MB29 = idEnvio,
                IdVehiculo_MB29 = idVehiculo,
                IdOperarioCarga_MB29 = idOperarioCarga,
                FechaHoraCarga_MB29 = DateTime.Now
            };
            carga.IdCarga_MB29 = _dal.GuardarCarga_MB29(carga);
            _dal.ActualizarEstadoEnvio_MB29(idEnvio, "Cargado");

            bitacoracambiosBLL.instancia.RegistrarModificacion_MB29(usuarioLogueado, "Envio", idEnvio,
                $"Estado={estadoAnterior}", $"Estado=Cargado; Vehiculo={idVehiculo}");

            return carga;
        }

        
        public EntregaEnvio_MB29 RecibirEnvioParaDistribucion_MB29(string usuarioLogueado, int idEnvio, int idRepartidor)
        {
            if (_dal.ObtenerEstadoEnvio_AB29(idEnvio) != "Cargado")
                throw new Exception("El envío no está cargado en el vehículo.");
            if (_dal.ObtenerIdEntregaPorEnvio_AB29(idEnvio) != 0)
                throw new Exception("El envío ya fue recibido para su distribución.");

            var entrega = new EntregaEnvio_MB29
            {
                IdEnvio_MB29 = idEnvio,
                IdRepartidor_MB29 = idRepartidor,
                FechaHoraRecepcion_MB29 = DateTime.Now
            };
            entrega.IdEntrega_MB29 = _dal.GuardarEntrega_MB29(entrega);
            return entrega;
        }
       
        public void IniciarEntrega_MB29(string usuarioLogueado, int idEntrega, int idEnvio)
        {
            string estadoAnterior = _dal.ObtenerEstadoEnvio_AB29(idEnvio);
            if (estadoAnterior != "Cargado")
                throw new Exception("El envío no está en condiciones de iniciar la entrega.");

            var ahora = DateTime.Now;
            _dal.RegistrarInicioEntrega_MB29(idEntrega, ahora.Date, ahora.TimeOfDay);
            _dal.ActualizarEstadoEnvio_MB29(idEnvio, "En camino");

            bitacoracambiosBLL.instancia.RegistrarModificacion_MB29(usuarioLogueado, "Envio", idEnvio,
                $"Estado={estadoAnterior}", "Estado=En camino");
        }

     
        public void RegistrarEntregaFinal_MB29(string usuarioLogueado, int idEntrega, int idEnvio, string firmaDestinatario)
        {
            if (string.IsNullOrWhiteSpace(firmaDestinatario))
                throw new Exception("Se requiere la conformidad (firma) del destinatario.");

            string estadoAnterior = _dal.ObtenerEstadoEnvio_AB29(idEnvio);
            if (estadoAnterior != "En camino")
                throw new Exception("El envío no está en camino.");

            var ahora = DateTime.Now;
            _dal.RegistrarEntregaFinal_MB29(idEntrega, ahora.Date, ahora.TimeOfDay, firmaDestinatario);
            _dal.ActualizarEstadoEnvio_MB29(idEnvio, "Entregado");

            bitacoracambiosBLL.instancia.RegistrarModificacion_MB29(usuarioLogueado, "Envio", idEnvio,
                $"Estado={estadoAnterior}", "Estado=Entregado");
        }


        public List<Envio_MB29> ObtenerEnviosListosParaDespacho_AB29()
        {
            return _dal.ObtenerEnviosListosParaDespacho_AB29();
        }

        public List<RepartidorDisponible_AB29> ObtenerRepartidoresDisponibles_AB29()
        {
            return _dal.ObtenerRepartidoresDisponibles_AB29();
        }



        public List<ReporteEnvioPaquetes_AB29> ObtenerReporteEnvioPaquetes_AB29()
        {
            return _dal.ObtenerReporteEnvioPaquetes_AB29();
        }

    }
}
