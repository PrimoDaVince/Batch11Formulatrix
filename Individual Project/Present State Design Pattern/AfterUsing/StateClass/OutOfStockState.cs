namespace AfterUsing.StateClass;

public class OutOfStockState : IVendingMachineState
{
     public void InsertCoin(VendingMachineController machine)
    {
        Console.WriteLine("Cannot insert coin. Out of stock.");
    }

    public void Dispense(VendingMachineController machine)
    {
        Console.WriteLine("Cannot dispense. Out of stock.");
    }

    public void EjectCoin(VendingMachineController machine)
    {
        Console.WriteLine("No coin to eject. Out of stock.");
    }

}
