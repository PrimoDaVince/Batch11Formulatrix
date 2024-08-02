namespace AnimalLib;

public class Animal
{
	public string name;
	public int age;
	public virtual void Eat()
	{
		Console.WriteLine("Eats");
	}
	public virtual void MakeSound()
	{
		Console.WriteLine("...");
	}
	class Cat:Animal
	{
		public override void Eat()
		{
			Console.WriteLine("Wiskas");
		}
			public override void MakeSound()
		{
			Console.WriteLine("Meow");
		}
	}
	class Sheep:Animal
	{
		public override void Eat()
		{
		   Console.WriteLine("Grass");
		}
		public override void MakeSound()
		{
			Console.WriteLine("BAAA");
			
		}
	}	
}
