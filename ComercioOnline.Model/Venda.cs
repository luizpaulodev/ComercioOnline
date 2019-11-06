using dn32.infraestrutura.Generico;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComercioOnline.Model
{
    public class Venda : ModelGenerico
    {
        public eStatusDaVenda Status { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal DescontoTotal { get; set; }
    }
}
