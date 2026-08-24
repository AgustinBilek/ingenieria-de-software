using Servicio_MB29;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio_MB29
{
   public class BitacoraBLL_MB29
    {
       private static BitacoraBLL_MB29 _instancia;
        
        public static BitacoraBLL_MB29 instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new BitacoraBLL_MB29();
                return _instancia;
            }
        }

        private BitacoraDAL_MB29 _dal = new BitacoraDAL_MB29();
        private List<BitacoraServicio_MB29> _bitacora = new List<BitacoraServicio_MB29>();

        public List<BitacoraServicio_MB29> CargarBitacora_MB29()
        {
            _bitacora = _dal.CargarBitacora_MB29();
            return _bitacora;
        }

        public void Registrar_MB29(string usuario, string accion, string modulo, string descripcion, int criticidad)
        {           
            BitacoraServicio_MB29 bitacora = new BitacoraServicio_MB29(usuario,
                accion,
                modulo,
                DateTime.Now,
                descripcion,
                criticidad);

            _dal.Guardar_MB29(bitacora);
        }

        public int ObtenerIntentosFallidos_MB29(string usuario)
        {
            return _dal.ObtenerIntentosFallidos_MB29(usuario);
        }
    }
}
