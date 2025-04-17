using ClothesWebAPI.Models;
using ClothesWebUI.Models;

namespace ClothesWebUI.LibsServices
{
    public interface IProductService
    {
        Task<ResponseModels<List<Products>>> GetAllProductsAsync();

        Task<Products> GetProductById(int id);

        Task<bool> InsertProduct(Products product);

        Task<bool> UpdateProduct(int id, Products updatedProduct);

        Task<bool> DeleteProducts(int id);

        Task<List<Products>> SearchByName(string keyword);
    }
}
