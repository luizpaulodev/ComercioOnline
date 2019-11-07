using ComercioOnline.Model;
using ComercioOnline.Model.Utilitarios;
using dn32.infraestrutura.Atributos;
using dn32.infraestrutura.Generico;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComercioOnline.Validacao
{
    [ValidacaoDe(typeof(ProdutoNaVenda))]
    public class ValidacaoDeProdutoNaVenda : ValidacaoGenerica<ProdutoNaVenda>
    {
        public void Cadastre(Produto produto, Venda venda, int quantidade)
        {
            if (produto == null)
            {
                throw new Exception(ConstantesValidacaoModel.O_PRODUTO_INFORMADO_NAO_FOI_ENCONTRADO_NO_SISTEMA);
            }

            
            if (venda == null)
            {
                throw new Exception(ConstantesValidacaoModel.A_VENDA_INFORMADO_NAO_FOI_ENCONTRADO_NO_SISTEMA);
            }
            
        }
    }
}
