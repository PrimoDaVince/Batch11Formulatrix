namespace DelgetePractice;

public delegate void DelegetSaya(string pesan);
class Publisher
{
	private DelegetSaya _subs;
	private List<DelegetSaya>_historySubs = new();
	public bool AddSubscriber(DelegetSaya del)
	{ Console.WriteLine("Fungsi Add Subs Berjalan");
		if(!CheckValidasi(del))
		{
			
		_subs+=del;
		Console.WriteLine(" Validasi  Bernilai Salah : Berjalan Dan Berhasil Add Subscriber");
		return true;
		}
		Console.WriteLine(" Validasi Bernilai Benar : Tidak Berhasil Add Subscriber");
		return false;
		
	}
	public bool RemoveSubscriber(DelegetSaya del)
	{
		_subs-=del;
		return false;
	}
	public void SentNotification()
	{
		_subs?.Invoke("hello all");
	}
	public bool CheckValidasi(DelegetSaya del)
	{
		if (_subs != null)
		{
			Delegate[]delegates = _subs.GetInvocationList();
			if(delegates.Contains(del))
			{
				Console.WriteLine("Chekvalidasi True");
				return true;
			}
		}
		Console.WriteLine("Chekvalidasi False");
		return false;
		
	}
	public List<DelegetSaya>GetHistoricalSubs()
	{
		return _historySubs;
	}
	public bool CheckHistoriSubs(DelegetSaya sub)
	{
		if(_historySubs.Contains(sub))
		{
			return true;
		}
		return false;
	}
	
	
}
