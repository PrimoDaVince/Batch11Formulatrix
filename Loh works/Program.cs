﻿class Program 
{
	static void Main() 
	{
		float[] myFloats = new float[100*1024];//400KB
		float[] myFloats1 = new float[1];//Under 85KB
		Console.WriteLine($"Whiout LOH -> Heap Memory Gen : {GC.GetGeneration(myFloats1)}");
		Console.WriteLine($"With LOH -> Heap Memory Gen : {GC.GetGeneration(myFloats)}");
		
	}
}