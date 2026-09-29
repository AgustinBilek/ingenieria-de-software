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
using BLL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Servicio_MB29;

namespace IngenieriaSoftware
{
    public partial class formreporte: Form, IObserverIdioma_MB29
    {
        public formreporte()
        {
            InitializeComponent();

            Gestoridioma_MB29.Instancia_MB29.Agregar_MB29(this);
            actualizar_MB29(Gestoridioma_MB29.Instancia_MB29.IdiomaActual_MB29);

        }
        private readonly EnvioBLL_MB29 _envioBLL = new EnvioBLL_MB29();



        private void CargarReporte_AB29()
        {
            dgvReporte.DataSource = null;
            dgvReporte.DataSource = _envioBLL.ObtenerReporteRecepcionPaquetes_AB29();

            if (dgvReporte.Columns["IdEnvio_AB29"] != null) dgvReporte.Columns["IdEnvio_AB29"].HeaderText = "ID Envío";
            if (dgvReporte.Columns["DNIRemitente_AB29"] != null) dgvReporte.Columns["DNIRemitente_AB29"].HeaderText = "DNI Remitente";
            if (dgvReporte.Columns["NombreRemitente_AB29"] != null) dgvReporte.Columns["NombreRemitente_AB29"].HeaderText = "Nombre Remitente";
            if (dgvReporte.Columns["ApellidoRemitente_AB29"] != null) dgvReporte.Columns["ApellidoRemitente_AB29"].HeaderText = "Apellido Remitente";
            if (dgvReporte.Columns["NombreDestinatario_AB29"] != null) dgvReporte.Columns["NombreDestinatario_AB29"].HeaderText = "Destinatario";
            if (dgvReporte.Columns["TipoPaquete_AB29"] != null) dgvReporte.Columns["TipoPaquete_AB29"].HeaderText = "Tipo Paquete";
            if (dgvReporte.Columns["Contenido_AB29"] != null) dgvReporte.Columns["Contenido_AB29"].HeaderText = "Contenido";
            if (dgvReporte.Columns["Prioridad_AB29"] != null) dgvReporte.Columns["Prioridad_AB29"].HeaderText = "Prioridad";
            if (dgvReporte.Columns["EstadoEnvio_AB29"] != null) dgvReporte.Columns["EstadoEnvio_AB29"].HeaderText = "Estado";
            if (dgvReporte.Columns["FechaIngreso_AB29"] != null) dgvReporte.Columns["FechaIngreso_AB29"].HeaderText = "Fecha Ingreso";
            if (dgvReporte.Columns["HoraIngreso_AB29"] != null) dgvReporte.Columns["HoraIngreso_AB29"].HeaderText = "Hora Ingreso";
            if (dgvReporte.Columns["TipoPago_AB29"] != null) dgvReporte.Columns["TipoPago_AB29"].HeaderText = "Tipo Pago";
            if (dgvReporte.Columns["MontoTotal_AB29"] != null) dgvReporte.Columns["MontoTotal_AB29"].HeaderText = "Monto";
        }

        public void actualizar_MB29(string idioma)
        {
            var g =Gestoridioma_MB29.Instancia_MB29;
            BtnExportarPDF.Text = g.Traducir_MB29("btnExportarPDF");
            BtnActualizar.Text = g.Traducir_MB29("btnActualizar");
            BtnSalir.Text = g.Traducir_MB29("btnSalir");

        }



        private void ExportarPDF_AB29(string archivo)
        {
            Document doc = new Document(PageSize.A4.Rotate()); // horizontal, hay muchas columnas
            PdfWriter.GetInstance(doc, new FileStream(archivo, FileMode.Create));
            doc.Open();

            doc.Add(new Paragraph("Reporte A01 - Recepción de paquetes"));
            doc.Add(new Paragraph("Generado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm")));
            doc.Add(new Paragraph(" "));

            int columnasVisibles = dgvReporte.Columns.Cast<System.Windows.Forms.DataGridViewColumn>()
                .Count(c => c.Visible);

            PdfPTable tabla = new PdfPTable(columnasVisibles);
            tabla.WidthPercentage = 100;
            tabla.SpacingBefore = 10f;

            foreach (System.Windows.Forms.DataGridViewColumn columna in dgvReporte.Columns)
            {
                if (columna.Visible)
                    tabla.AddCell(new Phrase(columna.HeaderText));
            }

            foreach (System.Windows.Forms.DataGridViewRow fila in dgvReporte.Rows)
            {
                if (fila.IsNewRow) continue;

                foreach (System.Windows.Forms.DataGridViewCell celda in fila.Cells)
                {
                    if (dgvReporte.Columns[celda.ColumnIndex].Visible)
                        tabla.AddCell(new Phrase(celda.Value?.ToString() ?? ""));
                }
            }

            doc.Add(tabla);
            doc.Close();
        }
        private void formreporte_Load(object sender, EventArgs e)
        {
            CargarReporte_AB29();
        }

        private void BtnExportarPDF_Click(object sender, EventArgs e)
        {
            if (dgvReporte.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Sin datos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var dialogo = new SaveFileDialog())
            {
                dialogo.Filter = "Archivo PDF (*.pdf)|*.pdf";
                dialogo.DefaultExt = "pdf";
                dialogo.FileName = "ReporteRecepcionPaquetes_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";
                dialogo.Title = "Guardar reporte PDF";

                if (dialogo.ShowDialog() != DialogResult.OK) return;

                try
                {
                    ExportarPDF_AB29(dialogo.FileName);
                    MessageBox.Show("El reporte fue generado correctamente.\n" + dialogo.FileName,
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al generar el PDF: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            CargarReporte_AB29();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvReporte_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
