﻿using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API_Angular_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public IProductRepo productRepo { get; }

        public ProductController(IProductRepo _productRepo)
        {
            productRepo = _productRepo;
        }


        [HttpGet]
        public IActionResult getAll()
        {
            List<Product> products = productRepo.GetAll();
            return Ok(products);
        }

        [HttpPost]
        public IActionResult add(Product product)
        {
            productRepo.Add(product);
            productRepo.Save();
            return CreatedAtAction("GetById", new { id = product.Id }, product);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            Product product = productRepo.GetById(id);
            return Ok(product);
        }

        [HttpPut("{id:int}")]
        public IActionResult update(int id, Product prod)
        {
            Product product = productRepo.GetById(id);

            product.Name = prod.Name;
            product.Description = prod.Description;
            product.Category = prod.Category;
            product.CategoryId = prod.CategoryId;
            product.Brand = prod.Brand;
            product.BrandId = prod.BrandId;
            product.Reviews = prod.Reviews;
            product.Price = prod.Price;
            product.CreatedAt = prod.CreatedAt;
            product.UpdatedAt = prod.UpdatedAt;
            product.StockQuantity = prod.StockQuantity;

            productRepo.Save();

            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public IActionResult DeleteById(int id)
        {
            productRepo.Delete(id);
            productRepo.Save();

            return NoContent();
        }

    }
}