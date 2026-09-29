using DAL;
using Servicio_MB29;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class DistribucionBLL
    {
        private readonly DistribucionDAL _dal = new DistribucionDAL();

        // ---------------- CU1 PN2: Asignar Envío a Repartidor ----------------
        // Pasos 1 y 2: preparar el paquete para despacho y asignarlo a un repartidor.
        public AsignacionRepartidor_MB29 AsignarEnvioARepartidor_MB29(string usuarioLogueado, int idEnvio, int idRepartidor)
        {
            var asignacion = new AsignacionRepartidor_MB29
            {
                IdEnvio_MB29 = idEnvio,
                IdRepartidor_MB29 = idRepartidor,
                FechaAsignacion_MB29 = DateTime.Now
            };
            asignacion.IdAsignacion_MB29 = _dal.GuardarAsignacion_MB29(asignacion);
            _dal.ActualizarEstadoEnvio_MB29(idEnvio, "Asignado");

            bitacoracambiosBLL.instancia.RegistrarModificacion_MB29(usuarioLogueado, "Envio", idEnvio,
                "Estado=Listo para despacho", $"Estado=Asignado; Repartidor={idRepartidor}");

            return asignacion;
        }

        // ---------------- CU2 PN2: Cargar Paquete en Vehículo ----------------
        // Paso 3: el operario de carga recibe el paquete y lo carga al vehículo.
        public CargaVehiculo_MB29 CargarPaqueteEnVehiculo_MB29(string usuarioLogueado, int idEnvio, int idVehiculo, int idOperarioCarga)
        {
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
                "Estado=Asignado", $"Estado=Cargado; Vehiculo={idVehiculo}");

            return carga;
        }

        // ---------------- CU3 PN2: Realizar Entrega ----------------
        // Paso 4: el repartidor recibe los envíos asignados para su distribución.
        public EntregaEnvio_MB29 RecibirEnvioParaDistribucion_MB29(string usuarioLogueado, int idEnvio, int idRepartidor)
        {
            var entrega = new EntregaEnvio_MB29
            {
                IdEnvio_MB29 = idEnvio,
                IdRepartidor_MB29 = idRepartidor,
                FechaHoraRecepcion_MB29 = DateTime.Now
            };
            entrega.IdEntrega_MB29 = _dal.GuardarEntrega_MB29(entrega);
            return entrega;
        }

        // Paso 5: inicio de la entrega -> estado "en camino"
        public void IniciarEntrega_MB29(string usuarioLogueado, int idEntrega, int idEnvio)
        {
            var ahora = DateTime.Now;
            _dal.RegistrarInicioEntrega_MB29(idEntrega, ahora.Date, ahora.TimeOfDay);
            _dal.ActualizarEstadoEnvio_MB29(idEnvio, "En camino");

            bitacoracambiosBLL  .instancia.RegistrarModificacion_MB29(usuarioLogueado, "Envio", idEnvio,
                "Estado=Cargado", "Estado=En camino");
        }

        // Paso 6: entrega final + firma del destinatario -> estado "entregado"
        public void RegistrarEntregaFinal_MB29(string usuarioLogueado, int idEntrega, int idEnvio, string firmaDestinatario)
        {
            if (string.IsNullOrWhiteSpace(firmaDestinatario))
                throw new Exception("Se requiere la conformidad (firma) del destinatario.");

            var ahora = DateTime.Now;
            _dal.RegistrarEntregaFinal_MB29(idEntrega, ahora.Date, ahora.TimeOfDay, firmaDestinatario);
            _dal.ActualizarEstadoEnvio_MB29(idEnvio, "Entregado");

            bitacoracambiosBLL.instancia.RegistrarModificacion_MB29(usuarioLogueado, "Envio", idEnvio,
                "Estado=En camino", "Estado=Entregado");
        }
    }
}
