using System.Collections;
using System.Text;
using System.Text.Json;
using VshopWeb.Models;
using VshopWeb.Services.Interfaces;

namespace VshopWeb.Services;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _httpClientFactory;

    private const string _apiEndpoint = "/api/Products;";

    private readonly JsonSerializerOptions _options;

    private ProductViewModel _productVM;

    private IEnumerable<ProductViewModel> productVMs;

    public ProductService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<ProductViewModel> CreateProduct(ProductViewModel productViewModel)
    {
        var client = _httpClientFactory.CreateClient("ProductApi");
        StringContent content = new StringContent(JsonSerializer
            .Serialize(productViewModel), Encoding.UTF8, "application/json");

        using ( var response = await client.PostAsync(_apiEndpoint, content)){

            if(response.IsSuccessStatusCode){

                var apiResponse = await response.Content.ReadAsStreamAsync();
                _productVM = await JsonSerializer
                    .DeserializeAsync<ProductViewModel>(apiResponse,_options);
            }
            else { return null; }

            return _productVM;
        }
    }

    public async Task<bool> DeleteProductById(int id)
    {
        var client = _httpClientFactory.CreateClient("ProductApi");

        using (var clientResponse = await client.GetAsync(_apiEndpoint + id)) {
            if(clientResponse.IsSuccessStatusCode){
                return true;
            }
        }
        return false;
    }

    public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
    {
        //start creating a new instance of client, named ProductApi
        var client = _httpClientFactory.CreateClient("ProductApi");
        /* the using word here means that we are going to release unused resources
        then we use the method GetAsync, passing the endpoint. This is the route control. If we need to pass
        a id, we passa _apiEndPoint + id. Or if we need to access another layer of the API, we pass
        _apiEndPoint + "layer"+ id, for example */
        using (var response = await client.GetAsync(_apiEndpoint)){

            if(response.IsSuccessStatusCode){

                var apiResponse = await response.Content.ReadAsStreamAsync();
                productVMs = await JsonSerializer
                    .DeserializeAsync<IEnumerable<ProductViewModel>>(apiResponse,_options);
            }
            else{
                return null;
            }
        }
        return productVMs;
    }

    public async Task<ProductViewModel> GetProductById(int id)
    {
        var client = _httpClientFactory.CreateClient("ProductApi");

        using(var response = await client.GetAsync(_apiEndpoint + id)){
            if(response.IsSuccessStatusCode){
                var responseApi = await response.Content.ReadAsStreamAsync();

                _productVM = await JsonSerializer
                    .DeserializeAsync<ProductViewModel>(responseApi);
            
            }
            else { return null; }
        }
        return _productVM;
    }

    public async Task<ProductViewModel> UpdateProduct(ProductViewModel productViewModel)
    {
        var client = _httpClientFactory.CreateClient("ProductApi");

        /*StringContent content = new StringContent(JsonSerializer.Serialize(productViewModel),
            Encoding.UTF8, "application/json");
        
        same as using string content to serialize the object beforehand*/
        using(var response = await client.PutAsJsonAsync(_apiEndpoint, productViewModel)){
            if(response.IsSuccessStatusCode){
                var responseApi = await response.Content.ReadAsStreamAsync();
                _productVM = await JsonSerializer
                    .DeserializeAsync<ProductViewModel>(responseApi);
            }
            else { return null; }
        }
        return _productVM;
    }
}