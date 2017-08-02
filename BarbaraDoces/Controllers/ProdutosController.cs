using BarbaraDoces.Context;
using BarbaraDoces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BarbaraDoces.Controllers
{
    public class ProdutosController : Controller
    {

        // GET: Produto
        public ActionResult Todos(string id)
        {
            if (String.IsNullOrEmpty(id))
                return View("Produtos");
            else
            {
                try
                {
                    int id_pro = int.Parse(id);
                    return Produtos(id_pro);
                }
                catch
                {
                    ViewBag.Error("Produto não encontrado");
                    return View("Produtos", ViewBag);
                }
            }
        }

        // GET: Produto
        public string Teste(string produto, int idproduto)
        {
            return "<br/> Teste 02 " +
                   "<br/> Controller " +
                   "<br/> Teste de Parâmetros: " +
                   "<br/> Produto: " + produto +
                   "<br/> Id: " + idproduto;
        }

        // GET: Produto
        public ActionResult Index(int id)
        {
            return View();
        }

        #region "Itens do Menu"

        //// GET: Produtos
        public ActionResult Produtos(int? id)
        {
            List<ProdutoViewModel> lLstProduto = new List<ProdutoViewModel>();
            //IQueryable<Novidade> lLstProduto;
            try
            {
                using (var context = new Entities())
                {
                    if (id == 0)
                        //Select sem categoria selecionada
                        lLstProduto = (
                         from a in context.TipoProdutoXProdutoXSaborXPreco
                         from b in context.TipoProduto.Where(b => b.Id_TipoProduto == a.Id_TipoProduto && b.Ativo == true)
                         from c in context.Produto.Where(c => c.Id_Produto == a.Id_Produto && c.Ativo == true)
                         from d in context.Preco.Where(d => d.Id_Preco == a.Id_Preco)
                         from e in context.Sabor.Where(e => e.Id_Sabor == a.Id_Sabor && e.Ativo == true)
                         select new ProdutoViewModel
                         {
                             Nome_Produto = c.Nome_Produto,
                             Desc_Produto = c.Desc_Produto,
                             Ativo = c.Ativo,
                             Vali_Produto = c.Vali_Produto,
                             Preco = new PrecoViewModel() { Id_Preco = a.Id_Preco, Valor_Produto = d.Valor_Produto },
                             Sabor = new SaborViewModel() { Id_Sabor = a.Id_Sabor, Ativo = e.Ativo, Desc_Sabor = e.Desc_Sabor },
                             TipoProduto = new TipoProdutoViewModel() { Id_TipoProduto = a.Id_TipoProduto, Desc_TipoProduto = b.Desc_TipoProduto, Ativo = b.Ativo }
                         }).ToList();
                    else
                        //Select com categoria selecionada
                        lLstProduto = (
                         from a in context.TipoProdutoXProdutoXSaborXPreco.Where(a => a.Id_TipoProduto == id)

                         from b in context.TipoProduto.Where(b => b.Id_TipoProduto == a.Id_TipoProduto && b.Ativo == true)
                         from c in context.Produto.Where(c => c.Id_Produto == a.Id_Produto && c.Ativo == true)
                         from d in context.Preco.Where(d => d.Id_Preco == a.Id_Preco)
                         from e in context.Sabor.Where(e => e.Id_Sabor == a.Id_Sabor && e.Ativo == true)
                         select new ProdutoViewModel
                         {
                             Nome_Produto = c.Nome_Produto,
                             Desc_Produto = c.Desc_Produto,
                             Ativo = c.Ativo,
                             Vali_Produto = c.Vali_Produto,
                             Preco = new PrecoViewModel() { Id_Preco = a.Id_Preco, Valor_Produto = d.Valor_Produto },
                             Sabor = new SaborViewModel() { Id_Sabor = a.Id_Sabor, Ativo = e.Ativo, Desc_Sabor = e.Desc_Sabor },
                             TipoProduto = new TipoProdutoViewModel() { Id_TipoProduto = a.Id_TipoProduto, Desc_TipoProduto = b.Desc_TipoProduto, Ativo = b.Ativo }
                         }).ToList();

                }
                return View("Produtos", lLstProduto);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return View("Produtos");
            }
        }

        //// GET: Ingredientes
        public ActionResult Ingredientes(int id)
        {
            return View();
        }

        //// GET: Sabores
        public ActionResult Sabores(int id)
        {
            return View();
        }

        //// GET: Precos
        public ActionResult Precos(int id)
        {
            return View();
        }

        //// GET: Validade
        public ActionResult Validade(int id)
        {
            return View();
        }

        #region "Produtos"

        //// GET: Pães de Mel
        public ActionResult Paes_de_mel(string id)
        {
            if (String.IsNullOrEmpty(id))
                return View("Produtos");
            else
            {
                try
                {
                    int id_pro = int.Parse(id);
                    return Produtos(id_pro);
                }
                catch
                {
                    ViewBag.Error("Produto não encontrado");
                    return View("Produtos", ViewBag);
                }
            }
        }

        //// GET: Trufas
        public ActionResult Trufas(string id)
        {
            if (String.IsNullOrEmpty(id))
                return View("Produtos");
            else
            {
                try
                {
                    int id_pro = int.Parse(id);
                    return Produtos(id_pro);
                }
                catch
                {
                    ViewBag.Error("Produto não encontrado");
                    return View("Produtos", ViewBag);
                }
            }
        }

        //// GET: Bolos de Pote
        public ActionResult Bolos_de_Pote(string id)
        {
            if (String.IsNullOrEmpty(id))
                return View("Produtos");
            else
            {
                try
                {
                    int id_pro = int.Parse(id);
                    return Produtos(id_pro);
                }
                catch
                {
                    ViewBag.Error("Produto não encontrado");
                    return View("Produtos", ViewBag);
                }
            }
        }

        #endregion

        //// GET: Bolos de Pote
        //Ver todos os produtos de uma categoria expecífica
        public ActionResult Todos_os_produtos(string id)
        {
            //if (idproduto <= 0)
            if (String.IsNullOrEmpty(id))
            {
                return Produtos(0);
            }
            else
            {
                try
                {
                    int id_pro = int.Parse(id);
                    return Produtos(id_pro);
                }
                catch
                {
                    return View("Produtos");
                }
            }
        }

        #endregion

        // GET: Produto/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Produto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produto/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Produto/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Produto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Produto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Produto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
