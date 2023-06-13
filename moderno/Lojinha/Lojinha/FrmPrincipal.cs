using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lojinha
{
    public partial class FrmPrincipal : Form
    {

        private Button currentButton;
        private Random random;//sorteia as cores
        private int tempIndex;
        private Form activeForm;

        public FormWindowState WindowsState { get; private set; }

        public FrmPrincipal()
        {
            InitializeComponent();
            random = new Random();
            btnCloseChildForm.Visible = false;
            //esvaziar borda - tira todas as bordas e icones padão de janelas
            this.Text = string.Empty;
            this.ControlBox = false;

            //Não ultrapassar a borda da janela de tarefas do computador
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }
        //Redimencionamento
        [DllImport("user32.Dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.Dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        //Metodo p/selecionar uma cor aleatória p/ o tema da lista de cores(pode usar as cores de sua preferencia)
        private Color SelectThemeColor()
        {

            int index = random.Next(ThemeColor.ColorList.Count);
            while (tempIndex == index)
            {
                index = random.Next(ThemeColor.ColorList.Count);
            }
            tempIndex = index;
            string color = ThemeColor.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = SelectThemeColor();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    panelTitleBar.BackColor = color;
                    panelLogo.BackColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                    ThemeColor.PrimaryColor = color;
                    ThemeColor.SecondaryColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                    btnCloseChildForm.Visible = true;

                }
            }
        }

        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(54, 54, 54);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }


        private void openChildForm(Form childForm, object btnSender)
        {
            //se estiver aberto fecha
            if (activeForm != null)
            {
                activeForm.Close();
            }

            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktoppane.Controls.Add(childForm);
            this.panelDesktoppane.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;
        }
        
        private void btnProdutos_Click_1(object sender, EventArgs e)
        {
            //chmar o metodo ativar botao
            // ActivateButton(sender);testar cores
            //Abrir o método do formulario
            //o nome da pasta. nome form
            openChildForm(new Forms.FrmProdutos(), sender);
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            //chmar o metodo ativar botao
            //ActivateButton(sender);
            openChildForm(new Forms.FrmUsuarios(), sender);

        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            //chmar o metodo ativar botao
            // ActivateButton(sender);
            openChildForm(new Forms.FrmClientes(), sender);

        }

        private void btnMovimentacoes_Click(object sender, EventArgs e)
        {
            //chmar o metodo ativar botao
            //ActivateButton(sender);
            openChildForm(new Forms.FrmMovimentacoes(), sender);

        }

        private void btnCaixa_Click(object sender, EventArgs e)
        {
            //chmar o metodo ativar botao
            //ActivateButton(sender);
            openChildForm(new Forms.FrmCaixa(), sender);

        }

        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            //chmar o metodo ativar botao
            // ActivateButton(sender);
            openChildForm(new Forms.FrmRelatorios(), sender);

        }

        private void btnFuncionarios_Click(object sender, EventArgs e)
        {
            openChildForm(new Forms.FrmFuncionarios(), sender);

        }

        private void btnCloseChildForm_Click(object sender, EventArgs e)
        {
            //Sair sse clicar neel e ir pra home
            if(activeForm != null)
                activeForm.Close();

            Reset();
        }

        private void Reset()
        {
            DisableButton();
            lblTitle.Text = "HOME";
            panelTitleBar.BackColor = Color.FromArgb(114, 193, 242);
            panelLogo.BackColor = Color.FromArgb(64, 64, 64);
            currentButton = null;
            btnCloseChildForm.Visible = false;
        }

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }



        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;

            }

        }
    }
}
