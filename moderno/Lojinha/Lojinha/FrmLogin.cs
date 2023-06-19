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
using MySql.Data.MySqlClient;
using System.Drawing.Drawing2D;


namespace Lojinha
{
    public partial class FrmLogin : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;
        string id;
        
        
        public FrmLogin()
        {

            InitializeComponent();
        }

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        public static extern void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern void SendMessage(System.IntPtr hwnd, int wmsg, int vparam, int lparam);

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            //timer1.Start();


        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            try
            { if (txtCod.Text.Trim() == "")
                {
                    txtCod.Text = "";
                    return;
                }
                txtCod.ForeColor = Color.Black;
                InvalidUsername.Visible = false;

            }
            catch { }
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPass.Text.Trim() == "" || txtPass.Text == "Senha")
                {
                    
                    return;
                }
                txtPass.ForeColor = Color.Black;
                InvalidPassword.Visible = false;
                if (checkBox.Checked)
                {
                    txtPass.UseSystemPasswordChar = false;
                    txtPass.Focus();
                }
                else
                {
                    txtPass.UseSystemPasswordChar = true;
                    txtPass.Focus();
                }




            }
            catch { }
        }

        private void txtUsername_Click(object sender, EventArgs e)
        {
            txtCod.SelectAll();
        }

        private void txtPass_Click(object sender, EventArgs e)
        {
            txtPass.SelectAll();

        }




        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        System.Windows.Forms.Timer tiner = new System.Windows.Forms.Timer();

        private void timer1_Tick(object sender, EventArgs e)
        {

            InvalidNomeUser.Visible = InvalidPassword.Visible = InvalidSenha.Visible = InvalidCodFunc.Visible = InvalidConfirmSenha.Visible = false;
        }




        private void txtNomeUser_TextChanged_1(object sender, EventArgs e)
        {
            txtNomeUser.ForeColor = Color.Black;
        }

        private void txtCodFunc_TextChanged_1(object sender, EventArgs e)
        {
            txtCodFunc.ForeColor = Color.Black;
        }

        private void txtPassword_TextChanged_1(object sender, EventArgs e)
        {
            txtPassword.ForeColor = Color.Black;
        }

        private void txtConfirmPassword_TextChanged_1(object sender, EventArgs e)
        {
            txtConfirmPassword.ForeColor = Color.Black;
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            if (txtCod.Text == "Codigo do Funcionário")
            {
                InvalidUsername.Visible = true;
                txtCod.Focus();
                return;
            }
            if (txtPass.Text == "Senha")
            {
                InvalidPassword.Visible = true;
                txtPass.Focus();

                return;

            }

            chamarLogin();
        }

        private void btnSignup_Click_1(object sender, EventArgs e)
        {

            if (txtNomeUser.Text.Trim() == "Nome Usuário" || txtNomeUser.Text.Trim() == "")
            {
                InvalidNomeUser.Visible = true;
                txtNomeUser.Focus();
                txtNomeUser.SelectAll();
                return;
            }
            if (txtCodFunc.Text.Trim() == "Código do Funcionário" || txtCodFunc.Text.Trim() == "")
            {
                InvalidCodFunc.Visible = true;
                txtCodFunc.Focus();
                txtCodFunc.SelectAll();
                return;
            }
            if (txtPassword.Text.Trim() == "Senha" || txtPassword.Text.Trim() == "")
            {
                InvalidSenha.Visible = true;
                txtPassword.Focus();
                txtPassword.SelectAll();
                return;
            }
            if (txtConfirmPassword.Text.Trim() == "Confirmar Senha" || txtConfirmPassword.Text.Trim() == "")
            {
                InvalidConfirmSenha.Visible = true;
                txtConfirmPassword.Focus();
                txtConfirmPassword.SelectAll();
                return;
            }
            //Cadastro User
            //Verificar se os dados do cadastro já existem
            try
            {
                con.AbrirConexao();

                sql = "INSERT INTO funcionarios(nomeUser, senha, codFunc, data)" +
                    "VALUES(@nomeUser, @senha, @codFunc, curDate())";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@nomeUser", txtNomeUser.Text);
                cmd.Parameters.AddWithValue("@senha", txtPassword.Text);
                cmd.Parameters.AddWithValue("@codFunc", txtCodFunc.Text);
                cmd.ExecuteNonQuery();
                con.FecharConexao();

                MessageBox.Show("Cadastro com Sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Quando clicar em salvar
                txtNomeUser.Text = "";
                txtPassword.Text = "";
                txtConfirmPassword.Text = "";
                txtCodFunc.Text = "";
                //Ao salvar ele limpa todos os campos
                IrLogin();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR " + ex);
            }
            //ele impede que o user envie apenas um input com espaços para o banco
            //o trin tira os espaços


            //CPF

            //Telefone

            //Insert
          

        }

        private void IrLogin()
        {
            pnlLogin.Visible = true;
            pnlLogin.Dock = DockStyle.Fill;
            pnlSignup.Visible = false;
            pnlLogo.Dock = DockStyle.Left;
        }



        //ou cadastre-se
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlLogin.Visible = false;
            pnlSignup.Visible = true;
            pnlLogo.Dock = DockStyle.Left;
            pnlSignup.Dock = DockStyle.Fill;


        }

        //ja tem uma conta?
        private void lblLinkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlLogin.Visible = true;
            pnlLogin.Dock = DockStyle.Fill;
            pnlSignup.Visible = false;
            pnlLogo.Dock = DockStyle.Left;
        }

        /*Banco de dados*/
        private void chamarLogin()
        {
            //Verificar os campos
            if (txtCod.Text.Trim() == "" || txtPass.Text.Trim() == "")
            {
                MessageBox.Show("Campos Inválidos!");
                return;
            }

            try
            {
                con.AbrirConexao();
                MySqlCommand cmdVerificar;
                MySqlDataReader reader;//Com o reader vou conseguir extrair dados da tabela e usar em outro form
                cmdVerificar = new MySqlCommand("SELECT * FROM usuarios WHERE codFunc = @codFunc AND senha = @senha", con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmdVerificar;
                cmdVerificar.Parameters.AddWithValue("@codFunc", txtCod.Text);
                cmdVerificar.Parameters.AddWithValue("@senha", txtPass.Text);
                reader = cmdVerificar.ExecuteReader();
                con.FecharConexao();
                if (reader.HasRows)
                {
                    //se o que foi pego tem linha
                    //enquanto o reader

                    con.AbrirConexao();
                        //Com o reader vou conseguir extrair dados da tabela e usar em outro form
                        cmdVerificar = new MySqlCommand("SELECT * FROM funcionarios WHERE codFunc = @codFunc", con.con);
                        da.SelectCommand = cmdVerificar;
                        cmdVerificar.Parameters.AddWithValue("@codFunc", txtCod.Text);
                        reader = cmdVerificar.ExecuteReader();
                        //pega o Verificar.cs, ele extrai da tabela e Guarda na var dentro da Classe Verificar
                        while (reader.Read())
                        {
                            Verificar.NomeUser = Convert.ToString(reader["nome"]);
                            Verificar.CargoUser = Convert.ToString(Convert.ToString(reader["cargo"]));
                        }

                       
                        // frm.FormClosed += encerraSessao;
                        this.Hide();//oculta o atual
                     con.FecharConexao();

                    
                }
                else
                {
                    MessageBox.Show("Usuário ou senha inválidos!");
                    txtCod.Text = "";
                    txtPass.Text = "";
                    txtCod.Focus();
                    return;
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR " + ex);
            }

            FrmPrincipal frm = new FrmPrincipal();
            frm.ShowDialog();

            //Chamar banco de dados

        }

        private void btnAcessar_Click(object sender, EventArgs e)
        {
            chamarLogin();
        }

        private void pnlLogin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Entre com os dados [04, masterkey], código de funcionário e senha respectivamente ");
        }



        //Metodo p/selecionar uma cor aleatória p/ o tema da lista de cores(pode usar as cores de sua preferencia)
    }

}


