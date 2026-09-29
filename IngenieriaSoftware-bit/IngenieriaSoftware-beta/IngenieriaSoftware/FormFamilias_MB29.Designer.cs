namespace IngenieriaSoftware
{
    partial class FormGESTIONPERFIL_MB29
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
            this.LBFamilias = new System.Windows.Forms.ListBox();
            this.LBPermisos = new System.Windows.Forms.ListBox();
            this.btnCrear = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFamilia = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnElimPerm = new System.Windows.Forms.Button();
            this.LBPermisosFamilia = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LBFamilias
            // 
            this.LBFamilias.FormattingEnabled = true;
            this.LBFamilias.ItemHeight = 16;
            this.LBFamilias.Location = new System.Drawing.Point(16, 31);
            this.LBFamilias.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LBFamilias.Name = "LBFamilias";
            this.LBFamilias.Size = new System.Drawing.Size(397, 148);
            this.LBFamilias.TabIndex = 0;
            this.LBFamilias.SelectedIndexChanged += new System.EventHandler(this.LBFamilias_SelectedIndexChanged);
            // 
            // LBPermisos
            // 
            this.LBPermisos.FormattingEnabled = true;
            this.LBPermisos.ItemHeight = 16;
            this.LBPermisos.Location = new System.Drawing.Point(440, 31);
            this.LBPermisos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LBPermisos.Name = "LBPermisos";
            this.LBPermisos.Size = new System.Drawing.Size(341, 356);
            this.LBPermisos.TabIndex = 1;
            // 
            // btnCrear
            // 
            this.btnCrear.Location = new System.Drawing.Point(207, 231);
            this.btnCrear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(100, 52);
            this.btnCrear.TabIndex = 3;
            this.btnCrear.Text = "Crear Familia";
            this.btnCrear.UseVisualStyleBackColor = true;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(315, 231);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(100, 52);
            this.btnEliminar.TabIndex = 4;
            this.btnEliminar.Text = "Eliminar Familia";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(683, 514);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(100, 52);
            this.btnSalir.TabIndex = 5;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(436, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Permisos";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Familias";
            // 
            // txtFamilia
            // 
            this.txtFamilia.Location = new System.Drawing.Point(119, 199);
            this.txtFamilia.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFamilia.Name = "txtFamilia";
            this.txtFamilia.Size = new System.Drawing.Size(295, 22);
            this.txtFamilia.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 203);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Nueva Familia";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(648, 395);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(135, 52);
            this.btnAgregar.TabIndex = 10;
            this.btnAgregar.Text = "Agregar Permiso/Familia";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnElimPerm
            // 
            this.btnElimPerm.Location = new System.Drawing.Point(315, 514);
            this.btnElimPerm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnElimPerm.Name = "btnElimPerm";
            this.btnElimPerm.Size = new System.Drawing.Size(100, 52);
            this.btnElimPerm.TabIndex = 11;
            this.btnElimPerm.Text = "Eliminar Permiso";
            this.btnElimPerm.UseVisualStyleBackColor = true;
            this.btnElimPerm.Click += new System.EventHandler(this.btnElimPerm_Click);
            // 
            // LBPermisosFamilia
            // 
            this.LBPermisosFamilia.FormattingEnabled = true;
            this.LBPermisosFamilia.ItemHeight = 16;
            this.LBPermisosFamilia.Location = new System.Drawing.Point(16, 326);
            this.LBPermisosFamilia.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LBPermisosFamilia.Name = "LBPermisosFamilia";
            this.LBPermisosFamilia.Size = new System.Drawing.Size(397, 180);
            this.LBPermisosFamilia.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 306);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 16);
            this.label4.TabIndex = 13;
            this.label4.Text = "Permisos familia seleccionada";
            // 
            // FormGESTIONPERFIL_MB29
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 578);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LBPermisosFamilia);
            this.Controls.Add(this.btnElimPerm);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFamilia);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.LBPermisos);
            this.Controls.Add(this.LBFamilias);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormGESTIONPERFIL_MB29";
            this.Text = "FormGESTIONPERFIL";
            this.Load += new System.EventHandler(this.FormGESTIONPERFIL_MB29_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LBFamilias;
        private System.Windows.Forms.ListBox LBPermisos;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFamilia;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnElimPerm;
        private System.Windows.Forms.ListBox LBPermisosFamilia;
        private System.Windows.Forms.Label label4;
    }
}