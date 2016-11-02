namespace ClinicaFrba.Listados
{
    partial class FormEstadisticas
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
            this.btnEspecialidadesCanceladas = new System.Windows.Forms.Button();
            this.btnProfesionalesConsultados = new System.Windows.Forms.Button();
            this.btnProfesionalesHoras = new System.Windows.Forms.Button();
            this.btnAfiliadosBonos = new System.Windows.Forms.Button();
            this.btnEspecialidadesBonos = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnEspecialidadesCanceladas
            // 
            this.btnEspecialidadesCanceladas.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEspecialidadesCanceladas.Location = new System.Drawing.Point(12, 12);
            this.btnEspecialidadesCanceladas.Name = "btnEspecialidadesCanceladas";
            this.btnEspecialidadesCanceladas.Size = new System.Drawing.Size(410, 55);
            this.btnEspecialidadesCanceladas.TabIndex = 0;
            this.btnEspecialidadesCanceladas.Text = "Top 5 especialidades con cancelaciones";
            this.btnEspecialidadesCanceladas.UseVisualStyleBackColor = true;
            this.btnEspecialidadesCanceladas.Click += new System.EventHandler(this.btnEspecialidadesCanceladas_Click);
            // 
            // btnProfesionalesConsultados
            // 
            this.btnProfesionalesConsultados.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProfesionalesConsultados.Location = new System.Drawing.Point(12, 73);
            this.btnProfesionalesConsultados.Name = "btnProfesionalesConsultados";
            this.btnProfesionalesConsultados.Size = new System.Drawing.Size(410, 55);
            this.btnProfesionalesConsultados.TabIndex = 1;
            this.btnProfesionalesConsultados.Text = "Top 5 profesionales mas consultados";
            this.btnProfesionalesConsultados.UseVisualStyleBackColor = true;
            this.btnProfesionalesConsultados.Click += new System.EventHandler(this.btnProfesionalesConsultados_Click);
            // 
            // btnProfesionalesHoras
            // 
            this.btnProfesionalesHoras.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProfesionalesHoras.Location = new System.Drawing.Point(12, 134);
            this.btnProfesionalesHoras.Name = "btnProfesionalesHoras";
            this.btnProfesionalesHoras.Size = new System.Drawing.Size(410, 55);
            this.btnProfesionalesHoras.TabIndex = 2;
            this.btnProfesionalesHoras.Text = "Top 5 profesionales con menos horas trabajadas";
            this.btnProfesionalesHoras.UseVisualStyleBackColor = true;
            this.btnProfesionalesHoras.Click += new System.EventHandler(this.btnProfesionalesHoras_Click);
            // 
            // btnAfiliadosBonos
            // 
            this.btnAfiliadosBonos.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAfiliadosBonos.Location = new System.Drawing.Point(12, 195);
            this.btnAfiliadosBonos.Name = "btnAfiliadosBonos";
            this.btnAfiliadosBonos.Size = new System.Drawing.Size(410, 55);
            this.btnAfiliadosBonos.TabIndex = 3;
            this.btnAfiliadosBonos.Text = "Top 5 afiliados con mayor cantidad de bonos comprados";
            this.btnAfiliadosBonos.UseVisualStyleBackColor = true;
            this.btnAfiliadosBonos.Click += new System.EventHandler(this.btnAfiliadosBonos_Click);
            // 
            // btnEspecialidadesBonos
            // 
            this.btnEspecialidadesBonos.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEspecialidadesBonos.Location = new System.Drawing.Point(12, 256);
            this.btnEspecialidadesBonos.Name = "btnEspecialidadesBonos";
            this.btnEspecialidadesBonos.Size = new System.Drawing.Size(410, 55);
            this.btnEspecialidadesBonos.TabIndex = 4;
            this.btnEspecialidadesBonos.Text = "Top 5 especialidades con mas bonos utilizados";
            this.btnEspecialidadesBonos.UseVisualStyleBackColor = true;
            this.btnEspecialidadesBonos.Click += new System.EventHandler(this.btnEspecialidadesBonos_Click);
            // 
            // FormEstadisticas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(434, 324);
            this.Controls.Add(this.btnEspecialidadesBonos);
            this.Controls.Add(this.btnAfiliadosBonos);
            this.Controls.Add(this.btnProfesionalesHoras);
            this.Controls.Add(this.btnProfesionalesConsultados);
            this.Controls.Add(this.btnEspecialidadesCanceladas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormEstadisticas";
            this.Text = "Hospital - Estadisticas";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEspecialidadesCanceladas;
        private System.Windows.Forms.Button btnProfesionalesConsultados;
        private System.Windows.Forms.Button btnProfesionalesHoras;
        private System.Windows.Forms.Button btnAfiliadosBonos;
        private System.Windows.Forms.Button btnEspecialidadesBonos;
    }
}