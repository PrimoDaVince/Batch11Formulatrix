namespace PrinterLib;

interface IPrinter
{
	void Print();
}
interface IScan
{
	void Scan();
}
interface IFax
{
	void Fax();
}
interface Iprinter3jtkeats : IPrinter, IFax, IScan
{

}
class PrinterMurah200rb : IPrinter
{
	public void Print()
	{
		Console.WriteLine("i can print :)");
	}
}
class Printer700rb : IPrinter
{
	public void Print()
	{
		Console.WriteLine("i can print :)");
	}
	public void Scan()
	{
		Console.WriteLine("i can scan :)");
	}
}
class print3juta : Iprinter3jtkeats
{
	public void Print()
	{
		Console.WriteLine("i can print !");
	}
	public void Scan()
	{
		Console.WriteLine("i can scan !");
	}
	public void Fax()
	{
		Console.WriteLine("I also can do fax");
	}
}