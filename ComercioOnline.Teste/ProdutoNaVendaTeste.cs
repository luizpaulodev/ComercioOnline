using ComercioOnline.Model;
using ComercioOnline.Model.Utilitarios;
using ComercioOnline.Servico;
using ComercioOnline.Teste.Utilitarios;
using dn32.infraestrutura.Fabrica;
using dn32.infraestrutura.Generico;
using System;
using Xunit;

namespace ComercioOnline.Teste
{
    public class ProdutoNaVendaTeste : TesteGenerico<ProdutoNaVenda>
    {
        public override void InicializarInfraestrutura()
        {
            UtilitariosDeTeste.InicializarInfraestrutura();
        }

        [Fact(DisplayName = nameof(AdicionarProdutoNaVenda))]
        public void AdicionarProdutoNaVenda()
        {
            var servico = FabricaDeServico.Crie<ProdutoNaVenda>() as ServicoDeProdutoNaVenda;
            var venda = VendaTeste.CadastreUmaVenda();
            var produto = ProdutoTeste.CadastreUmProduto();
            var produtoNaVenda = servico.Cadastre(venda.Codigo, produto.Codigo, 1);

            Assert.NotEqual(0, produtoNaVenda.Codigo);

            servico.Remova(produtoNaVenda.Codigo);
            VendaTeste.Remova(venda.Codigo);
            ProdutoTeste.Remova(produto.Codigo);
        }

        [Fact(DisplayName = nameof(AdicionarProdutoErradoNaVenda))]
        public void AdicionarProdutoErradoNaVenda()
        {
            var servico = FabricaDeServico.Crie<ProdutoNaVenda>() as ServicoDeProdutoNaVenda;
            var venda = VendaTeste.CadastreUmaVenda();
            var codigoDoPoduto = ObtenhaUmCodigo();

            var ex = Assert.Throws<Exception>(() => servico.Cadastre(venda.Codigo, codigoDoPoduto, 1));

            Assert.Equal(ConstantesValidacaoModel.O_PRODUTO_INFORMADO_NAO_FOI_ENCONTRADO_NO_SISTEMA, ex.Message);

            servico.Remova(venda.Codigo);
            VendaTeste.Remova(venda.Codigo);
        }

        [Fact(DisplayName = nameof(AdicionarProdutoNaVendaProdutoEVendaNaoExistemErro))]
        public void AdicionarProdutoNaVendaProdutoEVendaNaoExistemErro()
        {
            var servico = FabricaDeServico.Crie<ProdutoNaVenda>() as ServicoDeProdutoNaVenda;
            var codigoDoProduto = ObtenhaUmCodigo();
            var codigoDaVenda = ObtenhaUmCodigo();

            var ex = Assert.Throws<Exception>(() => servico.Cadastre(codigoDaVenda, codigoDoProduto, 1));

            Assert.Equal(ConstantesValidacaoModel.O_PRODUTO_INFORMADO_NAO_FOI_ENCONTRADO_NO_SISTEMA, ex.Message);
        }

        [Fact(DisplayName = nameof(AdicionarProdutoNaVendaVendaNaoExisteErro))]
        public void AdicionarProdutoNaVendaVendaNaoExisteErro()
        {
            var servico = FabricaDeServico.Crie<ProdutoNaVenda>() as ServicoDeProdutoNaVenda;
            var produto = ProdutoTeste.CadastreUmProduto();
            var codigoDaVenda = ObtenhaUmCodigo();

            var ex = Assert.Throws<Exception>(() => servico.Cadastre(codigoDaVenda, produto.Codigo, 1));

            Assert.Equal(ConstantesValidacaoModel.A_VENDA_INFORMADO_NAO_FOI_ENCONTRADO_NO_SISTEMA, ex.Message);

            servico.Remova(produto.Codigo);
            ProdutoTeste.Remova(produto.Codigo);
        }

        [Theory(DisplayName = nameof(AdicionaProdutoECalculaODesconto))]
        [InlineData(3)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(17)]
        public void AdicionaProdutoECalculaODesconto(int quantidadeDeProdutos)
        {
            var servico = FabricaDeServico.Crie<ProdutoNaVenda>() as ServicoDeProdutoNaVenda;
            var venda = VendaTeste.CadastreUmaVenda();
            var produto = ProdutoTeste.CadastreUmProduto();

            var produtoNaVenda = servico.Cadastre(venda.Codigo, produto.Codigo, quantidadeDeProdutos);
            
            var valorTotal = quantidadeDeProdutos * produto.Valor;
            var desconto = 0m;

            if (quantidadeDeProdutos >= 10)
            {
                desconto = valorTotal * 0.1m;
            }

            Assert.NotEqual(0, produtoNaVenda.Codigo);
            Assert.Equal(desconto, produtoNaVenda.Desconto);

            servico.Remova(produtoNaVenda.Codigo);
            VendaTeste.Remova(venda.Codigo);
            ProdutoTeste.Remova(produto.Codigo);
        }

        [Fact(DisplayName = nameof(RemoverProdutoNaVenda))]
        public void RemoverProdutoNaVenda()
        {

        }

        [Fact(DisplayName = nameof(RemoverProdutoNaVendaFechada))]
        public void RemoverProdutoNaVendaFechada()
        {

        }
    }
}
