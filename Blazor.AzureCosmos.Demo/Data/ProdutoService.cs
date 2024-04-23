

using Microsoft.Azure.Cosmos;

namespace Blazor.AzureCosmos.Demo.Data
{
    public class ProdutoService : IProdutoService
    {
        //rodando localmente no cosmos emulator
        private readonly string CosmosDbConnectString = "<string de conexao do seu cosmos db>";
        private readonly string CosmosDbDatabaseName = "eDevDemo";
        private readonly string CosmosDbContainerName = "Produtos";

        private Container GetContainerClient()
        {

            CosmosClient cosmosDbClient = new CosmosClient(CosmosDbConnectString);
            Database database = cosmosDbClient.GetDatabase(CosmosDbDatabaseName);
            var container = database.GetContainer(CosmosDbContainerName);

            return container;
        }

        public async Task<List<Produto>> GetProdutosAsync()
        {
            try
            {
                var container = GetContainerClient();
                FeedIterator<Produto> queryIterator = container.GetItemQueryIterator<Produto>(new QueryDefinition("SELECT * FROM c"));

                var results = new List<Produto>();

                while (queryIterator.HasMoreResults)
                {
                    FeedResponse<Produto> responseResult = await queryIterator.ReadNextAsync();
                    foreach (var produto in responseResult)
                    {
                        results.Add(produto);
                    }

                }
                return results;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Produto> GetProdutoAsync(string? id)
        {
            var container = GetContainerClient();
            try
            {
                ItemResponse<Produto> response = await container.ReadItemAsync<Produto>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<Produto> AddProdutoAsync(Produto produto)
        {
            try
            {
                var container = GetContainerClient();
                produto.id = Guid.NewGuid();
                produto.DataCadastro = DateTime.Now;
                var response = await container.CreateItemAsync(produto, new PartitionKey(produto.id.ToString()));
                return response.Resource;
            }
            catch (Exception ex )
            {

                throw ex;
            }
        }

        public async Task<Produto> UpdateProdutoAsync(Produto produto)
        {
            try
            {
                var container = GetContainerClient();
                var response = await container.UpsertItemAsync(produto, new PartitionKey(produto.id.ToString()));
                return response.Resource;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task DeleteProdutoAsync(string? id)
        {
            try
            {
                var container = GetContainerClient();
                await container.DeleteItemAsync<Produto>(id, new PartitionKey(id));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
