using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio_MB29
{
   public class ClienteXML_AB29
    {
        public int IdPersona_MB29 { get; set; }
        public string Nombre_MB29 { get; set; }
        public string Apellido_MB29 { get; set; }
        public string Email_MB29 { get; set; }
        public string Telefono_MB29 { get; set; }
        public double DNI_MB29 { get; set; }
        public string Usuario_MB29 { get; set; }
        public int IdRol_MB29 { get; set; }
        public bool Bloqueado_MB29 { get; set; }
        public string Estado_MB29 { get; set; }
        public bool PrimerLogin_MB29 { get; set; }

        public ClienteXML_AB29() { }

        public ClienteXML_AB29(UsuarioServicio_MB29 u)
        {
            IdPersona_MB29 = u.IdPersona_MB29;
            Nombre_MB29 = u.Nombre_MB29;
            Apellido_MB29 = u.Apellido_MB29;
            Email_MB29 = u.Email_MB29;
            Telefono_MB29 = u.Telefono_MB29;
            DNI_MB29 = u.DNI_MB29;
            Usuario_MB29 = u.Usuario_MB29;
            IdRol_MB29 = u.IdRol_MB29;
            Bloqueado_MB29 = u.Bloqueado_MB29;
            Estado_MB29 = u.Estado_MB29;
            PrimerLogin_MB29 = u.PrimerLogin_MB29;
        }
    }
}
