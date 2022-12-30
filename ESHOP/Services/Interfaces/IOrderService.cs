using ESHOP.Models;

namespace ESHOP.Services.Interfaces;

public interface IOrderService
{
    Order GetOrderByUserId(int userId);
    OrderDetail GetOrderDetailByProductId(int productId, int orderId);
    void Save();
    void InsertOrderDetail(OrderDetail orderDetail);
    void InsertOrder(Order order);
    Order GetOrderIncludeDetailAndProduct(int userId);
    OrderDetail GetOrderDetail(int detailId);
    void DeleteOrderDetail(OrderDetail orderDetail);
    Order GetOrderIncludeDetails(int userId);
    Order GetOrderByOrderId(int orderId);
    void UpdateOrder(Order order);
}