using INFO;
using DAL;
using System.Collections.Generic;
using System;

namespace BLL
{
    public class BLLProduto
    {

        public List<ProdutoINFO> busDadosCompletosProdutos(string TipoProduto, string TipoEncomenda)
        {
            try
            {
                DALProduto DAL = new DALProduto();
                return DAL.dbDadosCompletosProdutos(TipoProduto, TipoEncomenda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<ProdutoINFO> busListaTipoPedido()
        {
            try
            {
                DALProduto DAL = new DALProduto();
                return DAL.dbListaTipoPedido();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoProdutoINFO> busListaProdutos()
        {
            try
            {
                DALProduto DAL = new DALProduto();
                return DAL.dbListaProdutos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
