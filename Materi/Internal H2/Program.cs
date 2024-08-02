//project ini sudah di ngereference Internal H1
using Internal_H1;
class Program
{
	static void Main()
	{
		Car car = new Car();
		//car.GoInternal();<----Error Karena Tidak Satu Project
		car.GoPublic();//<------Tidak Eror Karena Menggunakan Public
	}
}
