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

namespace Lojinha.Forms
{
    public partial class Cargos : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;
        string id;
        string nomeAntigo;

        public Cargos()
        {
            InitializeComponent();
        }

        private void FrmCargos_Load(object sender, EventArgs e)
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
            grid.Columns[1].HeaderText = "CARGO";
            grid.Columns[2].HeaderText = "DATA";

        }
        private void Listar()
        {
            try
            {
                con.AbrirConexao();
                sql = "SELECT * FROM cargos ORDER BY cargo asc";
                cmd = new MySqlCommand(sql, con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                grid.DataSource = dt;
                con.FecharConexao();
                FormatarGD();
            }
            catch(Exception e)
            {
                MessageBox.Show("ERROR: " + e);
            }

        }



        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtCargo.Enabled = true;
            btnNovo.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            return;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = false;
            btnNovo.Enabled = true;
            txtCargo.Text = "";
            txtCargo.Enabled = false;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtCargo.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o cargo nome", "Cadastro Cargos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCargo.Text = "";
                txtCargo.Focus();
                return;
            }

            con.AbrirConexao();
            sql = "UPDATE cargos SET cargo = @cargo WHERE id = @id";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@cargo", txtCargo.Text);

            if (txtCargo.Text != nomeAntigo)
            {
                MySqlCommand cmdVerificar;
                cmdVerificar = new MySqlCommand("SELECT * FROM cargos WHERE cargo = @cargo", con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmdVerificar;
                cmdVerificar.Parameters.AddWithValue("@cargo", txtCargo.Text);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Cargo   " + txtCargo.Text + "   já está registrado", "Cadastro Cargos", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCargo.Text = "";
                    txtCargo.Focus();
                    return;
                }
            }
            cmd.ExecuteNonQuery();
            con.FecharConexao();

            MessageBox.Show("Registro Editado com Sucesso!", "Cadastro Cargos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = false;
            btnNovo.Enabled = true;
            Listar();

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtCargo.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o cargo novo", "Cadastro Cargos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCargo.Text = "";
                txtCargo.Focus();
                return;
            }

            con.AbrirConexao();
            sql = "INSERT INTO cargos (cargo, data) VALUES (@cargo, curDate())";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@cargo", txtCargo.Text);
            cmd.ExecuteNonQuery();
            con.FecharConexao();

            MessageBox.Show("Registro realizado com sucesso!", "Cadastro Cargos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnNovo.Enabled = true;
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = false;
            txtCargo.Text = "";
            txtCargo.Enabled = false;
            Listar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Deseja realmente excluir o registro? ", "Cadastro Cargos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                con.AbrirConexao();
                sql = "delete from cargos where id = @id";//quando clica na grid ele pega o id
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.FecharConexao();
                Listar();

                MessageBox.Show("Registro excluido com sucesso!", "Cadastro Cargos", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                sql = "SELECT * FROM cargos WHERE CONCAT(cargo) LIKE '%" + valueToSearch + "%'";
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
    

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //verifica se tem dados,´para não cair o sistema
            if (e.RowIndex > -1)
            {
                btnEditar.Enabled = true;
                btnExcluir.Enabled = true;
                btnSalvar.Enabled = false;
                btnNovo.Enabled = false;
                txtCargo.Enabled = true;

                //captura os componentes
                //pega o valor no array grid 1, já que é string converte em string

                id = grid.CurrentRow.Cells[0].Value.ToString();//id

                txtCargo.Text = grid.CurrentRow.Cells[1].Value.ToString();

            }
        }
    }
}
