using System.Security.Claims;
using ESHOP.Models;
using ESHOP.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZarinpalSandbox;

namespace ESHOP.Controllers;

public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;
    public OrderController(IOrderService orderService, IProductService productService)
    {
        _orderService = orderService;
        _productService = productService;
    }

    [Authorize]
    public IActionResult AddToCart(int itemId)
    {
        var product = _productService.GetProductIncludeItem(itemId);
        if (product != null)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            var order = _orderService.GetOrderByUserId(userId);

            if (order != null)
            {
                var orderDetail = _orderService.GetOrderDetailByProductId(itemId, order.OrderId);
                if (orderDetail != null)
                {
                    orderDetail.Count += 1;
                }
                else
                {
                    _orderService.InsertOrderDetail(new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        Price = product.Item.Price,
                        ProductId = product.Id,
                        Count = 1
                    });
                }
            }
            else
            {
                order = new Order()
                {
                    UserId = userId,
                    CreateDate = DateTime.Now,
                    IsOrderFinished = false,
                };
                _orderService.InsertOrder(order);
                _orderService.Save();
                _orderService.InsertOrderDetail(new OrderDetail()
                {
                    OrderId = order.OrderId,
                    Price = product.Item.Price,
                    ProductId = product.Id,
                    Count = 1
                });
            }
        }
        _orderService.Save();
        return RedirectToAction("ShowCart");
    }

    [Authorize]
    public IActionResult ShowCart()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
        var order = _orderService.GetOrderIncludeDetailAndProduct(userId);

        return View(order);
    }
    [Authorize]
    public IActionResult RemoveItemFromCart(int detailId)
    {
        var orderDetail = _orderService.GetOrderDetail(detailId);
        if (orderDetail.Count > 1)
        {
            orderDetail.Count -= 1;
        }
        else
        {
            _orderService.DeleteOrderDetail(orderDetail);
        }
        _orderService.Save();
        
        return RedirectToAction("ShowCart");
    }

    [Authorize]
    public IActionResult Payment()
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var order = _orderService.GetOrderIncludeDetails(userId);
        if (order == null)
            return NotFound();

        var payment = new Payment((int)order.Details.Sum(d => d.Price));
        var res = payment.PaymentRequest($"پرداخت فاکتور شماره {order.OrderId}",
            "http://localhost:44373/Home/OnlinePayment/" + order.OrderId, User.Identity.Name, User.FindFirst("PhoneNumber").Value);
        if (res.Result.Status == 100) 
            return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
        
        else
            return BadRequest();
            

    }

    public IActionResult OnlinePayment(int id)
    {
        if (HttpContext.Request.Query["Status"] != "" &&
            HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
            HttpContext.Request.Query["Authority"] != "")
        {
            string authority = HttpContext.Request.Query["Authority"].ToString();
            var order = _orderService.GetOrderByOrderId(id);
            var payment = new Payment((int)order.Details.Sum(d => d.Price));
            var res = payment.Verification(authority).Result;
            if (res.Status == 100)
            {
                order.IsOrderFinished = true;
                _orderService.UpdateOrder(order);
                _orderService.Save();
                ViewBag.code = res.RefId;
                return View();
            }
        }
        return NotFound();
    }
}