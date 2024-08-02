using System.Security.Cryptography.X509Certificates;

class Bird

{
	
	public string color;
	
	public bool isMale;
	public int age;
	
	
	public void Eat(string jenisMakanan, int banyak)
	
	{
		Console.WriteLine("Brids Eat"+jenisMakanan+"Sebanyak "+banyak.ToString()+"Kali");
	}
		public void Poop()
	
	{
		Console.WriteLine("Tidak Sedang Pop");
	}
}


class Program
{
	static void Main()
	
	{
		Bird sibiru = new Bird();
		 	
		sibiru.color = "Blue";
		sibiru.isMale = false;
		sibiru.age = 2;
		
		Console.WriteLine(sibiru.color);
		Console.WriteLine(sibiru.isMale);
		Console.WriteLine(sibiru.age.ToString());
		
		sibiru.Poop();
		
		
		
	}
}