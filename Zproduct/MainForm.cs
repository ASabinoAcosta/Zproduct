using Npgsql;
using System.Data;
using Zproduct.Data;
using Zproduct.Ventas;

namespace Zproduct
{
    public partial class MainForm : Form
    {
        private readonly CarritoVenta _carrito = new();
        private ProductoVenta? _productoSeleccionado;
        private long? _ultimoNumeroGuardado;
        private DataTable? _datosReporte;
        private string _tituloReporte = "";
        private string _periodoReporte = "";
        private string _resumenReporte = "";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Text = $"POS - {Sesion.UsuarioActual?.NombreCompleto} ({Sesion.TurnoActual?.Nombre ?? Sesion.UsuarioActual?.Rol})";

            // Solo el administrador crea cajeros y gestiona turnos
            if (!Sesion.EsAdministrador)
            {
                tabControlPrincipal.TabPages.Remove(tabUsuarios);
            }

            try
            {
                ConfigurarCombos();
                ConfigurarGrids();
                ConfigurarVentas();
                ConfigurarReportes();
                BuscarProductos();
                CargarRecientes();

                if (Sesion.EsAdministrador)
                {
                    CargarUsuariosAdmin();
                }
            }
            catch (NpgsqlException ex)
            {
                MostrarError("No se pudo cargar la información inicial:\n" + ex.Message);
            }
        }

        private void ConfigurarCombos()
        {
            cboCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategoria.DisplayMember = "nombre";
            cboCategoria.ValueMember = "id";
            cboCategoria.DataSource = CategoriaRepository.ObtenerParaCombo(incluirTodas: true);

            cboCategoriaProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategoriaProducto.DisplayMember = "nombre";
            cboCategoriaProducto.ValueMember = "id";
            cboCategoriaProducto.DataSource = CategoriaRepository.ObtenerParaCombo(incluirTodas: false);

            cboImpuesto.DropDownStyle = ComboBoxStyle.DropDownList;
            cboImpuesto.DisplayMember = "nombre";
            cboImpuesto.ValueMember = "id";
            cboImpuesto.DataSource = ImpuestoRepository.ObtenerParaCombo();

            cboCategoriaProducto.SelectedIndex = -1;
            cboImpuesto.SelectedIndex = -1;

            cboCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCliente.DisplayMember = "nombre";
            cboCliente.ValueMember = "id";
            CargarClientes();

            cboTurnoNuevo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTurnoNuevo.DisplayMember = "nombre";
            cboTurnoNuevo.ValueMember = "id";
            cboTurnoNuevo.DataSource = TurnoRepository.ObtenerParaCombo();
            cboTurnoNuevo.SelectedIndex = -1;

            cboTurnoCambio.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTurnoCambio.DisplayMember = "nombre";
            cboTurnoCambio.ValueMember = "id";
            cboTurnoCambio.DataSource = TurnoRepository.ObtenerParaCombo();
            cboTurnoCambio.SelectedIndex = -1;
        }

        private void ConfigurarGrids()
        {
            foreach (DataGridView dgv in new[] { dgvProductos, dgvRecientes, dgvCarrito, dataGridView1, dgvReporte, dgvUsuariosAdmin })
            {
                dgv.ReadOnly = true;
                dgv.AllowUserToAddRows = false;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void ConfigurarVentas()
        {
            MostrarVistaCrearFactura();

            textBox2.ReadOnly = true;
            numericUpDown1.ReadOnly = true;
            txtItbisProducto.ReadOnly = true;
            txtStockActual.ReadOnly = true;
            txtStockRestante.ReadOnly = true;

            numericUpDown1.DecimalPlaces = 2;
            numericUpDown1.Maximum = 1_000_000;

            numericUpDown2.Minimum = 1;
            numericUpDown2.Maximum = 10_000;
            numericUpDown2.Value = 1;

            btnAgregarVenta.Enabled = false;
            btnGuardarFactura.Enabled = false;

            txtSubCantidad.Text = "0.00";
            txtItbis.Text = "0.00";
            txtTotal.Text = "0.00";
        }

        private void cboCategoria_SelectedIndexChanged(object sender, EventArgs e) => BuscarProductos();

        private void btnBuscar_Click(object sender, EventArgs e) => BuscarProductos();

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;
            BuscarProductos();
        }

        private void BuscarProductos()
        {
            int categoriaId = cboCategoria.SelectedValue is int id ? id : 0;

            try
            {
                dgvProductos.DataSource = ProductoRepository.Buscar(txtBuscar.Text.Trim(), categoriaId);
            }
            catch (NpgsqlException ex)
            {
                MostrarError("No se pudieron buscar los productos:\n" + ex.Message);
            }
        }

        private void CargarRecientes()
        {
            try
            {
                dgvRecientes.DataSource = MovimientoRepository.ObtenerRecientes();
            }
            catch (NpgsqlException ex)
            {
                MostrarError("No se pudieron cargar los movimientos:\n" + ex.Message);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPanelProducto();
            txtCodigo.Focus();
        }

        private void LimpiarPanelProducto()
        {
            textBox3.Clear();
            txtCodigo.Clear();
            txtNombre.Clear();
            nudPrecio.Value = 0;
            nudStock.Value = 0;
            cboCategoriaProducto.SelectedIndex = -1;
            cboImpuesto.SelectedIndex = -1;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigo.Text.Trim();
            string nombre = txtNombre.Text.Trim();

            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(nombre))
            {
                MostrarError("El código y el nombre son obligatorios.");
                return;
            }

            if (cboCategoriaProducto.SelectedValue is not int categoriaId)
            {
                MostrarError("Seleccione una categoría.");
                return;
            }

            int? impuestoId = cboImpuesto.SelectedValue is int imp ? imp : null;
            int stock = (int)nudStock.Value;
            decimal precio = nudPrecio.Value;

            try
            {
                int productoId = ProductoRepository.Crear(codigo, nombre, categoriaId, precio, impuestoId, stock);

                MessageBox.Show("Producto guardado correctamente.", "Catálogo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                textBox3.Text = productoId.ToString();
                BuscarProductos();
                CargarRecientes();
            }
            catch (PostgresException ex) when (ex.SqlState == "23505")
            {
                MostrarError("Ya existe un producto con ese código.");
            }
            catch (NpgsqlException ex)
            {
                MostrarError("No se pudo guardar el producto:\n" + ex.Message);
            }
        }

        private void btnCrearFactura_Click(object sender, EventArgs e)
        {
            MostrarVistaCrearFactura();
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            MostrarVistaFacturas();
            BuscarFacturas();
        }

        private void tsbNuevaFactura_Click(object sender, EventArgs e)
        {
            MostrarVistaCrearFactura();
            LimpiarVenta();
        }

        private void MostrarVistaCrearFactura()
        {
            gbFacturas.Visible = false;
            groupBox1.Visible = true;
            groupBox1.BringToFront();
        }

        private void MostrarVistaFacturas()
        {
            groupBox1.Visible = false;
            gbFacturas.Visible = true;
            gbFacturas.BringToFront();
        }

        private void btnBuscar2_Click(object sender, EventArgs e)
        {
            BuscarYSeleccionarProducto();
        }

        private void txtBuscar2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;
            BuscarYSeleccionarProducto();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            ActualizarVistaPrevia();
        }

        private void btnAgregarVenta_Click(object sender, EventArgs e)
        {
            if (_productoSeleccionado is null)
            {
                return;
            }

            try
            {
                _carrito.Agregar(_productoSeleccionado, (int)numericUpDown2.Value);
                _productoSeleccionado = null;
                txtBuscar2.Clear();
                numericUpDown2.Value = 1;

                ActualizarVistaPrevia();
                ActualizarCarrito();
            }
            catch (InvalidOperationException ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void btnGuardarFactura_Click(object sender, EventArgs e)
        {
            if (Sesion.UsuarioActual is null)
            {
                MostrarError("No hay un usuario autenticado para registrar la venta.");
                return;
            }

            // El cajero solo puede facturar dentro de su turno
            string? motivo = Sesion.MotivoFueraDeTurno();

            if (motivo is not null)
            {
                MostrarError(motivo);
                return;
            }

            if (_carrito.EstaVacio)
            {
                MostrarError("Agregue al menos un producto a la venta.");
                return;
            }

            bool esCredito = rbCredito.Checked;
            int? clienteId = cboCliente.SelectedValue is int c && c > 0 ? c : null;

            if (esCredito && clienteId is null)
            {
                MostrarError("Seleccione el cliente al que se le fía la venta.");
                return;
            }

            var (_, _, total) = _carrito.Totales();
            PagoVenta? pago = null;

            if (esCredito)
            {
                var respuesta = MessageBox.Show(
                    $"Total a pagar: {total:N2}\nSe registrará en las cuentas por cobrar del cliente.",
                    "Venta a crédito", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (respuesta != DialogResult.OK)
                {
                    return;
                }
            }
            else
            {
                // Total a pagar, monto pagado y devuelta
                using var formPago = new FormPago(total);

                if (formPago.ShowDialog(this) != DialogResult.OK || formPago.Pago is null)
                {
                    return;
                }

                pago = formPago.Pago;
            }

            try
            {
                var datos = new DatosVenta(
                    Sesion.UsuarioActual.Id,
                    Sesion.TurnoActual?.Id,
                    esCredito ? "credito" : "contado",
                    clienteId,
                    _carrito.Lineas.ToList(),
                    pago);

                long numeroFactura = VentaRepository.Confirmar(datos);
                _ultimoNumeroGuardado = numeroFactura;

                MessageBox.Show($"Factura {numeroFactura:D5} guardada correctamente.", "Ventas",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ImprimirFactura(numeroFactura);

                LimpiarVenta();
                CargarRecientes();
            }
            catch (InvalidOperationException ex)
            {
                MostrarError(ex.Message);
            }
            catch (NpgsqlException ex)
            {
                MostrarError("No se pudo guardar la factura:\n" + ex.Message);
            }
        }

        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            using var formCliente = new FormCliente();

            if (formCliente.ShowDialog(this) == DialogResult.OK)
            {
                CargarClientes(formCliente.ClienteId);
            }
        }

        private void btnImprimirFactura_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow is null)
            {
                MostrarError("Seleccione una factura de la lista.");
                return;
            }

            long numero = Convert.ToInt64(dataGridView1.CurrentRow.Cells["numero"].Value);
            ImprimirFactura(numero);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarVenta();
        }

        private void btnBuscar3_Click(object sender, EventArgs e)
        {
            BuscarFacturas();
        }

        private void txtBuscar3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;
            BuscarFacturas();
        }

        private void tsbGenerarPdf_Click(object sender, EventArgs e)
        {
            if (_ultimoNumeroGuardado is null)
            {
                MostrarError("No hay una factura reciente para generar PDF.");
                return;
            }

            try
            {
                string ruta = FacturaPdfGenerator.Generar(_ultimoNumeroGuardado.Value);
                MessageBox.Show("PDF generado en:\n" + ruta, "Ventas",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (NotImplementedException ex)
            {
                MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                MostrarError("No se pudo generar el PDF:\n" + ex.Message);
            }
        }

        private void BuscarYSeleccionarProducto()
        {
            using var selector = new FormSelectorProducto(txtBuscar2.Text.Trim());

            if (selector.ShowDialog(this) != DialogResult.OK || selector.ProductoSeleccionado is null)
            {
                return;
            }

            _productoSeleccionado = selector.ProductoSeleccionado;
            numericUpDown2.Value = 1;
            ActualizarVistaPrevia();
        }

        private void ActualizarVistaPrevia()
        {
            if (_productoSeleccionado is null)
            {
                textBox2.Clear();
                numericUpDown1.Value = 0;
                txtItbisProducto.Clear();
                txtStockActual.Clear();
                txtStockRestante.Clear();
                btnAgregarVenta.Enabled = false;
                return;
            }

            int cantidad = (int)numericUpDown2.Value;
            int enCarrito = _carrito.CantidadEnCarrito(_productoSeleccionado.Id);
            int stockRestante = _productoSeleccionado.Stock - enCarrito - cantidad;

            textBox2.Text = _productoSeleccionado.Nombre;
            numericUpDown1.Value = _productoSeleccionado.Precio;
            txtItbisProducto.Text = CalculadoraVenta
                .Impuesto(_productoSeleccionado.Precio, _productoSeleccionado.TasaImpuesto)
                .ToString("N2");
            txtStockActual.Text = _productoSeleccionado.Stock.ToString();
            txtStockRestante.Text = stockRestante.ToString();

            btnAgregarVenta.Enabled = !_productoSeleccionado.ControlaInventario || stockRestante >= 0;
        }

        private void ActualizarCarrito()
        {
            dgvCarrito.DataSource = _carrito.Lineas.Select(l => new
            {
                Codigo = l.Producto.Codigo,
                Producto = l.Producto.Nombre,
                Cantidad = l.Cantidad,
                Precio = l.Producto.Precio,
                ITBIS = l.Impuesto,
                Importe = l.Importe
            }).ToList();

            var (subtotal, impuesto, total) = _carrito.Totales();
            txtSubCantidad.Text = subtotal.ToString("N2");
            txtItbis.Text = impuesto.ToString("N2");
            txtTotal.Text = total.ToString("N2");

            btnGuardarFactura.Enabled = !_carrito.EstaVacio;
        }

        private void BuscarFacturas()
        {
            try
            {
                dataGridView1.DataSource = FacturaRepository.Buscar(txtBuscar3.Text.Trim());
                FormatearGrid(dataGridView1);
            }
            catch (NpgsqlException ex)
            {
                MostrarError("No se pudieron buscar las facturas:\n" + ex.Message);
            }
        }

        private void LimpiarVenta()
        {
            _carrito.Limpiar();
            _productoSeleccionado = null;

            txtBuscar2.Clear();
            textBox2.Clear();
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 1;
            txtItbisProducto.Clear();
            txtStockActual.Clear();
            txtStockRestante.Clear();

            rbContado.Checked = true;
            if (cboCliente.Items.Count > 0)
            {
                cboCliente.SelectedIndex = 0;
            }

            ActualizarCarrito();
            btnAgregarVenta.Enabled = false;
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }

        private void CargarClientes(int? seleccionarId = null)
        {
            cboCliente.DataSource = ClienteRepository.ObtenerParaCombo(incluirGenerico: true);

            if (seleccionarId is int id)
            {
                cboCliente.SelectedValue = id;
            }
        }

        private void ImprimirFactura(long numero)
        {
            try
            {
                string ruta = FacturaPdfGenerator.Generar(numero);
                FacturaPdfGenerator.Imprimir(ruta);
            }
            catch (Exception ex)
            {
                MostrarError($"La factura {numero:D5} está guardada, pero no se pudo imprimir:\n" + ex.Message);
            }
        }

        // ---------- Reportes ----------

        private void ConfigurarReportes()
        {
            cboReporte.DropDownStyle = ComboBoxStyle.DropDownList;
            cboReporte.Items.AddRange(new object[]
            {
                "Cuadre del día",
                "Ventas por cajero",
                "Inventario en stock",
                "Facturas crédito o contado",
                "5 clientes con más ventas"
            });

            cboTipoReporte.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTipoReporte.Items.AddRange(new object[] { "Todas", "Contado", "Crédito" });
            cboTipoReporte.SelectedIndex = 0;

            cboCajeroReporte.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCajeroReporte.DisplayMember = "nombre";
            cboCajeroReporte.ValueMember = "id";
            cboCajeroReporte.DataSource = UsuarioRepository.ObtenerCajerosParaCombo(incluirTodos: true);

            // Un cajero solo ve sus propias ventas
            if (!Sesion.EsAdministrador && Sesion.UsuarioActual is not null)
            {
                cboCajeroReporte.SelectedValue = Sesion.UsuarioActual.Id;
            }

            cboReporte.SelectedIndex = 0;
        }

        private void cboReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            int reporte = cboReporte.SelectedIndex;

            lblDesde.Text = reporte == 0 ? "Fecha" : "Desde";
            dtpDesde.Enabled = reporte != 2;
            dtpHasta.Enabled = reporte is 1 or 3 or 4;
            cboCajeroReporte.Enabled = Sesion.EsAdministrador && reporte is 0 or 1 or 3;
            cboTipoReporte.Enabled = reporte == 3;
        }

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            if (cboReporte.SelectedIndex < 0)
            {
                MostrarError("Seleccione un reporte.");
                return;
            }

            if (dtpHasta.Enabled && dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                MostrarError("La fecha inicial no puede ser posterior a la final.");
                return;
            }

            int cajeroId = cboCajeroReporte.SelectedValue is int c ? c : 0;

            try
            {
                DataTable datos;
                string resumen;

                switch (cboReporte.SelectedIndex)
                {
                    case 0:
                        datos = ReporteRepository.CuadreDelDia(dtpDesde.Value, cajeroId);
                        decimal efectivo = Suma(datos, "total", "concepto = 'Contado - Efectivo'");
                        decimal tarjeta = Suma(datos, "total", "concepto = 'Contado - Tarjeta'");
                        decimal credito = Suma(datos, "total", "concepto = 'Crédito'");
                        decimal totalDia = Suma(datos, "total");
                        resumen = $"Efectivo: {efectivo:N2}   |   Tarjeta: {tarjeta:N2}   |   Crédito: {credito:N2}   |   Total: {totalDia:N2}";
                        break;

                    case 1:
                        datos = ReporteRepository.PorCajero(dtpDesde.Value, dtpHasta.Value, cajeroId);
                        resumen = $"{Suma(datos, "facturas"):N0} facturas   |   Total vendido: {Suma(datos, "total"):N2}";
                        break;

                    case 2:
                        datos = ReporteRepository.Inventario();
                        resumen = $"{datos.Rows.Count} productos   |   Valor del inventario: {Suma(datos, "valor"):N2}";
                        break;

                    case 3:
                        string tipo = cboTipoReporte.SelectedIndex switch { 1 => "contado", 2 => "credito", _ => "" };
                        datos = ReporteRepository.Facturas(dtpDesde.Value, dtpHasta.Value, cajeroId, tipo);
                        resumen = $"{datos.Rows.Count} facturas   |   Total: {Suma(datos, "total"):N2}";
                        break;

                    default:
                        datos = ReporteRepository.TopClientes(dtpDesde.Value, dtpHasta.Value);
                        resumen = $"Total de estos clientes: {Suma(datos, "total"):N2}";
                        break;
                }

                dgvReporte.DataSource = datos;
                FormatearGrid(dgvReporte);
                lblResumen.Text = datos.Rows.Count == 0 ? "No hay datos para ese filtro." : resumen;

                // Se guarda para poder exportar exactamente lo que se ve en pantalla
                _datosReporte = datos;
                _tituloReporte = cboTipoReporte.Enabled
                    ? $"{cboReporte.Text} - {cboTipoReporte.Text}"
                    : cboReporte.Text;
                _periodoReporte = DescribirPeriodo(cboReporte.SelectedIndex);
                _resumenReporte = lblResumen.Text;
            }
            catch (NpgsqlException ex)
            {
                MostrarError("No se pudo generar el reporte:\n" + ex.Message);
            }
        }

        private void btnPdfReporte_Click(object sender, EventArgs e)
        {
            if (_datosReporte is null)
            {
                MostrarError("Primero genere el reporte en pantalla.");
                return;
            }

            try
            {
                string ruta = ReportePdfGenerator.Generar(_tituloReporte, _periodoReporte, _resumenReporte, _datosReporte);
                ReportePdfGenerator.Abrir(ruta);
            }
            catch (Exception ex)
            {
                MostrarError("No se pudo generar el PDF del reporte:\n" + ex.Message);
            }
        }

        private string DescribirPeriodo(int reporte)
        {
            string cajero = cboCajeroReporte.SelectedValue is int c && c > 0
                ? cboCajeroReporte.Text
                : "Todos los cajeros";

            return reporte switch
            {
                0 => $"Fecha: {dtpDesde.Value:dd/MM/yyyy}   |   Cajero: {cajero}",
                1 => $"Del {dtpDesde.Value:dd/MM/yyyy} al {dtpHasta.Value:dd/MM/yyyy}   |   Cajero: {cajero}",
                2 => "Inventario actual",
                3 => $"Del {dtpDesde.Value:dd/MM/yyyy} al {dtpHasta.Value:dd/MM/yyyy}   |   Cajero: {cajero}",
                _ => $"Del {dtpDesde.Value:dd/MM/yyyy} al {dtpHasta.Value:dd/MM/yyyy}"
            };
        }

        private static decimal Suma(DataTable tabla, string columna, string filtro = "")
        {
            object resultado = tabla.Compute($"SUM({columna})", filtro);
            return resultado is DBNull ? 0m : Convert.ToDecimal(resultado);
        }

        private static void FormatearGrid(DataGridView dgv)
        {
            foreach (DataGridViewColumn columna in dgv.Columns)
            {
                if (columna.ValueType == typeof(decimal))
                {
                    columna.DefaultCellStyle.Format = "N2";
                    columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                else if (columna.ValueType == typeof(DateTime))
                {
                    columna.DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt";
                }
            }
        }

        // ---------- Usuarios (solo administrador) ----------

        private void CargarUsuariosAdmin()
        {
            try
            {
                dgvUsuariosAdmin.DataSource = UsuarioRepository.ObtenerConTurno();

                if (dgvUsuariosAdmin.Columns["id"] is DataGridViewColumn id)
                {
                    id.Visible = false;
                }
            }
            catch (NpgsqlException ex)
            {
                MostrarError("No se pudo cargar la lista de usuarios:\n" + ex.Message);
            }
        }

        private void btnCrearCajero_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuarioNuevo.Text.Trim();
            string nombre = txtNombreNuevo.Text.Trim();
            string contrasena = txtContrasenaNuevo.Text;

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(nombre))
            {
                MostrarError("El usuario y el nombre son obligatorios.");
                return;
            }

            if (contrasena.Length < 6)
            {
                MostrarError("La contraseña debe tener al menos 6 caracteres.");
                return;
            }

            if (cboTurnoNuevo.SelectedValue is not int turnoId)
            {
                MostrarError("Seleccione el turno del cajero (mañana, tarde o noche).");
                return;
            }

            try
            {
                var nuevo = new Usuario
                {
                    NombreUsuario = usuario,
                    NombreCompleto = nombre,
                    PasswordHash = PasswordHasher.Hash(contrasena),
                    Rol = "Cajero",
                    Activo = true
                };

                UsuarioRepository.CrearCajero(nuevo, turnoId);

                MessageBox.Show("Cajero creado correctamente.", "Usuarios",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtUsuarioNuevo.Clear();
                txtNombreNuevo.Clear();
                txtContrasenaNuevo.Clear();
                cboTurnoNuevo.SelectedIndex = -1;
                CargarUsuariosAdmin();
                cboCajeroReporte.DataSource = UsuarioRepository.ObtenerCajerosParaCombo(incluirTodos: true);
            }
            catch (PostgresException ex) when (ex.SqlState == "23505")
            {
                MostrarError("Ya existe un usuario con ese nombre.");
            }
            catch (NpgsqlException ex)
            {
                MostrarError("No se pudo crear el cajero:\n" + ex.Message);
            }
        }

        private void btnCambiarTurno_Click(object sender, EventArgs e)
        {
            if (UsuarioSeleccionado() is not { } seleccionado)
            {
                MostrarError("Seleccione un usuario de la lista.");
                return;
            }

            if (seleccionado.Rol != "Cajero")
            {
                MostrarError("Solo los cajeros tienen turno.");
                return;
            }

            if (cboTurnoCambio.SelectedValue is not int turnoId)
            {
                MostrarError("Seleccione el nuevo turno.");
                return;
            }

            try
            {
                UsuarioRepository.AsignarTurno(seleccionado.Id, turnoId);
                CargarUsuariosAdmin();
            }
            catch (NpgsqlException ex)
            {
                MostrarError("No se pudo cambiar el turno:\n" + ex.Message);
            }
        }

        private void btnCambiarEstado_Click(object sender, EventArgs e)
        {
            if (UsuarioSeleccionado() is not { } seleccionado)
            {
                MostrarError("Seleccione un usuario de la lista.");
                return;
            }

            if (seleccionado.Id == Sesion.UsuarioActual?.Id)
            {
                MostrarError("No puede desactivar su propia cuenta.");
                return;
            }

            try
            {
                UsuarioRepository.CambiarEstado(seleccionado.Id, !seleccionado.Activo);
                CargarUsuariosAdmin();
            }
            catch (NpgsqlException ex)
            {
                MostrarError("No se pudo cambiar el estado:\n" + ex.Message);
            }
        }

        private (int Id, string Rol, bool Activo)? UsuarioSeleccionado()
        {
            DataGridViewRow? fila = dgvUsuariosAdmin.CurrentRow;

            if (fila is null)
            {
                return null;
            }

            return (Convert.ToInt32(fila.Cells["id"].Value),
                    fila.Cells["rol"].Value?.ToString() ?? "",
                    fila.Cells["estado"].Value?.ToString() == "Activo");
        }

        private static void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
