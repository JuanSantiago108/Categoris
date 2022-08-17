using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Categoris.Models;
using Microsoft.EntityFrameworkCore;

namespace Categoris.Controllers;


public class CategoryController : Controller
{
    private int? uid
    {
        get
        {
            return HttpContext.Session.GetInt32("UUID");
        }
    }
    private MyContext _context;

    public CategoryController(MyContext context)
    {
        _context = context;
    }

    [HttpGet("/categories")]
    public IActionResult  AllCategory()
    {
        ViewBag.AllCategoriesList = _context.Categories.ToList();
        return View("AllCategory");
    }

    [HttpPost("/categories/create")]
    public IActionResult CreateCategory(Category newCategory)
    {
        if (ModelState.IsValid)
        {
            _context.Add(newCategory);
            _context.SaveChanges();
        }
        return AllCategory();
    }

    [HttpGet("/categories/{categoriesId}")]
    public IActionResult ViewCategory(int categoriesId)
    {
        ViewBag.productWithoutCategory = _context.Products
        .Include(p => p.ProductList).ThenInclude(p => p.Category)
        .Where(p => !p.ProductList.Any(p => p.CategoryId == categoriesId));

        Category? cat = _context.Categories
        .Include(c => c.CategoryList).ThenInclude(asc => asc.Product).FirstOrDefault(c => c.CategoryId == categoriesId);

        ViewBag.oneCategory = cat;

        return View("ViewCategory");
    }

    [HttpPost("/categories/{categoriesId}/create")]
    public IActionResult CreateProductWithCategory( int categoriesId, Association assoc)
    {
        if (ModelState.IsValid)
        {
            assoc.CategoryId = categoriesId;
            _context.Add(assoc);
            _context.SaveChanges();
        }
        return ViewCategory(categoriesId);
    }
    
}