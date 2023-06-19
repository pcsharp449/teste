using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lojinha
{
    class Produto
    {
        public string CodBarras { get; set; }
        public string Nome { get; set; }
        public decimal ValorUnitario { get; set; }
        public int Quantidade { get; set; }


        public Produto(string codBarras, string nome, decimal valorUnitario, int quantidade)
        {
            CodBarras = codBarras;
            Nome = nome;
            ValorUnitario = valorUnitario;
            Quantidade = quantidade;
        }
    
    
    }

}
