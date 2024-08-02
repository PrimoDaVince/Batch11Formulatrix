namespace CarStatic;

class Car
{
	public int productionNumber;
	public static int count = 0;
	
	public Car()
	{
		count++;
		productionNumber = count;
	}
	public int GetNumber()
	{
		return count;
	}
	public static int GetNumbers()
	{
		//return productionNumber; it will be error
		return count;
	}
	public static void Test()
	{
		Console.WriteLine("Ini Static Void Public");
		{
			
		}
	}
}