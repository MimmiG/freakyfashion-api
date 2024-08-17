using Dapper;
using FreakyFashionAPI.Models;
using FreakyFashionAPI.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreakyFashionAPI.Repositories
{
    public class CartRepository
    {
        private readonly DapperContext _context;

        public CartRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItem>> GetCartItems()
        {
            var query = "SELECT * FROM CartItems";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<CartItem>(query);
            }
        }

        public async Task<int> AddToCart(CartItem cartItem)
        {
            var query = "INSERT INTO CartItems (ProductId, Quantity) VALUES (@ProductId, @Quantity)";
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, cartItem);
            }
        }

        public async Task<int> RemoveFromCart(int id)
        {
            var query = "DELETE FROM CartItems WHERE CartItemId = @Id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
