using E_Commerce_API_Angular_Project.IRepository;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API_Angular_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserRepo appUser;

        public AppUserController(IAppUserRepo _appuser)
        {
            appUser = _appuser;
        }

        //****************Actions for any AppUser****************

        // view profile 

        //Edit profile

        //delete account


        //****************Actions for client AppUser****************

        //add product to cart

        //add product to favList

        //add review to product

        // checkout product and confirm order

        //cancel order


        //****************Actions for Admin AppUser****************

        //add new category

        //add new product

        //edit product details

        //

    }
}
