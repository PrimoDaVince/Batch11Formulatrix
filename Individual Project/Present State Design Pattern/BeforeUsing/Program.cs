using System;

public class VendingMachine
{
	private bool _coinInserted = false;
	private bool _isOutOfStock = false;
	public int Stock { get; private set; }

	public VendingMachine(int initialStock)
	{
		Stock = initialStock;
		_isOutOfStock = Stock <= 0;
	}

	public void InsertCoin()
	{
		if (_isOutOfStock)
		{
			Console.WriteLine("Cannot insert coin. Out of stock.");
			return;
		}

		if (_coinInserted)
		{
			Console.WriteLine("Coin already inserted.");
			return;
		}

		Console.WriteLine("Coin inserted.");
		_coinInserted = true;
	}

	public void Dispense()
	{
		if (!_coinInserted)
		{
			Console.WriteLine("Insert coin first.");
			return;
		}

		if (_isOutOfStock)
		{
			Console.WriteLine("Cannot dispense. Out of stock.");
			return;
		}

		Console.WriteLine("Dispensing item...");
		Stock--;

		_coinInserted = false;

		if (Stock <= 0)
		{
			_isOutOfStock = true;
			Console.WriteLine("Out of stock.");
		}
	}

	public void EjectCoin()
	{
		if (!_coinInserted)
		{
			Console.WriteLine("No coin to eject.");
			return;
		}

		Console.WriteLine("Coin ejected.");
		_coinInserted = false;
	}
}

class Program
{
	static void Main(string[] args)
	{
		VendingMachine machine = new VendingMachine(2);

		machine.InsertCoin();
		machine.Dispense();

		machine.InsertCoin();
		machine.EjectCoin();

		machine.InsertCoin();
		machine.Dispense();

		machine.InsertCoin();  // Should indicate Out of Stock

		Console.ReadLine();
	}
}



