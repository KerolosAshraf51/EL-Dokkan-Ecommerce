using E_Commerce_API_Angular_Project.DTO;
using E_Commerce_API_Angular_Project.IRepository;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace E_Commerce_API_Angular_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemsController(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderItem(int id)
        {
            var orderItem = _orderItemRepository.GetOrderItemById(id);
            if (orderItem == null) return NotFound();

            return Ok(orderItem);
        }

        [HttpGet("order/{orderId}")]
        public IActionResult GetOrderItemsByOrderId(int orderId)
        {
            List<OrderItem> orderItems = _orderItemRepository.GetOrderItemsByOrderId(orderId);
            return Ok(orderItems);
        }

        [HttpPost]
        [Route("CreateOrderItem")]
        public ActionResult CreateOrderItem(OrderItemDTO orderItemDto)
        {
            OrderItem orderItem = new OrderItem();
            orderItem.OrderId = orderItemDto.OrderId;
            orderItem.ProductId = orderItemDto.ProductId;
            orderItem.Quantity = orderItemDto.Quantity;
            orderItem.PriceAtPurchase = orderItemDto.Price;
            _orderItemRepository.AddOrderItem(orderItem);
            _orderItemRepository.Save();
            //return CreatedAtAction(nameof(GetOrderItem), new { id = orderItemDto.OrderItemId }, orderItemDto);
            return Ok(orderItem);
            
        }
        //[HttpPost]
        //[Route("CreateOrderItemsFromCartItems")]
        //public IActionResult CreateOrderItemsFromCartItems(CheckoutDTO checkoutDTO)
        //{
        //    foreach(var item in checkoutDTO.cartItems)
        //    {
        //         OrderItem orderItem = new OrderItem();
        //        orderItem.OrderId = checkoutDTO.orderID;
        //        orderItem.ProductId= item.ProductId;
        //        orderItem.PriceAtPurchase = item.Product.Price;
        //        orderItem.Quantity = item.Quantity;
        //         _orderItemRepository.AddOrderItem(orderItem);
        //            _orderItemRepository.Save();
        //    }
        //     return Ok();

        //}
        //[HttpPut("{id}")]
        //public IActionResult UpdateOrderItem(int id, OrderItemDTO orderItemDto)
        //{
        //    if (id != orderItemDto.OrderItemId) 
        //        return BadRequest();

        //    _orderItemRepository.UpdateOrderItem(orderItemDto);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public void DeleteOrderItem(int id)
        //{
        //    await _orderItemRepository.DeleteOrderItemAsync(id);
        //    return NoContent();
        //}
    }
}
