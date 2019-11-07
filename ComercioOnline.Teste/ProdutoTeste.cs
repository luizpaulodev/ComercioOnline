using ComercioOnline.Model;
using ComercioOnline.Model.Utilitarios;
using ComercioOnline.Teste.Utilitarios;
using dn32.infraestrutura;
using dn32.infraestrutura.Constantes;
using dn32.infraestrutura.Fabrica;
using dn32.infraestrutura.Generico;
using dn32.infraestrutura.Model;
using System;
using Xunit;

namespace ComercioOnline.Teste
{
    public class ProdutoTeste : TesteGenerico<Produto>
    {
        public override void InicializarInfraestrutura()
        {
            UtilitariosDeTeste.InicializarInfraestrutura();
        }

        #region CADASTRO
        [Fact(DisplayName = nameof(ProdutoCadastroTeste))]
        public void ProdutoCadastroTeste()
        {
            var servico = FabricaDeServico.Crie<Produto>();
            var produto = CadastreUmProduto();

            Assert.NotEqual(0, produto.Codigo);

            servico.Remova(produto.Codigo);
        }

        [Fact(DisplayName = nameof(ProdutoCadastroSemValorErroTeste))]
        public void ProdutoCadastroSemValorErroTeste()
        {
            var produto = ObtenhaUmProduto();
            produto.Valor = 0;

            var ex = Assert.Throws<Exception>(() =>
            {
                Servico.Cadastre(produto);
            });

            Assert.Equal(ConstantesValidacaoModel.O_VALOR_DO_PRODUTO_EH_OBRIGATORIO, ex.Message);
        }

        [Fact(DisplayName = nameof(ProdutoCadastroSemNomeErroTeste))]
        public void ProdutoCadastroSemNomeErroTeste()
        {
            var produto = ObtenhaUmProduto();
            produto.Nome = string.Empty;      

            var ex = Assert.Throws<Exception>(() =>
            {
                Servico.Cadastre(produto);
            });

            Assert.Equal(ConstantesDeValidacao.O_NOME_DO_ELEMENTO_DEVE_SER_INFORMADO, ex.Message);
        }
        #endregion

        #region CONSULTA
        [Fact(DisplayName = nameof(ProdutoConsultaTeste))]
        public void ProdutoConsultaTeste()
        {
            var produto = CadastreUmProduto();
                        
            var produtoDoBanco = Servico.Consulte(produto.Codigo);

            var ehIgual = Compare(produto, produtoDoBanco, nameof(Produto.DataDeAtualizacao), nameof(Produto.DataDeCadastro));

            Assert.True(ehIgual);
            Servico.Remova(produto.Codigo);
        }
        #endregion

        #region ATUALIZAÇÃO
        [Fact(DisplayName = nameof(ProdutoAtualizacaoSemValorErroTeste))]
        public void ProdutoAtualizacaoSemValorErroTeste()
        {
            var produto = CadastreUmProduto();
            produto.Valor = 0;

            var ex = Assert.Throws<Exception>(() =>
            {
                Servico.Atualize(produto);
            });

            Assert.Equal(ConstantesValidacaoModel.O_VALOR_DO_PRODUTO_EH_OBRIGATORIO, ex.Message);
            Servico.Remova(produto.Codigo);
        }

        [Fact(DisplayName = nameof(ProdutoAtualizacaoSemNomeErroTeste))]
        public void ProdutoAtualizacaoSemNomeErroTeste()
        {
            var produto = ObtenhaUmProduto();
            produto.Nome = string.Empty;

            var ex = Assert.Throws<Exception>(() =>
            {
                Servico.Atualize(produto);
            });

            Assert.Equal(ConstantesDeValidacao.O_NOME_DO_ELEMENTO_DEVE_SER_INFORMADO, ex.Message);
            Servico.Remova(produto.Codigo);
        }
        #endregion

        #region MÉTODOS ESTATICOS
        public static void Remova(int codigo)
        {
            var servico = FabricaDeServico.Crie<Produto>();
            servico.Remova(codigo);
        }

        public static Produto ObtenhaUmProduto()
        {
            return new Produto
            {
                Nome = "Computador com impressora",
                Valor = 1285.85m,
                CodigoDeBarras = "6545645644"
            };
        }

        public static Produto CadastreUmProduto()
        {
            var servico = FabricaDeServico.Crie<Produto>();
            var produto = ObtenhaUmProduto();
            servico.Cadastre(produto);
            return produto;
        }
        #endregion
    }
}
