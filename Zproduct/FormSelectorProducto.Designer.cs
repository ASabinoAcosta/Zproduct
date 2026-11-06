namespace Zproduct
{
    partial class FormSelectorProducto
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
            txtBuscarProducto = new TextBox();
            btnSeleccionar = new Button();
            dgvResultados = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvResultados).BeginInit();
            SuspendLayout();
            // 
            // txtBuscarProducto
            // 
            txtBuscarProducto.Location = new Point(12, 12);
            txtBuscarProducto.Name = "txtBuscarProducto";
            txtBuscarProducto.Size = new Size(488, 23);
            txtBuscarProducto.TabIndex = 0;
            txtBuscarProducto.TextChanged += txtBuscarProducto_TextChanged;
            txtBuscarProducto.KeyDown += txtBuscarProducto_KeyDown;
            // 
            // btnSeleccionar
            // 
            btnSeleccionar.Location = new Point(506, 12);
            btnSeleccionar.Name = "btnSeleccionar";
            btnSeleccionar.Size = new Size(104, 23);
            btnSeleccionar.TabIndex = 1;
            btnSeleccionar.Text = "Seleccionar";
            btnSeleccionar.UseVisualStyleBackColor = true;
            btnSeleccionar.Click += btnSeleccionar_Click;
            // 
            // dgvResultados
            // 
            dgvResultados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResultados.Location = new Point(12, 48);
            dgvResultados.Name = "dgvResultados";
            dgvResultados.Size = new Size(598, 290);
            dgvResultados.TabIndex = 2;
            dgvResultados.CellDoubleClick += dgvResultados_CellDoubleClick;
            // 
            // FormSelectorProducto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(622, 350);
            Controls.Add(dgvResultados);
            Controls.Add(btnSeleccionar);
            Controls.Add(txtBuscarProducto);
            Name = "FormSelectorProducto";
            Text = "Seleccionar producto";
            Load += FormSelectorProducto_Load;
            ((System.ComponentModel.ISupportInitialize)dgvResultados).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtBuscarProducto;
        private Button btnSeleccionar;
        private DataGridView dgvResultados;
    }
}
