namespace Zproduct
{
    partial class loginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            groupBox1 = new GroupBox();
            btnRegistrar = new Button();
            btnIngresar = new Button();
            dgvUsuarios = new DataGridView();
            txtContrasena = new TextBox();
            label3 = new Label();
            txtUsuario = new TextBox();
            label2 = new Label();
            label1 = new Label();
            timerRefresco = new System.Windows.Forms.Timer(components);
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.White;
            groupBox1.Controls.Add(btnRegistrar);
            groupBox1.Controls.Add(btnIngresar);
            groupBox1.Controls.Add(dgvUsuarios);
            groupBox1.Controls.Add(txtContrasena);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtUsuario);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(172, 41);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(506, 382);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // btnRegistrar
            // 
            btnRegistrar.BackColor = Color.MediumSeaGreen;
            btnRegistrar.FlatAppearance.BorderSize = 0;
            btnRegistrar.Font = new Font("Inter", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRegistrar.Location = new Point(41, 167);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(140, 28);
            btnRegistrar.TabIndex = 7;
            btnRegistrar.Text = "Crear administrador";
            btnRegistrar.UseVisualStyleBackColor = false;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // btnIngresar
            // 
            btnIngresar.BackColor = Color.MediumSeaGreen;
            btnIngresar.FlatAppearance.BorderSize = 0;
            btnIngresar.Font = new Font("Inter", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnIngresar.Location = new Point(220, 167);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(82, 28);
            btnIngresar.TabIndex = 6;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = false;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarios.Location = new Point(41, 226);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.Size = new Size(424, 150);
            dgvUsuarios.TabIndex = 5;
            // 
            // txtContrasena
            // 
            txtContrasena.Location = new Point(115, 137);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.PasswordChar = '*';
            txtContrasena.Size = new Size(172, 24);
            txtContrasena.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Roboto", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(41, 141);
            label3.Name = "label3";
            label3.Size = new Size(70, 14);
            label3.TabIndex = 3;
            label3.Text = "Contraseña";
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(115, 91);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(172, 24);
            txtUsuario.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Roboto", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(41, 95);
            label2.Name = "label2";
            label2.Size = new Size(49, 14);
            label2.TabIndex = 1;
            label2.Text = "Usuario";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Inter Medium", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(71, 20);
            label1.Name = "label1";
            label1.Size = new Size(189, 39);
            label1.TabIndex = 0;
            label1.Text = "Iniciar Sesión";
            // 
            // loginForm
            // 
            AcceptButton = btnIngresar;
            AutoScaleDimensions = new SizeF(8F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(5, 116, 84);
            ClientSize = new Size(914, 450);
            Controls.Add(groupBox1);
            Font = new Font("Segoe UI Black", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "loginForm";
            Text = "Form1";
            timerRefresco.Tick += timerRefresco_Tick;
            Load += LoginForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private TextBox txtUsuario;
        private DataGridView dgvUsuarios;
        private TextBox txtContrasena;
        private Label label3;
        private Button btnIngresar;
        private Button btnRegistrar;
        private System.Windows.Forms.Timer timerRefresco;
    }
}
