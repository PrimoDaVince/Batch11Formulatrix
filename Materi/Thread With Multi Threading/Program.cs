﻿using System.Diagnostics;
using System.Linq.Expressions;

class Program
{
	static void Main()
	{	
		Console.WriteLine("Program starting");
	
		Thread t1 = new Thread(()=>
		{
			try
			{
				Print();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Eror Happend on \n {ex}");
			}
		});//Thread is delegate
		Thread t2 = new Thread(Fax);
		Thread t3 = new Thread(Scan);
		
		t1.Start();
		t2.Start();
		t3.Start();
		//Multi Threading is random
		
		t1.Join();//Join to main thread so thread main wait till other thread finsih
		t2.Join();
		t3.Join();
		
		
		Console.WriteLine("Program finished");
	}
	static int Print() 
	{	
		int[]myArray = null;
		
		Console.WriteLine("Print start");
		Thread.Sleep(500);
		return (myArray[4]);
	}
	
	static void Fax() 
	{
		Console.WriteLine("Fax start");
		Thread.Sleep(11000);
		Console.WriteLine("Fax finished");
	}
	static void Scan() 
	{
		Console.WriteLine("Scan start");
		Thread.Sleep(5000);
		Console.WriteLine("Scan finished");
	}
}