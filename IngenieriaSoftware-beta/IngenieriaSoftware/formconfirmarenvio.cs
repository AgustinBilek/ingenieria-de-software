using BLL;
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
    public partial class formconfirmarenvio: Form, IObserverIdioma_MB29
    {
        public formconfirmarenvio()
        {
            InitializeComponent();
            Gestoridioma_MB29.Instancia_MB29.Agregar_MB29(this);
            actualizar_MB29(Gestoridioma_MB29.Instancia_MB29.IdiomaActual_MB29);
        }
        private readonly EnvioBLL_MB29 _envioBLL = new EnvioBLL_MB29();
        private int _idEnvio;
        private bool _condicionesGuardadas_AB29;
        private bool _pagoRegistrado_AB29;



        public void actualizar_MB29(string idioma)
        {
            var g = Gestoridioma_MB29.Instancia_MB29;
            label1.Text = g.Traducir_MB29("lblcondiciones");
            label2.Text = g.Traducir_MB29("lblprioridad");
            label3.Text = g.Traducir_MB29("lbltipoenvio");
            label4.Text = g.Traducir_MB29("lblpago");
            label5.Text = g.Traducir_MB29("lbltipopago");
            label6.Text = g.Traducir_MB29("lblmonto");
            codigoLBL.Text = g.Traducir_MB29("lblcodigo");
            label8.Text = g.Traducir_MB29("lblEnvio");
            confirmarBTN.Text = g.Traducir_MB29("btnConfirmar");
            guardarCondicionesBTN.Text = g.Traducir_MB29("btnGuardarCondiciones");
            registrarPagoBTN.Text = g.Traducir_MB29("btnRegistrarPago");
            salirBTN.Text = g.Traducir_MB29("btnSalir");

        }


        private void CargarEnviosPendientes_AB29()
        {
            var pendientes = _envioBLL.ObtenerEnviosPendientesConfirmacion_AB29();

            envioCB.DisplayMember = "Descripcion_MB29";
            envioCB.ValueMember = "IdEnvio_MB29";
            envioCB.DataSource = pendientes;
            envioCB.SelectedIndex = -1;
        }

        private void formconfirmarenvio_Load(object sender, EventArgs e)
        {
            CargarEnviosPendientes_AB29();

            prioridadCB.DropDownStyle = ComboBoxStyle.DropDownList;
            prioridadCB.Items.Clear();
            prioridadCB.Items.AddRange(new object[] { "Alta", "Media", "Baja" });

            tipoEnvioCB.DropDownStyle = ComboBoxStyle.DropDownList;
            tipoEnvioCB.Items.Clear();
            tipoEnvioCB.Items.AddRange(new object[] { "Normal", "Express" });

            tipoPagoCB.DropDownStyle = ComboBoxStyle.DropDownList;
            tipoPagoCB.Items.Clear();
            tipoPagoCB.Items.AddRange(new object[] { "Efectivo", "Tarjeta", "Transferencia" });

            codigoLBL.Text = "";

        }

        private void envioCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            _idEnvio = envioCB.SelectedIndex >= 0 ? (int)envioCB.SelectedValue : 0;

            _condicionesGuardadas_AB29 = false;
            _pagoRegistrado_AB29 = false;
            codigoLBL.Text = "";

            prioridadCB.SelectedIndex = -1;
            tipoEnvioCB.SelectedIndex = -1;
            tipoPagoCB.SelectedIndex = -1;
            montoTB.Clear();
        }

        private void prioridadCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tipoEnvioCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tipoPagoCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void montoTB_TextChanged(object sender, EventArgs e)
        {

        }

        private void guardarCondicionesBTN_Click(object sender, EventArgs e)
        {
            if (_idEnvio <= 0)
            {
                MessageBox.Show("Seleccione un envío.", "Datos incompletos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (prioridadCB.SelectedIndex < 0 || tipoEnvioCB.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione la prioridad y el tipo de envío.",
                    "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string usuario = SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29;
                _envioBLL.RegistrarCondiciones_MB29(usuario, _idEnvio,
                    prioridadCB.SelectedItem.ToString(), tipoEnvioCB.SelectedItem.ToString());

                _condicionesGuardadas_AB29 = true;
                MessageBox.Show("Las condiciones fueron registradas correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void registrarPagoBTN_Click(object sender, EventArgs e)
        {
            if (!_condicionesGuardadas_AB29)
            {
                MessageBox.Show("Primero debe registrar las condiciones del envío.",
                    "Orden incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (tipoPagoCB.SelectedIndex < 0 ||
                !decimal.TryParse(montoTB.Text, out decimal monto) || monto <= 0)
            {
                MessageBox.Show("Ingrese un tipo de pago y un monto válido mayor a cero.",
                    "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string usuario = SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29;
                _envioBLL.RegistrarPago_MB29(usuario, _idEnvio, tipoPagoCB.SelectedItem.ToString(), monto);

                _pagoRegistrado_AB29 = true;
                MessageBox.Show("El pago fue registrado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void confirmarBTN_Click(object sender, EventArgs e)
        {
            if (!_condicionesGuardadas_AB29 || !_pagoRegistrado_AB29)
            {
                MessageBox.Show("Debe registrar las condiciones y el pago antes de confirmar.",
                    "Orden incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string usuario = SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29;
                var envio = _envioBLL.ConfirmarEnvio_MB29(usuario, _idEnvio);

                codigoLBL.Text = "Código: " + envio.CodigoSeguimiento_MB29 + "-" + envio.DigitoVerificador_MB29;

                MessageBox.Show("El envío fue confirmado y quedó listo para despacho.\n" + codigoLBL.Text,
                    "Envío confirmado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarEnviosPendientes_AB29(); // este envío ya no vuelve a aparecer
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void salirBTN_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void codigoLBL_Click(object sender, EventArgs e)
        {

        }
    }
}
