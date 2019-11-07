using ComercioOnline.Model;
using dn32.infraestrutura;
using dn32.infraestrutura.Atributos;
using dn32.infraestrutura.Generico;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComercioOnline.Repositorio
{
    [RepositorioDe(typeof(ProdutoNaVenda))]
    public class RepositorioDeProdutoNaVenda : RepositorioGenerico<ProdutoNaVenda>
    {
        public List<ProdutoNaVenda> ConsulteProdutoPorVenda(int codigo)
        {
            using (IDocumentSession session = Contexto.Store.OpenSession())
            {
                var query = $"IdVenda:{Utilitarios.ObtenhaIdDoElemento<Venda>(codigo)}";
                
                return session
                    .Advanced
                    .DocumentQuery<ProdutoNaVenda>()
                    .Where(query).ToList();
            }
        }
    }
}
