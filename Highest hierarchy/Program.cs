﻿class Program 
{
	static void Main() 
	{
		Car car = new();
		car.name = "toyota";
		Console.WriteLine(car);//if car doesent have override doesnt declare to retrun name,output is name class wich is "Car"
	}
}
class Car 
{
	public string name;
	public override string ToString()// class can overide because class have parent superclass object in suerclass objet there already virtual 
	{
		return name;
	}
	
}