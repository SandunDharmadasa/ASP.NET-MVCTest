using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVCTest.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Xml.Linq;
using System;

namespace MVCTest.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Post()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Post(Product product)
        {
            sampleContext sampleContext = new sampleContext();
            sampleContext.Products.Add(product);
            sampleContext.SaveChanges();
            return View(product);
        }
       
        [HttpGet]
        public IActionResult Get()
        {
            sampleContext sampleContext = new sampleContext();
            var data = sampleContext.Products.ToList();
            return View(data);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            using (var sampleContext = new sampleContext())
            {
                var product = sampleContext.Products.FirstOrDefault(p => p.Id == id);

                if (product == null)
                {
                    return NotFound(); // Returns a 404 Not Found response
                }

                return View(product);
                // Assuming "ProductDetail" is the name of your View for displaying a single product
            }
        }
        
       [HttpPost]
       public IActionResult Update(Product updatedProduct)
        {
            using (var samplecontext = new sampleContext())
            {
                var existingProduct = samplecontext.Products.FirstOrDefault(p => p.Id == updatedProduct.Id);
                if (existingProduct == null)
                {
                    return NotFound();
                }
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.Quantity = updatedProduct.Quantity;
                existingProduct.Status = updatedProduct.Status;
                samplecontext.SaveChanges();
                return RedirectToAction("Get");
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            using (var sampleContext = new sampleContext())
            {
                var product = sampleContext.Products.FirstOrDefault(p => p.Id == id);

                if (product == null)
                {
                    return NotFound(); // Returns a 404 Not Found response
                }

                sampleContext.Products.Remove(product);
                sampleContext.SaveChanges();

                return RedirectToAction("Get"); // Redirects to the action that lists all products
            }
        }

    }
}
