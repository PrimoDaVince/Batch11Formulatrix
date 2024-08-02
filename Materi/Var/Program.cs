﻿//var
class Program {
	static void Main() {
		int x = 3;
		var y = "kinara";
		var car = new Car();
		Car car2=  new();
		car.Start();
	}
}
class Car 
{
	public void Start()
	{
		Console.WriteLine("Start Car");
	}
}