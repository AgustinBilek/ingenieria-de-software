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
using Servicio_MB29;
namespace IngenieriaSoftware
{
    public partial class formbitacoracambios: Form,IObserverIdioma_MB29
    {
        public formbitacoracambios()
        {
            InitializeComponent();
            Gestoridioma_MB29.Instancia_MB29.Agregar_MB29(this);
            actualizar_MB29(Gestoridioma_MB29.Instancia_MB29.IdiomaActual_MB29);
        }
        private void CargarBitacora_AB29()
        {
            if (comboBox1.SelectedItem == null) return;

            string tabla = comboBox1.SelectedItem.ToString();
            var lista = bitacoracambiosBLL.instancia.CargarPorTabla_MB29(tabla);

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = lista;

            if (dataGridView1.Columns["IdCambio_MB29"] != null) dataGridView1.Columns["IdCambio_MB29"].HeaderText = "ID";
            if (dataGridView1.Columns["Usuario_MB29"] != null) dataGridView1.Columns["Usuario_MB29"].HeaderText = "Usuario";
            if (dataGridView1.Columns["Tabla_MB29"] != null) dataGridView1.Columns["Tabla_MB29"].HeaderText = "Tabla";
            if (dataGridView1.Columns["Operacion_MB29"] != null) dataGridView1.Columns["Operacion_MB29"].HeaderText = "Operación";
            if (dataGridView1.Columns["IdRegistro_MB29"] != null) dataGridView1.Columns["IdRegistro_MB29"].HeaderText = "ID Registro";
            if (dataGridView1.Columns["FechaHora_MB29"] != null) dataGridView1.Columns["FechaHora_MB29"].HeaderText = "Fecha y hora";
            if (dataGridView1.Columns["DetalleAnterior_MB29"] != null) dataGridView1.Columns["DetalleAnterior_MB29"].HeaderText = "Detalle anterior";
            if (dataGridView1.Columns["DetalleNuevo_MB29"] != null) dataGridView1.Columns["DetalleNuevo_MB29"].HeaderText = "Detalle nuevo";
        }

       public void  actualizar_MB29(string idioma)
        {
            var g = Gestoridioma_MB29.Instancia_MB29;
           
            button1.Text = g.Traducir_MB29("btnActualizar");
            button2.Text = g.Traducir_MB29("btnSalir");
        }


        private void formbitacoracambios_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new object[] { "Paquete", "Remitente", "Destinatario", "Envio", "PagoEnvio" });
            comboBox1.SelectedIndex = 3; // "Envio" por defecto

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            CargarBitacora_AB29();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CargarBitacora_AB29();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarBitacora_AB29();
        }
    }
}
