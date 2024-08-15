using AfterUsing;
class Program
{
	static void Main(string[] args)
	{
		VendingMachineController machine = new (2);

		machine.InsertCoin();
		machine.Dispense();

		machine.InsertCoin();
		machine.EjectCoin();

		machine.InsertCoin();
		machine.Dispense();

		machine.InsertCoin();  

		Console.ReadLine();
	}
}