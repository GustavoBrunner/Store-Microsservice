using System.Collections;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging.Console;
using VshopWeb.Models;
using VshopWeb.Services.Interfaces;

namespace VshopWeb.Services;

public class CategoryService : ICategoryService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string _apiEndPoint = "api/Category";
    private readonly JsonSerializerOptions _options;
    private IEnumerable<CategoryViewModel> _categoriesVM;

    private CategoryViewModel _categoryVM;

    public CategoryService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }


    public async Task<IEnumerable<CategoryViewModel>> GetAllCategories()
    {
        var client = _httpClientFactory.CreateClient("ProductApi");

        using ( var response = await client.GetAsync(_apiEndPoint)){
            if(response.IsSuccessStatusCode){
                var responseApi = await response.Content.ReadAsStreamAsync();

                _categoriesVM = await JsonSerializer
                    .DeserializeAsync<IEnumerable<CategoryViewModel>>(responseApi);
            }
            else{ return null; }
        }
        return _categoriesVM;
    }

    public async Task<CategoryViewModel> GetCategoryById(int id)
    {
        var client = _httpClientFactory.CreateClient("ProductApi");

        using(var response = await client.GetAsync(_apiEndPoint + id)){
            if(response.IsSuccessStatusCode){
                var responseApi = await response.Content.ReadAsStreamAsync();

                _categoryVM = JsonSerializer.Deserialize<CategoryViewModel>(responseApi);

            }
            else { return null; }
        }   
        return _categoryVM;
    }

    public async Task<CategoryViewModel> AddCategory(CategoryViewModel categoryViewModel)
    {
        var client = _httpClientFactory.CreateClient("ProductApi");

        StringContent content = new StringContent(JsonSerializer.Serialize(categoryViewModel),
            Encoding.UTF8, "application/json");

        using( var response = await client.PostAsync(_apiEndPoint ,content) ){
            if(response.IsSuccessStatusCode){

                var responseApi = await response.Content.ReadAsStreamAsync();

                _categoryVM = await JsonSerializer
                    .DeserializeAsync<CategoryViewModel>(responseApi,_options);
            }
            else { return null; }
        }

        return _categoryVM;
    }

    public async Task<bool> DeleteCategory(int id)
    {
        var client = _httpClientFactory.CreateClient("ProductApi");

        using ( var response = await client.GetAsync(_apiEndPoint + id)){
            if(response.IsSuccessStatusCode){
                return true;
            }
        }
        return false;
    }


    public async Task<CategoryViewModel> UpdateCategory(CategoryViewModel categoryViewModel)
    {
        var client = _httpClientFactory.CreateClient("ProductApi");

        using(var response = await client.PutAsJsonAsync(_apiEndPoint,categoryViewModel)){
            if(response.IsSuccessStatusCode){
                var responseApi = await response.Content.ReadAsStreamAsync();

                _categoryVM = await JsonSerializer
                    .DeserializeAsync<CategoryViewModel>(responseApi,_options);
            }
        }

        return _categoryVM;
    }
}