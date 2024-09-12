using E_Commerce_API_Angular_Project.DTO;
using E_Commerce_API_Angular_Project.IRepository;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API_Angular_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("{id}")]
     
        public IActionResult GetOrder(int id)
        {
            Order order =  _orderRepository.GetOrderById(id);
            OrderWithOrderItemsDTO orderWithOrderItemsDTO = new OrderWithOrderItemsDTO();
            orderWithOrderItemsDTO.items=new List<OrderItemsOfOrderDTO>();
            orderWithOrderItemsDTO.OrderDate =order.OrderDate;
            orderWithOrderItemsDTO.OrderId = order.Id;
            orderWithOrderItemsDTO.ClientName = order.User.UserName;
            orderWithOrderItemsDTO.UpdatedAt = order.UpdatedAt;
            orderWithOrderItemsDTO.TotalAmount = _orderRepository.TotalPriceOfOrder(id);
            orderWithOrderItemsDTO.Status = order.Status;

            foreach (var item in order.OrderItems)
            {
                Product p =_orderRepository.GetProductByOrderItemId(item);

                orderWithOrderItemsDTO.items.Add(new OrderItemsOfOrderDTO
                {
                    ProductId = item.ProductId,
                    ProductName = p.Name,
                    Quantity = item.Quantity,
                    Price = item.PriceAtPurchase,
                }
                );
            }
            if (order == null) return NotFound();

            return Ok(orderWithOrderItemsDTO);
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            List<Order> orders =  _orderRepository.GetAllOrders();
            List< OrderWithOrderItemsDTO > orderWithOrderItemsDTOlist=new List<OrderWithOrderItemsDTO>();
            foreach (var order in orders)
            {
                OrderWithOrderItemsDTO orderWithOrderItemsDTO = new OrderWithOrderItemsDTO();
                orderWithOrderItemsDTO.items = new List<OrderItemsOfOrderDTO>();
                orderWithOrderItemsDTO.OrderDate = order.OrderDate;
                orderWithOrderItemsDTO.OrderId = order.Id;
                orderWithOrderItemsDTO.ClientName = order.User.UserName;
                orderWithOrderItemsDTO.UpdatedAt = order.UpdatedAt;
                orderWithOrderItemsDTO.TotalAmount = _orderRepository.TotalPriceOfOrder(order.Id);
                orderWithOrderItemsDTO.Status = order.Status;

                foreach (var item in order.OrderItems)
                {
                    Product p = _orderRepository.GetProductByOrderItemId(item);

                    orderWithOrderItemsDTO.items.Add(new OrderItemsOfOrderDTO
                    {
                        ProductId = item.ProductId,
                        ProductName = p.Name,
                        Quantity = item.Quantity,
                        Price = item.PriceAtPurchase,
                    }
                    );
                }
                orderWithOrderItemsDTOlist.Add(orderWithOrderItemsDTO);
            }
            return Ok(orderWithOrderItemsDTOlist);
        }

        [HttpPost]
        [Route("CreateOrder")]
        public ActionResult CreateOrder(OrderDTO orderDto)
        {
            Order order = new Order();

            order.UserId = orderDto.UserId;
            order.OrderDate = DateTime.Now;
            order.UpdatedAt = DateTime.Now;
            order.TotalAmount = orderDto.TotalAmount; //should calculated by another function
            order.Status = OrderStatus.Pending;

            _orderRepository.AddOrder(order);
            _orderRepository.Save();
            return Ok(order);

        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOrder( int id, OrderDTO orderDto)
        {
            if (id != orderDto.OrderId) return BadRequest();
            Order order = new Order();
            order.UserId = orderDto.UserId;
            order.OrderDate = DateTime.Now;
            order.UpdatedAt = DateTime.Now;
            order.TotalAmount= orderDto.TotalAmount;
            order.Status = orderDto.Status;
           // _orderRepository.UpdateOrder(order);
            //return NoContent();
            if (order != null)
            {
               _orderRepository.UpdateOrder(order);
                _orderRepository.Save();
                return NoContent();
            }
            else
            {
                return NotFound("Order Not VAlid");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            _orderRepository.DeleteOrder(id);
            return NoContent();
        }
    }
}
