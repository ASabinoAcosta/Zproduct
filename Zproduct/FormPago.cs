using System;
using System.Collections.Generic;
using System.Text;
using Zproduct.Ventas;

namespace Zproduct
{
    public partial class FormPago : Form
    {
        private readonly decimal total;

        public PagoVenta? Pago { get; private set; }

        public FormPago(decimal total)
        {
            InitializeComponent();
            this.total = total;
        }

        private void FormPago_Load(object sender, EventArgs e)
        {
            txtTotalPagar.ReadOnly = true;
            txtDevuelta.ReadOnly = true;
            txtTotalPagar.Text = total.ToString("N2");

            cboMetodo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMetodo.Items.AddRange(new object[] { "Efectivo", "Tarjeta" });

            nudRecibido.DecimalPlaces = 2;
            nudRecibido.Minimum = 0;
            nudRecibido.Maximum = 100_000_000;
            nudRecibido.Value = total;

            cboMetodo.SelectedIndex = 0;
            ActualizarDevuelta();
            nudRecibido.Select(0, nudRecibido.Text.Length);
        }

        private void cboMetodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool esTarjeta = cboMetodo.SelectedIndex == 1;

            // Con tarjeta se cobra el total exacto, no hay devuelta
            if (esTarjeta)
            {
                nudRecibido.Value = total;
            }

            nudRecibido.Enabled = !esTarjeta;
            ActualizarDevuelta();
        }

        private void nudRecibido_ValueChanged(object sender, EventArgs e) => ActualizarDevuelta();

        private void ActualizarDevuelta()
        {
            decimal devuelta = CalculadoraVenta.Cambio(total, nudRecibido.Value);
            bool alcanza = devuelta >= 0;

            txtDevuelta.Text = (alcanza ? devuelta : 0m).ToString("N2");
            btnConfirmar.Enabled = alcanza;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            decimal recibido = nudRecibido.Value;
            string metodo = cboMetodo.SelectedIndex == 1 ? "tarjeta" : "efectivo";

            Pago = new PagoVenta(metodo, recibido, CalculadoraVenta.Cambio(total, recibido));
            DialogResult = DialogResult.OK;
        }
    }
}
