﻿using E_Commerce_API_Angular_Project.DTO;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API_Angular_Project.IRepository
{
    public interface IOrderRepository
    {
       public Order GetOrderById(int orderId);
        //Task<IEnumerable<OrderDTO>> GetAllOrdersA();
       public List<Order> GetAllOrders();
       public void AddOrder(Order order);
       public void UpdateOrder(Order order);
       public void DeleteOrder(int orderId);
        public void Save();
        public Product GetProductByOrderItemId(OrderItem orderItem);
        public double TotalPriceOfOrder(int orderID);
    }
}