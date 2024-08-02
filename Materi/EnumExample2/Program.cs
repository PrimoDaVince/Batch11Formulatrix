﻿
class Program {
	static void Main() {
		
		int result = (int) MonthOfYear.Feb; //Cari tau Enum Keberapa
		Console.WriteLine(result);
		
		MonthOfYear result2 = (MonthOfYear)5;//Cari Tau String Enum Berdasarkan Angka
		Console.WriteLine(result2);
		
		string x = MonthOfYear.Feb.ToString();//Cari Tau String Enum Berdasarkan String Enum
		Console.WriteLine(x);
	}
}
public enum MonthOfYear {
	Jan = 1, // Nilai Default Enum Dimulai dari 0 nilai dari enum bisa diganti <-- seperti jan = 1 
	Feb, //Setiap Pergantian Nilai enum maka enum dibawahnya akan mengikuti dari nilai di atas jadi isi dari feb menjadi = 2
	Mar,
	Apr,
	May,
	Jun,
	Jul,
	Aug,
	Sep,
	Oct,
	Nov,
	Dec
}