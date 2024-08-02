/*class Car {
	public Engine engine;
	public Car(Engine engine) {
		this.engine = engine;
	}
}
class Engine {
	public int size;
	public string brand;
}
class ElectricEngine : Engine {
}
class DieselEngine : Engine {} 
class PistonEngine : Engine {}
*/

namespace cameraLib;

class Camera
{
	public Board board;
	public Camera(Board board)
	{
		this.board = board;
	}
}
class Board
{

	public string boardType;
	public string boardName;
	public int boardSize;
	public bool waterproof;
	public void Start()
	{
		Console.WriteLine("Board Created");
	}

}
class MirorlessBoard : Board
{
	
	public void Start()
	{
		Console.WriteLine("MirorlessBoardCreated");
		waterproof=true;
	}
}
class DslrBoard : Board
{
	public void Start()
	{
		Console.WriteLine("DSLR BOARD CREATED");
		waterproof=false;
	}
}



