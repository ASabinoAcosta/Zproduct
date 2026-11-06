using System;
using System.Collections.Generic;
using System.Text;
using Zproduct.Data;

namespace Zproduct;

/// <summary>
/// Pantalla que se muestra la primera vez (o cuando la conexión guardada deja de funcionar).
/// Está construida por código para que puedas agregarla sin usar el diseñador.
/// </summary>
public class SetupForm : Form
{
    private readonly TextBox txtPassword = new() { UseSystemPasswordChar = true, Width = 300 };
    private readonly TextBox txtHost = new() { Text = "localhost", Width = 200 };
    private readonly TextBox txtPort = new() { Text = "5432", Width = 200 };
    private readonly TextBox txtUser = new() { Text = "postgres", Width = 200 };
    private readonly CheckBox chkAvanzado = new() { Text = "Opciones avanzadas", AutoSize = true };
    private readonly TableLayoutPanel panelAvanzado = new() { AutoSize = true, ColumnCount = 2, Visible = false };
    private readonly Label lblError = new() { ForeColor = Color.Firebrick, AutoSize = false, Width = 300, Height = 40 };
    private readonly Button btnGuardar = new() { Text = "Guardar y continuar", Width = 300, Height = 32 };

    /// <summary>Configuración validada. Solo tiene valor si el usuario guardó con éxito.</summary>
    public DbConfig? Result { get; private set; }

    public SetupForm()
    {
        Text = "Configuración inicial";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterScreen;
        MaximizeBox = false;
        MinimizeBox = false;
        ClientSize = new Size(340, 340);
        AcceptButton = btnGuardar;

        // Sección avanzada: servidor, puerto y usuario
        panelAvanzado.Controls.Add(new Label { Text = "Servidor:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 0);
        panelAvanzado.Controls.Add(txtHost, 1, 0);
        panelAvanzado.Controls.Add(new Label { Text = "Puerto:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 1);
        panelAvanzado.Controls.Add(txtPort, 1, 1);
        panelAvanzado.Controls.Add(new Label { Text = "Usuario:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 2);
        panelAvanzado.Controls.Add(txtUser, 1, 2);

        // Contenedor vertical principal
        var flow = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            Padding = new Padding(16)
        };

        flow.Controls.Add(new Label
        {
            Text = "Escribe la contraseña de tu PostgreSQL.\nSolo se te pedirá esta vez.",
            AutoSize = true
        });
        flow.Controls.Add(txtPassword);
        flow.Controls.Add(chkAvanzado);
        flow.Controls.Add(panelAvanzado);
        flow.Controls.Add(lblError);
        flow.Controls.Add(btnGuardar);

        Controls.Add(flow);

        chkAvanzado.CheckedChanged += (_, _) => panelAvanzado.Visible = chkAvanzado.Checked;
        btnGuardar.Click += BtnGuardar_Click;
    }

    private async void BtnGuardar_Click(object? sender, EventArgs e)
    {
        lblError.Text = "";

        if (string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            lblError.Text = "Escribe la contraseña.";
            return;
        }

        if (!int.TryParse(txtPort.Text, out var puerto))
        {
            lblError.Text = "El puerto debe ser un número.";
            return;
        }

        var config = new DbConfig
        {
            Host = txtHost.Text.Trim(),
            Port = puerto,
            Username = txtUser.Text.Trim(),
            Password = txtPassword.Text
        };

        // Probamos en otro hilo para que la ventana no se congele.
        btnGuardar.Enabled = false;
        lblError.ForeColor = Color.DimGray;
        lblError.Text = "Probando conexión...";

        var (ok, mensaje) = await Task.Run(() => ConfigService.TestConnection(config));

        btnGuardar.Enabled = true;
        lblError.ForeColor = Color.Firebrick;

        if (!ok)
        {
            lblError.Text = mensaje;   // No se guarda nada si la conexión falla.
            return;
        }

        ConfigService.Save(config);
        Result = config;
        DialogResult = DialogResult.OK;
    }
}
