using ComercioOnline.Model;
using ComercioOnline.Validacao;
using dn32.infraestrutura.Atributos;
using dn32.infraestrutura.Fabrica;
using dn32.infraestrutura.Generico;
using System;

namespace ComercioOnline.Servico
{
    [ServicoDe(typeof(ProdutoNaVenda))]
    public class ServicoDeProdutoNaVenda : ServicoGenerico<ProdutoNaVenda>
    {
        public ProdutoNaVenda Cadastre(int codigoDaVenda, int codigoDoProduto, int quantidade)
        {
            var desconto = 0m;
            var servicoDeVenda = FabricaDeServico.Crie<Venda>();
            var servicoDeproduto = FabricaDeServico.Crie<Produto>();

            var venda = servicoDeVenda.Consulte(codigoDaVenda);
            var produto = servicoDeproduto.Consulte(codigoDoProduto);

            var validacaoDeProdutoNaVenda = FabricaDeValidacao.Crie<ProdutoNaVenda>() as ValidacaoDeProdutoNaVenda;
            validacaoDeProdutoNaVenda.Cadastre(produto, venda, quantidade);

            var produtoNaVenda = new ProdutoNaVenda
            {
                IdVenda = venda.Id,
                IdProduto = produto.Id,
                Quantidade = quantidade,
                ValorTotal = produto.Valor * quantidade
            };

            if (quantidade >= 10)
            {
                desconto = produtoNaVenda.ValorTotal * 10 / 100;
            }

            produtoNaVenda.ValorTotal -= desconto;
            produtoNaVenda.Desconto = desconto;

            Cadastre(produtoNaVenda);

            return produtoNaVenda;
        }

        public override void Remova(int codigo)
        {
            //var servicoProduto = FabricaDeServico.Crie<Produto>();
            //var servicoVenda = FabricaDeServico.Crie<Venda>();
            base.Remova(codigo);
        }
    }
}
