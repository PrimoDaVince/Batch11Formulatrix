using System.Security.Cryptography.X509Certificates;

namespace Internal_H1;

public class car

{
	
}
class Engine
{
	private int a=10;
	private int b=20;
	
	internal void GoInternal()
	{
		Console.WriteLine("GO Internal Created!");
	}
	public  void GoPublic()
	{
		Console.Write("GO Public Created");
	}
	public void GoMath(int a, int b)
	{
	}
}
