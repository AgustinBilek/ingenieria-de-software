using BLL;
using Servicio_MB29;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IngenieriaSoftware
{
    public partial class Formdistribucion: Form
    {
        private readonly DistribucionBLL_MB29 _distribucionBLL = new DistribucionBLL_MB29();

        private int IdRepartidor
        {
            get { return SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.IdPersona_MB29; }
        }

        private string Usuario
        {
            get { return SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29; }
        }

        public Formdistribucion()
        {
            InitializeComponent();
        }

        private void Formdistribucion_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.MultiSelect = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (!CargarEnvios())   // FA01
            {
                MessageBox.Show("No tiene envíos asignados para distribuir.", "Sin envíos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }

        // Pasos 1 y 2 del CU: envíos cargados que le asignaron a este repartidor
        private bool CargarEnvios()
        {
            var envios = _distribucionBLL.ObtenerEnviosCargadosDeRepartidor_AB29(IdRepartidor);

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = envios;

            if (dataGridView1.Columns.Contains("IdEnvio_AB29"))
                dataGridView1.Columns["IdEnvio_AB29"].HeaderText = "ID envío";
            if (dataGridView1.Columns.Contains("Direccion_AB29"))
                dataGridView1.Columns["Direccion_AB29"].HeaderText = "Dirección del destinatario";
            if (dataGridView1.Columns.Contains("Estado_AB29"))
                dataGridView1.Columns["Estado_AB29"].HeaderText = "Estado";

            return envios.Count > 0;
        }

        private void Informar(int ok, string errores, string accion)
        {
            string mensaje = ok > 0 ? $"{ok} envío(s): {accion} correctamente.\n" : "";
            mensaje += errores;
            MessageBox.Show(mensaje, errores == "" ? "Éxito" : "Resultado", MessageBoxButtons.OK,
                errores == "" ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        }

        // Botón Registrar recepción (pasos 3 y 4 del CU)
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione al menos un envío.", "Datos incompletos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int ok = 0;
            string errores = "";

            foreach (DataGridViewRow fila in dataGridView1.SelectedRows)
            {
                int idEnvio = ((EnvioParaEntrega_AB29)fila.DataBoundItem).IdEnvio_AB29;
                try
                {
                    _distribucionBLL.RecibirEnvioParaDistribucion_MB29(Usuario, idEnvio, IdRepartidor);
                    ok++;
                }
                catch (Exception ex)
                {
                    errores += $"Envío {idEnvio}: {ex.Message}\n";
                }
            }

            Informar(ok, errores, "recepción registrada");
        }

        // Botón Iniciar entrega (pasos 5 a 7 del CU)
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione al menos un envío.", "Datos incompletos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int ok = 0;
            string errores = "";

            foreach (DataGridViewRow fila in dataGridView1.SelectedRows)
            {
                int idEnvio = ((EnvioParaEntrega_AB29)fila.DataBoundItem).IdEnvio_AB29;
                try
                {
                    int idEntrega = _distribucionBLL.ObtenerIdEntrega_AB29(idEnvio);
                    if (idEntrega == 0)
                        throw new Exception("primero registre la recepción.");

                    _distribucionBLL.IniciarEntrega_MB29(Usuario, idEntrega, idEnvio);
                    ok++;
                }
                catch (Exception ex)
                {
                    errores += $"Envío {idEnvio}: {ex.Message}\n";
                }
            }

            Informar(ok, errores, "entrega iniciada");
            CargarEnvios();
        }

        // Botón Salir
        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }
    }
}

