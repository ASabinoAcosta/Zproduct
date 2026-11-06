namespace Zproduct
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tabControlPrincipal = new TabControl();
            tabCatalogo = new TabPage();
            grpInfo = new GroupBox();
            cboImpuesto = new ComboBox();
            textBox3 = new TextBox();
            label7 = new Label();
            nudStock = new NumericUpDown();
            label6 = new Label();
            label5 = new Label();
            nudPrecio = new NumericUpDown();
            label4 = new Label();
            txtNombre = new TextBox();
            label3 = new Label();
            txtCodigo = new TextBox();
            label2 = new Label();
            cboCategoriaProducto = new ComboBox();
            label1 = new Label();
            dgvRecientes = new DataGridView();
            btnGuardar = new Button();
            btnNuevo = new Button();
            btnBuscar = new Button();
            cboCategoria = new ComboBox();
            txtBuscar = new TextBox();
            dgvProductos = new DataGridView();
            tabPage2 = new TabPage();
            splitVentas = new SplitContainer();
            btnFacturas = new Button();
            btnCrearFactura = new Button();
            groupBox1 = new GroupBox();
            btnLimpiar = new Button();
            btnGuardarFactura = new Button();
            txtTotal = new Label();
            txtItbis = new Label();
            label16 = new Label();
            label15 = new Label();
            txtSubCantidad = new Label();
            label14 = new Label();
            grpVistaPrevia = new GroupBox();
            btnAgregarVenta = new Button();
            numericUpDown2 = new NumericUpDown();
            label13 = new Label();
            txtStockRestante = new TextBox();
            label12 = new Label();
            txtStockActual = new TextBox();
            label11 = new Label();
            numericUpDown1 = new NumericUpDown();
            txtItbisProducto = new TextBox();
            label10 = new Label();
            label9 = new Label();
            textBox2 = new TextBox();
            label8 = new Label();
            btnBuscar2 = new Button();
            txtBuscar2 = new TextBox();
            dgvCarrito = new DataGridView();
            tsVentas = new ToolStrip();
            tsbNuevaFactura = new ToolStripButton();
            tsbGenerarPdf = new ToolStripButton();
            gbFacturas = new GroupBox();
            dataGridView1 = new DataGridView();
            btnBuscar3 = new Button();
            txtBuscar3 = new TextBox();
            btnImprimirFactura = new Button();
            lblCliente = new Label();
            cboCliente = new ComboBox();
            btnNuevoCliente = new Button();
            rbContado = new RadioButton();
            rbCredito = new RadioButton();
            tabReportes = new TabPage();
            lblReporte = new Label();
            cboReporte = new ComboBox();
            lblDesde = new Label();
            dtpDesde = new DateTimePicker();
            lblHasta = new Label();
            dtpHasta = new DateTimePicker();
            btnGenerarReporte = new Button();
            btnPdfReporte = new Button();
            lblCajeroReporte = new Label();
            cboCajeroReporte = new ComboBox();
            lblTipoReporte = new Label();
            cboTipoReporte = new ComboBox();
            dgvReporte = new DataGridView();
            lblResumen = new Label();
            tabUsuarios = new TabPage();
            dgvUsuariosAdmin = new DataGridView();
            grpNuevoCajero = new GroupBox();
            lblUsuarioNuevo = new Label();
            txtUsuarioNuevo = new TextBox();
            lblNombreNuevo = new Label();
            txtNombreNuevo = new TextBox();
            lblContrasenaNuevo = new Label();
            txtContrasenaNuevo = new TextBox();
            lblTurnoNuevo = new Label();
            cboTurnoNuevo = new ComboBox();
            btnCrearCajero = new Button();
            grpGestionUsuario = new GroupBox();
            cboTurnoCambio = new ComboBox();
            btnCambiarTurno = new Button();
            btnCambiarEstado = new Button();
            tabControlPrincipal.SuspendLayout();
            tabCatalogo.SuspendLayout();
            grpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudStock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPrecio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvRecientes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitVentas).BeginInit();
            splitVentas.Panel1.SuspendLayout();
            splitVentas.Panel2.SuspendLayout();
            splitVentas.SuspendLayout();
            groupBox1.SuspendLayout();
            grpVistaPrevia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvCarrito).BeginInit();
            tsVentas.SuspendLayout();
            gbFacturas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabReportes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReporte).BeginInit();
            tabUsuarios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuariosAdmin).BeginInit();
            grpNuevoCajero.SuspendLayout();
            grpGestionUsuario.SuspendLayout();
            SuspendLayout();
            // 
            // tabControlPrincipal
            // 
            tabControlPrincipal.Controls.Add(tabCatalogo);
            tabControlPrincipal.Controls.Add(tabPage2);
            tabControlPrincipal.Controls.Add(tabReportes);
            tabControlPrincipal.Controls.Add(tabUsuarios);
            tabControlPrincipal.Location = new Point(0, 1);
            tabControlPrincipal.Name = "tabControlPrincipal";
            tabControlPrincipal.SelectedIndex = 0;
            tabControlPrincipal.Size = new Size(801, 451);
            tabControlPrincipal.TabIndex = 0;
            // 
            // tabCatalogo
            // 
            tabCatalogo.Controls.Add(grpInfo);
            tabCatalogo.Controls.Add(dgvRecientes);
            tabCatalogo.Controls.Add(btnGuardar);
            tabCatalogo.Controls.Add(btnNuevo);
            tabCatalogo.Controls.Add(btnBuscar);
            tabCatalogo.Controls.Add(cboCategoria);
            tabCatalogo.Controls.Add(txtBuscar);
            tabCatalogo.Controls.Add(dgvProductos);
            tabCatalogo.Location = new Point(4, 24);
            tabCatalogo.Name = "tabCatalogo";
            tabCatalogo.Padding = new Padding(3);
            tabCatalogo.Size = new Size(793, 423);
            tabCatalogo.TabIndex = 0;
            tabCatalogo.Text = "Catálogo";
            tabCatalogo.UseVisualStyleBackColor = true;
            // 
            // grpInfo
            // 
            grpInfo.Controls.Add(cboImpuesto);
            grpInfo.Controls.Add(textBox3);
            grpInfo.Controls.Add(label7);
            grpInfo.Controls.Add(nudStock);
            grpInfo.Controls.Add(label6);
            grpInfo.Controls.Add(label5);
            grpInfo.Controls.Add(nudPrecio);
            grpInfo.Controls.Add(label4);
            grpInfo.Controls.Add(txtNombre);
            grpInfo.Controls.Add(label3);
            grpInfo.Controls.Add(txtCodigo);
            grpInfo.Controls.Add(label2);
            grpInfo.Controls.Add(cboCategoriaProducto);
            grpInfo.Controls.Add(label1);
            grpInfo.Location = new Point(519, 44);
            grpInfo.Name = "grpInfo";
            grpInfo.Size = new Size(265, 269);
            grpInfo.TabIndex = 7;
            grpInfo.TabStop = false;
            // 
            // cboImpuesto
            // 
            cboImpuesto.FormattingEnabled = true;
            cboImpuesto.Location = new Point(177, 201);
            cboImpuesto.Name = "cboImpuesto";
            cboImpuesto.Size = new Size(82, 23);
            cboImpuesto.TabIndex = 15;
            // 
            // textBox3
            // 
            textBox3.Enabled = false;
            textBox3.Location = new Point(79, 24);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(162, 23);
            textBox3.TabIndex = 14;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(15, 27);
            label7.Name = "label7";
            label7.Size = new Size(18, 15);
            label7.TabIndex = 13;
            label7.Text = "ID";
            // 
            // nudStock
            // 
            nudStock.DecimalPlaces = 2;
            nudStock.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            nudStock.Location = new Point(61, 222);
            nudStock.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudStock.Name = "nudStock";
            nudStock.Size = new Size(72, 23);
            nudStock.TabIndex = 12;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(15, 222);
            label6.Name = "label6";
            label6.Size = new Size(36, 15);
            label6.TabIndex = 11;
            label6.Text = "Stock";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(139, 204);
            label5.Name = "label5";
            label5.Size = new Size(32, 15);
            label5.TabIndex = 9;
            label5.Text = "ITBIS";
            // 
            // nudPrecio
            // 
            nudPrecio.DecimalPlaces = 2;
            nudPrecio.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            nudPrecio.Location = new Point(61, 182);
            nudPrecio.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudPrecio.Name = "nudPrecio";
            nudPrecio.Size = new Size(72, 23);
            nudPrecio.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 182);
            label4.Name = "label4";
            label4.Size = new Size(40, 15);
            label4.TabIndex = 6;
            label4.Text = "Precio";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(79, 138);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(162, 23);
            txtNombre.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 141);
            label3.Name = "label3";
            label3.Size = new Size(51, 15);
            label3.TabIndex = 4;
            label3.Text = "Nombre";
            // 
            // txtCodigo
            // 
            txtCodigo.Location = new Point(79, 98);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(162, 23);
            txtCodigo.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 101);
            label2.Name = "label2";
            label2.Size = new Size(46, 15);
            label2.TabIndex = 2;
            label2.Text = "Código";
            // 
            // cboCategoriaProducto
            // 
            cboCategoriaProducto.FormattingEnabled = true;
            cboCategoriaProducto.Location = new Point(79, 53);
            cboCategoriaProducto.Name = "cboCategoriaProducto";
            cboCategoriaProducto.Size = new Size(162, 23);
            cboCategoriaProducto.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 56);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 0;
            label1.Text = "Categoría";
            // 
            // dgvRecientes
            // 
            dgvRecientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRecientes.Location = new Point(6, 313);
            dgvRecientes.Name = "dgvRecientes";
            dgvRecientes.Size = new Size(485, 100);
            dgvRecientes.TabIndex = 6;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(570, 373);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(88, 40);
            btnGuardar.TabIndex = 5;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(666, 373);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(88, 40);
            btnNuevo.TabIndex = 6;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnBuscar
            // 
            btnBuscar.Font = new Font("Segoe UI", 10F);
            btnBuscar.Location = new Point(435, 6);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(56, 32);
            btnBuscar.TabIndex = 4;
            btnBuscar.Text = "🔎";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // cboCategoria
            // 
            cboCategoria.FormattingEnabled = true;
            cboCategoria.Location = new Point(8, 15);
            cboCategoria.Name = "cboCategoria";
            cboCategoria.Size = new Size(161, 23);
            cboCategoria.TabIndex = 3;
            cboCategoria.SelectedIndexChanged += cboCategoria_SelectedIndexChanged;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(195, 15);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(234, 23);
            txtBuscar.TabIndex = 2;
            txtBuscar.KeyDown += txtBuscar_KeyDown;
            // 
            // dgvProductos
            // 
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Location = new Point(6, 44);
            dgvProductos.Name = "dgvProductos";
            dgvProductos.Size = new Size(485, 243);
            dgvProductos.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(splitVentas);
            tabPage2.Controls.Add(tsVentas);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(793, 423);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Ventas";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitVentas
            // 
            splitVentas.Dock = DockStyle.Fill;
            splitVentas.Location = new Point(3, 28);
            splitVentas.Name = "splitVentas";
            // 
            // splitVentas.Panel1
            // 
            splitVentas.Panel1.Controls.Add(btnFacturas);
            splitVentas.Panel1.Controls.Add(btnCrearFactura);
            // 
            // splitVentas.Panel2
            // 
            splitVentas.Panel2.Controls.Add(gbFacturas);
            splitVentas.Panel2.Controls.Add(groupBox1);
            splitVentas.Panel2.Paint += splitContainer1_Panel2_Paint;
            splitVentas.Size = new Size(787, 392);
            splitVentas.SplitterDistance = 141;
            splitVentas.TabIndex = 1;
            // 
            // btnFacturas
            // 
            btnFacturas.Location = new Point(0, 69);
            btnFacturas.Name = "btnFacturas";
            btnFacturas.Size = new Size(138, 23);
            btnFacturas.TabIndex = 1;
            btnFacturas.Text = "Facturas";
            btnFacturas.UseVisualStyleBackColor = true;
            btnFacturas.Click += btnFacturas_Click;
            // 
            // btnCrearFactura
            // 
            btnCrearFactura.Location = new Point(0, 30);
            btnCrearFactura.Name = "btnCrearFactura";
            btnCrearFactura.Size = new Size(138, 23);
            btnCrearFactura.TabIndex = 0;
            btnCrearFactura.Text = "Crear Factura";
            btnCrearFactura.UseVisualStyleBackColor = true;
            btnCrearFactura.Click += btnCrearFactura_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rbCredito);
            groupBox1.Controls.Add(rbContado);
            groupBox1.Controls.Add(btnNuevoCliente);
            groupBox1.Controls.Add(cboCliente);
            groupBox1.Controls.Add(lblCliente);
            groupBox1.Controls.Add(btnLimpiar);
            groupBox1.Controls.Add(btnGuardarFactura);
            groupBox1.Controls.Add(txtTotal);
            groupBox1.Controls.Add(txtItbis);
            groupBox1.Controls.Add(label16);
            groupBox1.Controls.Add(label15);
            groupBox1.Controls.Add(txtSubCantidad);
            groupBox1.Controls.Add(label14);
            groupBox1.Controls.Add(grpVistaPrevia);
            groupBox1.Controls.Add(btnBuscar2);
            groupBox1.Controls.Add(txtBuscar2);
            groupBox1.Controls.Add(dgvCarrito);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(642, 392);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Visible = false;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(17, 352);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(111, 40);
            btnLimpiar.TabIndex = 11;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnGuardarFactura
            // 
            btnGuardarFactura.Location = new Point(449, 357);
            btnGuardarFactura.Name = "btnGuardarFactura";
            btnGuardarFactura.Size = new Size(122, 35);
            btnGuardarFactura.TabIndex = 10;
            btnGuardarFactura.Text = "Guardar Factura";
            btnGuardarFactura.UseVisualStyleBackColor = true;
            btnGuardarFactura.Click += btnGuardarFactura_Click;
            // 
            // txtTotal
            // 
            txtTotal.AutoSize = true;
            txtTotal.Location = new Point(437, 319);
            txtTotal.Name = "txtTotal";
            txtTotal.Size = new Size(32, 15);
            txtTotal.TabIndex = 9;
            txtTotal.Text = "Total";
            // 
            // txtItbis
            // 
            txtItbis.AutoSize = true;
            txtItbis.Location = new Point(585, 291);
            txtItbis.Name = "txtItbis";
            txtItbis.Size = new Size(60, 15);
            txtItbis.TabIndex = 8;
            txtItbis.Text = "ITBIS_cant";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label16.Location = new Point(547, 291);
            label16.Name = "label16";
            label16.Size = new Size(37, 15);
            label16.TabIndex = 7;
            label16.Text = "ITBIS";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label15.Location = new Point(387, 319);
            label15.Name = "label15";
            label15.Size = new Size(34, 15);
            label15.TabIndex = 6;
            label15.Text = "Total";
            // 
            // txtSubCantidad
            // 
            txtSubCantidad.AutoSize = true;
            txtSubCantidad.Location = new Point(437, 291);
            txtSubCantidad.Name = "txtSubCantidad";
            txtSubCantidad.Size = new Size(104, 15);
            txtSubCantidad.TabIndex = 5;
            txtSubCantidad.Text = "Subtotal_Cantidad";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label14.Location = new Point(384, 290);
            label14.Name = "label14";
            label14.Size = new Size(54, 15);
            label14.TabIndex = 4;
            label14.Text = "Subtotal";
            // 
            // grpVistaPrevia
            // 
            grpVistaPrevia.Controls.Add(btnAgregarVenta);
            grpVistaPrevia.Controls.Add(numericUpDown2);
            grpVistaPrevia.Controls.Add(label13);
            grpVistaPrevia.Controls.Add(txtStockRestante);
            grpVistaPrevia.Controls.Add(label12);
            grpVistaPrevia.Controls.Add(txtStockActual);
            grpVistaPrevia.Controls.Add(label11);
            grpVistaPrevia.Controls.Add(numericUpDown1);
            grpVistaPrevia.Controls.Add(txtItbisProducto);
            grpVistaPrevia.Controls.Add(label10);
            grpVistaPrevia.Controls.Add(label9);
            grpVistaPrevia.Controls.Add(textBox2);
            grpVistaPrevia.Controls.Add(label8);
            grpVistaPrevia.Location = new Point(383, 48);
            grpVistaPrevia.Name = "grpVistaPrevia";
            grpVistaPrevia.Size = new Size(259, 233);
            grpVistaPrevia.TabIndex = 3;
            grpVistaPrevia.TabStop = false;
            grpVistaPrevia.Text = "Vista Previa";
            // 
            // btnAgregarVenta
            // 
            btnAgregarVenta.Location = new Point(50, 183);
            btnAgregarVenta.Name = "btnAgregarVenta";
            btnAgregarVenta.Size = new Size(150, 44);
            btnAgregarVenta.TabIndex = 13;
            btnAgregarVenta.Text = "Agregar a la Venta";
            btnAgregarVenta.UseVisualStyleBackColor = true;
            btnAgregarVenta.Click += btnAgregarVenta_Click;
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(66, 144);
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(66, 23);
            numericUpDown2.TabIndex = 12;
            numericUpDown2.ValueChanged += numericUpDown2_ValueChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(4, 146);
            label13.Name = "label13";
            label13.Size = new Size(55, 15);
            label13.TabIndex = 11;
            label13.Text = "Cantidad";
            // 
            // txtStockRestante
            // 
            txtStockRestante.Location = new Point(94, 113);
            txtStockRestante.Name = "txtStockRestante";
            txtStockRestante.Size = new Size(133, 23);
            txtStockRestante.TabIndex = 10;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(4, 117);
            label12.Name = "label12";
            label12.Size = new Size(84, 15);
            label12.TabIndex = 9;
            label12.Text = "Stock Restante";
            // 
            // txtStockActual
            // 
            txtStockActual.Location = new Point(83, 84);
            txtStockActual.Name = "txtStockActual";
            txtStockActual.Size = new Size(133, 23);
            txtStockActual.TabIndex = 8;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(4, 88);
            label11.Name = "label11";
            label11.Size = new Size(73, 15);
            label11.TabIndex = 7;
            label11.Text = "Stock Actual";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(50, 53);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(66, 23);
            numericUpDown1.TabIndex = 6;
            // 
            // txtItbisProducto
            // 
            txtItbisProducto.Enabled = false;
            txtItbisProducto.Location = new Point(164, 55);
            txtItbisProducto.Name = "txtItbisProducto";
            txtItbisProducto.Size = new Size(52, 23);
            txtItbisProducto.TabIndex = 5;
            txtItbisProducto.TextChanged += textBox5_TextChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(126, 58);
            label10.Name = "label10";
            label10.Size = new Size(32, 15);
            label10.TabIndex = 4;
            label10.Text = "ITBIS";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(4, 55);
            label9.Name = "label9";
            label9.Size = new Size(40, 15);
            label9.TabIndex = 2;
            label9.Text = "Precio";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(66, 22);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(171, 23);
            textBox2.TabIndex = 1;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(4, 26);
            label8.Name = "label8";
            label8.Size = new Size(56, 15);
            label8.TabIndex = 0;
            label8.Text = "Producto";
            // 
            // btnBuscar2
            // 
            btnBuscar2.Location = new Point(341, 21);
            btnBuscar2.Name = "btnBuscar2";
            btnBuscar2.Size = new Size(88, 23);
            btnBuscar2.TabIndex = 2;
            btnBuscar2.Text = "Buscar";
            btnBuscar2.UseVisualStyleBackColor = true;
            btnBuscar2.Click += btnBuscar2_Click;
            // 
            // txtBuscar2
            // 
            txtBuscar2.Location = new Point(3, 22);
            txtBuscar2.Name = "txtBuscar2";
            txtBuscar2.Size = new Size(332, 23);
            txtBuscar2.TabIndex = 1;
            txtBuscar2.KeyDown += txtBuscar2_KeyDown;
            // 
            // dgvCarrito
            // 
            dgvCarrito.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dgvCarrito.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCarrito.Location = new Point(3, 48);
            dgvCarrito.Name = "dgvCarrito";
            dgvCarrito.Size = new Size(374, 233);
            dgvCarrito.TabIndex = 0;
            // 
            // tsVentas
            // 
            tsVentas.Items.AddRange(new ToolStripItem[] { tsbNuevaFactura, tsbGenerarPdf });
            tsVentas.Location = new Point(3, 3);
            tsVentas.Name = "tsVentas";
            tsVentas.Size = new Size(787, 25);
            tsVentas.TabIndex = 0;
            tsVentas.Text = "toolStrip1";
            // 
            // tsbNuevaFactura
            // 
            tsbNuevaFactura.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbNuevaFactura.Image = (Image)resources.GetObject("tsbNuevaFactura.Image");
            tsbNuevaFactura.ImageTransparentColor = Color.Magenta;
            tsbNuevaFactura.Name = "tsbNuevaFactura";
            tsbNuevaFactura.Size = new Size(23, 22);
            tsbNuevaFactura.Text = "Crear Nueva Factura";
            tsbNuevaFactura.Click += tsbNuevaFactura_Click;
            // 
            // tsbGenerarPdf
            // 
            tsbGenerarPdf.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbGenerarPdf.Image = (Image)resources.GetObject("tsbGenerarPdf.Image");
            tsbGenerarPdf.ImageTransparentColor = Color.Magenta;
            tsbGenerarPdf.Name = "tsbGenerarPdf";
            tsbGenerarPdf.Size = new Size(23, 22);
            tsbGenerarPdf.Text = "Generar Pdf";
            tsbGenerarPdf.Click += tsbGenerarPdf_Click;
            // 
            // gbFacturas
            // 
            gbFacturas.Controls.Add(btnImprimirFactura);
            gbFacturas.Controls.Add(txtBuscar3);
            gbFacturas.Controls.Add(btnBuscar3);
            gbFacturas.Controls.Add(dataGridView1);
            gbFacturas.Dock = DockStyle.Fill;
            gbFacturas.Location = new Point(3, 19);
            gbFacturas.Name = "gbFacturas";
            gbFacturas.Size = new Size(636, 370);
            gbFacturas.TabIndex = 13;
            gbFacturas.TabStop = false;
            gbFacturas.Text = "Facturas";
            gbFacturas.Visible = false;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(14, 73);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(616, 291);
            dataGridView1.TabIndex = 0;
            // 
            // btnBuscar3
            // 
            btnBuscar3.Font = new Font("Segoe UI", 11F);
            btnBuscar3.Location = new Point(500, 27);
            btnBuscar3.Name = "btnBuscar3";
            btnBuscar3.Size = new Size(96, 40);
            btnBuscar3.TabIndex = 1;
            btnBuscar3.Text = "🔎";
            btnBuscar3.UseVisualStyleBackColor = true;
            btnBuscar3.Click += btnBuscar3_Click;
            // 
            // txtBuscar3
            // 
            txtBuscar3.Location = new Point(120, 38);
            txtBuscar3.Name = "txtBuscar3";
            txtBuscar3.Size = new Size(376, 23);
            txtBuscar3.TabIndex = 2;
            txtBuscar3.KeyDown += txtBuscar3_KeyDown;
            // 
            // btnImprimirFactura
            // 
            btnImprimirFactura.Location = new Point(14, 38);
            btnImprimirFactura.Name = "btnImprimirFactura";
            btnImprimirFactura.Size = new Size(100, 23);
            btnImprimirFactura.TabIndex = 3;
            btnImprimirFactura.Text = "Imprimir";
            btnImprimirFactura.UseVisualStyleBackColor = true;
            btnImprimirFactura.Click += btnImprimirFactura_Click;
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(6, 295);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(44, 15);
            lblCliente.TabIndex = 12;
            lblCliente.Text = "Cliente";
            // 
            // cboCliente
            // 
            cboCliente.FormattingEnabled = true;
            cboCliente.Location = new Point(60, 291);
            cboCliente.Name = "cboCliente";
            cboCliente.Size = new Size(200, 23);
            cboCliente.TabIndex = 13;
            // 
            // btnNuevoCliente
            // 
            btnNuevoCliente.Location = new Point(266, 290);
            btnNuevoCliente.Name = "btnNuevoCliente";
            btnNuevoCliente.Size = new Size(70, 25);
            btnNuevoCliente.TabIndex = 14;
            btnNuevoCliente.Text = "Nuevo";
            btnNuevoCliente.UseVisualStyleBackColor = true;
            btnNuevoCliente.Click += btnNuevoCliente_Click;
            // 
            // rbContado
            // 
            rbContado.AutoSize = true;
            rbContado.Checked = true;
            rbContado.Location = new Point(6, 322);
            rbContado.Name = "rbContado";
            rbContado.Size = new Size(70, 19);
            rbContado.TabIndex = 15;
            rbContado.TabStop = true;
            rbContado.Text = "Contado";
            rbContado.UseVisualStyleBackColor = true;
            // 
            // rbCredito
            // 
            rbCredito.AutoSize = true;
            rbCredito.Location = new Point(90, 322);
            rbCredito.Name = "rbCredito";
            rbCredito.Size = new Size(65, 19);
            rbCredito.TabIndex = 16;
            rbCredito.Text = "Crédito";
            rbCredito.UseVisualStyleBackColor = true;
            // 
            // tabReportes
            // 
            tabReportes.Controls.Add(btnPdfReporte);
            tabReportes.Controls.Add(lblResumen);
            tabReportes.Controls.Add(dgvReporte);
            tabReportes.Controls.Add(cboTipoReporte);
            tabReportes.Controls.Add(lblTipoReporte);
            tabReportes.Controls.Add(cboCajeroReporte);
            tabReportes.Controls.Add(lblCajeroReporte);
            tabReportes.Controls.Add(btnGenerarReporte);
            tabReportes.Controls.Add(dtpHasta);
            tabReportes.Controls.Add(lblHasta);
            tabReportes.Controls.Add(dtpDesde);
            tabReportes.Controls.Add(lblDesde);
            tabReportes.Controls.Add(cboReporte);
            tabReportes.Controls.Add(lblReporte);
            tabReportes.Location = new Point(4, 24);
            tabReportes.Name = "tabReportes";
            tabReportes.Padding = new Padding(3);
            tabReportes.Size = new Size(793, 423);
            tabReportes.TabIndex = 2;
            tabReportes.Text = "Reportes";
            tabReportes.UseVisualStyleBackColor = true;
            // 
            // lblReporte
            // 
            lblReporte.AutoSize = true;
            lblReporte.Location = new Point(8, 16);
            lblReporte.Name = "lblReporte";
            lblReporte.Size = new Size(48, 15);
            lblReporte.TabIndex = 0;
            lblReporte.Text = "Reporte";
            // 
            // cboReporte
            // 
            cboReporte.FormattingEnabled = true;
            cboReporte.Location = new Point(62, 12);
            cboReporte.Name = "cboReporte";
            cboReporte.Size = new Size(190, 23);
            cboReporte.TabIndex = 1;
            cboReporte.SelectedIndexChanged += cboReporte_SelectedIndexChanged;
            // 
            // lblDesde
            // 
            lblDesde.AutoSize = true;
            lblDesde.Location = new Point(262, 16);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(40, 15);
            lblDesde.TabIndex = 2;
            lblDesde.Text = "Desde";
            // 
            // dtpDesde
            // 
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(305, 12);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(105, 23);
            dtpDesde.TabIndex = 3;
            // 
            // lblHasta
            // 
            lblHasta.AutoSize = true;
            lblHasta.Location = new Point(418, 16);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(38, 15);
            lblHasta.TabIndex = 4;
            lblHasta.Text = "Hasta";
            // 
            // dtpHasta
            // 
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(460, 12);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(105, 23);
            dtpHasta.TabIndex = 5;
            // 
            // btnGenerarReporte
            // 
            btnGenerarReporte.Location = new Point(580, 10);
            btnGenerarReporte.Name = "btnGenerarReporte";
            btnGenerarReporte.Size = new Size(110, 27);
            btnGenerarReporte.TabIndex = 6;
            btnGenerarReporte.Text = "Generar";
            btnGenerarReporte.UseVisualStyleBackColor = true;
            btnGenerarReporte.Click += btnGenerarReporte_Click;
            // 
            // btnPdfReporte
            // 
            btnPdfReporte.Location = new Point(580, 44);
            btnPdfReporte.Name = "btnPdfReporte";
            btnPdfReporte.Size = new Size(110, 27);
            btnPdfReporte.TabIndex = 17;
            btnPdfReporte.Text = "Generar PDF";
            btnPdfReporte.UseVisualStyleBackColor = true;
            btnPdfReporte.Click += btnPdfReporte_Click;
            // 
            // lblCajeroReporte
            // 
            lblCajeroReporte.AutoSize = true;
            lblCajeroReporte.Location = new Point(8, 50);
            lblCajeroReporte.Name = "lblCajeroReporte";
            lblCajeroReporte.Size = new Size(42, 15);
            lblCajeroReporte.TabIndex = 7;
            lblCajeroReporte.Text = "Cajero";
            // 
            // cboCajeroReporte
            // 
            cboCajeroReporte.FormattingEnabled = true;
            cboCajeroReporte.Location = new Point(62, 46);
            cboCajeroReporte.Name = "cboCajeroReporte";
            cboCajeroReporte.Size = new Size(190, 23);
            cboCajeroReporte.TabIndex = 8;
            // 
            // lblTipoReporte
            // 
            lblTipoReporte.AutoSize = true;
            lblTipoReporte.Location = new Point(262, 50);
            lblTipoReporte.Name = "lblTipoReporte";
            lblTipoReporte.Size = new Size(30, 15);
            lblTipoReporte.TabIndex = 9;
            lblTipoReporte.Text = "Tipo";
            // 
            // cboTipoReporte
            // 
            cboTipoReporte.FormattingEnabled = true;
            cboTipoReporte.Location = new Point(305, 46);
            cboTipoReporte.Name = "cboTipoReporte";
            cboTipoReporte.Size = new Size(105, 23);
            cboTipoReporte.TabIndex = 10;
            // 
            // dgvReporte
            // 
            dgvReporte.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReporte.Location = new Point(6, 78);
            dgvReporte.Name = "dgvReporte";
            dgvReporte.Size = new Size(781, 305);
            dgvReporte.TabIndex = 11;
            // 
            // lblResumen
            // 
            lblResumen.AutoSize = true;
            lblResumen.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblResumen.Location = new Point(8, 392);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(0, 15);
            lblResumen.TabIndex = 12;
            // 
            // tabUsuarios
            // 
            tabUsuarios.Controls.Add(grpGestionUsuario);
            tabUsuarios.Controls.Add(grpNuevoCajero);
            tabUsuarios.Controls.Add(dgvUsuariosAdmin);
            tabUsuarios.Location = new Point(4, 24);
            tabUsuarios.Name = "tabUsuarios";
            tabUsuarios.Padding = new Padding(3);
            tabUsuarios.Size = new Size(793, 423);
            tabUsuarios.TabIndex = 3;
            tabUsuarios.Text = "Usuarios";
            tabUsuarios.UseVisualStyleBackColor = true;
            // 
            // dgvUsuariosAdmin
            // 
            dgvUsuariosAdmin.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuariosAdmin.Location = new Point(6, 6);
            dgvUsuariosAdmin.Name = "dgvUsuariosAdmin";
            dgvUsuariosAdmin.Size = new Size(485, 405);
            dgvUsuariosAdmin.TabIndex = 0;
            // 
            // grpNuevoCajero
            // 
            grpNuevoCajero.Controls.Add(btnCrearCajero);
            grpNuevoCajero.Controls.Add(cboTurnoNuevo);
            grpNuevoCajero.Controls.Add(lblTurnoNuevo);
            grpNuevoCajero.Controls.Add(txtContrasenaNuevo);
            grpNuevoCajero.Controls.Add(lblContrasenaNuevo);
            grpNuevoCajero.Controls.Add(txtNombreNuevo);
            grpNuevoCajero.Controls.Add(lblNombreNuevo);
            grpNuevoCajero.Controls.Add(txtUsuarioNuevo);
            grpNuevoCajero.Controls.Add(lblUsuarioNuevo);
            grpNuevoCajero.Location = new Point(500, 6);
            grpNuevoCajero.Name = "grpNuevoCajero";
            grpNuevoCajero.Size = new Size(285, 215);
            grpNuevoCajero.TabIndex = 1;
            grpNuevoCajero.TabStop = false;
            grpNuevoCajero.Text = "Nuevo cajero";
            // 
            // lblUsuarioNuevo
            // 
            lblUsuarioNuevo.AutoSize = true;
            lblUsuarioNuevo.Location = new Point(15, 32);
            lblUsuarioNuevo.Name = "lblUsuarioNuevo";
            lblUsuarioNuevo.Size = new Size(47, 15);
            lblUsuarioNuevo.TabIndex = 0;
            lblUsuarioNuevo.Text = "Usuario";
            // 
            // txtUsuarioNuevo
            // 
            txtUsuarioNuevo.Location = new Point(110, 29);
            txtUsuarioNuevo.MaxLength = 50;
            txtUsuarioNuevo.Name = "txtUsuarioNuevo";
            txtUsuarioNuevo.Size = new Size(155, 23);
            txtUsuarioNuevo.TabIndex = 1;
            // 
            // lblNombreNuevo
            // 
            lblNombreNuevo.AutoSize = true;
            lblNombreNuevo.Location = new Point(15, 66);
            lblNombreNuevo.Name = "lblNombreNuevo";
            lblNombreNuevo.Size = new Size(51, 15);
            lblNombreNuevo.TabIndex = 2;
            lblNombreNuevo.Text = "Nombre";
            // 
            // txtNombreNuevo
            // 
            txtNombreNuevo.Location = new Point(110, 63);
            txtNombreNuevo.MaxLength = 120;
            txtNombreNuevo.Name = "txtNombreNuevo";
            txtNombreNuevo.Size = new Size(155, 23);
            txtNombreNuevo.TabIndex = 3;
            // 
            // lblContrasenaNuevo
            // 
            lblContrasenaNuevo.AutoSize = true;
            lblContrasenaNuevo.Location = new Point(15, 100);
            lblContrasenaNuevo.Name = "lblContrasenaNuevo";
            lblContrasenaNuevo.Size = new Size(67, 15);
            lblContrasenaNuevo.TabIndex = 4;
            lblContrasenaNuevo.Text = "Contraseña";
            // 
            // txtContrasenaNuevo
            // 
            txtContrasenaNuevo.Location = new Point(110, 97);
            txtContrasenaNuevo.Name = "txtContrasenaNuevo";
            txtContrasenaNuevo.PasswordChar = '*';
            txtContrasenaNuevo.Size = new Size(155, 23);
            txtContrasenaNuevo.TabIndex = 5;
            // 
            // lblTurnoNuevo
            // 
            lblTurnoNuevo.AutoSize = true;
            lblTurnoNuevo.Location = new Point(15, 134);
            lblTurnoNuevo.Name = "lblTurnoNuevo";
            lblTurnoNuevo.Size = new Size(38, 15);
            lblTurnoNuevo.TabIndex = 6;
            lblTurnoNuevo.Text = "Turno";
            // 
            // cboTurnoNuevo
            // 
            cboTurnoNuevo.FormattingEnabled = true;
            cboTurnoNuevo.Location = new Point(110, 131);
            cboTurnoNuevo.Name = "cboTurnoNuevo";
            cboTurnoNuevo.Size = new Size(155, 23);
            cboTurnoNuevo.TabIndex = 7;
            // 
            // btnCrearCajero
            // 
            btnCrearCajero.Location = new Point(110, 168);
            btnCrearCajero.Name = "btnCrearCajero";
            btnCrearCajero.Size = new Size(155, 32);
            btnCrearCajero.TabIndex = 8;
            btnCrearCajero.Text = "Crear cajero";
            btnCrearCajero.UseVisualStyleBackColor = true;
            btnCrearCajero.Click += btnCrearCajero_Click;
            // 
            // grpGestionUsuario
            // 
            grpGestionUsuario.Controls.Add(btnCambiarEstado);
            grpGestionUsuario.Controls.Add(btnCambiarTurno);
            grpGestionUsuario.Controls.Add(cboTurnoCambio);
            grpGestionUsuario.Location = new Point(500, 230);
            grpGestionUsuario.Name = "grpGestionUsuario";
            grpGestionUsuario.Size = new Size(285, 115);
            grpGestionUsuario.TabIndex = 2;
            grpGestionUsuario.TabStop = false;
            grpGestionUsuario.Text = "Usuario seleccionado";
            // 
            // cboTurnoCambio
            // 
            cboTurnoCambio.FormattingEnabled = true;
            cboTurnoCambio.Location = new Point(15, 28);
            cboTurnoCambio.Name = "cboTurnoCambio";
            cboTurnoCambio.Size = new Size(140, 23);
            cboTurnoCambio.TabIndex = 0;
            // 
            // btnCambiarTurno
            // 
            btnCambiarTurno.Location = new Point(162, 26);
            btnCambiarTurno.Name = "btnCambiarTurno";
            btnCambiarTurno.Size = new Size(108, 27);
            btnCambiarTurno.TabIndex = 1;
            btnCambiarTurno.Text = "Cambiar turno";
            btnCambiarTurno.UseVisualStyleBackColor = true;
            btnCambiarTurno.Click += btnCambiarTurno_Click;
            // 
            // btnCambiarEstado
            // 
            btnCambiarEstado.Location = new Point(15, 68);
            btnCambiarEstado.Name = "btnCambiarEstado";
            btnCambiarEstado.Size = new Size(255, 30);
            btnCambiarEstado.TabIndex = 2;
            btnCambiarEstado.Text = "Activar / Desactivar";
            btnCambiarEstado.UseVisualStyleBackColor = true;
            btnCambiarEstado.Click += btnCambiarEstado_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControlPrincipal);
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            tabControlPrincipal.ResumeLayout(false);
            tabCatalogo.ResumeLayout(false);
            tabCatalogo.PerformLayout();
            grpInfo.ResumeLayout(false);
            grpInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudStock).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPrecio).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvRecientes).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            splitVentas.Panel1.ResumeLayout(false);
            splitVentas.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitVentas).EndInit();
            splitVentas.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            grpVistaPrevia.ResumeLayout(false);
            grpVistaPrevia.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvCarrito).EndInit();
            tsVentas.ResumeLayout(false);
            tsVentas.PerformLayout();
            gbFacturas.ResumeLayout(false);
            gbFacturas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabReportes.ResumeLayout(false);
            tabReportes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReporte).EndInit();
            tabUsuarios.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUsuariosAdmin).EndInit();
            grpNuevoCajero.ResumeLayout(false);
            grpNuevoCajero.PerformLayout();
            grpGestionUsuario.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControlPrincipal;
        private TabPage tabCatalogo;
        private TabPage tabPage2;
        private DataGridView dgvProductos;
        private Button btnBuscar;
        private ComboBox cboCategoria;
        private TextBox txtBuscar;
        private DataGridView dgvRecientes;
        private Button btnGuardar;
        private Button btnNuevo;
        private GroupBox grpInfo;
        private ComboBox cboCategoriaProducto;
        private Label label1;
        private TextBox txtCodigo;
        private Label label2;
        private NumericUpDown nudPrecio;
        private Label label4;
        private TextBox txtNombre;
        private Label label3;
        private Label label5;
        private NumericUpDown nudStock;
        private Label label6;
        private TextBox textBox3;
        private Label label7;
        private ComboBox cboImpuesto;
        private SplitContainer splitVentas;
        private ToolStrip tsVentas;
        private ToolStripButton tsbNuevaFactura;
        private Button btnFacturas;
        private Button btnCrearFactura;
        private ToolStripButton tsbGenerarPdf;
        private GroupBox groupBox1;
        private Button btnBuscar2;
        private TextBox txtBuscar2;
        private DataGridView dgvCarrito;
        private GroupBox grpVistaPrevia;
        private TextBox txtItbisProducto;
        private Label label10;
        private Label label9;
        private TextBox textBox2;
        private Label label8;
        private Button btnAgregarVenta;
        private NumericUpDown numericUpDown2;
        private Label label13;
        private TextBox txtStockRestante;
        private Label label12;
        private TextBox txtStockActual;
        private Label label11;
        private NumericUpDown numericUpDown1;
        private Button btnGuardarFactura;
        private Label txtTotal;
        private Label txtItbis;
        private Label label16;
        private Label label15;
        private Label txtSubCantidad;
        private Label label14;
        private Button btnLimpiar;
        private GroupBox gbFacturas;
        private DataGridView dataGridView1;
        private TextBox txtBuscar3;
        private Button btnBuscar3;
        private Button btnImprimirFactura;
        private Label lblCliente;
        private ComboBox cboCliente;
        private Button btnNuevoCliente;
        private RadioButton rbContado;
        private RadioButton rbCredito;
        private TabPage tabReportes;
        private Label lblReporte;
        private ComboBox cboReporte;
        private Label lblDesde;
        private DateTimePicker dtpDesde;
        private Label lblHasta;
        private DateTimePicker dtpHasta;
        private Button btnGenerarReporte;
        private Button btnPdfReporte;
        private Label lblCajeroReporte;
        private ComboBox cboCajeroReporte;
        private Label lblTipoReporte;
        private ComboBox cboTipoReporte;
        private DataGridView dgvReporte;
        private Label lblResumen;
        private TabPage tabUsuarios;
        private DataGridView dgvUsuariosAdmin;
        private GroupBox grpNuevoCajero;
        private Label lblUsuarioNuevo;
        private TextBox txtUsuarioNuevo;
        private Label lblNombreNuevo;
        private TextBox txtNombreNuevo;
        private Label lblContrasenaNuevo;
        private TextBox txtContrasenaNuevo;
        private Label lblTurnoNuevo;
        private ComboBox cboTurnoNuevo;
        private Button btnCrearCajero;
        private GroupBox grpGestionUsuario;
        private ComboBox cboTurnoCambio;
        private Button btnCambiarTurno;
        private Button btnCambiarEstado;
    }
}