namespace AfterUsing.StateClass;

public class IdleState : IVendingMachineState
{
     public void InsertCoin(VendingMachineController machine)
    {
        Console.WriteLine("Coin inserted.");
        machine.SetState(new CoinInsertedState());
    }

    public void Dispense(VendingMachineController machine)
    {
        Console.WriteLine("Insert coin first.");
    }

    public void EjectCoin(VendingMachineController machine)
    {
        Console.WriteLine("No coin to eject.");
    }

}
