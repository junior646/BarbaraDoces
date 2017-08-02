namespace INFO
{
    public class ProdutoINFO
    {
        public int Id { get; set; }
        public int Id_Produto { get; set; }
        public string Nome_Produto { get; set; }
        public string Desc_Produto { get; set; }
        public string Vali_Produto { get; set; }
        public bool Ativo { get; set; }
        public byte[] Imagem_Produto { get; set; }
        public PrecoINFO Preco { get; set; }
        public SaborINFO Sabor { get; set; }
        public TipoProdutoINFO TipoProduto { get; set; }
    }
}
