using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
  public  class bitacoracambiosBLL
    {
        private static bitacoracambiosBLL _instancia;

        public static bitacoracambiosBLL instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new bitacoracambiosBLL();
                return _instancia;
            }
        }

        private bitacoracambiosDAL _dal = new bitacoracambiosDAL();

        // usuario: viene de SessionManager_MB29 (usuario logueado)
        public void RegistrarAlta_MB29(string usuario, string tabla, int idRegistro, string detalleNuevo)
        {
            var cambio = new CambioServicio_MB29(usuario, tabla, "Alta", idRegistro, null, detalleNuevo);
            _dal.Guardar_MB29(cambio);
        }

        public void RegistrarModificacion_MB29(string usuario, string tabla, int idRegistro, string detalleAnterior, string detalleNuevo)
        {
            var cambio = new CambioServicio_MB29(usuario, tabla, "Modificacion", idRegistro, detalleAnterior, detalleNuevo);
            _dal.Guardar_MB29(cambio);
        }

        public void RegistrarBaja_MB29(string usuario, string tabla, int idRegistro, string detalleAnterior)
        {
            var cambio = new CambioServicio_MB29(usuario, tabla, "Baja", idRegistro, detalleAnterior, null);
            _dal.Guardar_MB29(cambio);
        }

        public List<CambioServicio_MB29> CargarPorTabla_MB29(string tabla)
        {
            return _dal.CargarPorTabla_MB29(tabla);
        }
    }
}
}
