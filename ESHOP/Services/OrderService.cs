using ESHOP.Data;
using ESHOP.Models;
using ESHOP.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ESHOP.Services;

public class OrderService : IOrderService
{
    private readonly EShopContext _context;
    public OrderService(EShopContext context)
    {
        _context = context;
    }

    public void DeleteOrderDetail(OrderDetail orderDetail) => _context.OrderDetails.Remove(orderDetail);

    public Order GetOrderByOrderId(int orderId)
    {
        return _context.Orders.Include(o => o.Details)
            .FirstOrDefault(o => o.OrderId == orderId);
    }

    public Order GetOrderByUserId(int userId) => _context.Orders.FirstOrDefault(o => o.UserId == userId && !o.IsOrderFinished);
    
    public OrderDetail GetOrderDetail(int detailId) => _context.OrderDetails.Find(detailId);

    public OrderDetail GetOrderDetailByProductId(int productId, int orderId)
    {
        return _context.OrderDetails.FirstOrDefault(o =>
            o.OrderId == orderId && o.ProductId == productId);
    }

    public Order GetOrderIncludeDetailAndProduct(int userId)
    {
        return _context.Orders.Where(o => o.UserId == userId && !o.IsOrderFinished)
            .Include(o => o.Details)
            .ThenInclude(p => p.Product).FirstOrDefault();
    }

    public Order GetOrderIncludeDetails(int userId)
    {
        return _context.Orders
            .Include(o => o.Details)
            .FirstOrDefault(o => o.UserId == userId && !o.IsOrderFinished);
    }

    public void InsertOrder(Order order) => _context.Orders.Add(order);
    
    public void InsertOrderDetail(OrderDetail orderDetail) => _context.OrderDetails.Add(orderDetail);

    public void Save() => _context.SaveChanges();
    
    public void UpdateOrder(Order order) => _context.Orders.Update(order);
    
}