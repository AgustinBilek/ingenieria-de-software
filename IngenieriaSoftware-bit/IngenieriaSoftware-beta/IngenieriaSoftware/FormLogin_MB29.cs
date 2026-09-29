using Servicio_MB29;
using BLL_MB29;
using IngenieriaSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_MB29
{
     public partial class FormLogin_MB29 : Form, IObserverIdioma_MB29
    {
        
        public FormLogin_MB29()
        {
            InitializeComponent();
            Gestoridioma_MB29.Instancia_MB29.Agregar_MB29(this);
            actualizar_MB29(Gestoridioma_MB29.Instancia_MB29.IdiomaActual_MB29);
        }

        public UsuarioServicio_MB29 UsuarioAutenticado { get; private set; }

        private void RecuperarBtn_Click(object sender, EventArgs e)
        {
            FormCambiarContaseña_MB29 Frec = new FormCambiarContaseña_MB29();
            Frec.Show();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string usuario = UserTxt.Text.Trim();
            string contra = ContraTxt.Text.Trim();

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contra))
            {
                MessageBox.Show("Ingrese usuario y contraseña.");
                return;
            }

            // Verificar si ya hay sesión activa
            if (SessionManager_MB29.Instancia_MB29.HaySesion())
            {
                MessageBox.Show("Ya hay un usuario logueado.");
                return;
            }

            UsuarioAutenticado = UsuarioBLL_MB29.Instancia.Login_MB29(usuario, contra);

            if (UsuarioAutenticado == null)
            {
                var user = UsuarioBLL_MB29.Instancia.ObtenerUsuarioPorNombre_MB29(usuario);
                if (user == null)
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.");
                }
                else if (UsuarioBLL_MB29.Instancia.EstaBloqueado_MB29(user))
                {
                    MessageBox.Show("Usuario bloqueado. Contacte al administrador.");
                }
                else if (UsuarioBLL_MB29.Instancia.EstaDeshabilitado_MB29(user))
                {
                    MessageBox.Show("Usuario deshabilitado. Contacte al administrador.");
                }
                else
                {
                    int intentos = BitacoraBLL_MB29.instancia.ObtenerIntentosFallidos_MB29(usuario);
                    int restantes = 3 - intentos;
                    MessageBox.Show($"Usuario o contraseña incorrectos. Intentos restantes: {restantes}");
                }
                UserTxt.Clear();
                ContraTxt.Clear();
                UserTxt.Focus();
                return;
            }

            Gestoridioma_MB29.Instancia_MB29.AplicarIdiomaUsuario_MB29();

            if (UsuarioAutenticado.PrimerLogin_MB29)
            {
                MessageBox.Show("Bienvenido. Como es tu primer ingreso, debés cambiar tu contraseña.");
                FormCambiarContaseña_MB29 fcc = new FormCambiarContaseña_MB29();
                fcc.Show();
                this.BeginInvoke(new Action(() => this.Close()));
            }
            else
            {
                MessageBox.Show($"Bienvenido de nuevo {UsuarioAutenticado.Usuario_MB29}!");
                FormPrincipal_MB29 FP = new FormPrincipal_MB29();
                FP.Show();
                this.BeginInvoke(new Action(() => this.Close()));
            }
        }

        public void actualizar_MB29(string idioma)
        {
            var g = Gestoridioma_MB29.Instancia_MB29;
            this.Text = g.Traducir_MB29("login_titulo");
            label1.Text = g.Traducir_MB29("login_usuario");
            label2.Text = g.Traducir_MB29("login_contrasena");
            BtnLogin.Text = g.Traducir_MB29("login_btn_ingresar");
            btnSalir.Text = g.Traducir_MB29("login_btn_salir");
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Gestoridioma_MB29.Instancia_MB29.Eliminar_MB29(this);
            base.OnFormClosed(e);
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
