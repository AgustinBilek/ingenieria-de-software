using Servicio_MB29;
using DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class EnvioBLL_MB29
    {
        private readonly EnvioDAL_MB29 _dal = new EnvioDAL_MB29();

        public Remitente_MB29 BuscarRemitentePorDNI_MB29(long dni)
        {
            return _dal.BuscarRemitentePorDNI_MB29(dni);
        }

        public Destinatario_MB29 BuscarDestinatarioPorDNI_MB29(long dni)
        {
            return _dal.BuscarDestinatarioPorDNI_MB29(dni);
        }
        public Paquete_MB29 RegistrarPaquete_MB29(string usuarioLogueado, string tipoPaquete, string contenido)
        {
            if (string.IsNullOrWhiteSpace(tipoPaquete))
                throw new Exception("Debe indicar el tipo de paquete.");
            if (string.IsNullOrWhiteSpace(contenido))
                throw new Exception("Debe indicar el contenido del paquete.");

            var paquete = new Paquete_MB29(tipoPaquete, contenido);
            paquete.IdPaquete_MB29 = _dal.GuardarPaquete_MB29(paquete);

            bitacoracambiosBLL.instancia.RegistrarAlta_MB29(
                usuarioLogueado, "Paquete", paquete.IdPaquete_MB29,
                $"Tipo={tipoPaquete}; Contenido={contenido}");

            return paquete;
        }


        public List<Paquete_MB29> ObtenerPaquetesSinEnvio_MB29()
        {
            return _dal.ObtenerPaquetesSinEnvio_MB29();
        }
        // ---------------- CU2: Registrar Envío ----------------
        // Pasos 2a, 2b y 3 de PN1: busca/da de alta remitente y destinatario
        // por DNI, y genera el envío asociándolos al paquete ya registrado.
        public Envio_MB29 RegistrarEnvio_MB29(string usuarioLogueado, int idPaquete,
            long dniRemitente, string nombreRem, string apellidoRem, string direccionRem, string telefonoRem, string emailRem,
            long dniDestinatario, string nombreDest, string direccionDest, string telefonoDest)
        {
            // 2a: remitente
            var remitente = _dal.BuscarRemitentePorDNI_MB29(dniRemitente);
            if (remitente == null)
            {
                remitente = new Remitente_MB29
                {
                    DNI_MB29 = dniRemitente,
                    Nombre_MB29 = nombreRem,
                    Apellido_MB29 = apellidoRem,
                    Direccion_MB29 = direccionRem,
                    Telefono_MB29 = telefonoRem,
                    Email_MB29 = emailRem
                };
                remitente.IdRemitente_MB29 = _dal.GuardarRemitente_MB29(remitente);
                bitacoracambiosBLL.instancia.RegistrarAlta_MB29(usuarioLogueado, "Remitente",
                    remitente.IdRemitente_MB29, $"DNI={dniRemitente}; Nombre={nombreRem} {apellidoRem}");
            }

            // 2b: destinatario
            var destinatario = _dal.BuscarDestinatarioPorDNI_MB29(dniDestinatario);
            if (destinatario == null)
            {
                destinatario = new Destinatario_MB29
                {
                    DNI_MB29 = dniDestinatario,
                    Nombre_MB29 = nombreDest,
                    Direccion_MB29 = direccionDest,
                    Telefono_MB29 = telefonoDest
                };
                destinatario.IdDestinatario_MB29 = _dal.GuardarDestinatario_MB29(destinatario);
                bitacoracambiosBLL.instancia.RegistrarAlta_MB29(usuarioLogueado, "Destinatario",
                    destinatario.IdDestinatario_MB29, $"DNI={dniDestinatario}; Nombre={nombreDest}");
            }

            // 3: generar el envío
            var envio = new Envio_MB29
            {
                IdPaquete_MB29 = idPaquete,
                IdRemitente_MB29 = remitente.IdRemitente_MB29,
                IdDestinatario_MB29 = destinatario.IdDestinatario_MB29,
                EstadoEnvio_MB29 = "Recibido en sucursal",
                FechaIngreso_MB29 = DateTime.Now.Date,
                HoraIngreso_MB29 = DateTime.Now.TimeOfDay
            };
            envio.IdEnvio_MB29 = _dal.GuardarEnvio_MB29(envio);

            bitacoracambiosBLL.instancia.RegistrarAlta_MB29(usuarioLogueado, "Envio",
                envio.IdEnvio_MB29, $"Paquete={idPaquete}; Remitente={remitente.IdRemitente_MB29}; Destinatario={destinatario.IdDestinatario_MB29}; Estado=Recibido en sucursal");

            return envio;
        }

        // ---------------- CU3: Gestionar Condiciones y Confirmación del Envío ----------------
        // Paso 4: condiciones (prioridad, tipo de envío)
        public void RegistrarCondiciones_MB29(string usuarioLogueado, int idEnvio, string prioridad, string tipoEnvio)
        {
            string estadoAnterior = _dal.ObtenerEstadoEnvio_MB29(idEnvio);
            _dal.ActualizarCondiciones_MB29(idEnvio, prioridad, tipoEnvio);

            bitacoracambiosBLL.instancia.RegistrarModificacion_MB29(usuarioLogueado, "Envio", idEnvio,
                $"Estado={estadoAnterior}", $"Prioridad={prioridad}; TipoEnvio={tipoEnvio}");
        }

        // Paso 5: pago
        public PagoEnvio_MB29 RegistrarPago_MB29(string usuarioLogueado, int idEnvio, string tipoPago, decimal monto)
        {
            if (monto <= 0)
                throw new Exception("El monto del pago debe ser mayor a cero.");

            var pago = new PagoEnvio_MB29
            {
                IdEnvio_MB29 = idEnvio,
                TipoPago_MB29 = tipoPago,
                MontoTotal_MB29 = monto,
                FechaPago_MB29 = DateTime.Now.Date
            };
            pago.IdPago_MB29 = _dal.GuardarPago_MB29(pago);

            bitacoracambiosBLL.instancia.RegistrarAlta_MB29(usuarioLogueado, "PagoEnvio",
                pago.IdPago_MB29, $"Envio={idEnvio}; TipoPago={tipoPago}; Monto={monto}");

            return pago;
        }

        // Paso 6: confirmación del envío -> genera código de seguimiento + DV y
        // pasa el estado a "Listo para despacho" (comprobante entregado al cliente)
        public Envio_MB29 ConfirmarEnvio_MB29(string usuarioLogueado, int idEnvio)
        {
            string estadoAnterior = _dal.ObtenerEstadoEnvio_MB29(idEnvio);
            if (estadoAnterior != "Recibido en sucursal")
                throw new Exception("El envío no está en condiciones de ser confirmado.");

            string codigoBase = GeneradorCodigoSeguimiento_MB29.GenerarCodigo_MB29(idEnvio);
            char dv = GeneradorCodigoSeguimiento_MB29.CalcularDigitoVerificador_MB29(codigoBase);
            string codigoCompleto = codigoBase; // el DV ya se calcula sobre este código y se guarda aparte

            _dal.ConfirmarEnvio_MB29(idEnvio, "Listo para despacho", codigoCompleto, dv);

            bitacoracambiosBLL.instancia.RegistrarModificacion_MB29(usuarioLogueado, "Envio", idEnvio,
                $"Estado={estadoAnterior}", $"Estado=Listo para despacho; CodigoSeguimiento={codigoCompleto}-{dv}");

            return new Envio_MB29
            {
                IdEnvio_MB29 = idEnvio,
                EstadoEnvio_MB29 = "Listo para despacho",
                CodigoSeguimiento_MB29 = codigoCompleto,
                DigitoVerificador_MB29 = dv
            };
        }
    }
}
