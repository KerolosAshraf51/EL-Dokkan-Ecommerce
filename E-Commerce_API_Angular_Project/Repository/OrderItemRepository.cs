using E_Commerce_API_Angular_Project.DTO;
using E_Commerce_API_Angular_Project.IRepository;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API_Angular_Project.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly EcommContext _context;

        public OrderItemRepository(EcommContext context)
        {
            _context = context;
        }

        public OrderItem GetOrderItemById(int orderItemId)
        {
            OrderItem orderItem =  _context.OrderItems
                .FirstOrDefault(oi => oi.Id == orderItemId);

            if (orderItem == null) return null;
            return orderItem;

            //return new OrderItemDTO
            //{
            //    OrderItemId = orderItem.Id,
            //    ProductId = orderItem.ProductId,
            //    Quantity = orderItem.Quantity,
            //    Price = orderItem.PriceAtPurchase
            //};
        }

        public List<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            List<OrderItem> orderItems =  _context.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .ToList();
            return orderItems;

            //return orderItems.Select(oi => new OrderItemDTO
            //{
            //    OrderItemId = oi.Id,
            //    ProductId = oi.ProductId,
            //    Quantity = oi.Quantity,
            //    Price = oi.PriceAtPurchase
            //});
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            //var orderItem = new OrderItem
            //{
            //    OrderId = orderItemDto.OrderId, 
            //    ProductId = orderItemDto.ProductId,
            //    Quantity = orderItemDto.Quantity,
            //    PriceAtPurchase = orderItemDto.Price
            //};

             _context.Add(orderItem);
            //Save();
        }

        public void UpdateOrderItem(OrderItem orderItem)
        {
            var _orderItem =  _context.OrderItems
                .FirstOrDefault(oi => oi.Id == orderItem.Id);

            if (orderItem == null) return;

            // Update properties
            _orderItem.Quantity = orderItem.Quantity;
            _orderItem.PriceAtPurchase = orderItem.PriceAtPurchase;

            Save();
        }

        public void DeleteOrderItem(int orderItemId)
        {
            var orderItem =  _context.OrderItems.Find(orderItemId);
            if (orderItem != null)
            {
                _context.OrderItems.Remove(orderItem);
                Save();
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public double TotalPriceOfEachItem(int orderItemId)
        {
            OrderItem orderItem = _context.OrderItems.Find(orderItemId);
            return (orderItem.Quantity* orderItem.PriceAtPurchase);
        }
    }
}
