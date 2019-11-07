using ComercioOnline.Model;
using ComercioOnline.Repositorio;
using ComercioOnline.Validacao;
using dn32.infraestrutura.Atributos;
using dn32.infraestrutura.Fabrica;
using dn32.infraestrutura.Generico;
using System.Collections.Generic;

namespace ComercioOnline.Servico
{
    [ServicoDe(typeof(Venda))]
    public class ServicoDeVenda : ServicoGenerico<Venda>
    {
        public void FinalizeVenda(Venda venda)
        {
            CalculeDescontoTotalDaVenda(venda);
            CalculeValorTotalDaVenda(venda);

            venda.Status = eStatusDaVenda.Fechada;
            Repositorio.Atualizar(venda);
        }

        private void CalculeDescontoTotalDaVenda(Venda venda)
        {
            var produtos = ObtenhaProdutosDaVenda(venda);
            venda.DescontoTotal = 0m;

            foreach (var produto in produtos)
            {
                venda.DescontoTotal += produto.Desconto;
            }
        }

        private void CalculeValorTotalDaVenda(Venda venda)
        {
            var produtos = ObtenhaProdutosDaVenda(venda);
            venda.ValorTotal = 0m;

            foreach (var produto in produtos)
            {
                venda.ValorTotal += produto.ValorTotal;
            }
        }

        public List<ProdutoNaVenda> ObtenhaProdutosDaVenda(Venda venda)
        {
            var repositorio = FabricaDeRepositorio.Crie<ProdutoNaVenda>() as RepositorioDeProdutoNaVenda;
            return repositorio.ConsulteProdutoPorVenda(venda.Codigo);
        }
    }
}
