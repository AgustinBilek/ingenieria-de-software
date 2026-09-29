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
    public partial class FormBACKUPRESTORE : Form
    {
        private List<string> _irregularidades;

        public FormBACKUPRESTORE(List<string> irregularidades)
        {
            InitializeComponent();

            if (!SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Rol_MB29.TienePermiso_MB29(Permisos_MB29.GestionUsuarios))
            {
                MessageBox.Show("No tiene permiso para acceder a esta sección.");
                this.Load += (s, e) => this.Close();
                return;
            }

            _irregularidades = irregularidades;
        }

        private void FormBACKUPRECOVERY_Load(object sender, EventArgs e)
        {
            LblInconsistencias.Text = string.Join(Environment.NewLine, _irregularidades);
        }
    }
}
