using ComercioOnline.Model;
using ComercioOnline.Teste.Utilitarios;
using dn32.infraestrutura.Fabrica;
using dn32.infraestrutura.Generico;
using System;
using System.Collections.Generic;
using System.Text;
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
            var servico = FabricaDeServico.Crie<Venda>();
            var venda = CadastreUmaVenda();

            Assert.NotEqual(0, venda.Codigo);
            servico.Remova(venda.Codigo);
        }
        #endregion

        #region CONSULTA
        [Fact(DisplayName = nameof(VendaConsultaTeste))]
        public void VendaConsultaTeste()
        {
            var servico = FabricaDeServico.Crie<Venda>();
            var venda = CadastreUmaVenda();
            var vendaBancoDeDados = servico.Consulte(venda.Codigo);
            var ehIgual = Compare(venda, vendaBancoDeDados, nameof(Venda.DataDeAtualizacao), nameof(Venda.DataDeCadastro));

            Assert.True(ehIgual);
            servico.Remova(venda.Codigo);
        }
        #endregion

        #region METODOS PRIVADOS
        private Venda CadastreUmaVenda()
        {
            var servico = FabricaDeServico.Crie<Venda>();
            var venda = ObtenhaUmaVenda();
            servico.Cadastre(venda);
            return venda;
        }

        private Venda ObtenhaUmaVenda()
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
