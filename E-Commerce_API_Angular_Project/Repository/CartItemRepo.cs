﻿using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.Repository
{
    public class CartItemRepo:ICartItemRepo
    {
        EcommContext context;

        public CartItemRepo(EcommContext _context)
        {
            this.context = _context;
        }

        public void Add(CartItem cartItem)
        {
            context.Add(cartItem);
        }
        public void Update(CartItem cartItem)
        {
            context.Update(cartItem);
        }
        public void Delete(int id)
        {
            CartItem deletedCartItem = GetById(id);
            if (deletedCartItem != null)
            {
                context.CartItems.Remove(deletedCartItem);

            }
        }
        public List<CartItem> GetAll(int cartId)
        {

            List<CartItem> CartItemlist =
               context.CartItems
               .Where(c=>c.CartId == cartId)
               .ToList();
            return CartItemlist;
        }
        public CartItem GetById(int id)
        {
            CartItem cartItem =
              context.CartItems.FirstOrDefault(c => c.Id == id);
            return cartItem;
        }
        public void Save()
        {
            context.SaveChanges();
        }


    }
}