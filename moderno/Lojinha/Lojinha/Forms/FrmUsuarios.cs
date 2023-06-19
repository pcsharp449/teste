using System;
using MySql.Data.MySqlClient;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Lojinha.Forms
{
    public partial class FrmUsuarios : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;
        string alterouImagem = "não";
        string id;
        //var que recebe a imagem
        string foto;

        public FrmUsuarios()
        {
            InitializeComponent();
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            LimparFoto();
            Listar();
            Cancelar();

            alterouImagem = "nao";//sempre que carregar o form ela é não
            txtSenha.Visible = false;
            lbSenha.Visible = false;
            id = grid.CurrentRow.Cells[0].Value.ToString();//id



            // this.grid.Columns[5].DefaultCellStyle.Format = "N2";
            this.grid.DefaultCellStyle.ForeColor = Color.White;
            this.grid.DefaultCellStyle.BackColor = Color.FromArgb(0, 74, 173);
            this.grid.DefaultCellStyle.Font = new Font("Arial", 9);
            this.grid.EnableHeadersVisualStyles = false;
            this.grid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
            this.grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }


        private void LimparFoto()
        {
            //o componnete imgae, sua propriedade image, do caminho resource(que guarda as imagens)
            image.Image = Properties.Resources._12_sPx13ICSI_transformed;
            //se não escolher foto nenhuma, ele salva no banco essa foto
            foto = "img/imagem.png";//pega a imagem dentro da pasta C:\C#\CadastroProdutos\CadastroProdutos\bin\Debug\img
        }

        private void habilitarCampos()
        {
            //habilita todos os campos só não o salvar, você não salva algo se nõa estiver criando
            btnSalvar.Enabled = true;
            txtNome.Enabled = true;
            txtSenha.Enabled = true;
            txtCodFunc.Enabled = true;

            btnNovo.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = true;

        }
        private void desabilitarCampos()
        {
            txtNome.Enabled = false;
            txtSenha.Enabled = false;
            txtCodFunc.Enabled = false;

        }
        private void LimparCampos()
        {
            txtNome.Text = "";
            txtSenha.Text = "";
            txtCodFunc.Text = "";
            LimparFoto();

        }
        //método listar dados
        private void Listar()
        {
            try
            {
                con.AbrirConexao();
                sql = "SELECT * FROM usuarios ORDER BY nome asc";//ordena ´pelo nome em ordem alfabetica
                cmd = new MySqlCommand(sql, con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                //Lista a tabela
                DataTable dt = new DataTable();
                //pega os campos, fill são os campos, ele pega os campos no dt
                da.Fill(dt);
                //Trazer os dados do dt
                grid.DataSource = dt;

                con.FecharConexao();
                FormatarGD();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex);
            }


        }

        //Formatar Grid

        private void FormatarGD()
        {
            //é como um array
            grid.Columns[0].HeaderText = "ID";
            grid.Columns[3].HeaderText = "COD.Func.";
            grid.Columns[1].HeaderText = "Nome";
            grid.Columns[2].HeaderText = "Senha";
            grid.Columns[4].HeaderText = "foto";
            grid.Columns[5].HeaderText = "Data";

            grid.Columns[2].Visible = false;
            //Define uma largura pro id
            grid.Columns[0].Width = 50;
            //DEFINE A LARGURA DA IMG
            grid.Columns[4].Width = 50;
            //grid.Columns[10].Width = 100;
            grid.Columns[4].Visible = false;
        }

   


        //Metodo que trata a imagem

        //o array byte pega o comprimenta dessa imagem
        //Sempre que for salvar uma imagem no banco usa esse código
        private byte[] img()
        {
            byte[] imagem_byte = null;
            //se o caminho for vazio
            if (foto == "")
            {
                //se não escolher foto nenhuma, ele salva no banco essa foto
                return null;
            }
            //metodo padrão: precisa do caminho da foto, tipo do arquivo, e o cesso do arquivo
            FileStream fa = new FileStream(foto, FileMode.Open, FileAccess.Read);

            BinaryReader br = new BinaryReader(fa);

            imagem_byte = br.ReadBytes((int)fa.Length);
            //o metodo retorna a image_byte, e é justamente ela qu eé salvada no banco
            return imagem_byte;
        }
        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            //verifica se tem dados,´para não cair o sistema
            if (e.RowIndex > -1)
            {
                habilitarCampos();
                btnEditar.Enabled = true;
                btnExcluir.Enabled = true;
                btnSalvar.Enabled = false;
                btnNovo.Enabled = false;

                //captura os componentes
                //pega o valor no array grid 1, já que é string converte em string

                id              = grid.CurrentRow.Cells[0].Value.ToString();//id
                txtCodFunc.Text = grid.CurrentRow.Cells[3].Value.ToString();
                txtNome.Text    = grid.CurrentRow.Cells[1].Value.ToString();
                txtSenha.Text   = grid.CurrentRow.Cells[2].Value.ToString();






                //Imagem precisa ser tratada, passada de byte pra imagem
                if (grid.CurrentRow.Cells[4].Value != DBNull.Value)//DBNull.Value campo que vem do DB
                {
                    byte[] imagem = (byte[])grid.Rows[e.RowIndex].Cells[4].Value;//Criado var byte imagem para receber o convertido em byte
                    MemoryStream ms = new MemoryStream(imagem);//recebe a var byte q já contem o valor da grid(decodificar/ convertido)

                    //o componente image sempre pede um System.Dialog entao
                    image.Image = Image.FromStream(ms);
                }
                else
                {
                    image.Image = Properties.Resources._12_sPx13ICSI_transformed;//coloca a imagem perfil na picture do form
                }

            }
            else
            {
                return;
            }
        }


        private void Cancelar()
        {
            //Cancelar
            btnNovo.Enabled = true;
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnCancelar.Enabled = false;
            grid.Columns[2].Visible = false;
            lbSenha.Visible = false;
            txtSenha.Visible = false;

            desabilitarCampos();
            LimparCampos();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            //habilitar campos
       
            habilitarCampos();
            LimparCampos();
            txtSenha.Visible = true;
            lbSenha.Visible = true;
            EsquecerSenha.Visible = false;
            //posiciona o cursor em nome ao clicar em novo
            txtCodFunc.Focus();
        }

        //Verificar se já existe um ususario com aquele código

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            txtCodFunc.Enabled = false;
            txtSenha.Visible = true;
            lbSenha.Visible = true;
            grid.Columns[2].Visible = true ;

            if (txtNome.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o campo", "Cadastro Produtos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Text = "";
                txtNome.Focus();
                return;
            }


            con.AbrirConexao();

            if (alterouImagem == "sim")
            {
                //ela vem como sim quando no botão de imagem
                try
                {

                    sql = "UPDATE usuarios SET  nome = @nome, senha =  @senha, foto = @foto , data = curDate() WHERE id = @id)";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@senha", txtSenha.Text);
                    cmd.Parameters.AddWithValue("@foto", img());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex);
                }

            }
            else if (alterouImagem == "nao")//se não clicou ele não altera a imagem
            {
                try
                {
                    sql = "UPDATE usuarios SET  nome = @nome, senha =  @senha, data = curDate() WHERE id = @id)";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@senha", txtSenha.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex);
                }
            }

            cmd.ExecuteNonQuery();
            con.FecharConexao();
            Listar();

            MessageBox.Show("Registro Editada com sucesso!", "Cadastro de Produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnNovo.Enabled = true;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = false;
            desabilitarCampos();
            LimparCampos();
            LimparFoto();
            alterouImagem = "nao";//p/ uso de editar imagem
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtCodFunc.Text.Trim() != "")
            {
                try
                {
                    con.AbrirConexao();
                    MySqlCommand cmdVerificar;
                    MySqlDataReader reader;//Com o reader vou conseguir extrair dados da tabela e usar em outro form
                    
                    cmdVerificar = new MySqlCommand("SELECT* FROM funcionarios WHERE codFunc = @codFunc", con.con);
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmdVerificar;
                    cmdVerificar.Parameters.AddWithValue("@codFunc", txtCodFunc.Text);
                    reader = cmdVerificar.ExecuteReader();
                    con.FecharConexao();

                    if (reader.HasRows)
                    {
                        //se o que foi pego tem linha
                        //enquanto o reader
                        con.AbrirConexao();
                        //Com o reader vou conseguir extrair dados da tabela e usar em outro form
                        cmdVerificar = new MySqlCommand("SELECT * FROM usuarios WHERE codFunc = @codFunc", con.con);
                        da.SelectCommand = cmdVerificar;
                        cmdVerificar.Parameters.AddWithValue("@codFunc", txtCodFunc.Text);
                        reader = cmdVerificar.ExecuteReader();
                        //pega o Verificar.cs, ele extrai da tabela e Guarda na var dentro da Classe Verificar
                        if (reader.HasRows)
                        {
                            MessageBox.Show("Código de Funcionario já em utilização!");
                            txtCodFunc.Text = "";
                            txtNome.Text = "";
                            txtSenha.Text = "";
                            txtCodFunc.Focus();
                            return;
                        }
                        // frm.FormClosed += encerraSessao;
                        //this.Hide();//oculta o atual
                        con.FecharConexao();
                    }
                    else
                    {
                        MessageBox.Show("Código de Funcionario Inválido");
                        txtCodFunc.Text = "";
                        txtNome.Text = "";
                        txtSenha.Text = "";
                        txtCodFunc.Focus();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex);
                }
            }

            //ele impede que o user envie apenas um input com espaços para o banco
            //o trin tira os espaços
            if (txtNome.Text.ToString().Trim() == "")
            {
                //envia um alerta
                //msg - titulo msg - botoes que aparece no caso ok - icone de alerta
                MessageBox.Show("Preencha o campo nome", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Text = "";
                txtNome.Focus();
                return;//se não colocar o return ele vai pra validaçãod e baixo
            }

            //Telefone
            if (txtSenha.Text.Trim() == "")
            {
                MessageBox.Show("Preencha o campo Senha", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSenha.Focus();
                return;
            }
            if (txtSenha.Text.Length <= 4 || txtSenha.Text == "1234")
            {
                senhaFacil.Visible = true;
            }

            try
            {
                con.AbrirConexao();

                sql = "INSERT INTO usuarios (codFunc, nome, senha, foto, data)" +
                    "VALUES(@codFunc, @nome, @senha, @foto, curDate())";
                cmd = new MySqlCommand(sql, con.con);

                cmd.Parameters.AddWithValue("@codFunc", txtCodFunc.Text);
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@senha", txtSenha.Text);
                cmd.Parameters.AddWithValue("@foto", img());
                cmd.ExecuteNonQuery();
                con.FecharConexao();

                //Quando salvar ele vai limpar o campo da foto 
                LimparFoto();
                //Listar();
                MessageBox.Show("Registro Salvo com Sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Quando clicar em salvar
                btnNovo.Enabled = true;
                btnSalvar.Enabled = false;
                btnEditar.Enabled = true;
                btnExcluir.Enabled = true;
                btnCancelar.Enabled = false;
                //Ao salvar ele limpa todos os campos
                LimparCampos();
                Listar();
                desabilitarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Deseja realmente excluir o registro?", "Cadastro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                try
                {
                    con.AbrirConexao();
                    sql = "DELETE FROM usuarios WHERE id = @id";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    con.FecharConexao();
                    Listar();
                    MessageBox.Show("Registro Excluido com sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex);
                }

            }
            else
            {
                //Cancelar
                Cancelar();
            }

            //Precisa perguntar se realmente deseja excluir
        }

        private void btnFoto_Click(object sender, EventArgs e)
        {
            //metodo open file
            //Abre uma caixa de dialogo que pergunta o vou querer fazer
            OpenFileDialog dialog = new OpenFileDialog();
            //diolog filter - "Arquivos .jpg, png, all"
            dialog.Filter = "Image Files(*.jpg *.png) | *.jpg;*.png";//mostra jpg e png
                                                                     //se clicar em abrir ele verifica
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foto = dialog.FileName.ToString();
                //nome do componte picture.Caminhoda imagem e foto recebe ess caminho
                image.ImageLocation = foto;//pega o caminho da imagem
                                           //alteraImagem = "sim";//para editar a imagem
            }
            alterouImagem = "sim";//quando clica vira sim
        }


        public void searchData(string valueToSearch)
        {
            
        }

        private void GridSearchLoad(object sender, EventArgs e)
        {
            searchData("");
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string valueToSearch = txtSearch.Text;
            try
            {
                con.AbrirConexao();
                sql = "SELECT * FROM usuarios WHERE CONCAT(codFunc, nome) LIKE '%"+valueToSearch+"%'";
                cmd = new MySqlCommand(sql, con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                //pega os campos, fill são os campos, ele pega os campos no dt
                da.Fill(dt);
                grid.DataSource = dt;
                con.FecharConexao();

            }
            catch(Exception ex)
            {
                MessageBox.Show("ERROR: " + ex);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            senhaFacil.Visible = false;
        }


        private void EsquecerSenha_Click(object sender, EventArgs e)
        {
            if(txtCodFunc.Text.Trim() != "")
            {var resp = MessageBox.Show("Seu código de funcionário é " + txtCodFunc.Text + "?", "Ver Senha", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if(resp == DialogResult.Yes)
                {
                    grid.Columns[2].Visible = true;
                }
                else
                {
                    return;
                }

            }
            else
            {
                MessageBox.Show("Preencha o Campo Cod.Func.", "Ver Senha", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }
    }
}

