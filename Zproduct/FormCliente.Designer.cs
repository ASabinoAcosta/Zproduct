namespace Zproduct
{
    partial class FormCliente
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblNombreCliente = new Label();
            txtNombreCliente = new TextBox();
            lblTelefono = new Label();
            txtTelefono = new TextBox();
            btnGuardarCliente = new Button();
            btnCancelarCliente = new Button();
            SuspendLayout();
            // 
            // lblNombreCliente
            // 
            lblNombreCliente.AutoSize = true;
            lblNombreCliente.Location = new Point(20, 25);
            lblNombreCliente.Name = "lblNombreCliente";
            lblNombreCliente.Size = new Size(51, 15);
            lblNombreCliente.TabIndex = 0;
            lblNombreCliente.Text = "Nombre";
            // 
            // txtNombreCliente
            // 
            txtNombreCliente.Location = new Point(90, 22);
            txtNombreCliente.MaxLength = 120;
            txtNombreCliente.Name = "txtNombreCliente";
            txtNombreCliente.Size = new Size(190, 23);
            txtNombreCliente.TabIndex = 1;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(20, 65);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(55, 15);
            lblTelefono.TabIndex = 2;
            lblTelefono.Text = "Teléfono";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(90, 62);
            txtTelefono.MaxLength = 20;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(190, 23);
            txtTelefono.TabIndex = 3;
            // 
            // btnGuardarCliente
            // 
            btnGuardarCliente.Location = new Point(40, 115);
            btnGuardarCliente.Name = "btnGuardarCliente";
            btnGuardarCliente.Size = new Size(100, 32);
            btnGuardarCliente.TabIndex = 4;
            btnGuardarCliente.Text = "Guardar";
            btnGuardarCliente.UseVisualStyleBackColor = true;
            btnGuardarCliente.Click += btnGuardarCliente_Click;
            // 
            // btnCancelarCliente
            // 
            btnCancelarCliente.DialogResult = DialogResult.Cancel;
            btnCancelarCliente.Location = new Point(160, 115);
            btnCancelarCliente.Name = "btnCancelarCliente";
            btnCancelarCliente.Size = new Size(100, 32);
            btnCancelarCliente.TabIndex = 5;
            btnCancelarCliente.Text = "Cancelar";
            btnCancelarCliente.UseVisualStyleBackColor = true;
            // 
            // FormCliente
            // 
            AcceptButton = btnGuardarCliente;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelarCliente;
            ClientSize = new Size(300, 170);
            Controls.Add(btnCancelarCliente);
            Controls.Add(btnGuardarCliente);
            Controls.Add(txtTelefono);
            Controls.Add(lblTelefono);
            Controls.Add(txtNombreCliente);
            Controls.Add(lblNombreCliente);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormCliente";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Nuevo cliente";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNombreCliente;
        private TextBox txtNombreCliente;
        private Label lblTelefono;
        private TextBox txtTelefono;
        private Button btnGuardarCliente;
        private Button btnCancelarCliente;
    }
}
