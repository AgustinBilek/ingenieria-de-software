using BLL;
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
using System.Xml.Serialization;

namespace IngenieriaSoftware
{
    public partial class Formserializar: Form,IObserverIdioma_MB29
    {
        private readonly EnvioBLL_AB29 _envioBLL = new EnvioBLL_AB29();
        public Formserializar()
        {
            InitializeComponent();
            Gestoridioma_MB29.Instancia_MB29.Agregar_MB29(this);
            actualizar_MB29(Gestoridioma_MB29.Instancia_MB29.IdiomaActual_MB29);
        }
        private void CargarGrid_AB29()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _envioBLL.ObtenerClientes_AB29();
        }
        private void Formserializar_Load(object sender, EventArgs e)
        {
            CargarGrid_AB29();
        }
        public void actualizar_MB29(string idioma)
        {
            var g = Gestoridioma_MB29.Instancia_MB29;
            button1.Text = g.Traducir_MB29("btnserializar");
            button2.Text = g.Traducir_MB29("btndeserializar");
            button3.Text = g.Traducir_MB29("btnSalir");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var enPantalla = dataGridView1.DataSource as List<Clientebackup_AB29>;
            if (enPantalla == null || enPantalla.Count == 0)
            {
                MessageBox.Show("No hay datos para serializar.", "Sin datos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var dialogo = new SaveFileDialog())
            {
                dialogo.Filter = "Archivo XML (*.xml)|*.xml";
                dialogo.DefaultExt = "xml";
                dialogo.FileName = "Clientes.xml";
                dialogo.Title = "Guardar archivo XML";

                if (dialogo.ShowDialog() != DialogResult.OK) return;

                try
                {
                    var serializador = new XmlSerializer(typeof(List<Clientebackup_AB29>));
                    using (var stream = new FileStream(dialogo.FileName, FileMode.Create))
                    {
                        serializador.Serialize(stream, enPantalla);
                    }

                    MessageBox.Show("El archivo XML fue generado correctamente.\n" + dialogo.FileName,
                        "Serialización exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al serializar: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var dialogo = new OpenFileDialog())
            {
                dialogo.Filter = "Archivo XML (*.xml)|*.xml";
                dialogo.Title = "Seleccionar archivo XML";

                if (dialogo.ShowDialog() != DialogResult.OK) return;

                try
                {
                    var serializador = new XmlSerializer(typeof(List<Clientebackup_AB29>));
                    List<Clientebackup_AB29> lista;

                    using (var stream = new FileStream(dialogo.FileName, FileMode.Open))
                    {
                        lista = (List<Clientebackup_AB29>)serializador.Deserialize(stream);
                    }

                    dataGridView1.DataSource = null;
                   dataGridView1.DataSource = lista;

                    MessageBox.Show("El archivo XML fue cargado correctamente en la grilla.",
                        "Deserialización exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al deserializar: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
