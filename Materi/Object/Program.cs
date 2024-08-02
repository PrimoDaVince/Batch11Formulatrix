class Calcultor
{
	public object Add(object a, object b)
	{
		return (int)a + (int)b;
	}
	
}
class Program
{
	static void Main()
	{
		int a = 2;
		int b = 3;
		Calcultor calc = new();
		Console.WriteLine(calc.Add(a, b));
		
	}
}