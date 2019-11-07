using dn32.infraestrutura.Generico;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComercioOnline.Model
{
    public class ProdutoNaVenda : ModelGenerico
    {
        public string IdProduto { get; set; }
        public string IdVenda { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal Desconto { get; set; }

    }
}
