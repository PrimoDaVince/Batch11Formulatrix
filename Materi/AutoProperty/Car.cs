namespace CarLib;


class Car
{
	private int _price;
	public int GetPrice()
	{
		return _price;
	}
}
//untuk akses private price perlu wadah public terlebih dahulu agar bisa akses private price

class CarWproperty
{
	public int Price { get; private set; }
}
//dengan menggunakan property bisa langsung dengan hanya menggunakan get; set


