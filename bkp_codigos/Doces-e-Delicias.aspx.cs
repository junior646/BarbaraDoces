using System;
using INFO;
using BLL;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Doces.Relatorios;
using Doces.Relatorios.dsRelatorioTableAdapters;

namespace Doces
{
    public partial class Doces_e_Delicias : System.Web.UI.Page
    {
        #region "Load do Form"

        crProdutos rpt;

        protected void Page_Init(object sender, EventArgs e)
        {

            if (Session["report"] != null)
            {
                crVisualizar.ReportSource = (crProdutos)Session["report"];

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!PreencherComboProdutos())
                {
                    lblMensagem.Text = "Desculpe, não conseguimos localizar os produtos em nosso banco de dados.";
                }

                if (!PreencherListaTipoPedido())
                {
                    lblMensagem.Text = "Desculpe, não conseguimos localizar os tipos de pedidos em nosso banco de dados.";
                }

            }

        }
        #endregion

        #region "Ação dos Objetos"
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            if (!Valdidacao())
            { return; }

            //HabilitarBotoes(false);
            if (PreencherGrid("Pesquisar"))
            { HabilitarBotoes(false); }
            else
            { if (lblMensagem.Text == "") { lblMensagem.Text = "Desculpe, não conseguimos localizar o seu pedido! Tente novamente, caso contrário tente mais tarde."; }; return; }
        }

        protected void btnGerarRelatorio_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            if (!Valdidacao())
            { return; }

            //HabilitarBotoes(false);
            if (PreencherGrid("PDF"))
            { HabilitarBotoes(false); }
            else
            { if (lblMensagem.Text == "") { lblMensagem.Text = "Desculpe, não conseguimos localizar o seu pedido! Tente novamente, caso contrário tente mais tarde."; }; return; }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            dgvResultado.DataSource = null;
            dgvResultado.DataBind();
            HabilitarBotoes(true);
        }
        #endregion

        #region "Métodos e Funções"

        //Validação do preenchimento dos campos do formulário
        protected bool Valdidacao()
        {
            if (ddlTipo.SelectedIndex < 0)
            { lblMensagem.Text = "Por favor, escolha ao menos um tipo de Produto!"; return false; }

            if (rblTipoEncomenda.SelectedIndex < 0)
            { lblMensagem.Text = "Por favor, escolha ao menos um tipo de Encomenda!"; return false; }

            //Validação das listas
            if (string.IsNullOrEmpty(ddlTipo.SelectedItem.Text))
            { lblMensagem.Text = "Tipo de Produto não selecionado, tente novamente."; return false; }

            if (string.IsNullOrEmpty(rblTipoEncomenda.SelectedValue))
            { lblMensagem.Text = "Tipo de Encomenda não selecionado, tente novamente."; return false; }

            UtilWeb util = new UtilWeb();
            util.ExibirMensagem(Page, "Validação OK!");

            return true;
        }

        //Habilita o bloqueia a visualização dos controles na tela
        protected void HabilitarBotoes(bool Visibilidade)
        {
            //Principal
            divBody.Visible = Visibilidade;
            lblMensagem.Text = "";
            //Resultados
            divResultado.Visible = (!Visibilidade);
            btnVoltar.Visible = (!Visibilidade);
            pnlGrid.Visible = (!Visibilidade);
            dgvResultado.Visible = (!Visibilidade);
            //Relatório
            if (divRelatorio.Visible)
            {
                divResultado.Visible = false;
                dgvResultado.Visible = false;
            }


        }

        //Preenche a combo com o nome dos produtos (Ex: Pão de mel || Trufa || ...)
        protected bool PreencherComboProdutos()
        {
            BLLProduto BLL = null;
            List<TipoProdutoINFO> lstProdutos = null;

            try
            {
                //Instânciando os objetos a serem utilizados
                lstProdutos = new List<TipoProdutoINFO>();

                BLL = new BLLProduto();

                lstProdutos = BLL.busListaProdutos();

                //Adicionando os itens ao dropdown
                ddlTipo.Items.Clear();
                ddlTipo.Items.Add(new ListItem("Todos", "0"));

                foreach (var item in lstProdutos)
                {
                    ddlTipo.Items.Add(new ListItem(item.Desc_TipoProduto, item.Id_TipoProduto.ToString()));
                }

                return true;
            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message.ToString();
                lblMensagem.Visible = true;
                return false;
            }
            finally
            {
                lstProdutos = null;
                BLL = null;
            }
        }

        //Preenhce a Lista de seleção do RadioButton com os tipos de pedidos (Ex: Avulso || Encomenda)
        protected bool PreencherListaTipoPedido()
        {
            BLLProduto BLL = null;
            List<ProdutoINFO> lstProdutos = null;

            try
            {
                //Instânciando os objetos a serem utilizados
                lstProdutos = new List<ProdutoINFO>();

                BLL = new BLLProduto();

                lstProdutos = BLL.busListaTipoPedido();

                //Adicionando os itens ao dropdown
                rblTipoEncomenda.Items.Clear();
                rblTipoEncomenda.Items.Add(new ListItem("Todos", "0"));

                foreach (var item in lstProdutos)
                {
                    rblTipoEncomenda.Items.Add(new ListItem(item.Desc_Produto, item.Desc_Produto.ToString()));
                }

                return true;
            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message.ToString();
                lblMensagem.Visible = true;
                return false;
            }
            finally
            {
                lstProdutos = null;
                BLL = null;
            }

        }

        //Preenche o Grid com os dados encontrados
        protected bool PreencherGrid(string tipoPesquisa)
        {
            string tipoProduto = "";
            string tipoEncomenda = "";
            BLLProduto BLL = null;
            List<ProdutoINFO> lstProdutos = null;

            try
            {
                //Instânciando os objetos a serem utilizados
                lstProdutos = new List<ProdutoINFO>();

                BLL = new BLLProduto();

                //Atribuindo o conteúdo dos drop down lists para as variaveis locais
                tipoProduto = ddlTipo.SelectedItem.Text.Trim();
                tipoEncomenda = rblTipoEncomenda.SelectedItem.Text.Trim();

                //Realizando a pesquisa no banco e retornando os dados
                lstProdutos = BLL.busDadosCompletosProdutos(tipoProduto, tipoEncomenda);

                //Populando o Grid com os dados retornados da lista
                if (lstProdutos.Count <= 0)
                {
                    dgvResultado.DataSource = null;
                    dgvResultado.DataBind();
                }
                else
                {
                    var dtProdutos = PreencherDataTable(lstProdutos);
                    var dsProdutos = PreencherDataSet(lstProdutos);

                    if (tipoPesquisa == "Pesquisar")
                    {
                        dgvResultado.DataSource = dtProdutos;
                        //dgvResultado.Columns[0].Visible = false;
                        dgvResultado.DataBind();
                        divRelatorio.Visible = false;
                    }
                    else
                    {
                        if (!MontarRelatorio(dsProdutos))
                        {
                            lblMensagem.Text = "Ocorreram alguns erros ao tentar gerar o relatório, tente novamente.";
                            return false;
                        }

                        divRelatorio.Visible = true;
                    }
                    dtProdutos = null;
                    dsProdutos = null;
                }

                return true;

            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message.ToString();
                lblMensagem.Visible = true;
                return false;
                //lblMensagem.Text = "Ops! Desculpe, ocorreu um erro interno, tente novamente ou entre em contato com o administrador do site.";
            }
            finally
            {
                BLL = null;
                lstProdutos = null;
                tipoProduto = null;
                tipoEncomenda = null;
            }

        }

        //Criação de um DataTable já populado com as informações da Lista de Produtos
        protected DataTable PreencherDataTable(List<ProdutoINFO> lLstProduto)
        {
            DataTable dtProduto;
            ProdutoINFO objProduto;

            try
            {

                dtProduto = new DataTable("Produtos");
                //Títulos das Colunas
                dtProduto.Columns.Add("Id", typeof(int));
                dtProduto.Columns.Add("TipoDeProduto", typeof(string));
                dtProduto.Columns.Add("Produto", typeof(string));
                dtProduto.Columns.Add("Sabor", typeof(string));
                dtProduto.Columns.Add("Descricao", typeof(string));
                dtProduto.Columns.Add("Validade", typeof(string));
                dtProduto.Columns.Add("Preco", typeof(string));
                //dtProduto.Columns.List[0].Visible = false;
                //Percorrendo a lista para a atribuição de valores as linhas(Rows) do DataTable(Table)
                foreach (var item in lLstProduto)
                {
                    //Atribuindo valor ao objetos do INFO
                    objProduto = new ProdutoINFO
                    {
                        //PedidoINFO
                        Id = item.Id,
                        Id_Produto = item.Id_Produto,
                        Nome_Produto = item.Nome_Produto,
                        Desc_Produto = item.Desc_Produto,
                        Vali_Produto = item.Vali_Produto,
                        Ativo = item.Ativo,
                        //PedidoINFO.PrecoINFO
                        Preco = new PrecoINFO { Id_Preco = item.Preco.Id_Preco, Valor_Produto = item.Preco.Valor_Produto },
                        //PedidoINFO.SaborINFO
                        Sabor = new SaborINFO { Id_Sabor = item.Sabor.Id_Sabor, Desc_Sabor = item.Sabor.Desc_Sabor, Ativo = item.Sabor.Ativo },
                        //PedidoINFO.TipoProdutoINFO
                        TipoProduto = new TipoProdutoINFO { Id_TipoProduto = item.TipoProduto.Id_TipoProduto, Desc_TipoProduto = item.TipoProduto.Desc_TipoProduto, Ativo = item.TipoProduto.Ativo },
                    };
                    //Adicionando máscara R$ 0,00 ao valor
                    string valorEmDinheiro = "R$ ";
                    valorEmDinheiro += string.Format("{0:#.00}", Convert.ToDecimal(objProduto.Preco.Valor_Produto.ToString()));
                    //Inserindo a Linha no Data Table
                    dtProduto.Rows.Add(objProduto.Id, objProduto.TipoProduto.Desc_TipoProduto, objProduto.Nome_Produto, objProduto.Sabor.Desc_Sabor, objProduto.Desc_Produto, objProduto.Vali_Produto, valorEmDinheiro);
                    //dtProduto.Rows.Add(1, "Coluna 2 String", "Coluna 3 String", "Coluna 4 String", 5);
                }
                return dtProduto;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtProduto = null;
                objProduto = null;
            }
        }

        //Criação de um DataSet já populado com as informações da Lista de Produtos
        protected dsRelProdutos PreencherDataSet(List<ProdutoINFO> lLstProduto)
        {
            dsRelProdutos ds;
            ProdutoINFO objProduto;

            try
            {
                ds = new dsRelProdutos();

                foreach (var item in lLstProduto)
                {
                    //Atribuindo valor ao objetos do INFO
                    objProduto = new ProdutoINFO
                    {
                        //PedidoINFO
                        Id = item.Id,
                        Id_Produto = item.Id_Produto,
                        Nome_Produto = item.Nome_Produto,
                        Desc_Produto = item.Desc_Produto,
                        Vali_Produto = item.Vali_Produto,
                        Ativo = item.Ativo,
                        //PedidoINFO.PrecoINFO
                        Preco = new PrecoINFO { Id_Preco = item.Preco.Id_Preco, Valor_Produto = item.Preco.Valor_Produto },
                        //PedidoINFO.SaborINFO
                        Sabor = new SaborINFO { Id_Sabor = item.Sabor.Id_Sabor, Desc_Sabor = item.Sabor.Desc_Sabor, Ativo = item.Sabor.Ativo },
                        //PedidoINFO.TipoProdutoINFO
                        TipoProduto = new TipoProdutoINFO { Id_TipoProduto = item.TipoProduto.Id_TipoProduto, Desc_TipoProduto = item.TipoProduto.Desc_TipoProduto, Ativo = item.TipoProduto.Ativo },
                    };
                    //Adicionando máscara R$ 0,00 ao valor
                    string valorEmDinheiro = "R$ ";
                    valorEmDinheiro += string.Format("{0:#.00}", Convert.ToDecimal(objProduto.Preco.Valor_Produto.ToString()));
                    //Inserindo a Linha no Data Table
                    ds.Produtos.Rows.Add(objProduto.Id, objProduto.TipoProduto.Desc_TipoProduto, objProduto.Nome_Produto, objProduto.Sabor.Desc_Sabor, objProduto.Desc_Produto, objProduto.Vali_Produto, valorEmDinheiro);
                }

                return ds;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ds = null;
                objProduto = null;
            }
        }

        //Gera o relatório no Crystal Reports
        protected bool MontarRelatorio(DataTable objDataTable)
        {
            //ReportDocument cryRpt;

            try
            {

                //crRelatorio = new crProdutos();
                //crRelatorio.SetDataSource(objDataSet);
                //crVisualizar.ReportSource = crRelatorio;


                rpt = new crProdutos();
                rpt.SetDataSource(objDataTable);

                crVisualizar.ReportSource = rpt;
                crVisualizar.RefreshReport();

                Session.Add("report", rpt);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //cryRpt = null;
            }

        }
        protected bool MontarRelatorio(dsRelProdutos objDataSet)
        {
            
            //ReportDocument cryRpt;
            try
            {
               RelatorioTableAdapter da = new RelatorioTableAdapter();

                dsRelatorio ds = new dsRelatorio();
                dsRelatorio.RelatorioDataTable dt = (dsRelatorio.RelatorioDataTable)ds.Tables["Relatorio"];
                da.Fill(dt, true);

                rpt = new crProdutos();
                rpt.SetDataSource(ds);
                crVisualizar.ReportSource = rpt;

                //crRelatorio = new crProdutos();
                //crRelatorio.SetDataSource(objDataSet);
                //crVisualizar.ReportSource = crRelatorio;


                //rpt = new crProdutos();
                //rpt.SetDataSource(objDataSet);

                //crVisualizar.ReportSource = rpt;
                //crVisualizar.RefreshReport();
                //crVisualizar.DisplayPage = true;
                Session.Add("report", rpt);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //cryRpt = null;
            }

        }

        #endregion

        #region "Trechos comentados para auto ajuda"
        // Preenchendo o objeto objProduto com *With ... End With do Vb.Net
        //
        //objProduto = new ProdutoINFO
        //{
        //    Preco = new PrecoINFO(),
        //    Sabor = new SaborINFO(),
        //    TipoProduto = new TipoProdutoINFO(),
        //};

        //objProduto = new ProdutoINFO
        //{
        //    Id = 1,
        //    Preco = new PrecoINFO { Id_Preco = 1, Valor_Produto = 7 },
        //    Sabor = new SaborINFO { Id_Sabor = 1, Desc_Sabor = "Limão", Ativo = true },
        //    TipoProduto = new TipoProdutoINFO { Id_TipoProduto = 1, Desc_TipoProduto = "Trufa", Ativo = true },
        //    Id_Produto = 1,
        //    Nome_Produto = "Trufa",
        //    Desc_Produto = "Trufa Pequena",
        //    Vali_Produto = "7 dias",
        //    Ativo = true
        //};
        #endregion
    }
}