namespace IngenieriaSoftware
{
    partial class FormBACKUPRESTORE
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
            this.btnBackUp = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LblInconsistencias = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnBackUp
            // 
            this.btnBackUp.BackColor = System.Drawing.Color.Red;
            this.btnBackUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackUp.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnBackUp.Location = new System.Drawing.Point(199, 173);
            this.btnBackUp.Name = "btnBackUp";
            this.btnBackUp.Size = new System.Drawing.Size(362, 116);
            this.btnBackUp.TabIndex = 0;
            this.btnBackUp.Text = "BACK UP";
            this.btnBackUp.UseVisualStyleBackColor = false;
            // 
            // btnRestore
            // 
            this.btnRestore.BackColor = System.Drawing.Color.Red;
            this.btnRestore.FlatAppearance.BorderColor = System.Drawing.Color.RosyBrown;
            this.btnRestore.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestore.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRestore.Location = new System.Drawing.Point(199, 341);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(362, 116);
            this.btnRestore.TabIndex = 1;
            this.btnRestore.Text = "RESTORE";
            this.btnRestore.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(33, 292);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(723, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "*Crear un nuevo backup con los datos actuales, manteniendo también las inconsiste" +
    "ncias detectadas";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(74, 460);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(614, 40);
            this.label2.TabIndex = 3;
            this.label2.Text = "        *Recupera los ultimos datos guardados antes de la inconsistencia de datos" +
    ", \r\nperdiendo todos los datos agregados a la base de datos despues del ultimo gu" +
    "ardado";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(61, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(674, 37);
            this.label3.TabIndex = 4;
            this.label3.Text = "¡Inconsistencia en la base de datos detectada!";
            // 
            // LblInconsistencias
            // 
            this.LblInconsistencias.AutoSize = true;
            this.LblInconsistencias.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblInconsistencias.Location = new System.Drawing.Point(353, 98);
            this.LblInconsistencias.Name = "LblInconsistencias";
            this.LblInconsistencias.Size = new System.Drawing.Size(35, 13);
            this.LblInconsistencias.TabIndex = 5;
            this.LblInconsistencias.Text = "label4";
            // 
            // FormBACKUPRECOVERY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Brown;
            this.ClientSize = new System.Drawing.Size(800, 513);
            this.Controls.Add(this.LblInconsistencias);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnBackUp);
            this.Name = "FormBACKUPRECOVERY";
            this.Text = "FormBACKUPRECOVERY";
            this.Load += new System.EventHandler(this.FormBACKUPRECOVERY_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBackUp;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LblInconsistencias;
    }
}