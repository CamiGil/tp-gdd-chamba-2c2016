namespace ClinicaFrba.Registro_agenda
{
    partial class FormRango
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
            this.label6 = new System.Windows.Forms.Label();
            this.cboDia = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboHoraDesde = new System.Windows.Forms.ComboBox();
            this.cboMinutoDesde = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboMinutoHasta = new System.Windows.Forms.ComboBox();
            this.cboHoraHasta = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cboEspecialidad = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 19);
            this.label6.TabIndex = 17;
            this.label6.Text = "Hora de inicio:";
            // 
            // cboDia
            // 
            this.cboDia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDia.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDia.FormattingEnabled = true;
            this.cboDia.Items.AddRange(new object[] {
            "Lunes",
            "Martes",
            "Miercoles",
            "Jueves",
            "Viernes",
            "Sabado"});
            this.cboDia.Location = new System.Drawing.Point(153, 12);
            this.cboDia.Name = "cboDia";
            this.cboDia.Size = new System.Drawing.Size(182, 24);
            this.cboDia.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 19);
            this.label1.TabIndex = 17;
            this.label1.Text = "Dia:";
            // 
            // cboHoraDesde
            // 
            this.cboHoraDesde.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHoraDesde.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboHoraDesde.FormattingEnabled = true;
            this.cboHoraDesde.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23"});
            this.cboHoraDesde.Location = new System.Drawing.Point(153, 42);
            this.cboHoraDesde.Name = "cboHoraDesde";
            this.cboHoraDesde.Size = new System.Drawing.Size(78, 24);
            this.cboHoraDesde.TabIndex = 1;
            // 
            // cboMinutoDesde
            // 
            this.cboMinutoDesde.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMinutoDesde.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMinutoDesde.FormattingEnabled = true;
            this.cboMinutoDesde.Items.AddRange(new object[] {
            "00",
            "30"});
            this.cboMinutoDesde.Location = new System.Drawing.Point(257, 42);
            this.cboMinutoDesde.Name = "cboMinutoDesde";
            this.cboMinutoDesde.Size = new System.Drawing.Size(78, 24);
            this.cboMinutoDesde.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(236, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 19);
            this.label2.TabIndex = 19;
            this.label2.Text = ":";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(236, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 19);
            this.label3.TabIndex = 23;
            this.label3.Text = ":";
            // 
            // cboMinutoHasta
            // 
            this.cboMinutoHasta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMinutoHasta.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMinutoHasta.FormattingEnabled = true;
            this.cboMinutoHasta.Items.AddRange(new object[] {
            "00",
            "30"});
            this.cboMinutoHasta.Location = new System.Drawing.Point(257, 72);
            this.cboMinutoHasta.Name = "cboMinutoHasta";
            this.cboMinutoHasta.Size = new System.Drawing.Size(78, 24);
            this.cboMinutoHasta.TabIndex = 4;
            // 
            // cboHoraHasta
            // 
            this.cboHoraHasta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHoraHasta.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboHoraHasta.FormattingEnabled = true;
            this.cboHoraHasta.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23"});
            this.cboHoraHasta.Location = new System.Drawing.Point(153, 72);
            this.cboHoraHasta.Name = "cboHoraHasta";
            this.cboHoraHasta.Size = new System.Drawing.Size(78, 24);
            this.cboHoraHasta.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 19);
            this.label4.TabIndex = 20;
            this.label4.Text = "Hora de fin:";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(86, 140);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(175, 37);
            this.btnAceptar.TabIndex = 6;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 19);
            this.label5.TabIndex = 17;
            this.label5.Text = "Especialidad:";
            // 
            // cboEspecialidad
            // 
            this.cboEspecialidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEspecialidad.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEspecialidad.FormattingEnabled = true;
            this.cboEspecialidad.Items.AddRange(new object[] {
            "Lunes",
            "Martes",
            "Miercoles",
            "Jueves",
            "Viernes",
            "Sabado"});
            this.cboEspecialidad.Location = new System.Drawing.Point(153, 102);
            this.cboEspecialidad.Name = "cboEspecialidad";
            this.cboEspecialidad.Size = new System.Drawing.Size(182, 24);
            this.cboEspecialidad.TabIndex = 5;
            // 
            // FormRango
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(347, 189);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboMinutoHasta);
            this.Controls.Add(this.cboHoraHasta);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboMinutoDesde);
            this.Controls.Add(this.cboHoraDesde);
            this.Controls.Add(this.cboEspecialidad);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboDia);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRango";
            this.Text = "Hospital - Registro de agenda";
            this.Load += new System.EventHandler(this.FormRango_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboDia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboHoraDesde;
        private System.Windows.Forms.ComboBox cboMinutoDesde;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboMinutoHasta;
        private System.Windows.Forms.ComboBox cboHoraHasta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboEspecialidad;
    }
}