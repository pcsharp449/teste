
namespace Lojinha
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnFuncionarios = new System.Windows.Forms.Button();
            this.btnRelatorio = new System.Windows.Forms.Button();
            this.btnCaixa = new System.Windows.Forms.Button();
            this.btnMovimentacoes = new System.Windows.Forms.Button();
            this.btnClientes = new System.Windows.Forms.Button();
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.btnProdutos = new System.Windows.Forms.Button();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.btnMaximizar = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.btnCloseChildForm = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelDesktoppane = new System.Windows.Forms.Panel();
            this.panelMenu.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.panelTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.panelMenu.Controls.Add(this.btnFuncionarios);
            this.panelMenu.Controls.Add(this.btnRelatorio);
            this.panelMenu.Controls.Add(this.btnCaixa);
            this.panelMenu.Controls.Add(this.btnMovimentacoes);
            this.panelMenu.Controls.Add(this.btnClientes);
            this.panelMenu.Controls.Add(this.btnUsuarios);
            this.panelMenu.Controls.Add(this.btnProdutos);
            this.panelMenu.Controls.Add(this.panelLogo);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(220, 523);
            this.panelMenu.TabIndex = 0;
            // 
            // btnFuncionarios
            // 
            this.btnFuncionarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnFuncionarios.FlatAppearance.BorderSize = 0;
            this.btnFuncionarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFuncionarios.ForeColor = System.Drawing.Color.White;
            this.btnFuncionarios.Image = global::Lojinha.Properties.Resources.icons8_workers_50;
            this.btnFuncionarios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFuncionarios.Location = new System.Drawing.Point(0, 440);
            this.btnFuncionarios.Name = "btnFuncionarios";
            this.btnFuncionarios.Size = new System.Drawing.Size(220, 60);
            this.btnFuncionarios.TabIndex = 7;
            this.btnFuncionarios.Text = "    Funcionarios";
            this.btnFuncionarios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFuncionarios.UseVisualStyleBackColor = true;
            this.btnFuncionarios.Click += new System.EventHandler(this.btnFuncionarios_Click);
            // 
            // btnRelatorio
            // 
            this.btnRelatorio.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRelatorio.FlatAppearance.BorderSize = 0;
            this.btnRelatorio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRelatorio.ForeColor = System.Drawing.Color.White;
            this.btnRelatorio.Image = global::Lojinha.Properties.Resources.report;
            this.btnRelatorio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRelatorio.Location = new System.Drawing.Point(0, 380);
            this.btnRelatorio.Name = "btnRelatorio";
            this.btnRelatorio.Size = new System.Drawing.Size(220, 60);
            this.btnRelatorio.TabIndex = 6;
            this.btnRelatorio.Text = "    Relatorios";
            this.btnRelatorio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRelatorio.UseVisualStyleBackColor = true;
            this.btnRelatorio.Click += new System.EventHandler(this.btnRelatorio_Click);
            // 
            // btnCaixa
            // 
            this.btnCaixa.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCaixa.FlatAppearance.BorderSize = 0;
            this.btnCaixa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaixa.ForeColor = System.Drawing.Color.White;
            this.btnCaixa.Image = global::Lojinha.Properties.Resources.box;
            this.btnCaixa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCaixa.Location = new System.Drawing.Point(0, 320);
            this.btnCaixa.Name = "btnCaixa";
            this.btnCaixa.Size = new System.Drawing.Size(220, 60);
            this.btnCaixa.TabIndex = 5;
            this.btnCaixa.Text = "    Caixa";
            this.btnCaixa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCaixa.UseVisualStyleBackColor = true;
            this.btnCaixa.Click += new System.EventHandler(this.btnCaixa_Click);
            // 
            // btnMovimentacoes
            // 
            this.btnMovimentacoes.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMovimentacoes.FlatAppearance.BorderSize = 0;
            this.btnMovimentacoes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMovimentacoes.ForeColor = System.Drawing.Color.White;
            this.btnMovimentacoes.Image = global::Lojinha.Properties.Resources.icons8_rotate_left_50__3_;
            this.btnMovimentacoes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMovimentacoes.Location = new System.Drawing.Point(0, 260);
            this.btnMovimentacoes.Name = "btnMovimentacoes";
            this.btnMovimentacoes.Size = new System.Drawing.Size(220, 60);
            this.btnMovimentacoes.TabIndex = 4;
            this.btnMovimentacoes.Text = "    Movimentações";
            this.btnMovimentacoes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMovimentacoes.UseVisualStyleBackColor = true;
            this.btnMovimentacoes.Click += new System.EventHandler(this.btnMovimentacoes_Click);
            // 
            // btnClientes
            // 
            this.btnClientes.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnClientes.FlatAppearance.BorderSize = 0;
            this.btnClientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClientes.ForeColor = System.Drawing.Color.White;
            this.btnClientes.Image = global::Lojinha.Properties.Resources.client;
            this.btnClientes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClientes.Location = new System.Drawing.Point(0, 200);
            this.btnClientes.Name = "btnClientes";
            this.btnClientes.Size = new System.Drawing.Size(220, 60);
            this.btnClientes.TabIndex = 3;
            this.btnClientes.Text = "    Clientes";
            this.btnClientes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClientes.UseVisualStyleBackColor = true;
            this.btnClientes.Click += new System.EventHandler(this.btnClientes_Click);
            // 
            // btnUsuarios
            // 
            this.btnUsuarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUsuarios.FlatAppearance.BorderSize = 0;
            this.btnUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsuarios.ForeColor = System.Drawing.Color.White;
            this.btnUsuarios.Image = global::Lojinha.Properties.Resources.peopleGroup;
            this.btnUsuarios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsuarios.Location = new System.Drawing.Point(0, 140);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Size = new System.Drawing.Size(220, 60);
            this.btnUsuarios.TabIndex = 2;
            this.btnUsuarios.Text = "    Usuarios";
            this.btnUsuarios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUsuarios.UseVisualStyleBackColor = true;
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);
            // 
            // btnProdutos
            // 
            this.btnProdutos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProdutos.FlatAppearance.BorderSize = 0;
            this.btnProdutos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProdutos.ForeColor = System.Drawing.Color.White;
            this.btnProdutos.Image = global::Lojinha.Properties.Resources.shoppingCar;
            this.btnProdutos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProdutos.Location = new System.Drawing.Point(0, 80);
            this.btnProdutos.Name = "btnProdutos";
            this.btnProdutos.Size = new System.Drawing.Size(220, 60);
            this.btnProdutos.TabIndex = 1;
            this.btnProdutos.Text = "    Produtos";
            this.btnProdutos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnProdutos.UseVisualStyleBackColor = true;
            this.btnProdutos.Click += new System.EventHandler(this.btnProdutos_Click_1);
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelLogo.Controls.Add(this.label1);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(220, 80);
            this.panelLogo.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(58, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "LOJINHA";
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(193)))), ((int)(((byte)(242)))));
            this.panelTitleBar.Controls.Add(this.btnMaximizar);
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Controls.Add(this.btnMinimizar);
            this.panelTitleBar.Controls.Add(this.btnCloseChildForm);
            this.panelTitleBar.Controls.Add(this.lblTitle);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(220, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(742, 80);
            this.panelTitleBar.TabIndex = 1;
            this.panelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseDown);
            // 
            // btnMaximizar
            // 
            this.btnMaximizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximizar.FlatAppearance.BorderSize = 0;
            this.btnMaximizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaximizar.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaximizar.ForeColor = System.Drawing.Color.White;
            this.btnMaximizar.Location = new System.Drawing.Point(608, 24);
            this.btnMaximizar.Name = "btnMaximizar";
            this.btnMaximizar.Size = new System.Drawing.Size(75, 34);
            this.btnMaximizar.TabIndex = 4;
            this.btnMaximizar.Text = "O";
            this.btnMaximizar.UseVisualStyleBackColor = true;
            this.btnMaximizar.Click += new System.EventHandler(this.btnMaximizar_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(667, 24);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 34);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "O";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimizar.FlatAppearance.BorderSize = 0;
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinimizar.ForeColor = System.Drawing.Color.White;
            this.btnMinimizar.Location = new System.Drawing.Point(553, 24);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(75, 34);
            this.btnMinimizar.TabIndex = 2;
            this.btnMinimizar.Text = "O";
            this.btnMinimizar.UseVisualStyleBackColor = true;
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click);
            // 
            // btnCloseChildForm
            // 
            this.btnCloseChildForm.FlatAppearance.BorderSize = 0;
            this.btnCloseChildForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseChildForm.Image = global::Lojinha.Properties.Resources.icons8_close_50;
            this.btnCloseChildForm.Location = new System.Drawing.Point(3, 12);
            this.btnCloseChildForm.Name = "btnCloseChildForm";
            this.btnCloseChildForm.Size = new System.Drawing.Size(60, 54);
            this.btnCloseChildForm.TabIndex = 1;
            this.btnCloseChildForm.UseVisualStyleBackColor = true;
            this.btnCloseChildForm.Click += new System.EventHandler(this.btnCloseChildForm_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(352, 31);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(70, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "HOME";
            // 
            // panelDesktoppane
            // 
            this.panelDesktoppane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktoppane.Location = new System.Drawing.Point(220, 80);
            this.panelDesktoppane.Name = "panelDesktoppane";
            this.panelDesktoppane.Size = new System.Drawing.Size(742, 443);
            this.panelDesktoppane.TabIndex = 2;
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 523);
            this.Controls.Add(this.panelDesktoppane);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.panelMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(980, 570);
            this.Name = "FrmPrincipal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.panelMenu.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelLogo.PerformLayout();
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnProdutos;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Button btnRelatorio;
        private System.Windows.Forms.Button btnCaixa;
        private System.Windows.Forms.Button btnMovimentacoes;
        private System.Windows.Forms.Button btnClientes;
        private System.Windows.Forms.Button btnUsuarios;
        private System.Windows.Forms.Panel panelTitleBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelDesktoppane;
        private System.Windows.Forms.Button btnFuncionarios;
        private System.Windows.Forms.Button btnCloseChildForm;
        private System.Windows.Forms.Button btnMaximizar;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMinimizar;
    }
}

