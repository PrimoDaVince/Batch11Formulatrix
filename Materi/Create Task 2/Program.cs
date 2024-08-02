﻿using System.Diagnostics;

class Program
{
	static void Main()
	{
		Console.WriteLine("Program starting");
		Task t1 = Task.Run(() => Print());
		Task t2 = Task.Run(() => Fax());
		Task t3 = Task.Run(() => Scan());
		Task t4 = Task.Run(() => Add(2,3));

		Task.WaitAll(t1, t2, t3);
		//Task.WaitAny(t1, t2, t3);
		
		Console.WriteLine("Program finished");
	}
	static async Task  Print() 
	{
		Console.WriteLine("Print start");
		await Task.Delay(10000);
		Console.WriteLine("Print finished");
	}
	static async Task  Fax() 
	{
		Console.WriteLine("Fax start");
		await Task.Delay(1150);
		Console.WriteLine("Fax finished");
	}
	static async Task  Scan() 
	{
		Console.WriteLine("Scan start");
		await Task.Delay(11000);
		Console.WriteLine("Scan finished");
	}
	static async Task<int> Add(int a, int b) 
	{
		await Task.Delay(11000);
		return a + b;
	}
}