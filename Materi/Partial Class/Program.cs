﻿//namespace harus sama
//namespace must stay same for partial class to works
namespace MyProgram
{
	class Program
	{
		static void Main()
		{
			Car car = new Car();
			car.Run(); //namespace of Car harus sama
		}
	}
	partial class Car
	{

	}
}