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
    public partial class formasignarenvio: Form
    {
        private readonly DistribucionBLL_MB29 _distribucionBLL = new DistribucionBLL_MB29();
        private bool _cargando = false;

        public formasignarenvio()
        {
            InitializeComponent();
        }

        private void formasignarenvio_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            CargarEnvios();
        }

        // Pasos 1 y 2 del CU. FA01: si no hay envíos listos, informa y finaliza.
        private void CargarEnvios()
        {
            _cargando = true;

            var envios = _distribucionBLL.ObtenerEnviosListosParaDespacho_AB29();

            if (envios.Count == 0)
            {
                _cargando = false;
                MessageBox.Show("No hay envíos listos para despacho.", "Sin envíos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
                return;
            }

            comboBox1.DataSource = null;
            comboBox2.DataSource = null;

            comboBox1.DisplayMember = "Descripcion_MB29";
            comboBox1.ValueMember = "IdEnvio_MB29";
            comboBox1.DataSource = envios;
            comboBox1.SelectedIndex = -1;

            _cargando = false;
        }

        // Pasos 3 a 6 del CU. FA02: si no hay repartidores disponibles, informa y
        // el envío conserva su estado.
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargando) return;

            comboBox2.DataSource = null;
            if (comboBox1.SelectedIndex < 0) return;

            var repartidores = _distribucionBLL.ObtenerRepartidoresDisponibles_AB29();

            if (repartidores.Count == 0)
            {
                MessageBox.Show("No hay repartidores disponibles. No se puede asignar el envío.",
                    "Sin repartidores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox1.SelectedIndex = -1;
                return;
            }

            _cargando = true;
            comboBox2.DisplayMember = "NombreCompleto_AB29";
            comboBox2.ValueMember = "IdPersona_AB29";
            comboBox2.DataSource = repartidores;
            comboBox2.SelectedIndex = -1;
            _cargando = false;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // No hace falta hacer nada al elegir el repartidor.
        }

        // Pasos 7 a 10 del CU: botón Asignar
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un envío.", "Datos incompletos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox2.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un repartidor disponible.", "Datos incompletos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmar = MessageBox.Show("¿Confirma la asignación del envío al repartidor seleccionado?",
                "Confirmar asignación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar != DialogResult.Yes) return;

            try
            {
                int idEnvio = (int)comboBox1.SelectedValue;
                int idRepartidor = (int)comboBox2.SelectedValue;
                string usuario = SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29;

                _distribucionBLL.AsignarEnvioARepartidor_MB29(usuario, idEnvio, idRepartidor);

                MessageBox.Show("La asignación fue registrada correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarEnvios();
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

