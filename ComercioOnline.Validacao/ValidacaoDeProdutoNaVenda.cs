using ComercioOnline.Model;
using ComercioOnline.Model.Utilitarios;
using dn32.infraestrutura;
using dn32.infraestrutura.Atributos;
using dn32.infraestrutura.Fabrica;
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

            if (venda.Status == eStatusDaVenda.Fechada)
            {
                throw new Exception(ConstantesValidacaoModel.NAO_EH_POSSIVEL_ALTERAR_UMA_VENDA_FECHADA);
            }
        }

        public override void Remova(int codigo)
        {
            var produtoNaVenda = Repositorio.Consulte(codigo);
            var codigoDaVenda = Utilitarios.ObtenhaCodigoDoElemento(produtoNaVenda.IdVenda);
            var venda = FabricaDeRepositorio.Crie<Venda>().Consulte(codigoDaVenda);

            if (venda == null)
            {
                throw new Exception(ConstantesValidacaoModel.A_VENDA_INFORMADO_NAO_FOI_ENCONTRADO_NO_SISTEMA);
            }

            if (venda.Status == eStatusDaVenda.Fechada)
            {
                throw new Exception(ConstantesValidacaoModel.NAO_EH_POSSIVEL_ALTERAR_UMA_VENDA_FECHADA);
            }

            base.Remova(codigo);
        }
    }
}
