namespace Blazor.AzureCosmos.Demo.Data
{
    public interface IProdutoService
    {
        Task<List<Produto>> GetProdutosAsync();
        Task<Produto> GetProdutoAsync(string? id);
        Task<Produto> AddProdutoAsync(Produto produto);
        Task<Produto> UpdateProdutoAsync(Produto produto);
        Task DeleteProdutoAsync(string id);
    }
}