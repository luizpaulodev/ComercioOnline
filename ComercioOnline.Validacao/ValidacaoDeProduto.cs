using ComercioOnline.Model;
using dn32.infraestrutura.Atributos;
using dn32.infraestrutura.Generico;
using System;

namespace ComercioOnline.Validacao
{
    [ValidacaoDe(typeof(Produto))]
    public class ValidacaoDeProduto : ValidacaoGenerica<Produto>
    {
        public override void Cadastre(Produto item)
        {
            if (item.Valor == 0m)
            {
                throw new Exception("O valor do produto é obrigatório");
            }

            base.Cadastre(item);
        }
    }
}
