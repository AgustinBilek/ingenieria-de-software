using BLL;
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

namespace IngenieriaSoftware
{
    public partial class FormRoles_MB29 : Form, IObserverSesion_MB29
    {

        private readonly RolBLL_MB29 rolBLL = new RolBLL_MB29();
        private readonly FamiliaBLL_MB29 familiaBLL = new FamiliaBLL_MB29();
        private readonly PermisoBLL_MB29 permisoBLL = new PermisoBLL_MB29();
        public FormRoles_MB29()
        {
            InitializeComponent();
            CargarRoles_MB29();
            CargarFamilias_MB29();
            CargarPermisos_MB29();
            SessionManager_MB29.Instancia_MB29.AgregarObserverSesion_MB29(this);
            actualizar_MB29(Gestoridioma_MB29.Instancia_MB29.IdiomaActual_MB29);
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
            SessionManager_MB29.Instancia_MB29.EliminarObserverSesion_MB29(this);
            base.OnFormClosed(e);
        }

        /*private void EstaLogueado_MB29()
        {
            if (!SessionManager_MB29.Instancia_MB29.HaySesion())
            {
                this.Close();
                return;
            }
            else if (SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.IdRol_MB29 == 2)
            {
                this.Close();
                return;
            }
        }*/

        private void CargarRoles_MB29()
        {
            var roles = rolBLL.ObtenerRoles_MB29();
            LBRoles.DataSource = null;
            LBRoles.DataSource = roles;
            LBRoles.DisplayMember = "Nombre";
        }

        private void CargarFamilias_MB29()
        {
            var familias = familiaBLL.ObtenerFamilias_MB29();
            LBFamilias.DataSource = null;
            LBFamilias.DataSource = familias;
            LBFamilias.DisplayMember = "Nombre";
        }

        private void CargarPermisos_MB29()
        {
            var permisos = permisoBLL.ObtenerPermisos_MB29();
            LBPermisos.DataSource = null;
            LBPermisos.DataSource = permisos;
            LBPermisos.DisplayMember = "Nombre";
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRol.Text))
            {
                MessageBox.Show("Ingrese un nombre para el rol.");
                return;
            }

            try
            {
                var rol = new Rol_MB29 { Nombre = txtRol.Text.Trim() };
                rolBLL.GuardarRol_MB29(rol);
                txtRol.Clear();
                CargarRoles_MB29();
                MessageBox.Show("Rol creado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear rol: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (LBRoles.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un rol.");
                return;
            }

            try
            {
                var rol = (Rol_MB29)LBRoles.SelectedItem;
                rolBLL.EliminarRol_MB29(rol);
                CargarRoles_MB29();
                MessageBox.Show("Rol eliminado.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregarPerm_Click(object sender, EventArgs e)
        {
            if (LBRoles.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un rol.");
                return;
            }

            if (LBFamilias.SelectedItem == null && LBPermisos.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un permiso para agregar al rol.");
                return;
            }

            try
            {
                var rol = (Rol_MB29)LBRoles.SelectedItem;

                var permiso = (Permiso_MB29)LBPermisos.SelectedItem;
                rolBLL.AgregarComponente_MB29(rol, permiso);
                MessageBox.Show("Permiso agregado al rol correctamente.");

                CargarRoles_MB29();
                CargarComponentesRol_MB29();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnAgregarFam_Click(object sender, EventArgs e)
        {
            if (LBRoles.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un rol.");
                return;
            }

            if (LBFamilias.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una familia para agregar al rol.");
                return;
            }

            try
            {
                var rol = (Rol_MB29)LBRoles.SelectedItem;

                var familia = (Familia_MB29)LBFamilias.SelectedItem;
                rolBLL.AgregarComponente_MB29(rol, familia);
                MessageBox.Show("Familia agregada al rol correctamente.");

                CargarRoles_MB29();
                CargarComponentesRol_MB29();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void CargarComponentesRol_MB29()
        {
            if (LBRoles.SelectedItem == null) return;

            var rol = (Rol_MB29)LBRoles.SelectedItem;

            //Familias del rol
            var familias = rol.Componentes_MB29.OfType<Familia_MB29>().ToList();
            LBFamiliasRol.DataSource = null;
            LBFamiliasRol.DataSource = familias;
            LBFamiliasRol.DisplayMember = "Nombre";

            //Permisos sueltos del rol
            var permisos = rol.Componentes_MB29.OfType<Permiso_MB29>().ToList();
            LBPermisosRol.DataSource = null;
            LBPermisosRol.DataSource = permisos;
            LBPermisosRol.DisplayMember = "Nombre";
        }

        private void LBRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComponentesRol_MB29();
        }

        private void btnEliminarFamR_Click(object sender, EventArgs e)
        {
            if (LBRoles.SelectedItem == null || LBFamiliasRol.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un rol y una familia del rol.");
                return;
            }

            try
            {
                var rol = (Rol_MB29)LBRoles.SelectedItem;
                var familia = (Familia_MB29)LBFamiliasRol.SelectedItem;
                rolBLL.EliminarFamilia_MB29(rol, familia);
                CargarComponentesRol_MB29();
                MessageBox.Show("Familia eliminada del rol correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar familia: " + ex.Message);
            }
        }

        private void btnEliminarPermR_Click(object sender, EventArgs e)
        {
            if (LBRoles.SelectedItem == null || LBPermisosRol.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un rol y un permiso del rol.");
                return;
            }

            try
            {
                var rol = (Rol_MB29)LBRoles.SelectedItem;
                var permiso = (Permiso_MB29)LBPermisosRol.SelectedItem;
                rolBLL.EliminarPermiso_MB29(rol, permiso);
                CargarComponentesRol_MB29();
                MessageBox.Show("Permiso eliminado del rol correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar permiso: " + ex.Message);
            }
        }


        public void actualizar_MB29(string idioma)
        {
            var g = Gestoridioma_MB29.Instancia_MB29;
            label1.Text = g.Traducir_MB29("roles");
            label2.Text = g.Traducir_MB29("familias");
            label3.Text = g.Traducir_MB29("permisos");
            label4.Text = g.Traducir_MB29("familias_del_rol");
            label5.Text = g.Traducir_MB29("permisos_del_rol");
            btnCrear.Text = g.Traducir_MB29("crear");
            btnEliminar.Text = g.Traducir_MB29("eliminar");
            btnAgregarFam.Text = g.Traducir_MB29("agregar_familia");
            btnAgregarPerm.Text = g.Traducir_MB29("agregar_permiso");
            btnEliminarFamR.Text = g.Traducir_MB29("eliminar_familia");
            btnEliminarPermR.Text = g.Traducir_MB29("eliminar_permiso");
            label6.Text = g.Traducir_MB29("permisosrol_selec");
            btnSalir.Text = g.Traducir_MB29("salir");






        }

        private void FormRoles_MB29_Load(object sender, EventArgs e)
        {

        }
    }
}
