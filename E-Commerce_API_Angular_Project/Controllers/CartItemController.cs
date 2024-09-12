using E_Commerce_API_Angular_Project.DTO;
using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API_Angular_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        ICartItemRepo CartItemRepo;

        public CartItemController(ICartItemRepo cartItemRepo)
        {
            CartItemRepo = cartItemRepo;
        }
        [HttpPost]
        [Route("Create CartItem")]
        public ActionResult Create(CartItemsDto cartItemsDto)
        {
            CartItem cartItem = new CartItem();
            cartItem.CartId = cartItemsDto.CartId;
            cartItem.ProductId = cartItemsDto.ProductId;
            cartItem.Quantity = cartItemsDto.Quantity;
            CartItemRepo.Add(cartItem);
            CartItemRepo.Save();
            return Ok(cartItem);
        }


    }
}
