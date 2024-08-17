using Dapper;
using FreakyFashionAPI.Models;
using FreakyFashionAPI.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreakyFashionAPI.Repositories
{
    public class ProductRepository
    {
        private readonly DapperContext _context;

        public ProductRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var query = "SELECT * FROM Products";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Product>(query);
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            var query = "SELECT * FROM Products WHERE ProductId = @Id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Product>(query, new { Id = id });
            }
        }

        public async Task<int> AddProduct(Product product)
        {
            var query = "INSERT INTO Products (Name, Description, Price, ImageUrl) VALUES (@Name, @Description, @Price, @ImageUrl)";
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, product);
            }
        }

        public async Task<int> UpdateProduct(Product product)
        {
            var query = "UPDATE Products SET Name = @Name, Description = @Description, Price = @Price, ImageUrl = @ImageUrl WHERE ProductId = @ProductId";
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, product);
            }
        }

        public async Task<int> DeleteProduct(int id)
        {
            var query = "DELETE FROM Products WHERE ProductId = @Id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
