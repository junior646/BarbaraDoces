using BarbaraDoces.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BarbaraDoces.Controllers
{
    public class NovidadesController : Controller
    {
        // GET: Novidades
        public ActionResult Index()
        {
            List<Novidade> lLstProduto = new List<Novidade>();
            //IQueryable<Novidade> lLstProduto;
            try
            {
                using (var context = new Entities())
                {
                    lLstProduto = context.Novidade.Where(m => m.Desc_Novidade != null).OrderByDescending(m => m.Dt_Novidade).ToList();
                }
                return View("Novidades",lLstProduto);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return View("Novidades");
            }
        }
    }
}