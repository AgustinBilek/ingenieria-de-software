using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio_MB29
{
   public class Clientebackup_AB29
    {
        public string TipoCliente_AB29 { get; set; } // "Remitente" o "Destinatario"
        public int IdCliente_AB29 { get; set; }
        public long DNI_AB29 { get; set; }
        public string Nombre_AB29 { get; set; }
        public string Apellido_AB29 { get; set; }   // vacío si es Destinatario
        public string Direccion_AB29 { get; set; }
        public string Telefono_AB29 { get; set; }
        public string Email_AB29 { get; set; }       // vacío si es Destinatario

        public Clientebackup_AB29() { } // requerido por XmlSerializer

        public static Clientebackup_AB29 DesdeRemitente_AB29(Remitente_MB29 r)
        {
            return new Clientebackup_AB29
            {
                TipoCliente_AB29 = "Remitente",
                IdCliente_AB29 = r.IdRemitente_MB29,
                DNI_AB29 = r.DNI_MB29,
                Nombre_AB29 = r.Nombre_MB29,
                Apellido_AB29 = r.Apellido_MB29,
                Direccion_AB29 = r.Direccion_MB29,
                Telefono_AB29 = r.Telefono_MB29,
                Email_AB29 = r.Email_MB29
            };
        }

        public static Clientebackup_AB29 DesdeDestinatario_AB29(Destinatario_MB29 d)
        {
            return new Clientebackup_AB29
            {
                TipoCliente_AB29 = "Destinatario",
                IdCliente_AB29 = d.IdDestinatario_MB29,
                DNI_AB29 = d.DNI_MB29,
                Nombre_AB29 = d.Nombre_MB29,
                Apellido_AB29 = "",
                Direccion_AB29 = d.Direccion_MB29,
                Telefono_AB29 = d.Telefono_MB29,
                Email_AB29 = ""
            };
        }
    }
}
