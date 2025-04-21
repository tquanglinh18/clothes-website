using ClothesWebUI.Models;
using Newtonsoft.Json;
using System.Text;

namespace ClothesWebUI.LibsServices
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseModels<List<Products>>> GetAllProducts()
        {
            var response = await _httpClient.GetAsync("/Products/list-products");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseModels<List<Products>>>(content);

            return result ?? new ResponseModels<List<Products>>
            {
                Code = (int)response.StatusCode,
                Message = "Không thể lấy danh sách sản phẩm",
                IsSuccessed = false,
                Data = new List<Products>()
            };
        }

        public async Task<ResponseModels<Products>> GetProductById(int id)
        {
            var response = await _httpClient.PostAsync($"/Products/Detail/{id}", null);

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ResponseModels<Products>>(content);

            return result ?? new ResponseModels<Products>
            {
                Code = (int)response.StatusCode,
                Message = "Không thể lấy thông tin sản phẩm",
                IsSuccessed = false,
                Data = null,
            };
        }

        public async Task<ResponseModels<Products>> InsertProduct(Products product)
        {
            var json = JsonConvert.SerializeObject(product);
            var contentData = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Products/Insert", contentData);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseModels<Products>>(content);

            return result ?? new ResponseModels<Products>
            {
                Code = (int)response.StatusCode,
                Message = "Không thể thêm sản phẩm mới",
                IsSuccessed = false,
                Data = null,
            };
        }

        public async Task<ResponseModels<Products>> UpdateProduct(Products updatedProduct)
        {
            var json = JsonConvert.SerializeObject(updatedProduct);
            var contentData = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"/Products/Update/{updatedProduct.ProductID}", contentData);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseModels<Products>>(content);

            return result ?? new ResponseModels<Products>
            {
                Code = (int)response.StatusCode,
                Message = "Không thể cập nhật sản phẩm",
                IsSuccessed = false,
                Data = null,
            };
        }

        public async Task<ResponseModels<Products>> DeleteProduct(int id)
        {
            var response = await _httpClient.DeleteAsync($"/Products/Delete/{id}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseModels<Products>>(content);

            return result ?? new ResponseModels<Products>
            {
                Code = (int)response.StatusCode,
                Message = "Không thể xoá sản phẩm",
                IsSuccessed = false,
                Data = null,
            };
        }

        public async Task<ResponseModels<List<Products>>> SearchByName(string keyword)
        {
            var response = await _httpClient.PostAsync($"/Products/Search?keyword={keyword}", null);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseModels<List<Products>>>(content);

            return result ?? new ResponseModels<List<Products>>
            {
                Code = (int)response.StatusCode,
                Message = "Không thể tìm kiếm sản phẩm",
                IsSuccessed = false,
                Data = new List<Products>(),
            };
        }
    }
}
