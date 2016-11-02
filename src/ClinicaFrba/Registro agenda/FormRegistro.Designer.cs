namespace ClinicaFrba.Registro_agenda
{
    partial class FormRegistro
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
            this.lstRangos = new System.Windows.Forms.ListView();
            this.colDia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHoraDesde = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMinutoDesde = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHoraHasta = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRango = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.colMinutoHasta = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEspecialidad = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lstRangos
            // 
            this.lstRangos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDia,
            this.colHoraDesde,
            this.colMinutoDesde,
            this.colHoraHasta,
            this.colMinutoHasta,
            this.colEspecialidad});
            this.lstRangos.FullRowSelect = true;
            this.lstRangos.GridLines = true;
            this.lstRangos.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstRangos.Location = new System.Drawing.Point(16, 47);
            this.lstRangos.MultiSelect = false;
            this.lstRangos.Name = "lstRangos";
            this.lstRangos.Size = new System.Drawing.Size(658, 168);
            this.lstRangos.TabIndex = 2;
            this.lstRangos.UseCompatibleStateImageBehavior = false;
            this.lstRangos.View = System.Windows.Forms.View.Details;
            // 
            // colDia
            // 
            this.colDia.Text = "Dia";
            this.colDia.Width = 88;
            // 
            // colHoraDesde
            // 
            this.colHoraDesde.Text = "Hora desde";
            this.colHoraDesde.Width = 70;
            // 
            // colMinutoDesde
            // 
            this.colMinutoDesde.Text = "Min. desde";
            this.colMinutoDesde.Width = 67;
            // 
            // colHoraHasta
            // 
            this.colHoraHasta.Text = "Hora hasta";
            this.colHoraHasta.Width = 70;
            // 
            // dtpDesde
            // 
            this.dtpDesde.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(137, 9);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(182, 20);
            this.dtpDesde.TabIndex = 0;
            // 
            // dtpHasta
            // 
            this.dtpHasta.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(492, 9);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(182, 20);
            this.dtpHasta.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 19);
            this.label6.TabIndex = 15;
            this.label6.Text = "Fecha de inicio:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(386, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 19);
            this.label1.TabIndex = 15;
            this.label1.Text = "Fecha de fin:";
            // 
            // btnRango
            // 
            this.btnRango.Location = new System.Drawing.Point(16, 235);
            this.btnRango.Name = "btnRango";
            this.btnRango.Size = new System.Drawing.Size(658, 35);
            this.btnRango.TabIndex = 3;
            this.btnRango.Text = "Añadir rango";
            this.btnRango.UseVisualStyleBackColor = true;
            this.btnRango.Click += new System.EventHandler(this.btnRango_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(16, 276);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(658, 35);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // colMinutoHasta
            // 
            this.colMinutoHasta.Text = "Min. hasta";
            this.colMinutoHasta.Width = 71;
            // 
            // colEspecialidad
            // 
            this.colEspecialidad.Text = "Especialidad";
            this.colEspecialidad.Width = 236;
            // 
            // FormRegistro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(686, 327);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnRango);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.lstRangos);
            this.Name = "FormRegistro";
            this.Text = "Hospital - Registro de agenda";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstRangos;
        private System.Windows.Forms.ColumnHeader colDia;
        private System.Windows.Forms.ColumnHeader colHoraDesde;
        private System.Windows.Forms.ColumnHeader colMinutoDesde;
        private System.Windows.Forms.ColumnHeader colHoraHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRango;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ColumnHeader colMinutoHasta;
        private System.Windows.Forms.ColumnHeader colEspecialidad;
    }
}