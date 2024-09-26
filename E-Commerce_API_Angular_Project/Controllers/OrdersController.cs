using E_Commerce_API_Angular_Project.DTO;
using E_Commerce_API_Angular_Project.IRepository;
using E_Commerce_API_Angular_Project.Models;
using E_Commerce_API_Angular_Project.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using E_Commerce_API_Angular_Project.Interfaces;


namespace E_Commerce_API_Angular_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMailRepo _mailRepo;
        private readonly ICartRepo _cartRepo;
        private readonly EcommContext ecommContext;
        public OrdersController(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository
            , EcommContext ecommContext, ICartRepo cartRepo, IMailRepo mailRepo)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            this.ecommContext = ecommContext;
            _mailRepo = mailRepo;
            _cartRepo = cartRepo;
        }

        [HttpGet("{id}")]

        public IActionResult GetOrder(int id)
        {
            Order order = _orderRepository.GetOrderById(id);
            OrderWithOrderItemsDTO orderWithOrderItemsDTO = new OrderWithOrderItemsDTO();
            orderWithOrderItemsDTO.items = new List<OrderItemsOfOrderDTO>();
            orderWithOrderItemsDTO.OrderDate = order.OrderDate;
            orderWithOrderItemsDTO.OrderId = order.Id;
            //orderWithOrderItemsDTO.ClientName = order.User.UserName;
            orderWithOrderItemsDTO.UpdatedAt = order.UpdatedAt;
            //orderWithOrderItemsDTO.TotalAmount = _orderRepository.TotalPriceOfOrder(id);
            _orderRepository.CalculateTotal(order);
            orderWithOrderItemsDTO.TotalAmount = order.TotalAmount;
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
            if (order == null) return NotFound();

            return Ok(orderWithOrderItemsDTO);
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            List<Order> orders = _orderRepository.GetAllOrders();
            List<OrderWithOrderItemsDTO> orderWithOrderItemsDTOlist = new List<OrderWithOrderItemsDTO>();
            foreach (var order in orders)
            {
                OrderWithOrderItemsDTO orderWithOrderItemsDTO = new OrderWithOrderItemsDTO();
                orderWithOrderItemsDTO.items = new List<OrderItemsOfOrderDTO>();
                orderWithOrderItemsDTO.OrderDate = order.OrderDate;
                orderWithOrderItemsDTO.OrderId = order.Id;
                orderWithOrderItemsDTO.ClientName = order.User.UserName;
                orderWithOrderItemsDTO.UpdatedAt = order.UpdatedAt;
                // orderWithOrderItemsDTO.TotalAmount = _orderRepository.TotalPriceOfOrder(order.Id);
                _orderRepository.CalculateTotal(order);
                orderWithOrderItemsDTO.TotalAmount = order.TotalAmount;
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
        [HttpGet]
        [Route("GetOrdersByUserId")]
        public IActionResult GetOrderByUserId(int userId)
        {

            List<Order> orders = _orderRepository.GetOrdersByUserId(userId);
            List<OrderWithOrderItemsDTO> orderWithOrderItemsDTOlist = new List<OrderWithOrderItemsDTO>();
            foreach (var order in orders)
            {
                OrderWithOrderItemsDTO orderWithOrderItemsDTO = new OrderWithOrderItemsDTO();
                orderWithOrderItemsDTO.items = new List<OrderItemsOfOrderDTO>();
                orderWithOrderItemsDTO.OrderDate = order.OrderDate;
                orderWithOrderItemsDTO.OrderId = order.Id;
                orderWithOrderItemsDTO.ClientName = order.User.UserName;
                orderWithOrderItemsDTO.UpdatedAt = order.UpdatedAt;
                //orderWithOrderItemsDTO.TotalAmount = _orderRepository.TotalPriceOfOrder(order.Id);
                _orderRepository.CalculateTotal(order);
                orderWithOrderItemsDTO.TotalAmount = order.TotalAmount;
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
        //-------------------------
        [HttpPost]
        [Route("ProceedToCheckout")]
        public ActionResult ProceedToCheckout(CheckoutDTO checkoutDTO)
        {
            if (checkoutDTO == null)
            {
                return BadRequest("Checkout data is required.");
            }

            if (checkoutDTO.cartItems == null)
            {
                return BadRequest("Cart items are required.");
            }
            //get cartItems
            // List<CartItem> cartItems = _ca
            Order order = new Order
            {
                UserId = checkoutDTO.userID,
                OrderDate = DateTime.Now,
                UpdatedAt = DateTime.Now,
                TotalAmount = 0,
                Status = OrderStatus.Pending,
                PaymentMethod = PaymentMethod.Cash_on_delivery.ToString(),
                OrderItems = new List<OrderItem>()
            };
            foreach (var item in checkoutDTO.cartItems)
            {
                OrderItem orderItem = new OrderItem();
                orderItem.ProductId = item.ProductId;
                orderItem.Quantity = item.Quantity;
                orderItem.PriceAtPurchase = item.Price;
                
                order.TotalAmount += (orderItem.Quantity * orderItem.PriceAtPurchase);
                order.OrderItems.Add(orderItem);
               }
            _orderRepository.AddOrder(order);
            _orderRepository.Save();
            //then clear the cart
            return Ok(order);

        }
        [HttpPost]
        [Route("PlaceOrder")]
        public ActionResult PlaceOrder(PlaceOrderDTO placeOrderDTO)
        {
            if (placeOrderDTO == null) 
            { return BadRequest(); }

            Order order = _orderRepository.GetOrderById(placeOrderDTO.OrderId);
            if (order == null)
            {
                return BadRequest();
            }
            order.TotalAmount = order.OrderItems.Sum(oi => oi.Quantity * oi.PriceAtPurchase);
            switch (placeOrderDTO.Shipping)
            {
                case Shipping.Flat_rate:
                    order.TotalAmount += 40;
                    break;
                case Shipping.Local_pickup:
                    order.TotalAmount += 30;
                    break;
            }
            switch(placeOrderDTO.PaymentMethod)
            {
                case PaymentMethod.Direct_bank_transfer:
                    order.PaymentMethod = PaymentMethod.Direct_bank_transfer.ToString();
                    break;
                case PaymentMethod.Cash_on_delivery:
                    order.PaymentMethod = PaymentMethod.Cash_on_delivery.ToString();
                    break;
                case PaymentMethod.Check_payments:
                    order.PaymentMethod=PaymentMethod.Check_payments.ToString();
                    break;
            }
            order.Status = OrderStatus.Processing;

            _orderRepository.Save();

            //------------------------
            //try
            //{
            //    var email = new MimeMessage();
            //    email.Sender = MailboxAddress.Parse( "eldokanonlinestore@hotmail.com");
            //    email.To.Add(MailboxAddress.Parse(placeOrderDTO.BillingDetails.Email));
            //    email.Subject = "Your Order From ElDokan Site";

            //    var builder = new BodyBuilder();
            //    builder.HtmlBody = $"Your Order ID is: {placeOrderDTO.OrderId}";
            //    email.Body = builder.ToMessageBody();

            //    var smtp = new SmtpClient();

            //    //smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            //    //smtp.Authenticate("shaimafarag123@gmail.com", ""); // Replace with your email password
            //     smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
            //    smtp.Authenticate("eldokanonlinestore@hotmail.com", "dokan12345");
            //    smtp.Send(email);
            //    smtp.Disconnect(true);

            //}
            //catch (Exception ex)
            //{
            //    // Log exception (optional)
            //    return StatusCode(500, "Error sending email: " + ex.Message);
            //}
            //string subject = "Your Order From El-Dokan Site";
            //string body = $"Your Order ID is: {placeOrderDTO.OrderId}";
            //_mailRepo.SendEmail(placeOrderDTO.BillingDetails.Email,  subject, body);
            return Ok(order);
        }
        [HttpPost]
        [Route("CancelOrder")]
        public IActionResult CancelOrder(int orderId)
        {
            Order order = _orderRepository.GetOrderById(orderId);
            Cart cart =_cartRepo.GetCartByUserId(order.UserId);
            //Cart cart =_orderRepository.CancelOrder(orderId);
            //clear cart  
            _orderRepository.CancelOrder(orderId);
            _cartRepo.ClearCart(cart.Id);

           // ecommContext.CartItems.RemoveRange(cart.CartItems);
          //  _orderRepository.Save();
            //fill cart with items of the canceld order
            List<CartItem> newCartItems = new List<CartItem>();
            foreach (var item in order.OrderItems)
            {
                CartItem cartItem = new CartItem();
                cartItem.CartId = cart.Id;
                cartItem.Quantity = item.Quantity;
                cartItem.ProductId = item.ProductId;
                newCartItems.Add(cartItem);
            }
            cart.CartItems = newCartItems;
            _orderRepository.Save();

            return Ok(cart.CartItems);
           
        }
        [HttpPost]
        [Route("Orderdelivered")]
        public IActionResult Delivered(int orderId)
        {
            
            Order order= _orderRepository.GetOrderById(orderId);
            if (order != null)
            {
                order.Status = OrderStatus.Delivered;
                _orderRepository.Save();
                return Ok("Order Delivered successfully");
            }
            return BadRequest("Order not found");

        }
        //------------------------

        //[HttpPut("{id:int}")]
        //public IActionResult UpdateOrder( int id, OrderDTO orderDto)
        //{
        //    if (id != orderDto.OrderId) return BadRequest();
        //    Order order = new Order();
        //    order.UserId = orderDto.UserId;
        //    order.OrderDate = DateTime.Now;
        //    order.UpdatedAt = DateTime.Now;
        //    order.TotalAmount= orderDto.TotalAmount;
        //    order.Status = orderDto.Status;
        //   // _orderRepository.UpdateOrder(order);
        //    //return NoContent();
        //    if (order != null)
        //    {
        //       _orderRepository.UpdateOrder(order);
        //        _orderRepository.Save();
        //        return NoContent();
        //    }
        //    else
        //    {
        //        return NotFound("Order Not VAlid");
        //    }
        //}

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            _orderRepository.DeleteOrder(id);
            return NoContent();
        }
    }
}
