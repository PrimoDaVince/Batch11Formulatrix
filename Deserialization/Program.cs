﻿using System.IO;
using System.Xml.Serialization;
public enum Status
{
	StilWorking,
	Retaired
}

public class Human
{

	public string Name { get; set; }
	public int Age { get; set; }
	public Status status{ get; set; }
	public Human(string name, int age, Status status)
	{
		Name = name;
		Age = age;
	}
	public Human() { }
}
class Program
{
	static void Main()
	{

		List<Human> futurePresident = new();
		XmlSerializer serializer = new(typeof(List<Human>));
		using (FileStream fs = new("./human.txt", FileMode.Open))
		{
			futurePresident = (List<Human>)serializer.Deserialize(fs);
		}
		foreach (var i in futurePresident)
		{
			Console.WriteLine(i.Name);
			Console.WriteLine(i.Age);
			Console.WriteLine(i.status);
		}
		
	}
}