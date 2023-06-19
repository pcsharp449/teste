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
namespace Lojinha.Forms
{
    public partial class FrmFornecedores : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;
        string alterouImagem = "não";
        string id;
        string nomeAntigo;
        //var que recebe a imagem
        string foto;

        public FrmFornecedores()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void FrmFornecedores_Load(object sender, EventArgs e)
        {
            //se não colocar essa função vai dar erro se o campo de foto estiver vazio
            LimparFoto();
            Listar();
            alterouImagem = "nao";//sempre que carregar o form ela é não

            this.grid.DefaultCellStyle.ForeColor = Color.White;
            this.grid.DefaultCellStyle.BackColor = Color.FromArgb(0, 74, 173);
            this.grid.DefaultCellStyle.Font = new Font("Arial", 10);
            this.grid.EnableHeadersVisualStyles = false;
            this.grid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            this.grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void LimparFoto()
        {
            //o componnete imgae, sua propriedade image, do caminho resource(que guarda as imagens)
            image.Image = Properties.Resources._12_sPx13ICSI_transformed;
            //se não escolher foto nenhuma, ele salva no banco essa foto
            foto = "img/perfil.png";//pega a imagem dentro da pasta C:\C#\CadastroProdutos\CadastroProdutos\bin\Debug\img
        }

        private void habilitarCampos()
        {
            //habilita todos os campos só não o salvar, você não salva algo se nõa estiver criando
            btnSalvar.Enabled = true;
            txtNome.Enabled = true;
            txtEndereco.Enabled = true;
            txtTelefone.Enabled = true;
            txtTel2.Enabled = true;

            btnFoto.Enabled = true;
            btnNovo.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = true;

        }
        private void desabilitarCampos()
        {
            txtNome.Enabled = false;
            txtEndereco.Enabled = false;
            txtTelefone.Enabled = false;
            txtTel2.Enabled = false;

        }
        private void LimparCampos()
        {
            txtNome.Text = "";
            txtEndereco.Text = "";
            txtTelefone.Text = "";
            txtTel2.Text = "";
            LimparFoto();

        }
        //método listar dados
        private void Listar()
        {
            con.AbrirConexao();
            sql = "SELECT * FROM fornecedores ORDER BY nome asc";//ordena ´pelo nome em ordem alfabetica
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

        //Formatar Grid

        private void FormatarGD()
        {
            //é como um array
            grid.Columns[0].HeaderText = "ID";
            grid.Columns[1].HeaderText = "Nome";
            grid.Columns[2].HeaderText = "Endereco";
            grid.Columns[3].HeaderText = "Telefone1";
            grid.Columns[4].HeaderText = "Telefone2";
            grid.Columns[5].HeaderText = "Imagem";
            grid.Columns[6].HeaderText = "Data";
            //Define uma largura pro id
            grid.Columns[0].Width = 50;
            //DEFINE A LARGURA DA IMG
            grid.Columns[5].Width = 50;
            grid.Columns[0].Visible = false;
            grid.Columns[5].Visible = false;
        }

        //ao clicar na grid

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


        private void Cancelar()
        {
            //Cancelar
            btnNovo.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnNovo.Enabled = true;
            desabilitarCampos();
            LimparCampos();
        }


        private void btnNovo_Click(object sender, EventArgs e)
        {
            //habilitar campos
            habilitarCampos();
            LimparCampos();
            //posiciona o cursor em nome ao clicar em novo
            txtNome.Focus();
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            if (txtNome.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o campo ", "Cadastro fornecedores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Text = "";
                txtNome.Focus();
                return;
            }
            if (txtEndereco.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o campo ", "Cadastro fornecedores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Text = "";
                txtNome.Focus();
                return;
            }


            //botão editar
            con.AbrirConexao();

            if (alterouImagem == "sim")
            {
                //ela vem como sim quando no botão de imagem

                sql = "UPDATE fornecedores SET nome = @nome, endereco = @endereco, tel1 = @tel1, tel2 = @tel2,  foto = @foto, data = curDate() WHERE id = @id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@tel1", txtTelefone.Text);
                cmd.Parameters.AddWithValue("@tel2", txtTel2.Text);
                cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
                cmd.Parameters.AddWithValue("@foto", img());
            }
            else if (alterouImagem == "nao")//se não clicou ele não altera a imagem
            {

                sql = "UPDATE fornecedores SET nome = @nome, endereco = @endereco, tel1 = @tel1, tel2 = @tel2, data = curDate() WHERE id = @id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@tel1", txtTelefone.Text);
                cmd.Parameters.AddWithValue("@tel2", txtTel2.Text);
                cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);



            }

            //Verificar se o cpf já existe
            //cpfAntigo que pega o cpf anterior ao editado, ele guarda o valor antigo
            if (txtNome.Text != nomeAntigo)
            {
                //Var cmdVerificar
                MySqlCommand cmdVerificar;
                //Que recebe o Mysql command
                cmdVerificar = new MySqlCommand("SELECT * FROM fornecedores WHERE nome = @nome", con.con);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmdVerificar;
                cmdVerificar.Parameters.AddWithValue("@nome", txtNome.Text);
                DataTable dt = new DataTable();
                //Pega o dt. Rowm a linha capturada e pega o count e conta
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    //Se contem registro , >0, se vier resultado na conulta
                    MessageBox.Show("Fornecedor já registrado", "Cadastro de Fornecedores", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtNome.Text = "";
                    txtNome.Focus();
                    return;
                }
            }

            cmd.ExecuteNonQuery();
            con.FecharConexao();
            Listar();

            MessageBox.Show("Registro Editada com sucesso!", "Cadastro de Funcionários", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            if (txtNome.Text.ToString().Trim() == "")
            {
                //envia um alerta
                //msg - titulo msg - botoes que aparece no caso ok - icone de alerta
                MessageBox.Show("Preencha o campo nome", "Cadastro Funcionários", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Text = "";
                txtNome.Focus();
                return;//se não colocar o return ele vai pra validaçãod e baixo
            }
                try
                {
                    con.AbrirConexao();
                    MySqlCommand cmdVerificar;
                    MySqlDataReader reader;//Com o reader vou conseguir extrair dados da tabela e usar em outro form

                    cmdVerificar = new MySqlCommand("SELECT* FROM fornecedores WHERE nome = @nome", con.con);
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmdVerificar;
                    cmdVerificar.Parameters.AddWithValue("@nome", txtNome.Text);
                    reader = cmdVerificar.ExecuteReader();
                    con.FecharConexao();

                    if (reader.HasRows)
                    {
                        MessageBox.Show("Fornecedor já cadastrado");

                        txtNome.Text = "";
                        txtTelefone.Text = "";
                        txtTel2.Text = "";
                        txtEndereco.Text = "";

                        return;

                    }
                }
                
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex);
                }

                //ele impede que o user envie apenas um input com espaços para o banco
                //o trin tira os espaços
            

            //Telefone
            if (txtTelefone.Text == "(  )       -" || txtTelefone.Text.Length < 14)
                {
                    MessageBox.Show("Preencha o campo Telefone", "Cadastro Funcionários", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTelefone.Focus();
                    return;
                }

                //ele impede que o user envie apenas um input com espaços para o banco
                //o trin tira os espaços
                if (txtNome.Text.ToString().Trim() == "")
            {
                //envia um alerta
                //msg - titulo msg - botoes que aparece no caso ok - icone de alerta
                MessageBox.Show("Preencha o campo nome", "Cadastro Funcionários", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Text = "";
                txtNome.Focus();
                return;//se não colocar o return ele vai pra validaçãod e baixo
            }

            //CPF

            //Telefone
            if (txtTelefone.Text == "(  )      -" || txtTelefone.Text.Length < 15)
            {
                MessageBox.Show("Preencha o campo Telefone 1", "Cadastro Fornecedores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefone.Focus();
                return;
            }
            //Telefone
            if (txtTel2.Text == "(  )      -" || txtTel2.Text.Length < 15)
            {
                MessageBox.Show("Preencha o campo Telefone 2", "Cadastro Fornecedores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefone.Focus();
                return;
            }

            try
            {
                con.AbrirConexao();

                sql = "INSERT INTO fornecedores(nome, endereco,  tel1,  tel2, foto, data)" +
                    "VALUES(@nome, @endereco, @tel1, @tel2, @foto, curDate())";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
                cmd.Parameters.AddWithValue("@tel1", txtTelefone.Text);
                cmd.Parameters.AddWithValue("@tel2", txtTel2.Text);
                cmd.Parameters.AddWithValue("@foto", img());
                cmd.ExecuteNonQuery();
                con.FecharConexao();


                //Quando salvar ele vai limpar o campo da foto 
                LimparFoto();
                //Listar();
                MessageBox.Show("Registro Salvo com Sucesso!", "Cadastro fornecedores", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("ERROR " + ex);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Deseja realmente excluir o registro?", "Cadastro Fornecedores", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                con.AbrirConexao();
                sql = "DELETE FROM fornecedores WHERE id = @id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.FecharConexao();
                Listar();
                MessageBox.Show("Registro Excluido com sucesso!", "Cadastro Fornecedores", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimparCampos();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string valueToSearch = txtSearch.Text;
            try
            {
                con.AbrirConexao();
                sql = "SELECT * FROM fornecedores WHERE CONCAT(nome, endereco, tel1, tel2, data) LIKE '%" + valueToSearch + "%'";
                cmd = new MySqlCommand(sql, con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                //pega os campos, fill são os campos, ele pega os campos no dt
                da.Fill(dt);
                grid.DataSource = dt;
                con.FecharConexao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex);
            }
        }

        private void grid_CellClick_1(object sender, DataGridViewCellEventArgs e)
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

                id = grid.CurrentRow.Cells[0].Value.ToString();//i
                nomeAntigo = grid.CurrentRow.Cells[1].Value.ToString();//guarda o valor do codFunc
                txtNome.Text = grid.CurrentRow.Cells[1].Value.ToString();
                txtEndereco.Text = grid.CurrentRow.Cells[2].Value.ToString();
                txtTelefone.Text = grid.CurrentRow.Cells[3].Value.ToString();
                txtTel2.Text = grid.CurrentRow.Cells[4].Value.ToString();



                //Imagem precisa ser tratada, passada de byte pra imagem
                if (grid.CurrentRow.Cells[5].Value != DBNull.Value)//DBNull.Value campo que vem do DB
                {
                    byte[] imagem = (byte[])grid.Rows[e.RowIndex].Cells[5].Value;//Criado var byte imagem para receber o convertido em byte
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
    }
    
}
