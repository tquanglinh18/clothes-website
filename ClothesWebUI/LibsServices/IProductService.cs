using ClothesWebUI.Models;

namespace ClothesWebUI.LibsServices
{
    public interface IProductService
    {
        Task<ResponseModels<List<Products>>> GetAllProducts();

        Task<ResponseModels<Products>> GetProductById(int id);

        Task<ResponseModels<Products>> InsertProduct(Products product);

        Task<ResponseModels<Products>> UpdateProduct(int id, Products updatedProduct);

        Task<ResponseModels<Products>> DeleteProduct(int id);

        Task<ResponseModels<List<Products>>> SearchByName(string keyword);
    }
}
