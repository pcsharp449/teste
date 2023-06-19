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
    public partial class FrmCategoria : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;
        string nomeAntigo;
        string id;
        public FrmCategoria()
        {
            InitializeComponent();
        }
        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            Listar();
            this.grid.DefaultCellStyle.ForeColor = Color.White;
            this.grid.DefaultCellStyle.BackColor = Color.FromArgb(0, 74, 173);
            this.grid.DefaultCellStyle.Font = new Font("Arial", 10);
            this.grid.EnableHeadersVisualStyles = false;
            this.grid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            this.grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }


            private void FormatarGD()
            {
                grid.Columns[0].HeaderText = "ID";
                grid.Columns[1].HeaderText = "CATEGORIA";
                grid.Columns[2].HeaderText = "DATA";

            }
            private void Listar()
            {
                try
                {
                    con.AbrirConexao();
                    sql = "SELECT * FROM categorias ORDER BY categoria asc";
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



        private void btnNovo_Click_1(object sender, EventArgs e)
        {
            txtCategoria.Enabled = true;
            btnNovo.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            txtCategoria.Focus();
            return;
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = false;
            btnNovo.Enabled = true;
            txtCategoria.Text = "";
            txtCategoria.Enabled = false;
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            if (txtCategoria.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o nome da Categoria", "Cadastro Categorias", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategoria.Text = "";
                txtCategoria.Focus();
                return;
            }
            try
            {
                con.AbrirConexao();
                MySqlCommand cmdVerificar;
                MySqlDataReader reader;//Com o reader vou conseguir extrair dados da tabela e usar em outro form

                cmdVerificar = new MySqlCommand("SELECT* FROM categoria WHERE categoria = @nome", con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmdVerificar;
                cmdVerificar.Parameters.AddWithValue("@nome", txtCategoria.Text);
                reader = cmdVerificar.ExecuteReader();
                con.FecharConexao();

                if (reader.HasRows)
                {
                    MessageBox.Show("Categoria já cadastrado");

                    txtCategoria.Text = "";
                    txtCategoria.Focus();
                    return;

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex);
            }

            //ele impede que o user envie apenas um input com espaços para o banco
            //o trin tira os espaços
            con.AbrirConexao();
            sql = "UPDATE categorias SET categoria = @categoria, data = curDate() WHERE id = @id";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@categoria", txtCategoria.Text);
            cmd.ExecuteNonQuery();
            con.FecharConexao();

            MessageBox.Show("Registro Editado com Sucesso!", "Cadastro Cargos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = false;
            btnNovo.Enabled = true;
            Listar();
        }

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            if (txtCategoria.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha a nova categoria", "Cadastro Categorias", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategoria.Text = "";
                txtCategoria.Focus();
                return;
            }
            try
            {
                con.AbrirConexao();
                MySqlCommand cmdVerificar;
                MySqlDataReader reader;//Com o reader vou conseguir extrair dados da tabela e usar em outro form

                cmdVerificar = new MySqlCommand("SELECT* FROM categorias WHERE categoria = @nome", con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmdVerificar;
                cmdVerificar.Parameters.AddWithValue("@nome", txtCategoria.Text);
                reader = cmdVerificar.ExecuteReader();
                con.FecharConexao();

                if (reader.HasRows)
                {
                    MessageBox.Show("Fornecedor já cadastrado");

                    txtCategoria.Text = "";
                    txtCategoria.Focus();

                    return;

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex);
            }


            con.AbrirConexao();
            sql = "INSERT INTO categorias (categoria, data) VALUES (@categoria, curDate())";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@categoria", txtCategoria.Text);
            cmd.ExecuteNonQuery();
            con.FecharConexao();

            MessageBox.Show("Registro realizado com sucesso!", "Cadastro Categoria", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnNovo.Enabled = true;
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = false;
            txtCategoria.Text = "";
            txtCategoria.Enabled = false;
            Listar();
        }

        private void btnExcluir_Click_1(object sender, EventArgs e)
        {

            var res = MessageBox.Show("Deseja realmente excluir o registro? ", "Cadastro Cargos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                con.AbrirConexao();
                sql = "delete from categorias where id = @id";//quando clica na grid ele pega o id
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.FecharConexao();
                Listar();

                MessageBox.Show("Registro excluido com sucesso!", "Cadastro Categorias", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnNovo.Enabled = true;
                btnEditar.Enabled = false;
                btnExcluir.Enabled = false;
                btnSalvar.Enabled = false;
                Listar();

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            string valueToSearch = txtSearch.Text;
            try
            {
                con.AbrirConexao();
                sql = "SELECT * FROM categorias WHERE CONCAT(categoria) LIKE '%" + valueToSearch + "%'";
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
                btnEditar.Enabled = true;
                btnExcluir.Enabled = true;
                btnSalvar.Enabled = false;
                btnNovo.Enabled = false;
                txtCategoria.Enabled = true;

                //captura os componentes
                //pega o valor no array grid 1, já que é string converte em string

                id = grid.CurrentRow.Cells[0].Value.ToString();//id

                txtCategoria.Text = grid.CurrentRow.Cells[1].Value.ToString();

            }
        }
    }
    }
