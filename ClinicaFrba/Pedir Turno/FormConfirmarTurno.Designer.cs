namespace ClinicaFrba.Pedir_Turno
{
    partial class FormConfirmarTurno
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Calendario = new System.Windows.Forms.MonthCalendar();
            this.Info = new System.Windows.Forms.DataGridView();
            this.Aceptar = new System.Windows.Forms.Button();
            this.Cancelar = new System.Windows.Forms.Button();
            this.Horarios_disponibles = new System.Windows.Forms.Button();
            this.Nombre_del_profesional = new System.Windows.Forms.Label();
            this.Especialidad_del_profesional = new System.Windows.Forms.Label();
            this.DiasDeAtencion = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Info)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "Nombre del profesional";
            this.label3.Click += new System.EventHandler(this.label1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 19);
            this.label4.TabIndex = 1;
            this.label4.Text = "Especialidad";
            // 
            // Calendario
            // 
            this.Calendario.Location = new System.Drawing.Point(16, 73);
            this.Calendario.Name = "Calendario";
            this.Calendario.TabIndex = 2;
            // 
            // Info
            // 
            this.Info.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Info.Location = new System.Drawing.Point(245, 74);
            this.Info.Name = "Info";
            this.Info.Size = new System.Drawing.Size(331, 244);
            this.Info.TabIndex = 3;
            // 
            // Aceptar
            // 
            this.Aceptar.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Aceptar.Location = new System.Drawing.Point(16, 343);
            this.Aceptar.Name = "Aceptar";
            this.Aceptar.Size = new System.Drawing.Size(263, 46);
            this.Aceptar.TabIndex = 4;
            this.Aceptar.Text = "Aceptar";
            this.Aceptar.UseVisualStyleBackColor = true;
            this.Aceptar.Click += new System.EventHandler(this.button1_Click);
            // 
            // Cancelar
            // 
            this.Cancelar.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancelar.Location = new System.Drawing.Point(313, 343);
            this.Cancelar.Name = "Cancelar";
            this.Cancelar.Size = new System.Drawing.Size(263, 46);
            this.Cancelar.TabIndex = 5;
            this.Cancelar.Text = "Volver atrás";
            this.Cancelar.UseVisualStyleBackColor = true;
            this.Cancelar.Click += new System.EventHandler(this.button2_Click);
            // 
            // Horarios_disponibles
            // 
            this.Horarios_disponibles.Location = new System.Drawing.Point(16, 247);
            this.Horarios_disponibles.Name = "Horarios_disponibles";
            this.Horarios_disponibles.Size = new System.Drawing.Size(192, 23);
            this.Horarios_disponibles.TabIndex = 6;
            this.Horarios_disponibles.Text = "Ver horarios disponibles del día";
            this.Horarios_disponibles.UseVisualStyleBackColor = true;
            this.Horarios_disponibles.Click += new System.EventHandler(this.Horarios_disponibles_Click);
            // 
            // Nombre_del_profesional
            // 
            this.Nombre_del_profesional.AutoSize = true;
            this.Nombre_del_profesional.Location = new System.Drawing.Point(215, 14);
            this.Nombre_del_profesional.Name = "Nombre_del_profesional";
            this.Nombre_del_profesional.Size = new System.Drawing.Size(0, 13);
            this.Nombre_del_profesional.TabIndex = 7;
            // 
            // Especialidad_del_profesional
            // 
            this.Especialidad_del_profesional.AutoSize = true;
            this.Especialidad_del_profesional.Location = new System.Drawing.Point(215, 40);
            this.Especialidad_del_profesional.Name = "Especialidad_del_profesional";
            this.Especialidad_del_profesional.Size = new System.Drawing.Size(0, 13);
            this.Especialidad_del_profesional.TabIndex = 8;
            // 
            // DiasDeAtencion
            // 
            this.DiasDeAtencion.Location = new System.Drawing.Point(16, 276);
            this.DiasDeAtencion.Name = "DiasDeAtencion";
            this.DiasDeAtencion.Size = new System.Drawing.Size(192, 51);
            this.DiasDeAtencion.TabIndex = 9;
            this.DiasDeAtencion.Text = "Ver días de atención en el mes seleccionado";
            this.DiasDeAtencion.UseVisualStyleBackColor = true;
            this.DiasDeAtencion.Click += new System.EventHandler(this.DiasDeAtencion_Click);
            // 
            // FormConfirmarTurno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(634, 410);
            this.Controls.Add(this.DiasDeAtencion);
            this.Controls.Add(this.Especialidad_del_profesional);
            this.Controls.Add(this.Nombre_del_profesional);
            this.Controls.Add(this.Horarios_disponibles);
            this.Controls.Add(this.Cancelar);
            this.Controls.Add(this.Aceptar);
            this.Controls.Add(this.Info);
            this.Controls.Add(this.Calendario);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Name = "FormConfirmarTurno";
            this.Text = "Hospital - Reserva de turnos";
            this.Load += new System.EventHandler(this.FormConfirmarTurno_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Info)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MonthCalendar Calendario;
        private System.Windows.Forms.DataGridView Info;
        private System.Windows.Forms.Button Aceptar;
        private System.Windows.Forms.Button Cancelar;
        private System.Windows.Forms.Button Horarios_disponibles;
        private System.Windows.Forms.Label Nombre_del_profesional;
        private System.Windows.Forms.Label Especialidad_del_profesional;
        private System.Windows.Forms.Button DiasDeAtencion;
    }
}