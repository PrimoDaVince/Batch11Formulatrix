namespace MyApi.Interface;
using MyApi.Controllers;
using Microsoft.AspNetCore.Mvc;


	
public interface IController<T>
{
	IActionResult Get();
	IActionResult Get(int id);
	IActionResult Create(T data);
	IActionResult Update(int id, T data);
	IActionResult Delete(int id);
}

