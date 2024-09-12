using E_Commerce_API_Angular_Project.DTO;
using E_Commerce_API_Angular_Project.IRepository;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API_Angular_Project.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EcommContext _context;
       
        public OrderRepository(EcommContext context)
        {
            _context = context;
            
        }

        public Order GetOrderById(int orderId)
        {
            var order =  _context.Orders
                .Include(o => o.OrderItems)
               .Include(o=>o.User)
                .FirstOrDefault(o => o.Id == orderId);

            if (order == null) return null;

            //OrderDTO orderDTO = new OrderDTO
            //{
            //    OrderId = order.Id,
            //    UserId = order.UserId,
            //    OrderDate = order.OrderDate,
            //    TotalAmount = order.TotalAmount,
            //    Status = order.Status,
            //    IsPaid = order.IsPaid,
            //    OrderItems = order.OrderItems.Select(oi => new OrderItemDTO
            //    {
            //        OrderItemId = oi.Id,
            //        ProductId = oi.ProductId,
            //        Quantity = oi.Quantity,
            //        Price = oi.PriceAtPurchase
            //    }).ToList(),
            //    Payments = order.Payments.Select(p => new PaymentDTO
            //    {
            //        PaymentId = p.Id,
            //        Amount = p.Amount,
            //        PaymentMethod = p.PaymentMethod,
            //        PaymentDate = p.PaymentDate,
            //        IsSuccessful = p.IsSuccessful
            //    }).ToList() // Map payments
            //};
            return order;
        }

        public List<Order> GetAllOrders()
        {
            var orders =  _context.Orders
                .Include(o => o.OrderItems)
                 .Include (o=>o.User)
                .ToList();
            return orders;

            //return (List<Order>)orders.Select(order => new OrderDTO
            //{
            //    OrderId = order.Id,
            //    UserId = order.UserId,
            //    OrderDate = order.OrderDate,
            //    TotalAmount = order.TotalAmount,
            //    Status = order.Status,
               
            //    OrderItems = order.OrderItems.Select(oi => new OrderItemDTO
            //    {
            //        OrderItemId = oi.Id,
            //        ProductId = oi.ProductId,
            //        Quantity = oi.Quantity,
            //        Price = oi.PriceAtPurchase
            //    }).ToList(),
                
            //});
        }

        public void AddOrder(Order order)
        {
            _context.Add(order);
           // Save();
            //Order newOrder = new Order
            //{
            //    UserId = order.UserId,
            //    OrderDate = DateTime.UtcNow,
            //    TotalAmount = order.TotalAmount,
            //    Status = order.Status,
            //    IsPaid = order.IsPaid,
            //    OrderItems = order.OrderItems.Select(oi => new OrderItem
            //    {
            //        ProductId = oi.ProductId,
            //        Quantity = oi.Quantity,
            //        PriceAtPurchase = oi.Price
            //    }).ToList(),
            //    Payments = orderDto.Payments.Select(p => new Payment
            //    {
            //        Amount = p.Amount,
            //        PaymentMethod = p.PaymentMethod,
            //        PaymentDate = DateTime.UtcNow,
            //        IsSuccessful = true // Assuming the payment is successful upon adding
            //    }).ToList() // Map payments
            //};


        }

        public void UpdateOrder(Order _order)
        {
            Order order =  _context.Orders
                .Include(o => o.OrderItems)
                .Include(o=>o.User)
                .FirstOrDefault(o => o.Id == _order.Id);

            if (order == null) return;

            // Update order properties
            order.Status = _order.Status;
            
            order.TotalAmount = _order.TotalAmount;
            order.UpdatedAt = DateTime.UtcNow;

            // Update order items
            order.OrderItems.Clear(); // Clear existing items
            foreach (var item in _order.OrderItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    PriceAtPurchase = item.PriceAtPurchase
                });
            }

          


            Save();
        }
        public void DeleteOrder(int orderId)
        {
            Order order = GetOrderById(orderId);
            if (order != null)
            {
                _context.Remove(order);
                Save();
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        //--------------------
        public Product GetProductByOrderItemId(OrderItem orderItem)
        {
            return _context.Products.FirstOrDefault(p => p.Id == orderItem.ProductId);
        }
        public double TotalPriceOfOrder(int orderID)
        {
            Order order = _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.Id == orderID);
            double total = 0;
            foreach (var item in order.OrderItems)
            {
                total += (item.Quantity * item.PriceAtPurchase);
            }
            return total;
        }
        private bool ValidatePayment(PaymentDTO payment)
        {
            // Implement payment validation logic
            return true; // Simplified for example
        }
    }
}
