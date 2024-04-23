namespace Blazor.AzureCosmos.Demo.Data
{
    public class Produto 
    {
        public Guid? id { get; set; } // precisa ser minusculo para o cosmos
        public string? Nome { get; set; }
        public decimal? Preco { get; set; }
        public int? Quantidade { get; set; }
        public DateTime DataCadastro { get; set; }

    }
}
