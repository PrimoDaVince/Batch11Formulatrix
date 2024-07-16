using AbsLib;
class Program
{
	static void Main()
	{
		//buat objek baru bird dan cat
		Bird bird = new Bird();
		Cat cat = new Cat();
		//menjalankan fungis dari cat dan bird
		cat.MakeSound();
		bird.MakeSound();
		cat.Eat();
		cat.CheckAnimalEat();
		bird.Eat();
		bird.CheckAnimalEat();
	}
}