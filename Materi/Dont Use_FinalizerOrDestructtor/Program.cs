﻿class MyDestructor 
{
	//Because is its random behavior and expensive to use , its good to know and forbiden to use 
	public MyDestructor() 
	{
		Console.WriteLine($"MyDesctructor {GC.GetGeneration(this)} created");
	}
	~MyDestructor()
	{
		Console.WriteLine($"MyDestructor {GC.GetGeneration(this)}  destructed");
	}
}
class Program 
{
	static void Main() 
	{
		InstanceCreator();
		GC.Collect();
		GC.WaitForPendingFinalizers();
	}
	static void InstanceCreator() 
	{
		MyDestructor myDestructor = new();
	}
}