using BLL_MB29;
using Servicio_MB29;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_MB29;

namespace IngenieriaSoftware
{
    public partial class FormPrincipal_MB29 : Form, IObserverIdioma_MB29, IObserverSesion_MB29
    {
        public FormPrincipal_MB29()
        {
            InitializeComponent();
            if (SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.IdRol_MB29 == 1)
            {
                administradorToolStripMenuItem.Enabled = true;
                usuarioToolStripMenuItem.Enabled = true;
                rF1ToolStripMenuItem.Enabled = true;
                rF2ToolStripMenuItem.Enabled = true;
                ayudaToolStripMenuItem.Enabled = true;
            }
            else
            {
                administradorToolStripMenuItem.Enabled = false;
                administradorToolStripMenuItem.Visible = false;
                usuarioToolStripMenuItem.Enabled = true;
                rF1ToolStripMenuItem.Enabled = false;
                rF1ToolStripMenuItem.Visible = false;
                rF2ToolStripMenuItem.Enabled = false;
                rF2ToolStripMenuItem.Visible = false;
                ayudaToolStripMenuItem.Enabled = true;
            }
            //Registrar como observer y aplicar idioma actual
            Gestoridioma_MB29.Instancia_MB29.Agregar_MB29(this);
            actualizar_MB29(Gestoridioma_MB29.Instancia_MB29.IdiomaActual_MB29);
            SessionManager_MB29.Instancia_MB29.AgregarObserverSesion_MB29(this);
        }

        public void SesionCerrada_MB29()
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(() => this.Close()));
            else
                this.Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Gestoridioma_MB29.Instancia_MB29.Eliminar_MB29(this);
            SessionManager_MB29.Instancia_MB29.EliminarObserverSesion_MB29(this);
            base.OnFormClosed(e);

            if (Application.OpenForms.Count == 0)
                Application.Exit();
        }

        private void gestionDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGESTIONUSER_MB29 FGESTUS= new FormGESTIONUSER_MB29();
            FGESTUS.Show();
        }

        private void bitacoraDeEventosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBitacora_MB29 FBITA = new FormBitacora_MB29();
            FBITA.Show();
        }

        private void cerrarSesionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormLogin_MB29 _login = new FormLogin_MB29();
            _login.Show();

            UsuarioServicio_MB29 usuario = SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29;
            UsuarioBLL_MB29.Instancia.CerrarSesion_MB29(usuario);
        }

        private void iniciarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLogin_MB29 _login = new FormLogin_MB29();
            _login.Show();
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCambiarContaseña_MB29 CamCon = new FormCambiarContaseña_MB29();
            CamCon.Show();
            this.Close();
        }
        public void actualizar_MB29(string idioma)
        {
            var g = Gestoridioma_MB29.Instancia_MB29;
            this.Text = g.Traducir_MB29("menu_administrador");
            administradorToolStripMenuItem.Text = g.Traducir_MB29("menu_administrador");
            gestionDeUsuariosToolStripMenuItem.Text = g.Traducir_MB29("menu_gestion_usuarios");
            gestionDePerfilesToolStripMenuItem.Text = g.Traducir_MB29("menu_gestion_perfiles");
            bitacoraDeEventosToolStripMenuItem.Text = g.Traducir_MB29("menu_bitacora");
            cerrarSesionToolStripMenuItem.Text = g.Traducir_MB29("menu_cerrar_sesion");
            usuarioToolStripMenuItem.Text = g.Traducir_MB29("menu_usuario");
            cambiarContraseñaToolStripMenuItem.Text = g.Traducir_MB29("menu_cambiar_contrasena");
            cambairIdiomaToolStripMenuItem.Text = g.Traducir_MB29("menu_cambiar_idioma");
            cerrarSesionToolStripMenuItem1.Text = g.Traducir_MB29("menu_cerrar_sesion");
            iniciarSesionToolStripMenuItem.Text = g.Traducir_MB29("menu_iniciar_sesion");
            ayudaToolStripMenuItem.Text = g.Traducir_MB29("menu_ayuda");
            gestionRolesToolStripMenuItem.Text = g.Traducir_MB29("menu_gestion_roles");
            gestionFamiliasToolStripMenuItem.Text = g.Traducir_MB29("menu_gestion_familias");
        }

        private void españolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gestoridioma_MB29.Instancia_MB29.CambiarIdioma_MB29("ES");
            UsuarioBLL_MB29.Instancia.GuardarIdioma_MB29(SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29);
        }

        private void inglesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gestoridioma_MB29.Instancia_MB29.CambiarIdioma_MB29("EN");
            UsuarioBLL_MB29.Instancia.GuardarIdioma_MB29(SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29);          
        }

        private void portuguesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gestoridioma_MB29.Instancia_MB29.CambiarIdioma_MB29("PT");
            UsuarioBLL_MB29.Instancia.GuardarIdioma_MB29(SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29);
        }

        private void gestionFamiliasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGESTIONPERFIL_MB29 FPER = new FormGESTIONPERFIL_MB29();
            FPER.Show();
        }

        private void gestionRolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRoles_MB29 formRoles = new FormRoles_MB29();  
            formRoles.Show();

        }

        private void FormPrincipal_MB29_Load(object sender, EventArgs e)
        {

        }
    }
}
