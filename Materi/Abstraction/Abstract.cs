namespace AbsLib;

abstract class Animal // jika memakai abstrak harus class abstract
{
	public abstract void Name(string name);
	public int age;
	public abstract void MakeSound(); //Membuat fungsi abstract MakeSound()
	public virtual void Eat()
	{
		Console.WriteLine("EAT SOMETHING");
	}

}
class Cat : Animal
{
	
	public void CheckAnimalEat()//fungsi agar ngecek fungsi eat yang sudah di override di class child
	{
		base.Eat();	
	}
	public override void Eat()
	{
		Console.WriteLine("wiskas");
	}
    public override void Name(string name)
    {
        name = "Si oren";
    }
    
	public override void MakeSound()//wajib di override karena parent class dan fungsi abstract
	{
		Console.WriteLine("Meaow");
	}
	

}
class Bird : Animal
{
	public void CheckAnimalEat()//fungsi agar ngecek fungsi eat yang sudah di override di class child
	{
		base.Eat();	
	}
	public override void Eat()
	{
		Console.WriteLine("seeds");
	}
	public override void MakeSound() //wajib di override karena parent class dan fungsi abstract
	{
		Console.WriteLine("Kiw-kiw");
	}
}
