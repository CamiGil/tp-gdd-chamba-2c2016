namespace ClinicaFrba.Pedir_Turno
{
    partial class FormSeleccionProf
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
            this.lblNombre = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Nombre_profesional = new System.Windows.Forms.TextBox();
            this.Apellido_profesional = new System.Windows.Forms.TextBox();
            this.Especialidades_profesional = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Buscar = new System.Windows.Forms.Button();
            this.Limpiar = new System.Windows.Forms.Button();
            this.Info = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.Info)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNombre
            // 
            this.lblNombre.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(12, 23);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(95, 19);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "Especialidad";
            this.lblNombre.Click += new System.EventHandler(this.lblNombre_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nombre del profesional";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Apellido del profesional";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // Nombre_profesional
            // 
            this.Nombre_profesional.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Nombre_profesional.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Nombre_profesional.Location = new System.Drawing.Point(199, 56);
            this.Nombre_profesional.MaxLength = 255;
            this.Nombre_profesional.Name = "Nombre_profesional";
            this.Nombre_profesional.Size = new System.Drawing.Size(182, 23);
            this.Nombre_profesional.TabIndex = 1;
            this.Nombre_profesional.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Nombre_profesional_KeyPress);
            // 
            // Apellido_profesional
            // 
            this.Apellido_profesional.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Apellido_profesional.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Apellido_profesional.Location = new System.Drawing.Point(199, 94);
            this.Apellido_profesional.MaxLength = 255;
            this.Apellido_profesional.Name = "Apellido_profesional";
            this.Apellido_profesional.Size = new System.Drawing.Size(182, 23);
            this.Apellido_profesional.TabIndex = 2;
            this.Apellido_profesional.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Apellido_profesional_KeyPress);
            // 
            // Especialidades_profesional
            // 
            this.Especialidades_profesional.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Especialidades_profesional.FormattingEnabled = true;
            this.Especialidades_profesional.Location = new System.Drawing.Point(199, 21);
            this.Especialidades_profesional.Name = "Especialidades_profesional";
            this.Especialidades_profesional.Size = new System.Drawing.Size(182, 21);
            this.Especialidades_profesional.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(123, 373);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 45);
            this.button1.TabIndex = 6;
            this.button1.Text = "Aceptar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(409, 373);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(142, 45);
            this.button2.TabIndex = 7;
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Buscar
            // 
            this.Buscar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Buscar.Location = new System.Drawing.Point(478, 21);
            this.Buscar.Name = "Buscar";
            this.Buscar.Size = new System.Drawing.Size(180, 45);
            this.Buscar.TabIndex = 3;
            this.Buscar.Text = "Buscar";
            this.Buscar.UseVisualStyleBackColor = true;
            this.Buscar.Click += new System.EventHandler(this.Buscar_Click);
            // 
            // Limpiar
            // 
            this.Limpiar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Limpiar.Location = new System.Drawing.Point(478, 72);
            this.Limpiar.Name = "Limpiar";
            this.Limpiar.Size = new System.Drawing.Size(180, 45);
            this.Limpiar.TabIndex = 4;
            this.Limpiar.Text = "Limpiar";
            this.Limpiar.UseVisualStyleBackColor = true;
            this.Limpiar.Click += new System.EventHandler(this.Limpiar_Click);
            // 
            // Info
            // 
            this.Info.AllowUserToAddRows = false;
            this.Info.AllowUserToDeleteRows = false;
            this.Info.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Info.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Info.Location = new System.Drawing.Point(16, 134);
            this.Info.MultiSelect = false;
            this.Info.Name = "Info";
            this.Info.ReadOnly = true;
            this.Info.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Info.ShowEditingIcon = false;
            this.Info.Size = new System.Drawing.Size(642, 224);
            this.Info.TabIndex = 5;
            // 
            // FormSeleccionProf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(675, 435);
            this.Controls.Add(this.Info);
            this.Controls.Add(this.Limpiar);
            this.Controls.Add(this.Buscar);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Especialidades_profesional);
            this.Controls.Add(this.Apellido_profesional);
            this.Controls.Add(this.Nombre_profesional);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblNombre);
            this.Name = "FormSeleccionProf";
            this.Text = "Hospital - Reserva de turnos";
            this.Load += new System.EventHandler(this.FormSeleccionProf_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Info)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Nombre_profesional;
        private System.Windows.Forms.TextBox Apellido_profesional;
        private System.Windows.Forms.ComboBox Especialidades_profesional;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button Buscar;
        private System.Windows.Forms.Button Limpiar;
        private System.Windows.Forms.DataGridView Info;
    }
}