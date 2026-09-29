using Servicio_MB29;
using BLL_MB29;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace IngenieriaSoftware
{
    public partial class FormBitacora_MB29: Form, IObserverIdioma_MB29, IObserverSesion_MB29
    {
        public FormBitacora_MB29()
        {
            InitializeComponent();
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
            SessionManager_MB29.Instancia_MB29.EliminarObserverSesion_MB29(this);
            base.OnFormClosed(e);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            this.Close();
        }

        private void FormBitacora_MB29_Load(object sender, EventArgs e)
        {
            CBModulo.Items.Add("");
            CBModulo.Items.Add("Seguridad");
            CBModulo.Items.Add("Usuarios");
            CBModulo.Items.Add("Bitacora");
            CBModulo.SelectedIndex = 0;

            CargarDGV_MB29();
        }

        private void CargarDGV_MB29()
        {
            dataGridView1.DataSource = null;
            List<BitacoraServicio_MB29> bitacora = BitacoraBLL_MB29.instancia.CargarBitacora_MB29();

            var ultimos3Dias = bitacora
                .Where(b => b.fecha_MB29 >= DateTime.Today.AddDays(-3))
                .ToList();

            dataGridView1.DataSource = ultimos3Dias;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            DataGridViewRow fila = dataGridView1.SelectedRows[0];
            var usuarioLogin = fila.Cells["usuario_MB29"].Value?.ToString();
            var user = UsuarioBLL_MB29.Instancia.ObtenerUsuarioPorNombre_MB29(usuarioLogin);

            NombreTxt.Text = user?.Nombre_MB29 ?? "(usuario eliminado)";
            ApellidoTxt.Text = user?.Apellido_MB29 ?? "";
            CBModulo.SelectedItem = fila.Cells["modulo_MB29"].Value?.ToString();
            LoginTxt.Text = usuarioLogin;
            CBAccion.SelectedItem = fila.Cells["accion_MB29"].Value?.ToString();
            CriticidadTxt.Text = fila.Cells["Criticidad_MB29"].Value?.ToString();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            NombreTxt.Clear();
            ApellidoTxt.Clear();
            CBModulo.SelectedIndex = 0;
            CBAccion.Items.Clear();
            CBAccion.Items.Add("");
            CBAccion.SelectedIndex = 0;
            LoginTxt.Clear();
            CriticidadTxt.Clear();
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFinal.Value = DateTime.Now;

            CargarDGV_MB29();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            var resultado = BitacoraBLL_MB29.instancia.CargarBitacora_MB29();

            if (!string.IsNullOrWhiteSpace(NombreTxt.Text))
                resultado = resultado.Where(b => {
                    var user = UsuarioBLL_MB29.Instancia.ObtenerUsuarioPorNombre_MB29(b.usuario_MB29);
                    return user != null && user.Nombre_MB29.ToLower().Contains(NombreTxt.Text.ToLower());
                }).ToList();

            if (!string.IsNullOrWhiteSpace(ApellidoTxt.Text))
                resultado = resultado.Where(b => {
                    var user = UsuarioBLL_MB29.Instancia.ObtenerUsuarioPorNombre_MB29(b.usuario_MB29);
                    return user != null && user.Apellido_MB29.ToLower().Contains(ApellidoTxt.Text.ToLower());
                }).ToList();

            if (!string.IsNullOrWhiteSpace(LoginTxt.Text))
                resultado = resultado.Where(b => b.usuario_MB29.ToLower().Contains(LoginTxt.Text.ToLower())).ToList();

            string moduloFiltro = CBModulo.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(moduloFiltro))
                resultado = resultado.Where(b => b.modulo_MB29 == moduloFiltro).ToList();

            string eventoFiltro = CBAccion.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(eventoFiltro))
                resultado = resultado.Where(b => b.accion_MB29 == eventoFiltro).ToList();

            if (!string.IsNullOrWhiteSpace(CriticidadTxt.Text) && int.TryParse(CriticidadTxt.Text, out int crit))
                resultado = resultado.Where(b => b.Criticidad_MB29 == crit).ToList();

            if (dtpFechaInicio.Checked)
                resultado = resultado.Where(b => b.fecha_MB29 >= dtpFechaInicio.Value.Date).ToList();

            if (dtpFechaFinal.Checked)
                resultado = resultado.Where(b => b.fecha_MB29 <= dtpFechaFinal.Value.Date.AddDays(1)).ToList();

            dataGridView1.DataSource = resultado;
        }

        private void ExportarPDF_MB29()
        {
            string carpeta = Path.Combine(Application.StartupPath, "pdf");
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            //Nombre del archivo con fecha y hora para que no se pisen
            string archivo = Path.Combine(carpeta, "reporte_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf");

            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(archivo, FileMode.Create));
            doc.Open();

            doc.Add(new Paragraph("Reporte"));
            doc.Add(new Paragraph(" "));

            int columnasVisibles = dataGridView1.Columns
            .Cast<DataGridViewColumn>()
            .Count(c => c.Visible);

            PdfPTable tabla = new PdfPTable(columnasVisibles);
            tabla.WidthPercentage = 110;
            tabla.SpacingBefore = 12f;
            tabla.SpacingAfter = 12f;

            //encabezados
            foreach (DataGridViewColumn columna in dataGridView1.Columns)
            {
                if (columna.Visible)
                {
                    tabla.AddCell(new Phrase(columna.HeaderText));
                }
            }

            //filas
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                if (!fila.IsNewRow)
                {
                    foreach (DataGridViewCell celda in fila.Cells)
                    {
                        if (dataGridView1.Columns[celda.ColumnIndex].Visible)
                        {
                            tabla.AddCell(new Phrase(celda.Value?.ToString() ?? ""));
                        }
                    }
                }
            }

            BitacoraBLL_MB29.instancia.Registrar_MB29(
                SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29,
                "Exportar PDF",
                "Bitacora",
                $"Usuario {SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29} exportó a PDF la bitácora",
                criticidad: 1
            );

            doc.Add(tabla);
            doc.Close();

            MessageBox.Show("PDF guardado en: " + archivo);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ExportarPDF_MB29();
        }

        public void actualizar_MB29(string idioma)
        {
            var g = Gestoridioma_MB29.Instancia_MB29;
            this.Text = g.Traducir_MB29("bitacora_titulo");
            label1.Text = g.Traducir_MB29("bitacora_lbl_nombre");
            label2.Text = g.Traducir_MB29("bitacora_lbl_apellido");
            label3.Text = g.Traducir_MB29("bitacora_lbl_login");
            label4.Text = g.Traducir_MB29("bitacora_lbl_fecha_inicio");
            label5.Text = g.Traducir_MB29("bitacora_lbl_fecha_salida");
            label6.Text = g.Traducir_MB29("bitacora_lbl_modulo");
            label8.Text = g.Traducir_MB29("bitacora_lbl_criticidad");
            label9.Text = g.Traducir_MB29("bitacora_lbl_accion");
            btnAplicar.Text = g.Traducir_MB29("bitacora_btn_aplicar");
            btnLimpiar.Text = g.Traducir_MB29("bitacora_btn_limpiar");
            btnImprimir.Text = g.Traducir_MB29("bitacora_btn_imprimir");
            button4.Text = g.Traducir_MB29("bitacora_btn_salir");
        }

        private void CBModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CBAccion.Items.Clear();
            CBAccion.Items.Add("");

            if (CBModulo.SelectedItem?.ToString() == "Seguridad")
            {
                CBAccion.Items.Add("Login OK");
                CBAccion.Items.Add("Login Fallido");
                CBAccion.Items.Add("Login Bloqueado");
                CBAccion.Items.Add("Logout");
                CBAccion.Items.Add("Cambiar Contraseña");
                CBAccion.Items.Add("Desbloqueo");
            }
            else if (CBModulo.SelectedItem?.ToString() == "Usuarios")
            {
                CBAccion.Items.Add("Alta Usuario");
                CBAccion.Items.Add("Modificar Usuario");
                CBAccion.Items.Add("Deshabilitar Usuario");
                CBAccion.Items.Add("Desbloquear Usuario");
            }
            else if (CBModulo.SelectedItem?.ToString() == "Bitacora")
            {
                CBAccion.Items.Add("Exportar PDF");
            }

            CBAccion.SelectedIndex = 0;
        }
    }
}
