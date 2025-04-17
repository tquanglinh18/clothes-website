using System;
using System.Text.Json;
using ClothesWebAPI.Models;
using ClothesWebUI.Models;

namespace ClothesWebUI.LibsServices
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAPI = "https://localhost:7167/";

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_baseAPI);
        }

        public Task<ResponseModels<List<Products>>> DeleteProducts(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModels<List<Products>>> GetAllProductsAsync()
        {
            var response = await _httpClient.GetAsync("/Products/list-products");
            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var result = JsonSerializer.Deserialize<ResponseModels<List<Products>>>(content, options);

            return result ?? new ResponseModels<List<Products>>
            {
                Code = (int)response.StatusCode,
                Message = "Không thể lấy dữ liệu",
                IsSuccessed = false,
                Data = new List<Products>()
            };
        }

        public Task<Products> GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertProduct(Products product)
        {
            throw new NotImplementedException();
        }

        public Task<List<Products>> SearchByName(string keyword)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProduct(int id, Products updatedProduct)
        {
            throw new NotImplementedException();
        }

        Task<bool> IProductService.DeleteProducts(int id)
        {
            throw new NotImplementedException();
        }

    }
}

