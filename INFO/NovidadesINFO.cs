using System;

namespace INFO
{
    public class NovidadesINFO
    {
        public int IdNovidade { get; set; }
        public string TituloNovidade { get; set; }        
        public string DescNovidade { get; set; }
        public DateTime DtNovidade { get; set; }
        ProdutoINFO Produto { get; set; }
    }
}
