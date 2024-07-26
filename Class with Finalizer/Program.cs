﻿using System.Diagnostics;

class Program 
{
	static void Main() 
	{
		int iteration = 1_000_000;
		Stopwatch stopwatch = new();
		stopwatch.Start();
		for(int i = 0; i < iteration; i ++) 
		{
			MyClassA a = new();
			MyClassB b = new();
			MyClassC c = new();
		}
		stopwatch.Stop();
		Console.WriteLine(stopwatch.ElapsedMilliseconds);
	}
}


class MyClassA() 
{
	~MyClassA() { }
}
class MyClassB() 
{
	~MyClassB() { }
}
class MyClassC 
{
	~MyClassC() { }
}

//After running Class Whitout Finlaizer is 1354 milisecond slower than whiout finalizer with 49 milisecond