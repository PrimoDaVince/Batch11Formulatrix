//Menggunakan Lib Kalkulator yg sudah dibuat
using CalculatorLib;
class Program
{
	static void Main()
	{
		Calculator calculator = new Calculator();
		//Memilih Menu 	
		string welcomenotes = "Welcome To Simple Calculator ";
		
		Console.WriteLine(welcomenotes +  "\n Please Enter : ");
		//fungsi untuk menggunakan kalkulator lagi
		do
		{
			CalculatorStart(calculator);
		}
		while (Console.ReadLine().ToLower() == "y");

	}
	static void CalculatorStart(Calculator calculator)
	{
		Console.WriteLine("\n 1.Addition \n 2.Multipy \n 3.Divide \n 4.Subtraction");
		Console.WriteLine("\n Please Enter 1-4");
		string chose = Console.ReadLine();

		//Memassukan Angka Untuk Dihitung
		Console.WriteLine("Insert First Number");
		string input1 = Console.ReadLine();
		Console.WriteLine("Insert Second Number");
		string input2 = Console.ReadLine();
		//Convert String Menjadi INTEGER	
		int convertin1 = int.Parse(input1);
		int convertin2 = int.Parse(input2);
		//Menentukan Pilihan Model Perhitungan	
		if (chose == "1")
		{
			int result = calculator.Add(convertin1, convertin2);
			Console.WriteLine("Result " + convertin1 + " + " + convertin2 + " : " + result);
		}
		if (chose == "2")
		{
			int result = calculator.Multiply(convertin1, convertin2);
			Console.WriteLine("Result " + convertin1 + " * " + convertin2 + " : " + result);
		}
		if (chose == "3")
		{
			int result = calculator.Divide(convertin1, convertin2);
			Console.WriteLine("Result " + convertin1 + " / " + convertin2 + " : " + result);
		}
		if (chose == "4")
		{
			int result = calculator.Minus(convertin1, convertin2);
			Console.WriteLine("Result " + convertin1 + " - " + convertin2 + " : " + result);
		}
		Console.WriteLine("Please Input 'Y' To continue");

	}
}