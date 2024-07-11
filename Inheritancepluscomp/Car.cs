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
	
	public string Boardtype;
	
}
class MirorlessBoard:Board
{
	public string brand;
}
class DslrBoard:Board
{
	
}



