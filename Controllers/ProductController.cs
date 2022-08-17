using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Categoris.Models;
using Microsoft.EntityFrameworkCore;

namespace Categoris.Controllers;

public class ProductController : Controller
{
    private int? uid
    {
        get
        {
            return HttpContext.Session.GetInt32("UUID");
        }
    }
    private MyContext _context;

    public ProductController(MyContext context)
    {
        _context = context;
    }

    [HttpGet("/")]
    [HttpGet("/products")]
    public IActionResult AllProduct()
    {
        ViewBag.allProductsList  = _context.Products.ToList();
        return View("AllProduct");
    }

    [HttpPost("/products/create")]
    public IActionResult CreateProduct(Product newProduct)
    {
        if (ModelState.IsValid)
        {
            _context.Add(newProduct);
            _context.SaveChanges();
        }
        return AllProduct();

    }

    [HttpGet("/products/{productId}")]
    public IActionResult ViewProduct(int productId)
    {
        ViewBag.categoryWithoutProduct = _context.Categories
        .Include(c => c.CategoryList).ThenInclude(c => c.Product)
        .Where(c => !c.CategoryList.Any(p => p.ProductId == productId));

        ViewBag.oneProduct = _context.Products
        .Include(p => p.ProductList).ThenInclude(asc => asc.Category).FirstOrDefault(p => p.ProductId == productId);


        return View("ViewProduct");
    }

    [HttpPost("/products/{productId}/create")]
    public IActionResult CreateCategoryWithProduct(int productId, Association assoc)
    {
        if (ModelState.IsValid)
        {
            assoc.ProductId = productId;
            _context.Add(assoc);
            _context.SaveChanges();
        }
        return AllProduct();
    }

}
