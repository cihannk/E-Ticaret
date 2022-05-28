using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;
using Microsoft.EntityFrameworkCore;

namespace ETicaretWebApi.Application.Operations.OrderOperations.Queries.GetUserOrders
{
    public class GetUserOrdersQuery : IQuery<List<Order>>
    {
        private readonly ETicaretDbContext _context;
        public int UserId { get; set; }

        public GetUserOrdersQuery(ETicaretDbContext context)
        {
            _context = context;
        }
        public List<Order> Handle()
        {
            var orders = _context.Orders.Include(x => x.CartItems).ThenInclude(x => x.Product).Where(x => x.UserId == UserId).ToList();
            if (orders == null)
            {
                throw new InvalidOperationException("Order is not exist");
            }
            return orders;
        }
    }
}
