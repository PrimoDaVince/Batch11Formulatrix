﻿using System.IO;
//XMLSerializer
//Public
//Serialize Variable / Property
//Method ? Tidak bisa
using System.Xml.Serialization;

public enum Status
{
	StilWorking,
	Retaired
}
public class Human 
{
	private string _name;
	private int _age;
	public Status status{ get; set; }
	public Human(string name, int age, Status status) 
	{
		_name = name;
		_age = age;

	}
	public Human()
	{
	}
}
class Program 
{
	static void Main()
	{
		Human human = new Human("Jan", 2,Status.Retaired);
		Human human2 = new Human("Gibr4n",36,Status.StilWorking);
		Human human3 = new Human("Kaseang", 29,Status.StilWorking);

		List<Human> futurePresident = new();
		futurePresident.Add(human);
		futurePresident.Add(human2);
		futurePresident.Add(human3);

		XmlSerializer serializer = new(typeof(List<Human>));
		
		using (FileStream fs = new("./human.txt", FileMode.Create))
		{
			serializer.Serialize(fs, futurePresident);
		}
		
	}
}