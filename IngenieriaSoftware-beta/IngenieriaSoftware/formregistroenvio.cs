using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using Servicio_MB29;

namespace IngenieriaSoftware
{
    public partial class formregistroenvio: Form
    {



        public formregistroenvio()
        {
            InitializeComponent();
        }
        private readonly EnvioBLL_MB29 _envioBLL = new EnvioBLL_MB29();
        private int _idPaquete;

        private bool _remitenteExiste;
        private bool _destinatarioExiste;

        public Envio_MB29 EnvioRegistrado { get; private set; }

        // Se llama después de construir el form: fEnvio.CargarPaquete(idPaquete);
        public void CargarPaquete(int idPaquete)
        {
            _idPaquete = idPaquete;
            label14.Text = "Paquete #" + idPaquete;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (!long.TryParse(textBox1.Text, out long dni))
            {
                MessageBox.Show("Ingrese un DNI válido.", "Datos incompletos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var remitente = _envioBLL.BuscarRemitentePorDNI_MB29(dni);

            if (remitente != null)
            {
                _remitenteExiste = true;
                textBox2.Text = remitente.Nombre_MB29;
                textBox3.Text = remitente.Apellido_MB29;
                textBox4.Text = remitente.Direccion_MB29;
                textBox5.Text = remitente.Telefono_MB29;
                textBox6.Text = remitente.Email_MB29;
                textBox7.Text = "Registrado";
                HabilitarCamposRemitente(false);
            }
            else
            {
                _remitenteExiste = false;
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Text = "No encontrado - complete los datos";
                HabilitarCamposRemitente(true);
                textBox2.Focus();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!long.TryParse(textBox9.Text, out long dni))
            {
                MessageBox.Show("Ingrese un DNI válido.", "Datos incompletos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (long.TryParse(textBox1.Text, out long dniRem) && dniRem == dni)
            {
                MessageBox.Show("El destinatario no puede tener el mismo DNI que el remitente.",
                    "DNI duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            var destinatario = _envioBLL.BuscarDestinatarioPorDNI_MB29(dni);

            if (destinatario != null)
            {
                _destinatarioExiste = true;
                textBox13.Text = destinatario.Nombre_MB29;
                textBox12.Text = destinatario.Direccion_MB29;
                textBox11.Text = destinatario.Telefono_MB29;
                textBox10.Text = "Registrado";
                HabilitarCamposDestinatario(false);
            }
            else
            {
                _destinatarioExiste = false;
                textBox13.Clear();
                textBox12.Clear();
                textBox11.Clear();
                textBox10.Text = "No encontrado - complete los datos";
                HabilitarCamposDestinatario(true);
                textBox13.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_idPaquete <= 0)
            {
                MessageBox.Show("Debe seleccionar un paquete registrado.",
                    "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!long.TryParse(textBox1.Text, out long dniRem) ||
        !long.TryParse(textBox9.Text, out long dniDest))
            {
                MessageBox.Show("Debe buscar al remitente y al destinatario antes de continuar.",
                    "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dniRem == dniDest)
            {
                MessageBox.Show("El remitente y el destinatario no pueden tener el mismo DNI.",
                    "DNI duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Complete todos los datos del remitente.",
                    "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox13.Text) || string.IsNullOrWhiteSpace(textBox12.Text) ||
                string.IsNullOrWhiteSpace(textBox11.Text))
            {
                MessageBox.Show("Complete todos los datos del destinatario.",
                    "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string usuario = SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29;

                EnvioRegistrado = _envioBLL.RegistrarEnvio_MB29(
                    usuario, _idPaquete,
                    dniRem, textBox2.Text.Trim(), textBox3.Text.Trim(),
                    textBox4.Text.Trim(), textBox5.Text.Trim(), textBox6.Text.Trim(),
                    dniDest, textBox13.Text.Trim(), textBox12.Text.Trim(), textBox11.Text.Trim());

                MessageBox.Show(
                    "El envío fue registrado correctamente.\nID envío: " + EnvioRegistrado.IdEnvio_MB29,
                    "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void HabilitarCamposRemitente(bool habilitar)
        {
            textBox2.Enabled = habilitar; // nombre
            textBox3.Enabled = habilitar; // apellido
            textBox4.Enabled = habilitar; // direccion
            textBox5.Enabled = habilitar; // telefono
            textBox6.Enabled = habilitar; // email
        }

        private void HabilitarCamposDestinatario(bool habilitar)
        {
            textBox13.Enabled = habilitar; // nombre
            textBox12.Enabled = habilitar; // direccion
            textBox11.Enabled = habilitar; // telefono
        }
        private void CargarPaquetesPendientes()
        {
            var pendientes = _envioBLL.ObtenerPaquetesSinEnvio_MB29();

            comboBox1.DisplayMember = "Descripcion_MB29";
            comboBox1.ValueMember = "IdPaquete_MB29";
            comboBox1.DataSource = pendientes;
            comboBox1.SelectedIndex = -1;

            label14.Text = "Seleccione un paquete";
        }

        private void formregistroenvio_Load(object sender, EventArgs e)
        {
            CargarPaquetesPendientes();

            textBox7.Text = "";
            textBox10.Text = "";
            textBox7.ReadOnly = true;
            textBox10.ReadOnly = true;

            HabilitarCamposRemitente(false);
            HabilitarCamposDestinatario(false);
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0)
            {
                _idPaquete = 0;
                label14.Text = "Seleccione un paquete";
                return;
            }

            _idPaquete = (int)comboBox1.SelectedValue;
            label14.Text = "Paquete #" + _idPaquete;
        }
    }
}
