using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Servicio_MB29
{
    public class UsuarioServicio_MB29 : IInicioSesion_MB29
    {
        
        private int _idPersona_MB29;

        [DisplayName("IdPersona")]
        public int IdPersona_MB29
        {
            get { return _idPersona_MB29; }
            set { _idPersona_MB29 = value; }
        }

        private string _nombre_MB29;
        [DisplayName("Nombre")]
        public string Nombre_MB29
        {
            get { return _nombre_MB29; }
            set { _nombre_MB29 = value; }
        }

        private string _apellido_MB29;
        [DisplayName("Apellido")]
        public string Apellido_MB29
        {
            get { return _apellido_MB29; }
            set { _apellido_MB29 = value; }
        }

        private string _email_MB29;
        [DisplayName("Email")]
        public string Email_MB29
        {
            get { return _email_MB29; }
            set { _email_MB29 = value; }
        }

        private string _telefono_MB29;
        [DisplayName("Telefono")]
        public string Telefono_MB29
        {
            get { return _telefono_MB29; }
            set { _telefono_MB29 = value; }
        }

        private double _dni_MB29;
        [DisplayName("DNI")]
        public double DNI_MB29
        {
            get { return _dni_MB29; }
            set { _dni_MB29 = value; }
        }

        private string _usuario_MB29;
        [DisplayName("Usuario")]
        public string Usuario_MB29
        {
            get { return _usuario_MB29; }
            set { _usuario_MB29 = value; }
        }

        private string _passhash_MB29;
        public string PassHash_MB29
        {
            get { return _passhash_MB29; }
            set { _passhash_MB29 = value; }
        }
        public string contra_MB29;
        public string Contra_MB29
        {
            get { return contra_MB29; }
            set { contra_MB29 = value; }
        }
        private int _idrol_MB29;
        [DisplayName("IdRol")]
        public int IdRol_MB29
        {
            get { return _idrol_MB29; }
            set { _idrol_MB29 = value; }
        }
        private int _intentosErrados_MB29;
        public int IntentosErrados_MB29
        {
            get { return _intentosErrados_MB29; }
            set { _intentosErrados_MB29 = value; }
        }

        private bool _bloqueado_MB29;
        [DisplayName("Bloqueado")]
        public bool Bloqueado_MB29
        {
            get { return _bloqueado_MB29; }
            set { _bloqueado_MB29 = value; }
        }

        private string _estado_MB29;
        [DisplayName("Estado")]
        public string Estado_MB29
        {
            get { return _estado_MB29; }
            set { _estado_MB29 = value; }
        }

        private bool _primerLogin_MB29;
        [DisplayName("PrimerLogin")]
        public bool PrimerLogin_MB29
        {
            get { return _primerLogin_MB29; }
            set { _primerLogin_MB29 = value; }
        }

        private string _ultimoidioma_MB29;
        [DisplayName("IdiomaPreferido")]
        public string UltimoIdioma_MB29
        {
            get { return _ultimoidioma_MB29; }
            set { _ultimoidioma_MB29 = value; }
        }

        private Rol_MB29 _rol_MB29;
        [DisplayName("Rol")]
        public Rol_MB29 Rol_MB29
        {
            get { return _rol_MB29; }
            set { _rol_MB29 = value; }
        }

        public UsuarioServicio_MB29(int id, string usuario, string contra, bool esHash, string nombre, string apellido, double dni, int idrol, string email, string telefono, bool bloqueado)
        {
            IdPersona_MB29 = id;
            Usuario_MB29 = usuario;
            if (esHash)
            {
                PassHash_MB29 = contra;
            }
            else
            {
                PassHash_MB29 = Encriptador_MB29.EncriptarPassword_MB29(contra);
            }
            Nombre_MB29 = nombre;
            Apellido_MB29 = apellido;
            DNI_MB29 = dni;
            IdRol_MB29 = idrol;
            Email_MB29 = email;
            Telefono_MB29 = telefono;
            IntentosErrados_MB29 = 0;
            Bloqueado_MB29 = bloqueado;
        }

        public UsuarioServicio_MB29(string usuario, string contra)
        {
            Usuario_MB29 = usuario;
            PassHash_MB29 = Encriptador_MB29.EncriptarPassword_MB29(contra);
        }

        public bool IniciarSesion_MB29(string usuario, string passwordIngresada)
        {
            return Usuario_MB29 == usuario && Encriptador_MB29.EncriptarPassword_MB29(passwordIngresada) == this.PassHash_MB29;
        }
    }
}
