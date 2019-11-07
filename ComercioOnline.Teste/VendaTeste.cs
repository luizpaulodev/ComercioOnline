using ComercioOnline.Model;
using ComercioOnline.Model.Utilitarios;
using ComercioOnline.Servico;
using ComercioOnline.Teste.Utilitarios;
using dn32.infraestrutura.Fabrica;
using dn32.infraestrutura.Generico;
using Xunit;

namespace ComercioOnline.Teste
{
    public class VendaTeste : TesteGenerico<Venda>
    {
        public override void InicializarInfraestrutura()
        {
            UtilitariosDeTeste.InicializarInfraestrutura();
        }

        #region CADASTRO
        [Fact(DisplayName = nameof(VendaCadastroTeste))]
        public void VendaCadastroTeste()
        {
            var venda = CadastreUmaVenda();

            Assert.NotEqual(0, venda.Codigo);
            Servico.Remova(venda.Codigo);
        }
        #endregion

        #region CONSULTA
        [Fact(DisplayName = nameof(VendaConsultaTeste))]
        public void VendaConsultaTeste()
        {
            var venda = CadastreUmaVenda();
            var vendaBancoDeDados = Servico.Consulte(venda.Codigo);
            var ehIgual = Compare(venda, vendaBancoDeDados, nameof(Venda.DataDeAtualizacao), nameof(Venda.DataDeCadastro));

            Assert.True(ehIgual);
            Servico.Remova(venda.Codigo);
        }
        #endregion

        #region FINALIZAR VENDA
        [Fact(DisplayName = nameof(FinalizeAVenda))]
        public void FinalizeAVenda()
        {
            var venda = CadastreUmaVenda();
            var produto1 = ProdutoTeste.CadastreUmProduto();
            var produto2 = ProdutoTeste.CadastreUmProduto();
            var produtoNaVenda1 = ProdutoNaVendaTeste.CadastreUmProdutoNaVenda(venda, produto1, 1);
            var produtoNaVenda2 = ProdutoNaVendaTeste.CadastreUmProdutoNaVenda(venda, produto2, 1);

            var servico = Servico as ServicoDeVenda;
            servico.FinalizeVenda(venda);
            
            Servico.Remova(venda.Codigo);
            ProdutoTeste.Remova(produto1.Codigo);
            ProdutoTeste.Remova(produto2.Codigo);
        }

        [Fact(DisplayName = nameof(FinalizeAVendaCalculandoDescontoEValorTotal))]
        public void FinalizeAVendaCalculandoDescontoEValorTotal()
        {
            var venda = CadastreUmaVenda();
            var produto1 = ProdutoTeste.CadastreUmProduto();
            var produto2 = ProdutoTeste.CadastreUmProduto();
            var produtoNaVenda1 = ProdutoNaVendaTeste.CadastreUmProdutoNaVenda(venda, produto1, 11);
            var produtoNaVenda2 = ProdutoNaVendaTeste.CadastreUmProdutoNaVenda(venda, produto2, 2);

            var servico = Servico as ServicoDeVenda;
            servico.FinalizeVenda(venda);

            var produtosSalvos = servico.ObtenhaProdutosDaVenda(venda);

            var descontoTotal = 0m;
            var valorTotal = 0m;

            foreach (var produto in produtosSalvos)
            {
                descontoTotal += produto.Desconto;
                valorTotal += produto.ValorTotal;
            }

            Assert.Equal(descontoTotal, venda.DescontoTotal);
            Assert.Equal(valorTotal, venda.ValorTotal);

            Servico.Remova(venda.Codigo);
            ProdutoTeste.Remova(produto1.Codigo);
            ProdutoTeste.Remova(produto2.Codigo);
        }
        #endregion

        #region METODOS ESTATICOS
        public static void Remova(int codigo)
        {
            var servico = FabricaDeServico.Crie<Venda>();
            servico.Remova(codigo);
        }

        public static Venda CadastreUmaVenda()
        {
            var servico = FabricaDeServico.Crie<Venda>();
            var venda = ObtenhaUmaVenda();
            servico.Cadastre(venda);
            return venda;
        }

        public static Venda ObtenhaUmaVenda()
        {
            return new Venda
            {
                Status = eStatusDaVenda.Nova,
                ValorTotal = 0m,
                DescontoTotal = 0m
            };
        }
        #endregion
    }
}
