using System.Runtime.InteropServices.Marshalling;

class Program
{
	static void Main()
	{
		Console.WriteLine("Program starting");
		Task t1 = new Task(MethodA);
		Task t2 = new Task(MethodB);
		Task t3 = new Task(MethodC);
		
		try 
		{
			
			t1.Start();
			t2.Start();
			t3.Start();
			
			Task.WaitAll(t1, t2, t3);	
		}
		
		catch(Exception e) 
		{
			Console.WriteLine(e.Message);
		}
		
		Console.WriteLine("Program finished");
	}
	static void MethodA() 
	{
			Console.WriteLine("oh my");
	}
	static void MethodB() 
	{
			Console.WriteLine("Hi");
	}
	static void MethodC() 
	{
			throw new Exception();
	}
}