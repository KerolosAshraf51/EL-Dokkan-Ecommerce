using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;
using E_Commerce_API_Angular_Project.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API_Angular_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        public IReviewRepo reviewRepo { get; }
        public ReviewController(IReviewRepo _reviewRepo)
        {
            reviewRepo = _reviewRepo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Review> Reviews = reviewRepo.GetAll();
            return Ok(Reviews);
        }

        [HttpPost]
        public IActionResult add(Review review)
        {
            reviewRepo.Add(review);
            reviewRepo.Save();
            //return Ok(review);
            return CreatedAtAction("GetById", new { id = review.Id }, review);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            Review review = reviewRepo.GetById(id);
            return Ok(review);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteById(int id)
        {
            reviewRepo.Delete(id);
            reviewRepo.Save();

            return NoContent();
        }

    }
}
