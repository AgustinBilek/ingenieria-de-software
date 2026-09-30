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
    public partial class formregistropaquete: Form,IObserverIdioma_MB29
    {

        

     

        private readonly EnvioBLL_AB29 _envioBLL = new EnvioBLL_AB29();

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
            Gestoridioma_MB29.Instancia_MB29.Agregar_MB29(this);
        }

        public void actualizar_MB29(string idioma)
        {
            this.Text = T("paquete_titulo");
            label1.Text = ObtenerTextoCupo(); // se recalcula, no es fijo
            label2.Text = T("paquete_lbl_contenido");
            button1.Text = T("paquete_btn_registrar");
            button2.Text = T("paquete_btn_cancelar");
            button3.Text = T("paquete_btn_agregar");
            button4.Text = T("paquete_btn_eliminar");
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private string T(string clave)
        {
            return Gestoridioma_MB29.Instancia_MB29.Traducir_MB29(clave);
        }


        private string ObtenerTextoCupo()
        {
            if (comboBox1.SelectedIndex < 0)
                return T("paquete_cupo_seleccione");

            string tipo = comboBox1.SelectedItem.ToString();
            int max = _maxObjetos.ContainsKey(tipo) ? _maxObjetos[tipo] : 0;

            return max > 0
                ? string.Format(T("paquete_cupo_objetos"), dataGridView1.Rows.Count, max)
                : T("paquete_cupo_documento");
        }

        private void ActualizarControles()
        {
            label1.Text = ObtenerTextoCupo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show(T("paquete_msg_incompleto"),
                    T("paquete_titulo_incompleto"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show(T("paquete_msg_sin_objetos"),
                        T("paquete_titulo_incompleto"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                MessageBox.Show(T("paquete_msg_ok") + "\nID: " + PaqueteRegistrado.IdPaquete_MB29,
                    T("paquete_titulo_ok"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                comboBox1.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, T("paquete_titulo_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                MessageBox.Show(T("paquete_msg_sin_sesion"));
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

            actualizar_MB29(Gestoridioma_MB29.Instancia_MB29.IdiomaActual_MB29);
        }
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show(T ("paquete_msg_sin_tipo"), T("paquete_titulo_incompleto"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tipoSel = comboBox1.SelectedItem.ToString();
            int max = _maxObjetos.ContainsKey(tipoSel) ? _maxObjetos[tipoSel] : 0;

            if (max == 0)
            {
                MessageBox.Show(T("paquete_msg_sin_admite"), T("paquete_titulo_info"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show(T("paquete_msg_sin_nombre_objeto"), T("paquete_titulo_incompleto"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dataGridView1.Rows.Count >= max)
            {
                MessageBox.Show(string.Format(T("paquete_msg_limite"), max), T("paquete_titulo_limite"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
