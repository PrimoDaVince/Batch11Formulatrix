namespace MyApi.Mapper;
using AutoMapper;
using MyApi.Models;
using MyApi.DTOs;
public class MapProfile:Profile
{
	public MapProfile()
	{
		CreateMap<Category,CategoryDTO>().ReverseMap();
	}
}
