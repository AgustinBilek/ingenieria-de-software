using BLL;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Servicio_MB29;
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
    public partial class FormReporte2: Form, IObserverIdioma_MB29
    {
        public FormReporte2()
        {
            InitializeComponent();
            Gestoridioma_MB29.Instancia_MB29.Agregar_MB29(this);
            actualizar_MB29(Gestoridioma_MB29.Instancia_MB29.IdiomaActual_MB29);
        }
        private readonly DistribucionBLL_MB29 _distribucionBLL = new DistribucionBLL_MB29();
        private void FormReporte2_Load(object sender, EventArgs e)
        {
            CargarReporte_AB29();
        }




        private void CargarReporte_AB29()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _distribucionBLL.ObtenerReporteEnvioPaquetes_AB29();

            void Titulo(string columna, string texto)
            {
                if (dataGridView1.Columns[columna] != null) dataGridView1.Columns[columna].HeaderText = texto;
            }

            Titulo("IdEnvio_AB29", "ID Envío");
            Titulo("Repartidor_AB29", "Repartidor");
            Titulo("Destinatario_AB29", "Destinatario");
            Titulo("Direccion_AB29", "Dirección");
            Titulo("Vehiculo_AB29", "Vehículo");
            Titulo("EstadoEnvio_AB29", "Estado");
            Titulo("FechaAsignacion_AB29", "Asignación");
            Titulo("FechaCarga_AB29", "Carga");
            Titulo("FechaRecepcion_AB29", "Recepción");
            Titulo("FechaSalida_AB29", "Salida");
            Titulo("FechaEntrega_AB29", "Entrega");

            foreach (string c in new[] { "FechaAsignacion_AB29", "FechaCarga_AB29", "FechaRecepcion_AB29",
                                         "FechaSalida_AB29", "FechaEntrega_AB29" })
            {
                if (dataGridView1.Columns[c] != null)
                    dataGridView1.Columns[c].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        public void actualizar_MB29(string idioma)
        {
            var g = Gestoridioma_MB29.Instancia_MB29;
            button1.Text = g.Traducir_MB29("btnExportarPDF");
            button2.Text = g.Traducir_MB29("btnActualizar");
            button3.Text = g.Traducir_MB29("btnSalir");
        }

        private void ExportarPDF_AB29(string archivo)
        {
            Document doc = new Document(PageSize.A4.Rotate());
            PdfWriter.GetInstance(doc, new FileStream(archivo, FileMode.Create));
            doc.Open();

            doc.Add(new Paragraph("Reporte A02 - Envío de paquetes"));
            doc.Add(new Paragraph("Generado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm")));
            doc.Add(new Paragraph(" "));

            int columnasVisibles = dataGridView1.Columns.Cast<DataGridViewColumn>().Count(c => c.Visible);

            PdfPTable tabla = new PdfPTable(columnasVisibles);
            tabla.WidthPercentage = 100;
            tabla.SpacingBefore = 10f;

            foreach (DataGridViewColumn columna in dataGridView1.Columns)
            {
                if (columna.Visible)
                    tabla.AddCell(new Phrase(columna.HeaderText));
            }

            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                if (fila.IsNewRow) continue;

                foreach (DataGridViewCell celda in fila.Cells)
                {
                    if (dataGridView1.Columns[celda.ColumnIndex].Visible)
                        tabla.AddCell(new Phrase(celda.FormattedValue?.ToString() ?? ""));
                }
            }

            doc.Add(tabla);
            doc.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Sin datos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var dialogo = new SaveFileDialog())
            {
                dialogo.Filter = "Archivo PDF (*.pdf)|*.pdf";
                dialogo.DefaultExt = "pdf";
                dialogo.FileName = "ReporteEnvioPaquetes_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";
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

        private void button2_Click(object sender, EventArgs e)
        {
            CargarReporte_AB29();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
