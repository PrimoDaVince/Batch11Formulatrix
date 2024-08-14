namespace MyMvc.Controllers;
using MyMvc.Databases;
using MyMvc.Models;


using Microsoft.AspNetCore.Mvc;



public class CategoryController:Controller
{
	private readonly DataContex _db;
	public CategoryController(DataContex db) 
	{
		_db = db;
	}
	public IActionResult Index() 
	{
		List<Category> categories = _db.Categories.ToList();
		return View(categories);
	}
	public IActionResult Create() 
	{
		return View();
	}
	[HttpPost]
	public IActionResult Create(Category category) 
	{
		bool status = _db.Categories.Any(cat => cat.CategoryName == category.CategoryName);
		if(status) 
		{
			TempData["Error"] = "Category already exist";
			return RedirectToAction("Index");
		}
		_db.Categories.Add(category);
		_db.SaveChanges();
		return RedirectToAction("Index");
	}
	public IActionResult Update(int? id) 
	{
		if(id is null) 
		{
			return NotFound();
		}
		Category category = _db.Categories.Find(id);
		if(category is null) 
		{
			return NotFound();
		}
		return View(category);
	}
	[HttpPost]
	public IActionResult Update(Category category) 
	{
		bool status = _db.Categories.Any(cat => cat.CategoryName == category.CategoryName);
		if(status) 
		{
			TempData["Error"] = "Cannot Duplicate";
			return RedirectToAction("Index");
		}
		_db.Categories.Update(category);
		_db.SaveChanges();
		return RedirectToAction("Index");
	}
	public IActionResult Delete(int? id) 
	{
		if(id is null) 
		{
			return NotFound();
		}
		Category category = _db.Categories.Find(id);
		if(category is null) 
		{
			return NotFound();
		}
		return View(category);
		
		
	}
	[HttpPost]
	public IActionResult Delete(Category category) 
	{
		_db.Categories.Remove(category);
		_db.SaveChanges();
		return RedirectToAction("Index");
	}
}

