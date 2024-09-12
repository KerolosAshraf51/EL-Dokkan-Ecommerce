using E_Commerce_API_Angular_Project.DTO;
using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.IRepository
{
    public interface IOrderItemRepository
    {
        public OrderItem GetOrderItemById(int orderItemId);
        public List<OrderItem> GetOrderItemsByOrderId(int orderId);
        public void AddOrderItem(OrderItem orderItem);
        public void UpdateOrderItem(OrderItem orderItem);
        public void DeleteOrderItem(int orderItemId);
        public void Save();
        public double TotalPriceOfEachItem(int orderItemId);
    }
}
