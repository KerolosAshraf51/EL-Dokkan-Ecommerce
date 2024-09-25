using E_Commerce_API_Angular_Project.DTO;
using E_Commerce_API_Angular_Project.IRepository;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
//using MailKit.Net.Smtp;
using MimeKit;


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
            //var order =  _context.Orders
            //    .Include(o => o.OrderItems)
            //   .Include(o=>o.User)
            //    .FirstOrDefault(o => o.Id == orderId);
            var order = _context.Orders
            .Include(o => o.OrderItems)           
            .FirstOrDefault(o => o.Id == orderId);
            if (order == null) return null;
            return order;
        }

        public List<Order> GetAllOrders()
        {
            var orders =  _context.Orders
                .Include(o => o.OrderItems)
                 .Include (o=>o.User)
                .ToList();
            return orders;
        }
        public List<Order> GetOrdersByUserId(int userId)
        {
             appUser user = _context.Users.Include(u => u.Orders).FirstOrDefault(u => u.Id == userId);
             List<Order> orders = user.Orders.ToList();
            foreach (var order in orders) 
            {
                order.OrderItems = _context.OrderItems.Where(o => o.OrderId==order.Id).ToList();                   
            }
            return orders;
        }
        public void AddOrder(Order order)
        {
            _context.Add(order);
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
        public void CalculateTotal(Order order)
        {
            if(order == null || order.OrderItems==null) return;
            order.TotalAmount = order.OrderItems.Sum(oi => oi.Quantity * oi.PriceAtPurchase);
        }
        public void CancelOrder(int orderId)
        {
            Order order = GetOrderById(orderId);
            order.Status = OrderStatus.Canceled;
           //get cart by user id
            //Cart cart= _context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.UserId == order.UserId);
           // return cart;

        }
      
        //public double TotalPriceOfOrder(int orderID)
        //{
        //    Order order = _context.Orders
        //        .Include(o => o.OrderItems)
        //        .FirstOrDefault(o => o.Id == orderID);
        //    double total = 0;
        //    foreach (var item in order.OrderItems)
        //    {
        //        total += (item.Quantity * item.PriceAtPurchase);
        //    }
        //    return total;
        //}
        //private bool ValidatePayment(PaymentDTO payment)
        //{
        //    // Implement payment validation logic
        //    return true; // Simplified for example
        //}
    }
}
