using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Inheritance;
class Animal {
	public int age;
	public string name;
	
	
	public void Eat() {}
	public void Poop() {}
	public Animal(int age,
				  string name)
	{
		
	}
}
class Cat : Animal{
	public Cat(int age,
			   string name,
			   string moustache)
			   :base(age,name)
			   {
			   	
			   }
	public void meouw()
	{
		Console.WriteLine("MEOOOW");
	}
		
	
}
class Dog : Animal {
		public Dog(int age, string name,string moustache):base(age,name)
		{
			
		}
		public void bark()
		{
			Console.WriteLine("WOOOOOOOOOO");
		}
}
class Bird : Animal {
	public Bird(int age, string name,string wing):base(age,name)
		{
			
		}
	public void Fly() 
	{
		Console.WriteLine("AM FLYIIIIIIING");
	}
}