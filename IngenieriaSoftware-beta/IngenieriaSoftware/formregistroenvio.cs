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
    public partial class formregistroenvio: Form, IObserverIdioma_MB29
    {



        public formregistroenvio()
        {
            InitializeComponent();
            Gestoridioma_MB29.Instancia_MB29.Agregar_MB29(this);
        }
        private readonly EnvioBLL_MB29 _envioBLL = new EnvioBLL_MB29();
        private int _idPaquete;

        private bool _remitenteExiste;
        private bool _destinatarioExiste;

        public Envio_MB29 EnvioRegistrado { get; private set; }

        private string T(string clave)
        {
            return Gestoridioma_MB29.Instancia_MB29.Traducir_MB29(clave);
        }





        public void actualizar_MB29(string idioma)
        {
            label1.Text = T("envio_lbl_dni");
            label2.Text = T("envio_lbl_nombre");
            label3.Text = T("envio_lbl_apellido");
            label4.Text = T("envio_lbl_direccion");
            label5.Text = T("envio_lbl_telefono");
            label6.Text = T("envio_lbl_email");
            label7.Text = T("envio_lbl_estado");
            label9.Text = T("envio_lbl_dni");
            label13.Text = T("envio_lbl_nombre");
            label12.Text = T("envio_lbl_direccion");
            label11.Text = T("envio_lbl_telefono");
            label10.Text = T("envio_lbl_estado");

            button1.Text = T("envio_btn_registrar");
            button2.Text = T("envio_btn_cancelar");
            button3.Text = T("envio_btn_buscar_remitente");
            button4.Text = T("envio_btn_buscar_destinatario");

            label14.Text = _idPaquete > 0
                ? string.Format(T("envio_paquete_numero"), _idPaquete)
                : T("envio_paquete_seleccione");
        }

        // Se llama después de construir el form: fEnvio.CargarPaquete(idPaquete);
        public void CargarPaquete(int idPaquete)
        {
            _idPaquete = idPaquete;
            label14.Text = string.Format(T("envio_paquete_numero"), idPaquete);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (!long.TryParse(textBox1.Text, out long dni))
            {
                MessageBox.Show(T("envio_msg_dni_invalido"), T("envio_titulo_incompleto"),
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
                textBox7.Text = T("envio_estado_registrado");
                HabilitarCamposRemitente(false);
            }
            else
            {
                _remitenteExiste = false;
                textBox2.Clear(); textBox3.Clear(); textBox4.Clear();
                textBox5.Clear(); textBox6.Clear();
                textBox7.Text = T("envio_estado_no_encontrado");
                HabilitarCamposRemitente(true);
                textBox2.Focus();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!long.TryParse(textBox9.Text, out long dni))
            {
                MessageBox.Show(T("envio_msg_dni_invalido"), T("envio_titulo_incompleto"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (long.TryParse(textBox1.Text, out long dniRem) && dniRem == dni)
            {
                MessageBox.Show(T("envio_msg_dni_duplicado_dest"), T("envio_titulo_dni_duplicado"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var destinatario = _envioBLL.BuscarDestinatarioPorDNI_MB29(dni);

            if (destinatario != null)
            {
                _destinatarioExiste = true;
                textBox13.Text = destinatario.Nombre_MB29;
                textBox12.Text = destinatario.Direccion_MB29;
                textBox11.Text = destinatario.Telefono_MB29;
                textBox10.Text = T("envio_estado_registrado");
                HabilitarCamposDestinatario(false);
            }
            else
            {
                _destinatarioExiste = false;
                textBox13.Clear(); textBox12.Clear(); textBox11.Clear();
                textBox10.Text = T("envio_estado_no_encontrado");
                HabilitarCamposDestinatario(true);
                textBox13.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_idPaquete <= 0)
            {
                MessageBox.Show(T("envio_msg_sin_paquete"), T("envio_titulo_incompleto"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!long.TryParse(textBox1.Text, out long dniRem) ||
                !long.TryParse(textBox9.Text, out long dniDest))
            {
                MessageBox.Show(T("envio_msg_sin_busqueda"), T("envio_titulo_incompleto"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dniRem == dniDest)
            {
                MessageBox.Show(T("envio_msg_dni_duplicado"), T("envio_titulo_dni_duplicado"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show(T("envio_msg_remitente_incompleto"), T("envio_titulo_incompleto"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox13.Text) || string.IsNullOrWhiteSpace(textBox12.Text) ||
                string.IsNullOrWhiteSpace(textBox11.Text))
            {
                MessageBox.Show(T("envio_msg_destinatario_incompleto"), T("envio_titulo_incompleto"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                MessageBox.Show(string.Format(T("envio_msg_ok"), EnvioRegistrado.IdEnvio_MB29),
                    T("envio_titulo_ok"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, T("envio_titulo_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            label14.Text = T("envio_paquete_seleccione");
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

            actualizar_MB29(Gestoridioma_MB29.Instancia_MB29.IdiomaActual_MB29);
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
                label14.Text = T("envio_paquete_seleccione");
                return;
            }

            _idPaquete = (int)comboBox1.SelectedValue;
            label14.Text = string.Format(T("envio_paquete_numero"), _idPaquete);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Gestoridioma_MB29.Instancia_MB29.Eliminar_MB29(this);
            base.OnFormClosed(e);
        }
    }
    
}
