using System;
using System.Collections.Generic;
using System.Text;

namespace ComercioOnline.Model.Utilitarios
{
    public static class ConstantesValidacaoModel
    {
        public const string O_VALOR_DO_PRODUTO_EH_OBRIGATORIO = "O valor do produto é obrigatório";
        public const string O_PRODUTO_INFORMADO_NAO_FOI_ENCONTRADO_NO_SISTEMA = "O produto informado não foi encontrado";
        public const string A_VENDA_INFORMADO_NAO_FOI_ENCONTRADO_NO_SISTEMA = "A venda informado não foi encontrado";
        public const string A_VENDA_NAO_FOI_ENCONTRADA = "A venda solicitada não foi encontrada";
        public const string NAO_EH_POSSIVEL_ALTERAR_UMA_VENDA_FECHADA = "Não é possível alterar uma venda fechada";
    }
}
