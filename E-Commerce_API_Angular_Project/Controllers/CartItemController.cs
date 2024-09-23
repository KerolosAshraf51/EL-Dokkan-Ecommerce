using E_Commerce_API_Angular_Project.DTO;
using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;
using E_Commerce_API_Angular_Project.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace E_Commerce_API_Angular_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        ICartItemRepo CartItemRepo;
        ICartRepo CartRepo;
        public CartItemController(ICartItemRepo cartItemRepo, ICartRepo cartRepo)
        {
            CartItemRepo = cartItemRepo;
            CartRepo = cartRepo;
        }
        [HttpPost]
        [Route("CreateCartItem")]
        public ActionResult Create(CartItemsDto cartItemsDto)
        {
            Cart cart=CartRepo.GetCartByUserId(cartItemsDto.UserId);
            List<CartItem> cartItems = cart.CartItems.ToList();
            foreach (CartItem item in cartItems) 
            {
                if(item.ProductId == cartItemsDto.ProductId)
                {
                    CartItemRepo.UpdateCartItem(item);
                    CartItemRepo.Save();
                    return Ok(item);    

                }
                //else
                //{
                //    CartItem NewcartItem = new CartItem();
                //    NewcartItem.CartId = cart.Id;
                //    NewcartItem.ProductId = cartItemsDto.ProductId;
                //    NewcartItem.Quantity = cartItemsDto.Quantity;
                //    CartItemRepo.Add(NewcartItem);
                    
                //}
                
            }
            CartItem cartItem = new CartItem();
            cartItem.CartId = cart.Id;
            cartItem.ProductId = cartItemsDto.ProductId;
            cartItem.Quantity = cartItemsDto.Quantity;
            CartItemRepo.Add(cartItem);
            CartItemRepo.Save();
            return Ok(cartItem);
        }

        [HttpGet("{id}")]
        public IActionResult GetCartItem(int id)
        {
            CartItem cartItem =CartItemRepo.GetById(id);
            if (cartItem == null) return NotFound();

            return Ok(cartItem);
        }

        [HttpGet("cart/{cartId}")]
        public IActionResult GetOrderItemsByOrderId(int cartId)
        {
            List<CartItem> cartItems = CartItemRepo.GetAll(cartId);
            return Ok(cartItems);
        }
       



        [HttpDelete("RemoveItemFromcart")]

        public IActionResult RemoveItemFromcart(int userId, int productId)
        {
            var cart = CartRepo.GetCartByUserId(userId);
            bool del = CartItemRepo.removeItem(cart.Id, productId);
            if (del) 
            {
                CartItemRepo.Save();
                return Ok();
            }

            else
                return NotFound();

        }



    }
}
