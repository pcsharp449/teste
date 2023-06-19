using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System;
using System.Globalization;

namespace Lojinha.Forms
{
    public partial class FrmCaixa : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;
        string idProduto;
        string alterouImagem = "não";
        string cod;
        string codBarrasAntigo;
        int qtd, estoque;
        double preco;
        double subtotal, total;

        //var que recebe a imagem
        string foto;



        public FrmCaixa()
        {
            InitializeComponent();

        }

        private void FrmCaixa_Load(object sender, EventArgs e)
        {
            Listar();
            qtdProdutos.Text = "0";
        }
        private void Listar()
        {
            try
            {
                con.AbrirConexao();
                sql = "SELECT * FROM produtos ORDER BY cod asc";
                cmd = new MySqlCommand(sql, con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                grid.DataSource = dt;
                con.FecharConexao();
                FormatarGD();
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e);
            }

        }

        private void ListarCarrinho()
        {
            try
            {
                con.AbrirConexao();
                sql = "SELECT * FROM carrinho ORDER BY id asc";
                cmd = new MySqlCommand(sql, con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridCarrinho.DataSource = dt;
                con.FecharConexao();
                FormatarGDCar();
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e);
            }
        }

        private void FormatarGDCar()
        {
            //é como um array
            grid.Columns[0].HeaderText = "ID";
            grid.Columns[1].HeaderText = "COD.Barras.";
            grid.Columns[2].HeaderText = "Nome";
            grid.Columns[3].HeaderText = "Qtd.";
            grid.Columns[4].HeaderText = "Valor Unit.";
            grid.Columns[5].HeaderText = "Total";
            grid.Columns[6].HeaderText = "MET.PAG";
            grid.Columns[7].HeaderText = "CLIEN.";
            grid.Columns[8].HeaderText = "DATA";

            grid.Columns[5].Visible = false;
            grid.Columns[6].Visible = false;
            grid.Columns[7].Visible = false;
            //Define uma largura pro id
            //grid.Columns[0].Width = 50;
            //DEFINE A LARGURA DA IMG
            // grid.Columns[9].Width = 50;
            //grid.Columns[5].Width = 100;
            //grid.Columns[9].Visible = false;
        }
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

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //verifica se tem dados,´para não cair o sistema
            if (e.RowIndex > -1)
            {
                txtCodBarras.Enabled = false;
                txtEstoque.Enabled = false;
                txtFornecedor.Enabled = false;
                txtNome.Enabled = false;
                txtValorUnit.Enabled = false;

                //captura os componentes
                //pega o valor no array grid 1, já que é string converte em string

                cod = grid.CurrentRow.Cells[0].Value.ToString();//id
                txtCodBarras.Text = grid.CurrentRow.Cells[1].Value.ToString();
                codBarrasAntigo = grid.CurrentRow.Cells[1].Value.ToString();//guarda o valor do codFunc
                txtNome.Text = grid.CurrentRow.Cells[2].Value.ToString();
                txtFornecedor.Text = grid.CurrentRow.Cells[4].Value.ToString();
                txtValorUnit.Text = grid.CurrentRow.Cells[5].Value.ToString();
                txtEstoque.Text = grid.CurrentRow.Cells[6].Value.ToString();


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

                }
                {
                    image.Image = Properties.Resources._12_sPx13ICSI_transformed;//coloca a imagem perfil na picture do form
                }

            }
            else
            {
                return;
            }
        }

        private void Limpar()
        {
            txtCodBarras.Text = txtEstoque.Text = txtFornecedor.Text = txtNome.Text = txtValorUnit.Text = string.Empty;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpar();
        }

        private void gridCarrinho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                idProduto = gridCarrinho.CurrentRow.Cells[0].Value.ToString();
            }
        }

        private void btnAddCarrinho_Click(object sender, EventArgs e)
        {
            qtd = int.Parse(qtdProdutos.Text);
            preco = double.Parse(txtValorUnit.Text);

            subtotal = qtd * preco * 1.0;

            total += subtotal;

            estoque = int.Parse(txtEstoque.Text);

            if (estoque >= qtd)
            {
                try
                {

                    con.AbrirConexao();

                    sql = "INSERT INTO  carrinho(codBarrasProduto, nomeProduto, QtdProduto,ValorUnitario, data)" +
                        "VALUES(@codBarrasProduto, @nomeProduto, @QtdProduto,@ValorUnitario, curDate())";
                    cmd = new MySqlCommand(sql, con.con);

                    cmd.Parameters.AddWithValue("@codBarrasProduto", txtCodBarras.Text);
                    cmd.Parameters.AddWithValue("@nomeProduto", txtNome.Text);
                    cmd.Parameters.AddWithValue("@Qtdproduto", qtdProdutos.Text);
                    cmd.Parameters.AddWithValue("@ValorUnitario", txtValorUnit.Text);
                    //cmd.Parameters.AddWithValue("@totalPagar",valorTotalAnterior);

                    cmd.ExecuteNonQuery();
                    con.FecharConexao();
                    ListarCarrinho();
                    qtdProdutos.Text = "0";
                    //Valor Total
                    txtTotalPagar.Text = total.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex);
                }

            }
            else
            {
                MessageBox.Show("Estoque está menor que a venda", "Erro ao adicionar ao carrinho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (qtdProdutos.Text.Trim() == "" || qtdProdutos.Text == "0")
            {
                MessageBox.Show("Preencha a Quantidade de Produtos!");
                return;
            }

        }

        private void btnExcluirCarrinho_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Deseja realmente excluir o item?", "Cadastro Produtos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                try
                {
                    con.AbrirConexao();
                    sql = "DELETE FROM carrinho WHERE id = @id";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@id", idProduto);
                    cmd.ExecuteNonQuery();
                    con.FecharConexao();
                    Listar();
                    MessageBox.Show("Registro Excluido com sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarCarrinho();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex);
                }

            }
            else
            {
                return;
            }

        }

    }

}


