using ClothesWebAPI.Context;
using ClothesWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClothesWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _dbContext;
        public ProductsController(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        [HttpGet]
        [Route("/Products/list-products")]
        public IActionResult GetList()
        {
            var lstProducts = _dbContext.Products.ToList();

            if (lstProducts != null)
            {
                return Ok(new ResponseModels<List<Products>>
                {
                    Code = 200,
                    Data = lstProducts,
                    Message = "Lấy danh sách sản phẩm thành công!",
                    IsSuccessed = true
                });
            }
            else
            {
                return Ok(new ResponseModels<List<Products>>
                {
                    Code = 404,
                    Data = new List<Products>(),
                    Message = "Không có sản phẩm nào!",
                    IsSuccessed = true
                });
            }
        }

        [HttpPost]
        [Route("/Products/Insert")]
        public IActionResult AddProducts([FromBody] Products product)
        {
            //Handle Case: Dữ liệu đầu vào chưa hợp lệ
            if (string.IsNullOrWhiteSpace(product.Name) || product == null)
            {
                return BadRequest(new ResponseModels<Products>
                {
                    Code = 400,
                    IsSuccessed = false,
                    Message = "Dữ liệu đầu vào không hợp lệ!",
                    Data = null,
                });
            }

            //Handle Case: Nếu sản phẩm đã tồn tại
            var existingProd = _dbContext.Products.FirstOrDefault(p => p.Name == product.Name);

            if (existingProd != null)
            {
                return Conflict(new ResponseModels<Products>
                {
                    Code = 409,
                    Data = product,
                    Message = "Sản phẩm đã tồn tại!",
                    IsSuccessed = false,
                });
            }


            try
            {
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();

                return Ok(new ResponseModels<Products>
                {
                    Code = 200,
                    Data = product,
                    Message = "Thêm thành công!",
                    IsSuccessed = true
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModels<Products>
                {
                    Code = 500,
                    Data = null,
                    Message = "Thêm sản phẩm lỗi" + ex.Message,
                    IsSuccessed = false,
                });
            }
        }

        [HttpPost]
        [Route("/Products/Detail/{id}")]
        public IActionResult GetDetail(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(idItem => idItem.Id == id);

            if (product == null)
            {
                return Ok(new ResponseModels<Products>
                {
                    Code = 404,
                    Data = null,
                    Message = "Không tìm thấy sản phẩm!",
                    IsSuccessed = false,
                });
            }
            else
            {
                return Ok(new ResponseModels<Products>
                {
                    Code = 200,
                    Data = product,
                    Message = "Thành công!",
                    IsSuccessed = true,
                });
            }
        }

        [HttpPost]
        [Route("/Products/Update/{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Products updatedProduct)
        {
            var productCur = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (productCur == null)
            {
                return NotFound(new ResponseModels<Products>
                {
                    Code = 404,
                    Data = null,
                    Message = "Không tìm thấy sản phẩm!",
                    IsSuccessed = false
                });
            }

            try
            {
                // Cập nhật các trường (ưu tiên giữ giá trị cũ nếu client không truyền)
                productCur.Name = updatedProduct.Name ?? productCur.Name;
                productCur.Description = updatedProduct.Description ?? productCur.Description;
                productCur.Brand = updatedProduct.Brand ?? productCur.Brand;
                productCur.Category = updatedProduct.Category ?? productCur.Category;
                productCur.Gender = updatedProduct.Gender ?? productCur.Gender;
                productCur.Price = updatedProduct.Price != 0 ? updatedProduct.Price : productCur.Price;
                productCur.Discount = updatedProduct.Discount != 0 ? updatedProduct.Discount : productCur.Discount;
                productCur.Sizes = updatedProduct.Sizes ?? productCur.Sizes;
                productCur.Colors = updatedProduct.Colors ?? productCur.Colors;
                productCur.Material = updatedProduct.Material ?? productCur.Material;
                productCur.StockQuantity = updatedProduct.StockQuantity != 0 ? updatedProduct.StockQuantity : productCur.StockQuantity;
                productCur.Images = updatedProduct.Images ?? productCur.Images;
                productCur.IsNew = updatedProduct.IsNew;
                productCur.IsFeatured = updatedProduct.IsFeatured;
                productCur.Rating = updatedProduct.Rating ?? productCur.Rating;
                productCur.CreatedAt = updatedProduct.CreatedAt ?? productCur.CreatedAt;
                productCur.UpdatedAt = updatedProduct.UpdatedAt ?? DateTime.Now;

                _dbContext.Products.Update(productCur);
                _dbContext.SaveChanges();

                var productLaster = _dbContext.Products.FirstOrDefault(p => p.Id == id);

                return Ok(new ResponseModels<Products>
                {
                    Code = 200,
                    Data = productLaster,
                    Message = "Thành công!",
                    IsSuccessed = true,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModels<Products>
                {
                    Code = 500,
                    Data = null,
                    Message = "Lỗi " + ex.Message,
                    IsSuccessed = false,
                });
            };
        }

        [HttpDelete]
        [Route("/Products/Delete/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(idItem => idItem.Id == id);
            if (product == null)
            {
                return NotFound(new ResponseModels<Products>
                {
                    Code = 404,
                    Data = null,
                    Message = "Không tìm thấy sản phẩm!",
                    IsSuccessed = false
                });
            }

            try
            {
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
                return Ok(new ResponseModels<Products>
                {
                    Code = 200,
                    Data = null,
                    Message = "Xoá thành công!",
                    IsSuccessed = true,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModels<Products>
                {
                    Code = 500,
                    Data = null,
                    Message = "Lỗi " + ex.Message,
                    IsSuccessed = false,
                });
            };


        }

        [HttpPost]
        [Route("/Products/Search")]
        public IActionResult Search(string keyword)
        {

            try
            {
                var productsFilter = _dbContext.Products.Where(p => !string.IsNullOrEmpty(keyword) && p.Name.Contains(keyword)).ToList();

                if (productsFilter == null || !productsFilter.Any())
                {
                    return Ok(new ResponseModels<Products>
                    {
                        Code = 404,
                        Data = null,
                        Message = "Không tìm thấy sản phẩm nào!",
                        IsSuccessed = true,
                    });
                }
                else
                {
                    return Ok(new ResponseModels<List<Products>>
                    {
                        Code = 200,
                        Data = productsFilter,
                        Message = "Thành công!",
                        IsSuccessed = true,
                    });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModels<Products>
                {
                    Code = 500,
                    Data = null,
                    Message = "Lỗi " + ex.Message,
                    IsSuccessed = false,
                });
            };
        }
    }
}

