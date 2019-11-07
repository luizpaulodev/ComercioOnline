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
            var servico = Servico as ServicoDeProdutoNaVenda;
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
            var servico = Servico as ServicoDeProdutoNaVenda;
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
            var servico = Servico as ServicoDeProdutoNaVenda;
            var codigoDoProduto = ObtenhaUmCodigo();
            var codigoDaVenda = ObtenhaUmCodigo();

            var ex = Assert.Throws<Exception>(() => servico.Cadastre(codigoDaVenda, codigoDoProduto, 1));

            Assert.Equal(ConstantesValidacaoModel.O_PRODUTO_INFORMADO_NAO_FOI_ENCONTRADO_NO_SISTEMA, ex.Message);
        }

        [Fact(DisplayName = nameof(AdicionarProdutoNaVendaVendaNaoExisteErro))]
        public void AdicionarProdutoNaVendaVendaNaoExisteErro()
        {
            var servico = Servico as ServicoDeProdutoNaVenda;
            var produto = ProdutoTeste.CadastreUmProduto();
            var codigoDaVenda = ObtenhaUmCodigo();

            var ex = Assert.Throws<Exception>(() => servico.Cadastre(codigoDaVenda, produto.Codigo, 1));

            Assert.Equal(ConstantesValidacaoModel.A_VENDA_INFORMADO_NAO_FOI_ENCONTRADO_NO_SISTEMA, ex.Message);

            servico.Remova(produto.Codigo);
            ProdutoTeste.Remova(produto.Codigo);
        }

        [Theory(DisplayName = nameof(AdicionaProdutoECalculaODesconto))]
        [InlineData(3, 0)]
        [InlineData(9, 0)]
        [InlineData(10, 10)]
        [InlineData(17, 10)]
        [InlineData(50, 10)]
        public void AdicionaProdutoECalculaODesconto(int quantidadeDeProdutos, decimal descontoEsperado)
        {
            var servico = Servico as ServicoDeProdutoNaVenda;
            var venda = VendaTeste.CadastreUmaVenda();
            var produto = ProdutoTeste.CadastreUmProduto();

            var produtoNaVenda = servico.Cadastre(venda.Codigo, produto.Codigo, quantidadeDeProdutos);
            
            var valorTotal = quantidadeDeProdutos * produto.Valor;
            var desconto = valorTotal * (descontoEsperado / 100);

            Assert.NotEqual(0, produtoNaVenda.Codigo);
            Assert.Equal(desconto, produtoNaVenda.Desconto);

            servico.Remova(produtoNaVenda.Codigo);
            VendaTeste.Remova(venda.Codigo);
            ProdutoTeste.Remova(produto.Codigo);
        }

        [Fact(DisplayName = nameof(RemoverProdutoNaVenda))]
        public void RemoverProdutoNaVenda()
        {
            var servico = Servico as ServicoDeProdutoNaVenda;            
            var produto1 = ProdutoTeste.CadastreUmProduto();
            var produto2 = ProdutoTeste.CadastreUmProduto();
            var venda = VendaTeste.CadastreUmaVenda();

            var produtoNaVenda1 = servico.Cadastre(venda.Codigo, produto1.Codigo, 1);
            var produtoNaVenda2 = servico.Cadastre(venda.Codigo, produto2.Codigo, 1);

            servico.Remova(produtoNaVenda1.Codigo);
            servico.Remova(produtoNaVenda2.Codigo);

            VendaTeste.Remova(venda.Codigo);
            ProdutoTeste.Remova(produto1.Codigo);
            ProdutoTeste.Remova(produto2.Codigo);
        }

        [Fact(DisplayName = nameof(CadastrarProdutoNaVendaFechadaErro))]
        public void CadastrarProdutoNaVendaFechadaErro()
        {
            var servico = Servico as ServicoDeProdutoNaVenda;
            var venda = VendaTeste.CadastreUmaVenda();
            var produtoNaVenda = AdicionaProdutosEFinaliza(venda);

            var produto3 = ProdutoTeste.CadastreUmProduto();
            var ex = Assert.Throws<Exception>(() => servico.Cadastre(venda.Codigo, produto3.Codigo, 4));
            
            Assert.Equal(ex.Message, ConstantesValidacaoModel.NAO_EH_POSSIVEL_ALTERAR_UMA_VENDA_FECHADA);
        }

        [Fact(DisplayName = nameof(RemoverProdutoNaVendaFechadaErro))]
        public void RemoverProdutoNaVendaFechadaErro()
        {
            var servico = Servico as ServicoDeProdutoNaVenda;
            var venda = VendaTeste.CadastreUmaVenda();
            var produtoNaVenda = AdicionaProdutosEFinaliza(venda);

            var ex = Assert.Throws<Exception>(() => servico.Remova(produtoNaVenda.Codigo));
            Assert.Equal(ex.Message, ConstantesValidacaoModel.NAO_EH_POSSIVEL_ALTERAR_UMA_VENDA_FECHADA);
        }

        private ProdutoNaVenda AdicionaProdutosEFinaliza(Venda venda)
        {
            var servico = Servico as ServicoDeProdutoNaVenda;
            var produto1 = ProdutoTeste.CadastreUmProduto();
            var produto2 = ProdutoTeste.CadastreUmProduto();
            

            var produtoNaVenda1 = servico.Cadastre(venda.Codigo, produto1.Codigo, 1);
            var produtoNaVenda2 = servico.Cadastre(venda.Codigo, produto2.Codigo, 1);

            var servicoDeVenda = FabricaDeServico.Crie<Venda>() as ServicoDeVenda;
            servicoDeVenda.FinalizeVenda(venda);
            return produtoNaVenda2;
        }

        public static ProdutoNaVenda CadastreUmProdutoNaVenda(Venda venda, Produto produto, int quantidade)
        {
            var servico = FabricaDeServico.Crie<ProdutoNaVenda>() as ServicoDeProdutoNaVenda;
            var produtoNaVenda = servico.Cadastre(venda.Codigo, produto.Codigo, quantidade);

            return produtoNaVenda;
        }

        public static void Remova(int codigo)
        {
            var servico = FabricaDeServico.Crie<ProdutoNaVenda>();
            servico.Remova(codigo);
        }
    }
}
