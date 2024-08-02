//constract overloading
using System.Drawing;
using System.Net.Sockets;

class Phone
{
	public string screen;
	public string soc;
	public int size;
	public int ram;
	public int storage;
	public Phone(string screen,
				string soc,
				int size,
				int ram)
	{
	this.screen=screen;
	this.soc=soc;
	this.size=size;
	this.ram=ram;	
	Console.WriteLine($"Phone Created :{screen}");		
	}
	public Phone(int size,int ram)
	{
		this.size=size;
		this.ram=ram;
		Console.WriteLine("Phone Created");
	}
	
	public Phone(string soc ,string screen)
	{
		this.soc=soc;
		this.screen = screen;
		Console.WriteLine("Phone Created");
	}
	
	
}