namespace IngenieriaSoftware
{
    partial class formreporte
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
            this.dgvReporte = new System.Windows.Forms.DataGridView();
            this.BtnExportarPDF = new System.Windows.Forms.Button();
            this.BtnActualizar = new System.Windows.Forms.Button();
            this.BtnSalir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvReporte
            // 
            this.dgvReporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReporte.Location = new System.Drawing.Point(38, 22);
            this.dgvReporte.Name = "dgvReporte";
            this.dgvReporte.RowHeadersWidth = 51;
            this.dgvReporte.RowTemplate.Height = 24;
            this.dgvReporte.Size = new System.Drawing.Size(1003, 205);
            this.dgvReporte.TabIndex = 0;
            this.dgvReporte.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReporte_CellContentClick);
            // 
            // BtnExportarPDF
            // 
            this.BtnExportarPDF.Location = new System.Drawing.Point(87, 324);
            this.BtnExportarPDF.Name = "BtnExportarPDF";
            this.BtnExportarPDF.Size = new System.Drawing.Size(202, 63);
            this.BtnExportarPDF.TabIndex = 1;
            this.BtnExportarPDF.Text = "Generar reporte";
            this.BtnExportarPDF.UseVisualStyleBackColor = true;
            this.BtnExportarPDF.Click += new System.EventHandler(this.BtnExportarPDF_Click);
            // 
            // BtnActualizar
            // 
            this.BtnActualizar.Location = new System.Drawing.Point(395, 324);
            this.BtnActualizar.Name = "BtnActualizar";
            this.BtnActualizar.Size = new System.Drawing.Size(202, 63);
            this.BtnActualizar.TabIndex = 2;
            this.BtnActualizar.Text = "Actualizar";
            this.BtnActualizar.UseVisualStyleBackColor = true;
            this.BtnActualizar.Click += new System.EventHandler(this.BtnActualizar_Click);
            // 
            // BtnSalir
            // 
            this.BtnSalir.Location = new System.Drawing.Point(706, 324);
            this.BtnSalir.Name = "BtnSalir";
            this.BtnSalir.Size = new System.Drawing.Size(202, 63);
            this.BtnSalir.TabIndex = 3;
            this.BtnSalir.Text = "Salir";
            this.BtnSalir.UseVisualStyleBackColor = true;
            this.BtnSalir.Click += new System.EventHandler(this.BtnSalir_Click);
            // 
            // formreporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 450);
            this.Controls.Add(this.BtnSalir);
            this.Controls.Add(this.BtnActualizar);
            this.Controls.Add(this.BtnExportarPDF);
            this.Controls.Add(this.dgvReporte);
            this.Name = "formreporte";
            this.Text = "formreporte";
            this.Load += new System.EventHandler(this.formreporte_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvReporte;
        private System.Windows.Forms.Button BtnExportarPDF;
        private System.Windows.Forms.Button BtnActualizar;
        private System.Windows.Forms.Button BtnSalir;
    }
}