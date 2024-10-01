using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;
using E_Commerce_API_Angular_Project.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API_Angular_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public ICategoryRepo categoryRepo { get; }

        public CategoryController(ICategoryRepo _categoryRepo)
        {
            categoryRepo = _categoryRepo;
        }

        [HttpGet]
        public IActionResult getAll()
        {
            List<Category> categories = categoryRepo.GetAll();
            return Ok(categories);
        }

        [HttpPost]
        public IActionResult add(Category category)
        {
            categoryRepo.Add(category);
            categoryRepo.Save();
            return CreatedAtAction("GetById", new { id = category.Id }, category);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            Category category = categoryRepo.GetById(id);
            return Ok(category);
        }

        
        [HttpDelete("{id:int}")]
        public IActionResult DeleteById(int id)
        {
            Category category = categoryRepo.GetById(id);
            category.IsDeleted = true;
            categoryRepo.Update(category);
            categoryRepo.Save();

            return NoContent();
        }

    }
}
