namespace AfterUsing.StateClass;

public class CoinInsertedState : IVendingMachineState
{
    public void InsertCoin(VendingMachineController machine)
    {
        Console.WriteLine("Coin already inserted.");
    }

    public void Dispense(VendingMachineController machine)
    {
        if (machine.Stock > 0)
        {
            Console.WriteLine("Dispensing item...");
            machine.Stock--;
            if (machine.Stock == 0)
            {
                machine.SetState(new OutOfStockState());
            }
            else
            {
                machine.SetState(new IdleState());
            }
        }
        else
        {
            Console.WriteLine("Out of stock.");
            machine.SetState(new OutOfStockState());
        }
    }

    public void EjectCoin(VendingMachineController machine)
    {
        Console.WriteLine("Coin ejected.");
        machine.SetState(new IdleState());
    }

}
