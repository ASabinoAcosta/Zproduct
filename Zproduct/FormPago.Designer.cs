namespace Zproduct
{
    partial class FormPago
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
            lblTotalPagar = new Label();
            txtTotalPagar = new TextBox();
            lblMetodo = new Label();
            cboMetodo = new ComboBox();
            lblRecibido = new Label();
            nudRecibido = new NumericUpDown();
            lblDevuelta = new Label();
            txtDevuelta = new TextBox();
            btnConfirmar = new Button();
            btnCancelar = new Button();
            ((System.ComponentModel.ISupportInitialize)nudRecibido).BeginInit();
            SuspendLayout();
            // 
            // lblTotalPagar
            // 
            lblTotalPagar.AutoSize = true;
            lblTotalPagar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalPagar.Location = new Point(20, 25);
            lblTotalPagar.Name = "lblTotalPagar";
            lblTotalPagar.Size = new Size(79, 15);
            lblTotalPagar.TabIndex = 0;
            lblTotalPagar.Text = "Total a pagar";
            // 
            // txtTotalPagar
            // 
            txtTotalPagar.Location = new Point(130, 22);
            txtTotalPagar.Name = "txtTotalPagar";
            txtTotalPagar.Size = new Size(140, 23);
            txtTotalPagar.TabIndex = 1;
            txtTotalPagar.TabStop = false;
            // 
            // lblMetodo
            // 
            lblMetodo.AutoSize = true;
            lblMetodo.Location = new Point(20, 65);
            lblMetodo.Name = "lblMetodo";
            lblMetodo.Size = new Size(77, 15);
            lblMetodo.TabIndex = 2;
            lblMetodo.Text = "Método de pago";
            // 
            // cboMetodo
            // 
            cboMetodo.FormattingEnabled = true;
            cboMetodo.Location = new Point(130, 62);
            cboMetodo.Name = "cboMetodo";
            cboMetodo.Size = new Size(140, 23);
            cboMetodo.TabIndex = 3;
            cboMetodo.SelectedIndexChanged += cboMetodo_SelectedIndexChanged;
            // 
            // lblRecibido
            // 
            lblRecibido.AutoSize = true;
            lblRecibido.Location = new Point(20, 105);
            lblRecibido.Name = "lblRecibido";
            lblRecibido.Size = new Size(78, 15);
            lblRecibido.TabIndex = 4;
            lblRecibido.Text = "Monto pagado";
            // 
            // nudRecibido
            // 
            nudRecibido.Location = new Point(130, 102);
            nudRecibido.Name = "nudRecibido";
            nudRecibido.Size = new Size(140, 23);
            nudRecibido.TabIndex = 5;
            nudRecibido.ValueChanged += nudRecibido_ValueChanged;
            // 
            // lblDevuelta
            // 
            lblDevuelta.AutoSize = true;
            lblDevuelta.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDevuelta.Location = new Point(20, 145);
            lblDevuelta.Name = "lblDevuelta";
            lblDevuelta.Size = new Size(52, 15);
            lblDevuelta.TabIndex = 6;
            lblDevuelta.Text = "Devuelta";
            // 
            // txtDevuelta
            // 
            txtDevuelta.Location = new Point(130, 142);
            txtDevuelta.Name = "txtDevuelta";
            txtDevuelta.Size = new Size(140, 23);
            txtDevuelta.TabIndex = 7;
            txtDevuelta.TabStop = false;
            // 
            // btnConfirmar
            // 
            btnConfirmar.Location = new Point(40, 195);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(100, 32);
            btnConfirmar.TabIndex = 8;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(160, 195);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(100, 32);
            btnCancelar.TabIndex = 9;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // FormPago
            // 
            AcceptButton = btnConfirmar;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelar;
            ClientSize = new Size(300, 250);
            Controls.Add(btnCancelar);
            Controls.Add(btnConfirmar);
            Controls.Add(txtDevuelta);
            Controls.Add(lblDevuelta);
            Controls.Add(nudRecibido);
            Controls.Add(lblRecibido);
            Controls.Add(cboMetodo);
            Controls.Add(lblMetodo);
            Controls.Add(txtTotalPagar);
            Controls.Add(lblTotalPagar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormPago";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Pago";
            Load += FormPago_Load;
            ((System.ComponentModel.ISupportInitialize)nudRecibido).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTotalPagar;
        private TextBox txtTotalPagar;
        private Label lblMetodo;
        private ComboBox cboMetodo;
        private Label lblRecibido;
        private NumericUpDown nudRecibido;
        private Label lblDevuelta;
        private TextBox txtDevuelta;
        private Button btnConfirmar;
        private Button btnCancelar;
    }
}
