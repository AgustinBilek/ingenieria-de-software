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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
namespace IngenieriaSoftware
{
    public partial class formregistropaquete: Form
    {

        

     

        private readonly EnvioBLL_MB29 _envioBLL = new EnvioBLL_MB29();

        public Paquete_MB29 PaqueteRegistrado { get; private set; }

        // Cantidad máxima de objetos según el tipo de paquete
        private readonly Dictionary<string, int> _maxObjetos = new Dictionary<string, int>
{
    { "Documento", 0 },      // estándar, no lleva objetos
    { "Caja pequeña", 2 },
    { "Caja mediana", 3 },
    { "Caja grande", 4 },
    { "Frágil", 1 }
};
        public formregistropaquete()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Faltan datos necesarios para registrar el paquete.",
                    "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tipo = comboBox1.SelectedItem.ToString();
            int max = _maxObjetos[tipo];
            string contenido;

            if (max == 0)
            {
                contenido = "Documento estándar";
            }
            else
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Debe agregar al menos un objeto al paquete.",
                        "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var objetos = new List<string>();
                foreach (DataGridViewRow fila in dataGridView1.Rows)
                    objetos.Add(fila.Cells[0].Value.ToString());

                contenido = string.Join("; ", objetos);
            }

            try
            {
                string usuario = SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29;

                PaqueteRegistrado = _envioBLL.RegistrarPaquete_MB29(usuario, tipo, contenido);

                MessageBox.Show("El paquete fue registrado correctamente.\nID: " + PaqueteRegistrado.IdPaquete_MB29,
                    "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Deja el form listo para otro paquete
                comboBox1.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ActualizarControles()
        {
            if (comboBox1.SelectedIndex < 0)
            {
                label1.Text = "Seleccione un tipo de paquete";
                return;
            }

            string tipo = comboBox1.SelectedItem.ToString();
            int max = _maxObjetos.ContainsKey(tipo) ? _maxObjetos[tipo] : 0;

            label1.Text = max > 0
                ? $"Objetos: {dataGridView1.Rows.Count} de {max}"
                : "Documento estándar: no requiere agregar objetos";
        }

        private void formregistropaquete_Load(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            textBox1.Enabled = true;
            textBox1.ReadOnly = false;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            if (!SessionManager_MB29.Instancia_MB29.HaySesion())
            {
                MessageBox.Show("Debe iniciar sesión para registrar un paquete.");
                Close();
                return;
            }

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Items.Clear();
            foreach (var tipo in _maxObjetos.Keys)
                comboBox1.Items.Add(tipo);
            comboBox1.SelectedIndex = -1;

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Objeto", "Objeto");

            textBox1.MaxLength = 50;
            comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            ActualizarControles();
        }
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Primero seleccione un tipo de paquete.", "Datos incompletos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tipoSel = comboBox1.SelectedItem.ToString();
            int max = _maxObjetos.ContainsKey(tipoSel) ? _maxObjetos[tipoSel] : 0;

            if (max == 0)
            {
                MessageBox.Show("Este tipo de paquete no admite objetos.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Ingrese el nombre del objeto.", "Datos incompletos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dataGridView1.Rows.Count >= max)
            {
                MessageBox.Show($"Este tipo de paquete admite como máximo {max} objeto(s).",
                    "Límite alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dataGridView1.Rows.Add(textBox1.Text.Trim());
            textBox1.Clear();
            textBox1.Focus();
            ActualizarControles();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            ActualizarControles();
        }
    }
}
