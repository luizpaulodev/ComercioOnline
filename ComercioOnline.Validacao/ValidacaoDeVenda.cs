using ComercioOnline.Model;
using ComercioOnline.Model.Utilitarios;
using dn32.infraestrutura.Atributos;
using dn32.infraestrutura.Generico;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComercioOnline.Validacao
{
    [ValidacaoDe(typeof(Venda))]
    public class ValidacaoDeVenda : ValidacaoGenerica<Venda>
    {
        public void FinalizeVenda(Venda venda)
        {
            //if (venda == null)
            //{
            //    throw new Exception(ConstantesValidacaoModel.A_VENDA_NAO_FOI_ENCONTRADA);
            //}
        }
    }
}
