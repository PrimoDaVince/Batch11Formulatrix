using System.Data;
using PrinterLib;
class Program
{
	static void Main()
	{
		PrinterMurah200rb cheapPrinter = new PrinterMurah200rb();
		Printer700rb midPrinter = new Printer700rb();
		print3juta printerHighend = new print3juta();
		
		Console.WriteLine("Aku Printer Sesuai Harga");
		cheapPrinter.Print();
		
		Console.WriteLine("\n Aku Printer Gk berlebih ");
		midPrinter.Print();
		midPrinter.Scan();
		
		Console.WriteLine("\n Aku Printer Muahal ");
		printerHighend.Print();
		printerHighend.Scan();
		printerHighend.Fax();
		

		double x = 3.8;
		int f = (int)x;
		
		double y = Math.Ceiling(x);
		double z = Math.Floor(x);
	
	}
}
