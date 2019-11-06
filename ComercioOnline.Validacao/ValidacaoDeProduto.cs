using ComercioOnline.Model;
using ComercioOnline.Model.Utilitarios;
using dn32.infraestrutura.Atributos;
using dn32.infraestrutura.Generico;
using System;

namespace ComercioOnline.Validacao
{
    [ValidacaoDe(typeof(Produto))]
    public class ValidacaoDeProduto : ValidacaoGenericaComNome<Produto>
    {
        public override void Cadastre(Produto item)
        {
            if (item.Valor == 0m)
            {
                throw new Exception(ConstantesValidacaoModel.O_VALOR_DO_PRODUTO_EH_OBRIGATORIO);
            }

            base.Cadastre(item);
        }

        public override void Atualize(Produto item)
        {
            if (item.Valor == 0m)
            {
                throw new Exception(ConstantesValidacaoModel.O_VALOR_DO_PRODUTO_EH_OBRIGATORIO);
            }

            base.Cadastre(item);
        }
    }
}
