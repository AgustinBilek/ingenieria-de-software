using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio_MB29
{
   public class BitacoraServicio_MB29
    {

     private int _Id_MB29;
        [DisplayName("Id")]
        public int id_MB29
        {
            get { return _Id_MB29; }
            set { _Id_MB29 = value; }
        }
        private string _Usuario_MB29;
        [DisplayName("Usuario")]
        public string usuario_MB29
        {
            get { return _Usuario_MB29; }
            set { _Usuario_MB29 = value; }
        }
        private string _Accion_MB29;
        [DisplayName("Accion")]
        public string accion_MB29
        {
            get { return _Accion_MB29; }
            set { _Accion_MB29 = value; }
        }

       private string _Modulo_MB29;
        [DisplayName("Modulo")]
        public string modulo_MB29
        {
            get { return _Modulo_MB29; }
            set { _Modulo_MB29 = value; }
        }
        private DateTime _Fecha_MB29;
        [DisplayName("Fecha")]
        public DateTime fecha_MB29
        {
            get { return _Fecha_MB29; }
            set { _Fecha_MB29 = value; }
        }
      private string _descripcion_MB29;
        [DisplayName("Descripcion")]
        public string Descripcion_MB29
        {
            get { return _descripcion_MB29; }
            set { _descripcion_MB29 = value; }
        }
        //evaluamos del 1 al 5 , 5 acciones criticas, 1 acciones menores
        private int _criticidad_MB29;
        [DisplayName("Criticidad")]
        public int Criticidad_MB29
        {
            get { return _criticidad_MB29; }
            set { _criticidad_MB29 = value; }
        }
        public BitacoraServicio_MB29(string _usuario, string _accion, string _modulo, DateTime _fecha, string _descripcion, int _criticidad)
        {
            usuario_MB29 = _usuario;
            accion_MB29 = _accion;
            modulo_MB29 = _modulo;
            fecha_MB29 = _fecha;
            Descripcion_MB29 = _descripcion;
            Criticidad_MB29 = _criticidad;
        }

        public BitacoraServicio_MB29(int _id, string _usuario, string _accion, string _modulo, DateTime _fecha, string _descripcion, int _criticidad)
        {
            id_MB29 = _id;
            usuario_MB29 = _usuario;
            accion_MB29 = _accion;
            modulo_MB29 = _modulo;
            fecha_MB29 = _fecha;
            Descripcion_MB29 = _descripcion;
            Criticidad_MB29 = _criticidad;
        }
    }
}
