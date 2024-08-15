using System.Data.Common;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyApi.Databases;
using MyApi.DTOs;
using MyApi.Interface;
using MyApi.Models;


namespace MyApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController:ControllerBase,IController<Category>
{	private IMapper _map;
	private readonly DataContex _db;
	public CategoryController(DataContex db, IMapper map)
	{
		_db=db;
		_map=map;
	}
	[HttpPost]
	public IActionResult Create(Category data)
	{
	   Category category = _map.Map<Category>(data);
	   _db.Categories.Add(category);
	   _db.SaveChanges();
	   return Ok();
	}
	[HttpDelete]
	[Route("{id}")]
	public IActionResult Delete(int id)
	{
		Category? category = _db.Categories.Find(id);
		if (category is null)
		{
			return NotFound();
		}
		_db.Categories.Remove(category);
		_db.SaveChanges();
		return Ok();
	}
	[HttpGet]
	public IActionResult Get()
	{
		
		List<Category> categories =_db.Categories.ToList();
		List<CategoryDTO> categoryDTOs = _map.Map<List<CategoryDTO>>(categories);
		return Ok(categoryDTOs);
	}
	[HttpGet]
	[Route("{id}")]
	public IActionResult Get(int id)
	{
		Category? category = _db.Categories.Find(id);
		if (category is null)
		{
			return NotFound();
		}
		
		return Ok(category);
	}
	[HttpPut]
	[Route("{id}")]
	public IActionResult Update(int id, Category data)
	{
		Category? category = _db.Categories.Find(id);
		
		if (category is null)
		{
			return NotFound();
		}
		category.CategoryName = data.CategoryName;
		category.Description = data.Description;
		_db.Categories.Update(category);
		_db.SaveChanges();
		return Ok();
	}

}
