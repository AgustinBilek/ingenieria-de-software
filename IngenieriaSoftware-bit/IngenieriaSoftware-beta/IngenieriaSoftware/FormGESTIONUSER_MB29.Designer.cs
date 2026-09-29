namespace IngenieriaSoftware
{
    partial class FormGESTIONUSER_MB29
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DGVUsuarios = new System.Windows.Forms.DataGridView();
            this.DNITxt = new System.Windows.Forms.TextBox();
            this.NombreTxt = new System.Windows.Forms.TextBox();
            this.ApellidoTxt = new System.Windows.Forms.TextBox();
            this.EmailTxt = new System.Windows.Forms.TextBox();
            this.RolCB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnAplicar = new System.Windows.Forms.Button();
            this.BtnAgregar = new System.Windows.Forms.Button();
            this.BtnDeshabilitar = new System.Windows.Forms.Button();
            this.BtnModificar = new System.Windows.Forms.Button();
            this.ActivosRB = new System.Windows.Forms.RadioButton();
            this.BloqueadosRB = new System.Windows.Forms.RadioButton();
            this.BtnSalir = new System.Windows.Forms.Button();
            this.BtnDesbloquear = new System.Windows.Forms.Button();
            this.RBTodos = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.DGVUsuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVUsuarios
            // 
            this.DGVUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVUsuarios.Location = new System.Drawing.Point(50, 35);
            this.DGVUsuarios.Margin = new System.Windows.Forms.Padding(2);
            this.DGVUsuarios.Name = "DGVUsuarios";
            this.DGVUsuarios.RowHeadersWidth = 51;
            this.DGVUsuarios.RowTemplate.Height = 24;
            this.DGVUsuarios.Size = new System.Drawing.Size(644, 197);
            this.DGVUsuarios.TabIndex = 4;
            this.DGVUsuarios.SelectionChanged += new System.EventHandler(this.DGVUsuarios_SelectionChanged);
            // 
            // DNITxt
            // 
            this.DNITxt.Location = new System.Drawing.Point(28, 335);
            this.DNITxt.Margin = new System.Windows.Forms.Padding(2);
            this.DNITxt.Name = "DNITxt";
            this.DNITxt.Size = new System.Drawing.Size(163, 20);
            this.DNITxt.TabIndex = 5;
            // 
            // NombreTxt
            // 
            this.NombreTxt.Location = new System.Drawing.Point(259, 335);
            this.NombreTxt.Margin = new System.Windows.Forms.Padding(2);
            this.NombreTxt.Name = "NombreTxt";
            this.NombreTxt.Size = new System.Drawing.Size(171, 20);
            this.NombreTxt.TabIndex = 6;
            // 
            // ApellidoTxt
            // 
            this.ApellidoTxt.Location = new System.Drawing.Point(28, 429);
            this.ApellidoTxt.Margin = new System.Windows.Forms.Padding(2);
            this.ApellidoTxt.Name = "ApellidoTxt";
            this.ApellidoTxt.Size = new System.Drawing.Size(163, 20);
            this.ApellidoTxt.TabIndex = 7;
            // 
            // EmailTxt
            // 
            this.EmailTxt.Location = new System.Drawing.Point(259, 429);
            this.EmailTxt.Margin = new System.Windows.Forms.Padding(2);
            this.EmailTxt.Name = "EmailTxt";
            this.EmailTxt.Size = new System.Drawing.Size(171, 20);
            this.EmailTxt.TabIndex = 8;
            // 
            // RolCB
            // 
            this.RolCB.FormattingEnabled = true;
            this.RolCB.Location = new System.Drawing.Point(154, 496);
            this.RolCB.Margin = new System.Windows.Forms.Padding(2);
            this.RolCB.Name = "RolCB";
            this.RolCB.Size = new System.Drawing.Size(146, 21);
            this.RolCB.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 301);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 24);
            this.label1.TabIndex = 10;
            this.label1.Text = "DNI";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(354, 301);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 24);
            this.label2.TabIndex = 11;
            this.label2.Text = "Nombre";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(24, 391);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 24);
            this.label3.TabIndex = 12;
            this.label3.Text = "Apellido";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(374, 391);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 24);
            this.label4.TabIndex = 13;
            this.label4.Text = "Email";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(203, 457);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 24);
            this.label5.TabIndex = 14;
            this.label5.Text = "ROL";
            // 
            // BtnAplicar
            // 
            this.BtnAplicar.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.BtnAplicar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAplicar.Location = new System.Drawing.Point(809, 391);
            this.BtnAplicar.Margin = new System.Windows.Forms.Padding(2);
            this.BtnAplicar.Name = "BtnAplicar";
            this.BtnAplicar.Size = new System.Drawing.Size(128, 69);
            this.BtnAplicar.TabIndex = 15;
            this.BtnAplicar.Text = "Aplicar";
            this.BtnAplicar.UseVisualStyleBackColor = false;
            this.BtnAplicar.Click += new System.EventHandler(this.BtnAplicar_Click);
            // 
            // BtnAgregar
            // 
            this.BtnAgregar.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.BtnAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAgregar.Location = new System.Drawing.Point(809, 21);
            this.BtnAgregar.Margin = new System.Windows.Forms.Padding(2);
            this.BtnAgregar.Name = "BtnAgregar";
            this.BtnAgregar.Size = new System.Drawing.Size(128, 69);
            this.BtnAgregar.TabIndex = 16;
            this.BtnAgregar.Text = "Agregar";
            this.BtnAgregar.UseVisualStyleBackColor = false;
            this.BtnAgregar.Click += new System.EventHandler(this.BtnAgregar_Click);
            // 
            // BtnDeshabilitar
            // 
            this.BtnDeshabilitar.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.BtnDeshabilitar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDeshabilitar.Location = new System.Drawing.Point(809, 117);
            this.BtnDeshabilitar.Margin = new System.Windows.Forms.Padding(2);
            this.BtnDeshabilitar.Name = "BtnDeshabilitar";
            this.BtnDeshabilitar.Size = new System.Drawing.Size(128, 69);
            this.BtnDeshabilitar.TabIndex = 17;
            this.BtnDeshabilitar.Text = "Deshabilitar";
            this.BtnDeshabilitar.UseVisualStyleBackColor = false;
            this.BtnDeshabilitar.Click += new System.EventHandler(this.BtnDeshabilitar_Click);
            // 
            // BtnModificar
            // 
            this.BtnModificar.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.BtnModificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnModificar.Location = new System.Drawing.Point(809, 212);
            this.BtnModificar.Margin = new System.Windows.Forms.Padding(2);
            this.BtnModificar.Name = "BtnModificar";
            this.BtnModificar.Size = new System.Drawing.Size(128, 69);
            this.BtnModificar.TabIndex = 18;
            this.BtnModificar.Text = "Modificar";
            this.BtnModificar.UseVisualStyleBackColor = false;
            this.BtnModificar.Click += new System.EventHandler(this.BtnModificar_Click);
            // 
            // ActivosRB
            // 
            this.ActivosRB.AutoSize = true;
            this.ActivosRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActivosRB.Location = new System.Drawing.Point(545, 301);
            this.ActivosRB.Margin = new System.Windows.Forms.Padding(2);
            this.ActivosRB.Name = "ActivosRB";
            this.ActivosRB.Size = new System.Drawing.Size(94, 29);
            this.ActivosRB.TabIndex = 19;
            this.ActivosRB.TabStop = true;
            this.ActivosRB.Text = "Activos";
            this.ActivosRB.UseVisualStyleBackColor = true;
            this.ActivosRB.CheckedChanged += new System.EventHandler(this.ActivosRB_CheckedChanged);
            // 
            // BloqueadosRB
            // 
            this.BloqueadosRB.AutoSize = true;
            this.BloqueadosRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BloqueadosRB.Location = new System.Drawing.Point(545, 334);
            this.BloqueadosRB.Margin = new System.Windows.Forms.Padding(2);
            this.BloqueadosRB.Name = "BloqueadosRB";
            this.BloqueadosRB.Size = new System.Drawing.Size(134, 29);
            this.BloqueadosRB.TabIndex = 20;
            this.BloqueadosRB.TabStop = true;
            this.BloqueadosRB.Text = "Bloqueados";
            this.BloqueadosRB.UseVisualStyleBackColor = true;
            this.BloqueadosRB.CheckedChanged += new System.EventHandler(this.BloqueadosRB_CheckedChanged);
            // 
            // BtnSalir
            // 
            this.BtnSalir.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.BtnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSalir.Location = new System.Drawing.Point(809, 489);
            this.BtnSalir.Margin = new System.Windows.Forms.Padding(2);
            this.BtnSalir.Name = "BtnSalir";
            this.BtnSalir.Size = new System.Drawing.Size(128, 69);
            this.BtnSalir.TabIndex = 21;
            this.BtnSalir.Text = "Salir";
            this.BtnSalir.UseVisualStyleBackColor = false;
            this.BtnSalir.Click += new System.EventHandler(this.BtnSalir_Click);
            // 
            // BtnDesbloquear
            // 
            this.BtnDesbloquear.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.BtnDesbloquear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDesbloquear.Location = new System.Drawing.Point(809, 301);
            this.BtnDesbloquear.Margin = new System.Windows.Forms.Padding(2);
            this.BtnDesbloquear.Name = "BtnDesbloquear";
            this.BtnDesbloquear.Size = new System.Drawing.Size(128, 69);
            this.BtnDesbloquear.TabIndex = 22;
            this.BtnDesbloquear.Text = "Desbloquear";
            this.BtnDesbloquear.UseVisualStyleBackColor = false;
            this.BtnDesbloquear.Click += new System.EventHandler(this.BtnDesbloquear_Click);
            // 
            // RBTodos
            // 
            this.RBTodos.AutoSize = true;
            this.RBTodos.Checked = true;
            this.RBTodos.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RBTodos.Location = new System.Drawing.Point(545, 367);
            this.RBTodos.Margin = new System.Windows.Forms.Padding(2);
            this.RBTodos.Name = "RBTodos";
            this.RBTodos.Size = new System.Drawing.Size(86, 29);
            this.RBTodos.TabIndex = 24;
            this.RBTodos.TabStop = true;
            this.RBTodos.Text = "Todos";
            this.RBTodos.UseVisualStyleBackColor = true;
            this.RBTodos.CheckedChanged += new System.EventHandler(this.RBTodos_CheckedChanged);
            // 
            // FormGESTIONUSER_MB29
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(964, 569);
            this.Controls.Add(this.RBTodos);
            this.Controls.Add(this.BtnDesbloquear);
            this.Controls.Add(this.BtnSalir);
            this.Controls.Add(this.BloqueadosRB);
            this.Controls.Add(this.ActivosRB);
            this.Controls.Add(this.BtnModificar);
            this.Controls.Add(this.BtnDeshabilitar);
            this.Controls.Add(this.BtnAgregar);
            this.Controls.Add(this.BtnAplicar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RolCB);
            this.Controls.Add(this.EmailTxt);
            this.Controls.Add(this.ApellidoTxt);
            this.Controls.Add(this.NombreTxt);
            this.Controls.Add(this.DNITxt);
            this.Controls.Add(this.DGVUsuarios);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormGESTIONUSER_MB29";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormGESTIONUSER_MB29_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVUsuarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView DGVUsuarios;
        private System.Windows.Forms.TextBox DNITxt;
        private System.Windows.Forms.TextBox NombreTxt;
        private System.Windows.Forms.TextBox ApellidoTxt;
        private System.Windows.Forms.TextBox EmailTxt;
        private System.Windows.Forms.ComboBox RolCB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnAplicar;
        private System.Windows.Forms.Button BtnAgregar;
        private System.Windows.Forms.Button BtnDeshabilitar;
        private System.Windows.Forms.Button BtnModificar;
        private System.Windows.Forms.RadioButton ActivosRB;
        private System.Windows.Forms.RadioButton BloqueadosRB;
        private System.Windows.Forms.Button BtnSalir;
        private System.Windows.Forms.Button BtnDesbloquear;
        private System.Windows.Forms.RadioButton RBTodos;
    }
}