using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio_MB29
{
   public class Reporte_AB29
    {
        public int IdEnvio_AB29 { get; set; }
        public long DNIRemitente_AB29 { get; set; }
        public string NombreRemitente_AB29 { get; set; }
        public string ApellidoRemitente_AB29 { get; set; }
        public string NombreDestinatario_AB29 { get; set; }
        public string TipoPaquete_AB29 { get; set; }
        public string Contenido_AB29 { get; set; }
        public string Prioridad_AB29 { get; set; }
        public string EstadoEnvio_AB29 { get; set; }
        public DateTime FechaIngreso_AB29 { get; set; }
        public TimeSpan HoraIngreso_AB29 { get; set; }
        public string TipoPago_AB29 { get; set; }
        public decimal? MontoTotal_AB29 { get; set; }
    }
}
