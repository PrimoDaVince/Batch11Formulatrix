using System.Security.Cryptography.X509Certificates;

abstract class Engine { 
	public string type;
	protected abstract void Start();
	public void GenerateRotation() {
		Console.WriteLine("Generate rotation...");
	}
	public void AcssesStart()
	{
		Start();
	}
}
class Electric : Engine {
	protected override void Start() {
		Console.WriteLine("Electric Start");
	}

}
class Program {
	static void Main() {
		Electric electric = new Electric();
		electric.AcssesStart();
	}
}