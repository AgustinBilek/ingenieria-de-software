using BLL;
using Servicio_MB29;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IngenieriaSoftware
{
    public partial class Formregistroentrega: Form
    {
        private readonly DistribucionBLL_MB29 _distribucionBLL = new DistribucionBLL_MB29();

        private Bitmap _firmaBmp;
        private Point _ultimoPunto;
        private bool _firmando;
        private bool _hayFirma;

        private int IdRepartidor
        {
            get { return SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.IdPersona_MB29; }
        }

        private string Usuario
        {
            get { return SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29; }
        }

        public Formregistroentrega()
        {
            InitializeComponent();

            // Eventos que tu Designer todavía no tiene asociados
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            FormClosed += Formregistroentrega_FormClosed;
        }

        private void Formregistroentrega_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Recuadro de firma: tiene que estar en modo Normal para que los trazos coincidan con el mouse
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            pictureBox1.BackColor = Color.White;
            pictureBox1.Cursor = Cursors.Cross;
            _firmaBmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = _firmaBmp;
            LimpiarFirma();

            if (!CargarEnvios())   // FA01
            {
                MessageBox.Show("No tiene envíos en camino.", "Sin envíos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }

        private void Formregistroentrega_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_firmaBmp != null) _firmaBmp.Dispose();
        }

        // Pasos 1 y 2 del CU: envíos en camino de este repartidor
        private bool CargarEnvios()
        {
            var envios = _distribucionBLL.ObtenerEnviosEnCaminoDeRepartidor_AB29(IdRepartidor);

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = envios;

            if (dataGridView1.Columns.Contains("IdEnvio_AB29"))
                dataGridView1.Columns["IdEnvio_AB29"].HeaderText = "ID envío";
            if (dataGridView1.Columns.Contains("Direccion_AB29"))
                dataGridView1.Columns["Direccion_AB29"].HeaderText = "Dirección del destinatario";
            if (dataGridView1.Columns.Contains("Estado_AB29"))
                dataGridView1.Columns["Estado_AB29"].HeaderText = "Estado";

            LimpiarFirma();
            return envios.Count > 0;
        }

        // ---------- Firma dibujada con el mouse ----------
        private void LimpiarFirma()
        {
            if (_firmaBmp == null) return;

            using (var g = Graphics.FromImage(_firmaBmp))
            {
                g.Clear(Color.White);
            }
            _hayFirma = false;
            pictureBox1.Invalidate();
        }

        private void DibujarTrazo(Point desde, Point hasta)
        {
            var tinta = Color.FromArgb(20, 20, 90);

            using (var g = Graphics.FromImage(_firmaBmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                if (desde == hasta)
                {
                    using (var pincel = new SolidBrush(tinta))
                    {
                        g.FillEllipse(pincel, hasta.X - 1.25f, hasta.Y - 1.25f, 2.5f, 2.5f);
                    }
                }
                else
                {
                    using (var lapiz = new Pen(tinta, 2.5f))
                    {
                        lapiz.StartCap = LineCap.Round;
                        lapiz.EndCap = LineCap.Round;
                        lapiz.LineJoin = LineJoin.Round;
                        g.DrawLine(lapiz, desde, hasta);
                    }
                }
            }

            _hayFirma = true;
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // Clic derecho = borrar la firma y empezar de nuevo
            if (e.Button == MouseButtons.Right)
            {
                LimpiarFirma();
                return;
            }

            if (e.Button != MouseButtons.Left) return;

            _firmando = true;
            _ultimoPunto = e.Location;
            DibujarTrazo(e.Location, e.Location);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_firmando) return;

            DibujarTrazo(_ultimoPunto, e.Location);
            _ultimoPunto = e.Location;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            _firmando = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Los trazos se manejan con MouseDown, MouseMove y MouseUp.
        }

        private string ObtenerFirmaBase64()
        {
            using (var ms = new MemoryStream())
            {
                _firmaBmp.Save(ms, ImageFormat.Png);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            LimpiarFirma();   // cada envío lleva su propia firma
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // No hace falta hacer nada al hacer clic en una celda.
        }

        // Botón Registrar entrega
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione el envío que va a entregar.", "Datos incompletos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_hayFirma)   // FA02
            {
                MessageBox.Show("Se requiere la firma de conformidad del destinatario.", "Firma requerida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idEnvio = ((EnvioParaEntrega_AB29)dataGridView1.SelectedRows[0].DataBoundItem).IdEnvio_AB29;
                int idEntrega = _distribucionBLL.ObtenerIdEntrega_AB29(idEnvio);
                if (idEntrega == 0)
                    throw new Exception("El envío no tiene recepción registrada.");

                _distribucionBLL.RegistrarEntregaFinal_MB29(Usuario, idEntrega, idEnvio, ObtenerFirmaBase64());

                MessageBox.Show("La entrega fue registrada correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarEnvios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Botón Salir
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
