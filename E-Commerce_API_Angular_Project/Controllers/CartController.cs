using E_Commerce_API_Angular_Project.DTO;
using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API_Angular_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        ICartRepo CartRepo;

        public CartController(ICartRepo cartRepo)
        {
            CartRepo = cartRepo;
        }

        [HttpPost]
        public IActionResult Create(CartDto cartDto )
        {
            Cart cart = new Cart();

            cart.CreatedAt = DateTime.Now;
            cart.UpdatedAt = DateTime.Now;
            cart.UserId=cartDto.UserId;
            

            CartRepo.Add(cart);
            CartRepo.Save();
            return Ok();
        }



        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetByID(int id)
        {
            Cart cart =CartRepo.GetById(id);
     
            CartWithCartItemsDto cartWithCartItemsDto = new CartWithCartItemsDto();
            cartWithCartItemsDto.Items = new List<CartItemsOfCartDto>();
            cartWithCartItemsDto.CreatedAt=cart.CreatedAt;
            cartWithCartItemsDto.UpdatedAt=cart.UpdatedAt;
            cartWithCartItemsDto.CartId=cart.Id;
            foreach(var item in cart.CartItems)
            {
                Product product = CartRepo.GetProductByCartItem(item);

                cartWithCartItemsDto.Items.Add(new CartItemsOfCartDto
                {
                    ProductId=item.ProductId,
                    ProductName=product.Name,
                    Quantity= item.Quantity

                });
            }
            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cartWithCartItemsDto);
        }
        [HttpGet]
        public IActionResult GetCarts()
        {
            List<Cart> Carts = CartRepo.GetAll();
            List<CartWithCartItemsDto> cartWithCartItemsDtoList = new List<CartWithCartItemsDto>();
            foreach (var cart in Carts)
            {
                CartWithCartItemsDto cartWithCartItemsDto = new CartWithCartItemsDto();
                cartWithCartItemsDto.Items = new List<CartItemsOfCartDto>();
                cartWithCartItemsDto.CreatedAt = cart.CreatedAt;
                cartWithCartItemsDto.UpdatedAt = cart.UpdatedAt;
                cartWithCartItemsDto.CartId = cart.Id;
                foreach (var item in cart.CartItems)
                {
                    Product product = CartRepo.GetProductByCartItem(item);

                    cartWithCartItemsDto.Items.Add(new CartItemsOfCartDto
                    {
                        ProductId = item.ProductId,
                        ProductName = product.Name,
                        Quantity = item.Quantity

                    });
                }
                cartWithCartItemsDtoList.Add(cartWithCartItemsDto);
            }
            return Ok(cartWithCartItemsDtoList);
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetCartByUserId(int userId)
        {
            
            var cart =CartRepo.GetCartByUserId(userId);

            
            if (cart == null)
                return NotFound(new { Message = "Cart not found for this user." });

           
            return Ok(cart);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateCart(int id, CartDto updatedCartDto)
        {
            Cart cart = CartRepo.GetById(id);
            if (cart == null)
            {
                return NotFound();
            }
            //cart.CreatedAt=updatedCartDto.CreatedAt;
            cart.UpdatedAt = DateTime.Now;
            cart.UserId = updatedCartDto.UserId;
            //cart.CartItems = updatedCart.CartItems;

            CartRepo.Update(cart);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCart(int id)
        {
            CartRepo.Delete(id);
            CartRepo.Save();
            return NoContent();
        }
        //
        [HttpGet("user/{userId}/products")]
        public IActionResult GetCartProductsByUserId(int userId)
        {

            try
            {
                var cart = CartRepo.GetCartWithProductsByUserId(userId);

                if (cart == null)
                    return NotFound(new { Message = "Cart not found for this user." });

                List<Product> products = cart.CartItems.Select(item => item.Product).ToList();

                return Ok(products);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
