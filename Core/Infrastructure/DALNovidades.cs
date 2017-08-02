using BarbaraDoces.Models;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class DALNovidades
    {
        public List<NovidadeViewModel> dbRetornarNovidades()
        {
            try
            {
                return new List<NovidadeViewModel>()
                {
                    new NovidadeViewModel()
                    {
                        TituloNovidade = "Produto 1",
                        DescNovidade = "Descrição Produto 1",
                        DtNovidade = DateTime.Now.AddDays(-5)
                    },
                    new NovidadeViewModel()
                    {
                        TituloNovidade = "Produto 2",
                        DescNovidade = "Descrição Produto 2",
                        DtNovidade = DateTime.Now
                    },
                    new NovidadeViewModel()
                    {
                        TituloNovidade = "Produto 3",
                        DescNovidade = "Descrição Produto 3",
                        DtNovidade = DateTime.Now.AddDays(5)
                    },
                };

            }
            catch (Exception ex) { throw ex; }
        }
    }
}
