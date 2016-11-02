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
            this.Aceptar = new System.Windows.Forms.Button();
            this.Cancelar = new System.Windows.Forms.Button();
            this.Horarios_disponibles = new System.Windows.Forms.Button();
            this.Nombre_del_profesional = new System.Windows.Forms.Label();
            this.Especialidad_del_profesional = new System.Windows.Forms.Label();
            this.Info = new System.Windows.Forms.DataGridView();
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
            this.Calendario.MaxSelectionCount = 1;
            this.Calendario.Name = "Calendario";
            this.Calendario.TabIndex = 0;
            this.Calendario.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.Calendario_DateChanged);
            // 
            // Aceptar
            // 
            this.Aceptar.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Aceptar.Location = new System.Drawing.Point(16, 313);
            this.Aceptar.Name = "Aceptar";
            this.Aceptar.Size = new System.Drawing.Size(263, 46);
            this.Aceptar.TabIndex = 3;
            this.Aceptar.Text = "Aceptar";
            this.Aceptar.UseVisualStyleBackColor = true;
            this.Aceptar.Click += new System.EventHandler(this.button1_Click);
            // 
            // Cancelar
            // 
            this.Cancelar.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancelar.Location = new System.Drawing.Point(342, 313);
            this.Cancelar.Name = "Cancelar";
            this.Cancelar.Size = new System.Drawing.Size(263, 46);
            this.Cancelar.TabIndex = 4;
            this.Cancelar.Text = "Volver atrás";
            this.Cancelar.UseVisualStyleBackColor = true;
            this.Cancelar.Click += new System.EventHandler(this.button2_Click);
            // 
            // Horarios_disponibles
            // 
            this.Horarios_disponibles.Location = new System.Drawing.Point(16, 247);
            this.Horarios_disponibles.Name = "Horarios_disponibles";
            this.Horarios_disponibles.Size = new System.Drawing.Size(248, 40);
            this.Horarios_disponibles.TabIndex = 1;
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
            // Info
            // 
            this.Info.AllowUserToAddRows = false;
            this.Info.AllowUserToDeleteRows = false;
            this.Info.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Info.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Info.Location = new System.Drawing.Point(276, 73);
            this.Info.MultiSelect = false;
            this.Info.Name = "Info";
            this.Info.ReadOnly = true;
            this.Info.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Info.ShowEditingIcon = false;
            this.Info.Size = new System.Drawing.Size(329, 214);
            this.Info.TabIndex = 2;
            // 
            // FormConfirmarTurno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(634, 381);
            this.Controls.Add(this.Info);
            this.Controls.Add(this.Especialidad_del_profesional);
            this.Controls.Add(this.Nombre_del_profesional);
            this.Controls.Add(this.Horarios_disponibles);
            this.Controls.Add(this.Cancelar);
            this.Controls.Add(this.Aceptar);
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
        private System.Windows.Forms.Button Aceptar;
        private System.Windows.Forms.Button Cancelar;
        private System.Windows.Forms.Button Horarios_disponibles;
        private System.Windows.Forms.Label Nombre_del_profesional;
        private System.Windows.Forms.Label Especialidad_del_profesional;
        private System.Windows.Forms.DataGridView Info;
    }
}