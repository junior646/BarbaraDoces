using System.Collections.Generic;
using BarbaraDoces.Models;
using System.Text;
using System;
using System.Data;

namespace DAL
{
    public class DALProduto : DALGenerica
    {
        public List<ProdutoViewModel> dbDadosCompletosProdutos(string TipoProduto, string TipoEncomenda)
        {
            StringBuilder strSQL;
            List<IDataParameter> lLstParametros = null;
            IDataReader lObjReader = null;
            List<ProdutoViewModel> lLstProduto;

            try
            {
                strSQL = new StringBuilder();
                lLstProduto = new List<ProdutoViewModel>();
                lLstParametros = new List<IDataParameter>();

                strSQL.AppendLine(" SELECT ");
                strSQL.AppendLine("     A.[Id_TipoProdutoXPreco], A.[Id_TipoProduto], A.[Id_Produto], A.[Id_Sabor], A.[Id_Preco], ");
                strSQL.AppendLine("     B.[Desc_TipoProduto], B.[Ativo] AS TipoProdutoAtivo, ");
                strSQL.AppendLine("     C.[Nome_Produto], C.[Desc_Produto], C.[Vali_Produto], C.[Ativo] AS ProdutoAtivo, ");
                strSQL.AppendLine("     D.[Desc_Sabor], D.[Ativo] AS SaborAtivo, ");
                strSQL.AppendLine("     E.[Valor_Produto] ");
                strSQL.AppendLine(" FROM ");
                strSQL.AppendLine("     [dbo].[TipoProdutoXProdutoXSaborXPreco] A, ");
                strSQL.AppendLine("     [dbo].[TipoProduto] B, ");
                strSQL.AppendLine("     [dbo].[Produto] C, ");
                strSQL.AppendLine("     [dbo].[Sabor] D, ");
                strSQL.AppendLine("     [dbo].[Preco] E ");
                strSQL.AppendLine(" WHERE ");
                strSQL.AppendLine("     A.[Id_TipoProduto] = B.[Id_TipoProduto] AND ");
                strSQL.AppendLine("     A.[Id_Produto] = C.[Id_Produto] AND ");
                strSQL.AppendLine("     A.[Id_Sabor] = D.[Id_Sabor] AND ");
                strSQL.AppendLine("     A.[Id_Preco] = E.[Id_Preco] AND ");
                strSQL.AppendLine("     B.[Ativo] = @Ativo AND C.[Ativo] = @Ativo AND D.[Ativo] = @Ativo ");

                if (TipoProduto != "" && TipoProduto != "Todos")
                {
                    strSQL.AppendLine("    AND B.[Desc_TipoProduto] =  @Produto ");
                    lLstParametros.Add(dbParametroVarchar("@Produto", TipoProduto));
                }

                if (TipoEncomenda != "" && TipoEncomenda != "Todos")
                {
                    strSQL.AppendLine("    AND C.[Desc_Produto] =  @TipoEncomenda ");
                    lLstParametros.Add(dbParametroVarchar("@TipoEncomenda", TipoEncomenda));
                }

                lLstParametros.Add(dbParametroInteger("@Ativo", 1));

                lObjReader = SelecionarRegistros(strSQL.ToString(), lLstParametros);

                while (lObjReader.Read())
                {
                    lLstProduto.Add(MontarInfoProduto(lObjReader));
                }

                return lLstProduto;
            }
            catch (Exception ex)
            {
                strSQL = null;
                lLstParametros.Clear();
                FecharDataReader(lObjReader);

                throw ex;
                //return new List<ProdutoINFO>();
            }
            finally
            {
                strSQL = null;
                lLstParametros.Clear();
                FecharDataReader(lObjReader);
            }
        }
        
        public List<ProdutoViewModel> dbListaTipoPedido()
        {
            StringBuilder strSQL;
            List<IDataParameter> lLstParametros = null;
            IDataReader lObjReader = null;
            List<ProdutoViewModel> lLstProdutos;

            try
            {
                strSQL = new StringBuilder();
                lLstProdutos = new List<ProdutoViewModel>();
                lLstParametros = new List<IDataParameter>();

                strSQL.AppendLine(" SELECT ");
                strSQL.AppendLine("     Distinct (C.[Desc_Produto]), C.[Ativo] AS ProdutoAtivo ");
                strSQL.AppendLine(" FROM ");
                strSQL.AppendLine("     [dbo].[Produto] C ");
                strSQL.AppendLine(" WHERE ");
                strSQL.AppendLine("     C.[Ativo] = @Ativo  ");

                lLstParametros.Add(dbParametroInteger("@Ativo", 1));

                lObjReader = SelecionarRegistros(strSQL.ToString(), lLstParametros);

                while (lObjReader.Read())
                {
                    ProdutoViewModel lObjProduto = new ProdutoViewModel();

                    //Objeto ProdutoINFO
                    if (!String.IsNullOrEmpty(lObjReader["Desc_Produto"].ToString())) { lObjProduto.Desc_Produto = lObjReader["Desc_Produto"].ToString(); }

                    lLstProdutos.Add(lObjProduto);
                }

                return lLstProdutos;
            }
            catch (Exception ex)
            {
                strSQL = null;
                lLstParametros.Clear();
                FecharDataReader(lObjReader);

                throw ex;
                //return new List<ProdutoINFO>();
            }
            finally
            {
                strSQL = null;
                lLstParametros.Clear();
                FecharDataReader(lObjReader);
            }
        }


        public List<TipoProdutoViewModel> dbListaProdutos()
        {
            StringBuilder strSQL;
            List<IDataParameter> lLstParametros = null;
            IDataReader lObjReader = null;
            List<TipoProdutoViewModel> lLstProdutos;

            try
            {
                strSQL = new StringBuilder();
                lLstProdutos = new List<TipoProdutoViewModel>();
                lLstParametros = new List<IDataParameter>();

                strSQL.AppendLine(" SELECT ");
                strSQL.AppendLine("     B.[Desc_TipoProduto], B.[Id_TipoProduto], B.[Ativo] AS TipoProdutoAtivo ");
                strSQL.AppendLine(" FROM ");
                strSQL.AppendLine("     [dbo].[TipoProduto] B ");
                strSQL.AppendLine(" WHERE ");
                strSQL.AppendLine("     B.[Ativo] = @Ativo  ");

                lLstParametros.Add(dbParametroInteger("@Ativo", 1));

                lObjReader = SelecionarRegistros(strSQL.ToString(), lLstParametros);

                while (lObjReader.Read())
                {
                    TipoProdutoViewModel lObjProduto = new TipoProdutoViewModel();

                    //Objeto TipoProdutoINFO
                    if (!String.IsNullOrEmpty(lObjReader["Id_TipoProduto"].ToString())) { lObjProduto.Id_TipoProduto = int.Parse(lObjReader["Id_TipoProduto"].ToString()); }

                    if (!String.IsNullOrEmpty(lObjReader["Desc_TipoProduto"].ToString())) { lObjProduto.Desc_TipoProduto = lObjReader["Desc_TipoProduto"].ToString(); }

                    if (!String.IsNullOrEmpty(lObjReader["TipoProdutoAtivo"].ToString())) { lObjProduto.Ativo = (lObjReader["TipoProdutoAtivo"].ToString() == "1" ? true : false); }

                    lLstProdutos.Add(lObjProduto);
                }

                return lLstProdutos;
            }
            catch (Exception ex)
            {
                strSQL = null;
                lLstParametros.Clear();
                FecharDataReader(lObjReader);

                throw ex;
                //return new List<ProdutoINFO>();
            }
            finally
            {
                strSQL = null;
                lLstParametros.Clear();
                FecharDataReader(lObjReader);
            }
        }

        public static ProdutoViewModel MontarInfoProduto(IDataReader pObjReader)
        {
            try
            {
                //Instanciando os objetos para atribuição de valores
                ProdutoViewModel lObjProduto = new ProdutoViewModel
                {
                    TipoProduto = new TipoProdutoViewModel(),
                    Sabor = new SaborViewModel(),
                    Preco = new PrecoViewModel()
                };
                //Objeto ProdutoINFO
                if (!String.IsNullOrEmpty(pObjReader["Id_TipoProdutoXPreco"].ToString()))
                {
                    lObjProduto.Id = int.Parse(pObjReader["Id_TipoProdutoXPreco"].ToString());
                }

                if (!String.IsNullOrEmpty(pObjReader["Id_Produto"].ToString()))
                {
                    lObjProduto.Id_Produto = int.Parse(pObjReader["Id_Produto"].ToString());
                }

                if (!String.IsNullOrEmpty(pObjReader["Nome_Produto"].ToString()))
                {
                    lObjProduto.Nome_Produto = pObjReader["Nome_Produto"].ToString();
                }

                if (!String.IsNullOrEmpty(pObjReader["Desc_Produto"].ToString()))
                {
                    lObjProduto.Desc_Produto = pObjReader["Desc_Produto"].ToString();
                }

                if (!String.IsNullOrEmpty(pObjReader["Vali_Produto"].ToString()))
                {
                    lObjProduto.Vali_Produto = pObjReader["Vali_Produto"].ToString();
                }

                if (!String.IsNullOrEmpty(pObjReader["ProdutoAtivo"].ToString()))
                {
                    lObjProduto.Ativo = (pObjReader["ProdutoAtivo"].ToString() == "1" ? true : false);
                }
                //Objeto TipoProdutoINFO
                if (!String.IsNullOrEmpty(pObjReader["Id_TipoProduto"].ToString()))
                {
                    lObjProduto.TipoProduto.Id_TipoProduto = int.Parse(pObjReader["Id_TipoProduto"].ToString());
                }

                if (!String.IsNullOrEmpty(pObjReader["Desc_TipoProduto"].ToString()))
                {
                    lObjProduto.TipoProduto.Desc_TipoProduto = pObjReader["Desc_TipoProduto"].ToString();
                }

                if (!String.IsNullOrEmpty(pObjReader["TipoProdutoAtivo"].ToString()))
                {
                    lObjProduto.TipoProduto.Ativo = (pObjReader["TipoProdutoAtivo"].ToString() == "1" ? true : false);
                }
                //Objeto PrecoINFO
                if (!String.IsNullOrEmpty(pObjReader["Id_Preco"].ToString()))
                {
                    lObjProduto.Preco.Id_Preco = int.Parse(pObjReader["Id_Preco"].ToString());
                }

                if (!String.IsNullOrEmpty(pObjReader["Valor_Produto"].ToString()))
                {
                    lObjProduto.Preco.Valor_Produto = float.Parse(pObjReader["Valor_Produto"].ToString());
                }
                //Objeto SaborINFO
                if (!String.IsNullOrEmpty(pObjReader["Id_Sabor"].ToString()))
                {
                    lObjProduto.Sabor.Id_Sabor = int.Parse(pObjReader["Id_Sabor"].ToString());
                }

                if (!String.IsNullOrEmpty(pObjReader["Desc_Sabor"].ToString()))
                {
                    lObjProduto.Sabor.Desc_Sabor = pObjReader["Desc_Sabor"].ToString();
                }

                if (!String.IsNullOrEmpty(pObjReader["SaborAtivo"].ToString()))
                {
                    lObjProduto.Sabor.Ativo = (pObjReader["SaborAtivo"].ToString() == "1" ? true : false);
                }

                return lObjProduto;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
