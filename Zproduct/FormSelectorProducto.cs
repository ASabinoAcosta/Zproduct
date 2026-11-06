using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using Zproduct.Data;
using Zproduct.Ventas;

namespace Zproduct
{
    public partial class FormSelectorProducto : Form
    {
        private readonly string textoInicial;

        public ProductoVenta? ProductoSeleccionado { get; private set; }

        public FormSelectorProducto(string textoInicial = "")
        {
            InitializeComponent();
            this.textoInicial = textoInicial;
        }

        private void FormSelectorProducto_Load(object sender, EventArgs e)
        {
            dgvResultados.ReadOnly = true;
            dgvResultados.AllowUserToAddRows = false;
            dgvResultados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResultados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            txtBuscarProducto.Text = textoInicial;
            Buscar();
            txtBuscarProducto.Focus();
        }

        private void txtBuscarProducto_TextChanged(object sender, EventArgs e) => Buscar();

        private void txtBuscarProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Seleccionar();
            }
        }

        private void dgvResultados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                Seleccionar();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e) => Seleccionar();

        private void Buscar()
        {
            try
            {
                dgvResultados.DataSource =
                    ProductoRepository.BuscarParaVenta(txtBuscarProducto.Text.Trim());
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("No se pudieron buscar los productos:\n" + ex.Message,
                                "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Seleccionar()
        {
            if (dgvResultados.CurrentRow?.DataBoundItem is not ProductoVenta producto)
                return;

            ProductoSeleccionado = producto;
            DialogResult = DialogResult.OK;
        }
    }
}
