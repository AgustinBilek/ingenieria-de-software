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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace IngenieriaSoftware
{
    public partial class FormGESTIONUSER_MB29: Form, IObserverIdioma_MB29, IObserverSesion_MB29
    {
        public FormGESTIONUSER_MB29()
        {
            InitializeComponent();

            CargarDGV_MB29();

            //Limpiar campos
            NombreTxt.Clear();
            ApellidoTxt.Clear();
            DNITxt.Clear();
            EmailTxt.Clear();

            //Desactiva todos los campos de default
            NombreTxt.Enabled = false;
            ApellidoTxt.Enabled = false;
            DNITxt.Enabled = false;
            EmailTxt.Enabled = false;
            RolCB.Enabled = false;
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

        //modo: 0=ninguno, 1=agregar, 2=deshabilitar, 3=modificar, 4=desbloquear
        private int _modo = 0;

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            //Agregar usuario
            _modo = 1;

            //Limpiar campos
            NombreTxt.Clear();
            ApellidoTxt.Clear();
            DNITxt.Clear();
            EmailTxt.Clear();

            //Habilitar todos los campos para ingresar datos
            NombreTxt.Enabled = true;
            ApellidoTxt.Enabled = true;
            DNITxt.Enabled = true;
            EmailTxt.Enabled = true;
            RolCB.Enabled = true;
        }

        private void BtnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (DGVUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un usuario.");
                return;
            }

            //Deshabilitar usuario
            _modo = 2;

            //No habilitar ningun campo
            NombreTxt.Enabled = false;
            ApellidoTxt.Enabled = false;
            DNITxt.Enabled = false;
            EmailTxt.Enabled = false;
            RolCB.Enabled = false;

            var seleccionado = (UsuarioServicio_MB29)DGVUsuarios.CurrentRow.DataBoundItem;
            if(seleccionado.Estado_MB29 != "Deshabilitado")
            {
                MessageBox.Show($"Está seguro que desea deshabilitar al usuario {seleccionado.Nombre_MB29}? Para confirmar presione el boton Aplicar");
            }
            else
            {
                MessageBox.Show($"Este usuario ya se encuentra deshabilitado");
                _modo = 0;
            }           
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            //Modificar usuario
            _modo = 3;

            //Habilitar solo los campos permitidos para modificar
            NombreTxt.Enabled = false;
            ApellidoTxt.Enabled = false;
            DNITxt.Enabled = false;
            EmailTxt.Enabled = true;
            RolCB.Enabled = true;
        }

        private void BtnDesbloquear_Click(object sender, EventArgs e)
        {
            if (DGVUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un usuario.");
                return;
            }

            //Desbloquear usuario
            _modo = 4;

            //No habilitar ningun campo
            NombreTxt.Enabled = false;
            ApellidoTxt.Enabled = false;
            DNITxt.Enabled = false;
            EmailTxt.Enabled = false;
            RolCB.Enabled = false;

            var seleccionado = (UsuarioServicio_MB29)DGVUsuarios.CurrentRow.DataBoundItem;
            if (!seleccionado.Bloqueado_MB29)
            {
                MessageBox.Show($"El usuario {seleccionado.Usuario_MB29} no se encuentra bloqueado");
                _modo = 0;
            }
        }

        private void BtnAplicar_Click(object sender, EventArgs e)
        {
            switch (_modo)
            {
                case 1: //Agregar usuario
                    {
                        //Leer campos
                        string nombre = NombreTxt.Text.Trim();
                        string apellido = ApellidoTxt.Text.Trim();
                        string email = EmailTxt.Text.Trim();
                        string dniTexto = DNITxt.Text.Trim();

                        //Validaciones
                        if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) ||
                            string.IsNullOrEmpty(dniTexto) || string.IsNullOrEmpty(email))
                        {
                            MessageBox.Show("Completá todos los campos.");
                            return;
                        }

                        if (!double.TryParse(dniTexto, out double dni))
                        {
                            MessageBox.Show("El DNI ingresado no es válido.");
                            return;
                        }

                        foreach (UsuarioServicio_MB29 usuario in UsuarioBLL_MB29.Instancia.ObtenerUsuarios_MB29())
                        {
                            if (Convert.ToDouble(dniTexto) == usuario.DNI_MB29)
                            {
                                MessageBox.Show($"No puede haber dos DNI iguales.");
                                return;
                            }
                        }

                        bool valido = Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

                        if (!valido)
                        {
                            MessageBox.Show("Ingrese un email valido");
                            return;
                        }

                        //Generar credenciales automáticas
                        var (usuarioGen, contraGen) = UsuarioBLL_MB29.AutogenerarCredenciales_MB29(nombre, apellido, dni);

                        //Asignar prioridad segun combo box
                        int idrol = Convert.ToInt32(RolCB.SelectedValue);

                        //Crear usuario y guardarlo
                        var nuevoUsuario = new UsuarioServicio_MB29(
                            id: 0,               //No llega a la BD
                            usuario: usuarioGen,
                            contra: contraGen,
                            esHash: false,       //false porque es texto plano, la BLL lo hashea
                            nombre: nombre,
                            apellido: apellido,
                            dni: dni,
                            idrol: idrol,
                            email: email,
                            telefono: "",
                            false
                        );
                        nuevoUsuario.Estado_MB29 = "Habilitado";

                        UsuarioBLL_MB29.Instancia.Guardar_MB29(nuevoUsuario);
                        CargarDGV_MB29();

                        break;
                    }

                case 2: //Deshabilitar usuario
                    {
                        if (DGVUsuarios.CurrentRow != null)
                        {
                            var seleccionado = (UsuarioServicio_MB29)DGVUsuarios.CurrentRow.DataBoundItem;
                            UsuarioBLL_MB29.Instancia.Deshabilitar_MB29(seleccionado);
                            MessageBox.Show($"El usuario {seleccionado.Nombre_MB29} fue deshabilitado correctamente.");                           
                        }
                        CargarDGV_MB29();
                        break;
                    }

                case 3: //Modificar usuario
                    {
                        var seleccionado = (UsuarioServicio_MB29)DGVUsuarios.CurrentRow.DataBoundItem;
                        if (!string.IsNullOrEmpty(EmailTxt.Text))
                        {
                            seleccionado.Email_MB29 = EmailTxt.Text;
                        }
                        if(RolCB.SelectedItem != null)
                        {
                            int idrol = Convert.ToInt32(RolCB.SelectedValue);
                            seleccionado.IdRol_MB29 = idrol;
                        }
                        
                        UsuarioBLL_MB29.Instancia.Modificar_MB29(seleccionado);
                        MessageBox.Show($"El usuario {seleccionado.Nombre_MB29} fue modificado correctamente.");
                        CargarDGV_MB29();
                        break;
                    }

                case 4: //Desbloquear usuario
                    {
                        if(DGVUsuarios.CurrentRow != null)
                        {
                            var seleccionado = (UsuarioServicio_MB29)DGVUsuarios.CurrentRow.DataBoundItem;
                            UsuarioBLL_MB29.Instancia.Desbloquear_MB29(seleccionado);
                            MessageBox.Show($"El usuario {seleccionado.Nombre_MB29} fue desbloqueado correctamente.");
                        }
                        CargarDGV_MB29();
                        break;
                    }
            }
        }

        private void FiltrarUsuarios_MB29()
        {
            var usuarios = UsuarioBLL_MB29.Instancia.ObtenerUsuarios_MB29();

            if (BloqueadosRB.Checked)
                usuarios = usuarios.Where(u => u.Bloqueado_MB29).ToList();
            else if (ActivosRB.Checked)
                usuarios = usuarios.Where(u => !u.Bloqueado_MB29).ToList();
            else if (RBTodos.Checked)
                usuarios = usuarios.ToList();

                DGVUsuarios.DataSource = usuarios;
        }

        private void DGVUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (DGVUsuarios.SelectedRows.Count == 0) return;

            DataGridViewRow fila = DGVUsuarios.SelectedRows[0];

            NombreTxt.Text = fila.Cells["Nombre_MB29"].Value.ToString();
            ApellidoTxt.Text = fila.Cells["Apellido_MB29"].Value.ToString();
            DNITxt.Text = fila.Cells["DNI_MB29"].Value.ToString();
            EmailTxt.Text = fila.Cells["Email_MB29"].Value.ToString();
            RolCB.SelectedValue = Convert.ToInt32(fila.Cells["IdRol_MB29"].Value);
        }

        private void CargarDGV_MB29()
        {
            DGVUsuarios.DataSource = null;
            List<UsuarioServicio_MB29> usuarios = UsuarioBLL_MB29.Instancia.ObtenerUsuarios_MB29();
            DGVUsuarios.DataSource = usuarios;

            DGVUsuarios.Columns["IdPersona_MB29"].DisplayIndex = 0;
            DGVUsuarios.Columns["Nombre_MB29"].DisplayIndex = 1;
            DGVUsuarios.Columns["Apellido_MB29"].DisplayIndex = 2;
            DGVUsuarios.Columns["Email_MB29"].DisplayIndex = 3;
            DGVUsuarios.Columns["Telefono_MB29"].DisplayIndex = 4;
            DGVUsuarios.Columns["DNI_MB29"].DisplayIndex = 5;
            DGVUsuarios.Columns["Usuario_MB29"].DisplayIndex = 6;
            DGVUsuarios.Columns["IdRol_MB29"].DisplayIndex = 7;
            DGVUsuarios.Columns["Bloqueado_MB29"].DisplayIndex = 8;
            DGVUsuarios.Columns["Estado_MB29"].DisplayIndex = 9;
            DGVUsuarios.Columns["PrimerLogin_MB29"].DisplayIndex = 10;

            if (DGVUsuarios.Columns["PassHash_MB29"] != null)
                DGVUsuarios.Columns["PassHash_MB29"].Visible = false;
            if (DGVUsuarios.Columns["Contra_MB29"] != null)
                DGVUsuarios.Columns["Contra_MB29"].Visible = false;
            if (DGVUsuarios.Columns["IntentosErrados_MB29"] != null)
                DGVUsuarios.Columns["IntentosErrados_MB29"].Visible = false;
        }

        private void FormGESTIONUSER_MB29_Load(object sender, EventArgs e)
        {
            var roles = new List<Rol_MB29>
            {
                new Rol_MB29 { IdRol_MB29 = 1, Nombre = "Administrador" },
                new Rol_MB29 { IdRol_MB29 = 2, Nombre = "Usuario" }
            };

            RolCB.DataSource = roles;
            RolCB.DisplayMember = "NombreRol";
            RolCB.ValueMember = "IdRol";

            if (DGVUsuarios.Columns.Contains("Bloqueado"))
                DGVUsuarios.Columns.Remove("Bloqueado");

            var colBloqueado = new DataGridViewCheckBoxColumn();
            colBloqueado.Name = "Bloqueado";
            colBloqueado.HeaderText = "Bloqueado";
            colBloqueado.DataPropertyName = "Bloqueado";
            colBloqueado.ReadOnly = true;

            DGVUsuarios.Columns.Add(colBloqueado);
        }

        private void BloqueadosRB_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarUsuarios_MB29();
        }
        public void actualizar_MB29(string idioma)
        {
            var g = Gestoridioma_MB29.Instancia_MB29;
            this.Text = g.Traducir_MB29("gestion_titulo");
            label1.Text = g.Traducir_MB29("gestion_lbl_dni");
            label2.Text = g.Traducir_MB29("gestion_lbl_nombre");
            label3.Text = g.Traducir_MB29("gestion_lbl_apellido");
            label4.Text = g.Traducir_MB29("gestion_lbl_email");
            label5.Text = g.Traducir_MB29("gestion_lbl_rol");
            BtnAgregar.Text = g.Traducir_MB29("gestion_btn_agregar");
            BtnDeshabilitar.Text = g.Traducir_MB29("gestion_btn_deshabilitar");
            BtnModificar.Text = g.Traducir_MB29("gestion_btn_modificar");
            BtnDesbloquear.Text = g.Traducir_MB29("gestion_btn_desbloquear");
            BtnAplicar.Text = g.Traducir_MB29("gestion_btn_aplicar");
            BtnSalir.Text = g.Traducir_MB29("gestion_btn_salir");
            ActivosRB.Text = g.Traducir_MB29("gestion_rb_activos");
            BloqueadosRB.Text = g.Traducir_MB29("gestion_rb_bloqueados");
            RBTodos.Text = g.Traducir_MB29("gestion_rb_todos");
        }

        private void ActivosRB_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarUsuarios_MB29();
        }

        private void RBTodos_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarUsuarios_MB29();
        }
    }
}
