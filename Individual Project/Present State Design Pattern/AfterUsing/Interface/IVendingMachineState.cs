namespace AfterUsing;

public interface IVendingMachineState
{
    void InsertCoin(VendingMachineController machine);
    void Dispense(VendingMachineController  machine);
    void EjectCoin(VendingMachineController  machine);
}