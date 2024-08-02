using System.Text;

class Program
{
	static void Main(){
		string a="Hello";
		string b="World";
		for(int i=0;i<100_000_0;i++)
		{
			Console.WriteLine(a+b);
			Console.WriteLine(a+b+"!");
			a.Replace("o","i");
			
			Thread.Sleep(2);
		}
		
		Console.WriteLine(a);
	}
}
