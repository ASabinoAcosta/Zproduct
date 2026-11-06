using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using Zproduct.Data;

namespace Zproduct
{
    public partial class FormCliente : Form
    {
        public int ClienteId { get; private set; }

        public FormCliente()
        {
            InitializeComponent();
        }

        private void btnGuardarCliente_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreCliente.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre del cliente es obligatorio.", "Clientes",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                ClienteId = ClienteRepository.Crear(nombre, txtTelefono.Text.Trim());
                DialogResult = DialogResult.OK;
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("No se pudo guardar el cliente:\n" + ex.Message, "Clientes",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
