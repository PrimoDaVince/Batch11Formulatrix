﻿public delegate void MyDelegate();
class Publisher
{
	public event MyDelegate del;
	public void reset()
	{
		del=null;
	}
}
class Subscriber
{
	public void GetNotification(){}
	
}
class Program
{
	static void Main()
	{
		Publisher pub = new();
		Subscriber sub = new();
		
		pub.del+=sub.GetNotification();
		pub.del-=sub.GetNotification();
		//pub.del=sub.GetNotification(); <---Karena Menggunakan event maka tidak bisa menggunakan =
	}
}