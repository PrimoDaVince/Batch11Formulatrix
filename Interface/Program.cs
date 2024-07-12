using EngineLib;
class Program {
	static void Main() {
		IHeat ee = new ElectricEngine();
		IEngine ee2 = new ElectricEngine();
		
		ee.Exploded();
		ee2.Start();
		ee2.Stop();
		
		
		
	}
}