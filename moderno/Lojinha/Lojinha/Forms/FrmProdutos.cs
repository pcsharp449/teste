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

/*cod int not null primary key auto_increment,
codBarras varchar(200),
nome varchar(100),
descricao varchar(300),
fornecedor varchar(200),
valorUnitário double,
QtdItens int,
categoria varchar(100),
valorTotal double,
data date*/

namespace Lojinha.Forms
{
    public partial class FrmProdutos : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;
        string alterouImagem = "não";
        string cod;
        string codBarrasAntigo;
        //var que recebe a imagem
        string foto;
        int a;
        double b;
        double total;


        public FrmProdutos()
        {
            InitializeComponent();

        }


        private void FrmProdutos_Load(object sender, EventArgs e)
        {
            //se não colocar essa função vai dar erro se o campo de foto estiver vazio
            LimparFoto();
            Listar();
            ListarFornecedor();
            ListarCategoria();
            alterouImagem = "nao";//sempre que carregar o form ela é não
            cbCategoria.Text = "Selecione";
            cbFornecedor.Text = "Selecione";



            this.grid.Columns[5].DefaultCellStyle.Format = "N2";
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
            image.Image = Properties.Resources.addProduct;
            //se não escolher foto nenhuma, ele salva no banco essa foto
            foto = "img/addProduct.png";//pega a imagem dentro da pasta C:\C#\CadastroProdutos\CadastroProdutos\bin\Debug\img
        }

        private void habilitarCampos()
        {
            //habilita todos os campos só não o salvar, você não salva algo se nõa estiver criando
            btnSalvar.Enabled       = true;
            txtNome.Enabled         = true;
            txtCodBarras.Enabled    = true;
            txtNome.Enabled         = true;
            txtDescricao.Enabled    = true;
            cbFornecedor.Enabled    = true;
            btnFoto.Enabled         = true;
            cbCategoria.Enabled     = true;
            txtQtdItens.Enabled     = true;
            txtValorUnitario.Enabled = true;

            btnNovo.Enabled         = false;
            btnEditar.Enabled       = false;
            btnExcluir.Enabled      = false;
            btnCancelar.Enabled     = true;

        }
        private void desabilitarCampos()
        {
            txtCodBarras.Enabled = false;
            txtNome.Enabled = false;
            txtDescricao.Enabled = false;
            cbFornecedor.Enabled = false;
            btnFoto.Enabled = false;
            cbCategoria.Enabled = false;
            txtQtdItens.Enabled = false;
            txtValorUnitario.Enabled = false;

        }
        private void LimparCampos()
        { 
            txtCodBarras.Text       = "";
            txtNome.Text            = "";
            txtDescricao.Text       = "";
            cbFornecedor.Text       = "Selecione";
            cbCategoria.Text        = "Selecione";
            txtQtdItens.Text        = "0";
            txtValorUnitario.Text   = "0.0";
            LimparFoto();

        }
        //método listar dados
        private void Listar()
        {
            try
            {
            con.AbrirConexao();
            sql = "SELECT * FROM produtos ORDER BY nome asc";//ordena ´pelo nome em ordem alfabetica
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
            grid.Columns[0].HeaderText = "COD";
            grid.Columns[1].HeaderText = "COD.Barras.";
            grid.Columns[2].HeaderText = "Nome";
            grid.Columns[3].HeaderText = "Descricao";
            grid.Columns[4].HeaderText = "Fornecedor";
            grid.Columns[5].HeaderText = "Valor Unit.";
            grid.Columns[6].HeaderText = "Qtd.Itens";
            grid.Columns[7].HeaderText = "Categoria";
            grid.Columns[8].HeaderText = "Valor Total";
            grid.Columns[10].HeaderText = "DATA";
            //Define uma largura pro id
            grid.Columns[0].Width = 50;
            //DEFINE A LARGURA DA IMG
            grid.Columns[9].Width = 50;
            grid.Columns[10].Width = 100;
            grid.Columns[9].Visible = false;
        }

        private void ListarFornecedor()
        {
            try
            {
            con.AbrirConexao();
            sql = "SELECT * FROM fornecedores ORDER BY  nome asc";// ´pelo nome em ordem alfabetica
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            //Lista a tabela
            DataTable dt = new DataTable();
            //pega os campos, fill são os campos, ele pega os campos no dt
            da.Fill(dt);
            cbFornecedor.DataSource = dt;
            //cbCargos.ValueMember = "id";
            cbFornecedor.DisplayMember = "nome";//nome tabela

            //Trazer os dados do
            con.FecharConexao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex);
            }


        }

        //Categoria
        private void ListarCategoria()
        {
            try
            {
            con.AbrirConexao();
            sql = "SELECT * FROM categorias ORDER BY categoria asc";// ´pelo nome em ordem alfabetica
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            //Lista a tabela
            DataTable dt = new DataTable();
            //pega os campos, fill são os campos, ele pega os campos no dt
            da.Fill(dt);
            cbCategoria.DataSource = dt;
            //cbCargos.ValueMember = "id";
            cbCategoria.DisplayMember = "categoria";//nome tabela

            //Trazer os dados do dt


            con.FecharConexao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex);
            }

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
            txtCodBarras.Focus();
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            if (txtNome.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o campo", "Cadastro Produtos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Text = "";
                txtNome.Focus();
                return;
            }
            if (txtCodBarras.Text == "")
            {
                MessageBox.Show("Preencha o campo", "Cadastro Produtos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodBarras.Focus();
                return;
            }
            a = int.Parse(txtQtdItens.Text);
            b = double.Parse(txtValorUnitario.Text , CultureInfo.InvariantCulture);
            total = a * b;
            //botão editar
            con.AbrirConexao();

            if (alterouImagem == "sim")
            {
                //ela vem como sim quando no botão de imagem
                try
                {

                    sql = "UPDATE produtos SET codBarras = @codBarras, nome = @nome, descricao =  @descricao, fornecedor = @fornecedor, valorUnitario = @valorUnitario, " +
                        "QtdItens = @QtdItens, categoria = @categoria, valorTotal = @valorTotal, foto = @foto , data = curDate() WHERE cod = @cod)";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@cod", cod);
                    cmd.Parameters.AddWithValue("@codBarras", txtCodBarras.Text);
                    cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@descricao", txtDescricao.Text);
                    cmd.Parameters.AddWithValue("@fornecedor", cbFornecedor.Text); ;
                    cmd.Parameters.AddWithValue("@valorUnitario", txtValorUnitario.Text);//select
                    cmd.Parameters.AddWithValue("@QtdItens", txtQtdItens.Text);
                    cmd.Parameters.AddWithValue("@categoria", cbCategoria.Text);
                    total = double.Parse(total.ToString("F2", CultureInfo.InvariantCulture));
                    cmd.Parameters.AddWithValue("@valorTotal", total);
                    cmd.Parameters.AddWithValue("@foto", img());
                }
                catch(Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex);
                }

            }
            else if (alterouImagem == "nao")//se não clicou ele não altera a imagem
            {
                try
                {
                    sql = "UPDATE produtos SET codBarras = @codBarras, nome = @nome, descricao =  @descricao, fornecedor = @fornecedor, valorUnitario = @valorUnitario, " +
                    "QtdItens = @QtdItens, categoria = @categoria, valorTotal = @valorTotal, data = curDate() WHERE cod = @cod";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@cod", cod);
                    cmd.Parameters.AddWithValue("@codBarras", txtCodBarras.Text);
                    cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@descricao", txtDescricao.Text);
                    cmd.Parameters.AddWithValue("@fornecedor", cbFornecedor.Text); ;
                    cmd.Parameters.AddWithValue("@valorUnitario", txtValorUnitario.Text);//select
                    cmd.Parameters.AddWithValue("@QtdItens", txtQtdItens.Text);
                    cmd.Parameters.AddWithValue("@categoria", cbCategoria.Text);
                    total = double.Parse(total.ToString("F2", CultureInfo.InvariantCulture));
                    cmd.Parameters.AddWithValue("@valorTotal", total);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex);
                }




            }

            //Verificar se o cpf já existe
            //cpfAntigo que pega o cpf anterior ao editado, ele guarda o valor antigo
            if (txtCodBarras.Text != codBarrasAntigo)
            {
                //Var cmdVerificar
                MySqlCommand cmdVerificar;
                //Que recebe o Mysql command
                cmdVerificar = new MySqlCommand("SELECT * FROM produtos WHERE codBarras = @codBarras", con.con);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmdVerificar;
                cmdVerificar.Parameters.AddWithValue("@codBarras", txtCodBarras.Text);
                DataTable dt = new DataTable();
                //Pega o dt. Rowm a linha capturada e pega o count e conta
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    //Se contem registro , >0, se vier resultado na conulta
                    MessageBox.Show("Cod. produtos já registrado", "Cadastro de Produtos", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCodBarras.Text = "";
                    txtCodBarras.Focus();
                    return;
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

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            //ele impede que o user envie apenas um input com espaços para o banco
            //o trin tira os espaços
            if (txtNome.Text.ToString().Trim() == "")
            {
                //envia um alerta
                //msg - titulo msg - botoes que aparece no caso ok - icone de alerta
                MessageBox.Show("Preencha o campo nome", "Cadastro Produtos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Text = "";
                txtNome.Focus();
                return;//se não colocar o return ele vai pra validaçãod e baixo
            }

            //CPF
            if (txtCodBarras.Text.Trim() == "")
            {
                MessageBox.Show("Preencha o campo Cod. Func", "Cadastro Produtos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodBarras.Focus();
                return;
            }
            //Telefone
            if (txtValorUnitario.Text.Trim() == "")
            {
                MessageBox.Show("Preencha o campo Telefone", "Cadastro Produtos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValorUnitario.Focus();
                return;
            }
            //Valores
            a = int.Parse(txtQtdItens.Text);
            b = double.Parse(txtValorUnitario.Text, CultureInfo.InvariantCulture);
            total = a * b;
            //botão editar
            try
            {
            con.AbrirConexao();

            sql = "INSERT INTO produtos(codBarras, nome, descricao, fornecedor, valorUnitario, QtdItens, categoria, valorTotal, foto, data)" +
                "VALUES(@codBarras, @nome, @descricao, @fornecedor, @valorUnitario, @QtdItens, @categoria, @valorTotal, @foto, curDate())";
            cmd = new MySqlCommand(sql, con.con);

            cmd.Parameters.AddWithValue("@codBarras", txtCodBarras.Text);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@descricao", txtDescricao.Text);
            cmd.Parameters.AddWithValue("@fornecedor", cbFornecedor.Text); 
            cmd.Parameters.AddWithValue("@valorUnitario", txtValorUnitario.Text);//select
            cmd.Parameters.AddWithValue("@QtdItens", txtQtdItens.Text);
            cmd.Parameters.AddWithValue("@categoria", cbCategoria.Text);
            cmd.Parameters.AddWithValue("@valorTotal", total);
            cmd.Parameters.AddWithValue("@foto", img());
            cmd.ExecuteNonQuery();
            con.FecharConexao();

            //Quando salvar ele vai limpar o campo da foto 
            LimparFoto();
            //Listar();
            MessageBox.Show("Registro Salvo com Sucesso!", "Cadastro Produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Deseja realmente excluir o registro?", "Cadastro Produtos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                try
                {
                con.AbrirConexao();
                sql = "DELETE FROM produtos WHERE cod = @cod";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@cod", cod);
                cmd.ExecuteNonQuery();
                con.FecharConexao();
                Listar();
                MessageBox.Show("Registro Excluido com sucesso!", "Cadastro Produtos", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                cod                 = grid.CurrentRow.Cells[0].Value.ToString();//id
                txtCodBarras.Text   = grid.CurrentRow.Cells[1].Value.ToString();
                codBarrasAntigo     = grid.CurrentRow.Cells[1].Value.ToString();//guarda o valor do codFunc
                txtNome.Text        = grid.CurrentRow.Cells[2].Value.ToString();
                txtDescricao.Text   = grid.CurrentRow.Cells[3].Value.ToString();
                cbFornecedor.Text   = grid.CurrentRow.Cells[4].Value.ToString();
                txtValorUnitario.Text = grid.CurrentRow.Cells[5].Value.ToString();
                txtQtdItens.Text      = grid.CurrentRow.Cells[6].Value.ToString();
                cbCategoria.Text      = grid.CurrentRow.Cells[7].Value.ToString();




                //Imagem precisa ser tratada, passada de byte pra imagem
                if (grid.CurrentRow.Cells[9].Value != DBNull.Value)//DBNull.Value campo que vem do DB
                {
                    byte[] imagem = (byte[])grid.Rows[e.RowIndex].Cells[9].Value;//Criado var byte imagem para receber o convertido em byte
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

        private void btnFoto_Click_1(object sender, EventArgs e)
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
            sql = "SELECT * FROM produtos WHERE CONCAT(`cod`, `codBarras`,`nome`, `data` ,`Descricao`, `fornecedor`, `valorUnitario`, `QtdItens`, `categoria`, `valorTotal` ) like '%" +valueToSearch+"%'";//ordena ´pelo nome em ordem alfabetica
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            //pega os campos, fill são os campos, ele pega os campos no dt
            da.Fill(dt);
            grid.DataSource = dt;
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
                sql = "SELECT * FROM produtos WHERE CONCAT(codFunc, nome) LIKE '%" + valueToSearch + "%'";
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
    }
}
