using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lojinha
{
    class Conexao
    {
        //é publica pq vai acessar essa conexao nos formularios
        public string conec = "SERVER=192.168.1.200; DATABASE=PDV; UID=ana; PWD=123; PORT=3388";
        public MySqlConnection con = null;

        public void AbrirConexao()
        {
            try
            {
                con = new MySqlConnection(conec);
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro de conexao com o banco de dados: " + ex.Message);
            }
        }
        public void FecharConexao()
        {
            try
            {
                con = new MySqlConnection(conec);
                con.Close();
                con.Dispose();//derruba algumas conexoes abertas
                con.ClearAllPoolsAsync();//metodo de limpeza
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro de conexao com o banco: " + ex.Message);
            }
        }
    }
}
