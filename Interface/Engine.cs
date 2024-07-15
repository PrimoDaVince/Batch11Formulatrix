namespace EngineLib;

interface IEngine
{
	void Start();
	void Stop();
	void Exploded();
}
interface IHeat
{
	void Exploded();
}
class ElectricEngine : IEngine, IHeat
{
	public void Exploded()
	{
		Console.WriteLine("Exploded Duaaar");
	}
	public void Start()
	{
		
		Console.WriteLine("Engine Start");
	}
	public void Stop()
	{
		Console.WriteLine("Engine Stop");

	}
}
