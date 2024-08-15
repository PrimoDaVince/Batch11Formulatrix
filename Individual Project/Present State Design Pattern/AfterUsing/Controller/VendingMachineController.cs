namespace AfterUsing;
using AfterUsing.StateClass;
public class VendingMachineController
{
	
	private IVendingMachineState _currentState;
	public int Stock { get; set; }

	public  VendingMachineController(int initialStock)
	{
		Stock = initialStock;
		_currentState = initialStock > 0 ? new IdleState() : new OutOfStockState();
		
	}

	public void SetState(IVendingMachineState state)
	{
		_currentState = state;
	}

	public void InsertCoin()
	{
		_currentState.InsertCoin(this);
	}

	public void Dispense()
	{
		_currentState.Dispense(this);
	}

	public void EjectCoin()
	{
		_currentState.EjectCoin(this);
	}
}
