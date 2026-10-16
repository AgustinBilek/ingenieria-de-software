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
    public partial class Formcargarpaquete: Form
    {
        private readonly DistribucionBLL_MB29 _distribucionBLL = new DistribucionBLL_MB29();

        public Formcargarpaquete()
        {
            InitializeComponent();
        }

        // Tu clase no tenía evento Load, así que la carga inicial va acá.
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            CargarDatos();
        }

        // Pasos 1 a 3 del CU. FA01: si no hay envíos asignados, informa y finaliza.
        private void CargarDatos()
        {
            var envios = _distribucionBLL.ObtenerEnviosAsignados_AB29();

            if (envios.Count == 0)
            {
                MessageBox.Show("No hay envíos asignados pendientes de carga.", "Sin envíos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
                return;
            }

            var vehiculos = _distribucionBLL.ObtenerVehiculosDisponibles_AB29();

            if (vehiculos.Count == 0)
            {
                MessageBox.Show("No hay vehículos disponibles.", "Sin vehículos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
                return;
            }

            comboBox1.DataSource = null;
            comboBox1.DisplayMember = "Descripcion_MB29";
            comboBox1.ValueMember = "IdEnvio_MB29";
            comboBox1.DataSource = envios;
            comboBox1.SelectedIndex = -1;

            comboBox2.DataSource = null;
            comboBox2.DisplayMember = "Descripcion_AB29";
            comboBox2.ValueMember = "IdVehiculo_MB29";
            comboBox2.DataSource = vehiculos;
            comboBox2.SelectedIndex = -1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // No hace falta hacer nada al elegir el envío.
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // No hace falta hacer nada al elegir el vehículo.
        }

        // Pasos 4 a 10 del CU: botón Cargar
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0 || comboBox2.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione el envío y el vehículo.", "Datos incompletos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmar = MessageBox.Show("¿Confirma la carga del paquete en el vehículo seleccionado?",
                "Confirmar carga", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar != DialogResult.Yes) return;

            try
            {
                int idEnvio = (int)comboBox1.SelectedValue;
                int idVehiculo = (int)comboBox2.SelectedValue;
                var usuario = SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29;

                _distribucionBLL.CargarPaqueteEnVehiculo_MB29(usuario.Usuario_MB29, idEnvio, idVehiculo, usuario.IdPersona_MB29);

                MessageBox.Show("La carga fue registrada correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Botón Salir
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

