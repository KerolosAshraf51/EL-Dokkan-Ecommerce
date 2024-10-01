using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;
using E_Commerce_API_Angular_Project.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API_Angular_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        public IBrandRepo brandRepo { get; }
        public BrandController(IBrandRepo _brandRepo)
        {
            brandRepo = _brandRepo;
        }

        [HttpGet]
        public IActionResult getAll()
        {
            List<Brand> brands = brandRepo.GetAll();
            return Ok(brands);
        }

        [HttpPost]
        public IActionResult add(Brand brand)
        {
            brandRepo.Add(brand);
            brandRepo.Save();
            return CreatedAtAction("GetById", new { id = brand.Id }, brand);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            Brand brand = brandRepo.GetById(id);
            return Ok(brand);
        }


        [HttpDelete("{id:int}")]
        public IActionResult DeleteById(int id)
        {
            Brand brand = brandRepo.GetById(id);
            brand.IsDeleted = true;

            brandRepo.Update(brand);
            brandRepo.Save();

            return NoContent();
        }

    }
}
