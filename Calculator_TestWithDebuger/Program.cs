using System.ComponentModel;

class Program
{
	static void Main()
	{	
		Console.WriteLine("Start");
		Calculator calculator= new Calculator();
		int result = calculator.Add(5,2);
		Console.WriteLine(result);
	}
}
class Calculator
{	
	public int Add(int a, int b) /// this is Add method
	{
		return a * b;//but why this multiply
		//this is example of logic eror, program will stil running but the output is different
	
	}
	//best way to deal with logic eror is using debugger tools 
}