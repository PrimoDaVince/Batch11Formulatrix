namespace MyCalculator;

public class Calculator
{
	public int Add(int a, int b)
	{
		if (a == 0 && b == 0) 
		{
			throw new Exception("ngga ada otak");
		}
		return a + b;
	}
	public int Multiple(int a, int b)
	{
		if (a == 0 && b == 0) 
		{
			throw new Exception("No brain");
		}
		return a * b;
	}
}