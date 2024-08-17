using Dapper;
using FreakyFashionAPI.Models;
using FreakyFashionAPI.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreakyFashionAPI.Repositories
{
    public class OrderRepository
    {
        private readonly DapperContext _context;

        public OrderRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            var query = "SELECT * FROM Orders";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Order>(query);
            }
        }

        public async Task<int> PlaceOrder(Order order)
        {
            var query = "INSERT INTO Orders (ProductId, Quantity, TotalPrice, OrderDate) VALUES (@ProductId, @Quantity, @TotalPrice, @OrderDate)";
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, order);
            }
        }
    }
}
