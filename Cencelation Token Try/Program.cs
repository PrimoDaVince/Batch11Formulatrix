class Program
{
	static void Main()
	{
		StartDataTransfer();
	}

	private static void StartDataTransfer()
	{
		Console.WriteLine("Data Transfer Started...");
		Thread.Sleep(50);
		CancellationTokenSource alarmCts = new CancellationTokenSource();
		Task t2 = Task.Run(() => DataTransferMonitor(alarmCts.Token));
		Task t1 = Task.Run(() =>
		{
			Console.ReadLine();
			alarmCts.Cancel();
			Console.WriteLine("Data Transfer Has Been Cenceled");
		});
		Task t3 = Task.Run((() =>
        {
            //Simulasi Outage	
            NewMethod(alarmCts);
        }));
		Task.WaitAny(t1, t2,t3);
	}

    private static void NewMethod(CancellationTokenSource alarmCts)
    {
        Random random = new Random();
        bool isAvailable = true;

        while (true)
        {
            Thread.Sleep(200);
            if (random.Next(100) > 95)
            {
                if (isAvailable)
                {
                    isAvailable = false;
                    Console.WriteLine("System is now OUTAGE");
                    alarmCts.Cancel();
                    Console.WriteLine("Data Transfer Has Been Canceled");
                    Console.ReadLine();
                }

            }

        }
    }

    static void DataTransferMonitor(CancellationToken ct)
	{   int i=0;
		while (!ct.IsCancellationRequested&&i!=100)
		{   
			i++;
			Console.WriteLine($"Data Transfer Percentage{i}% ");
			Thread.Sleep(100);
		}
		if (i==100)
		{
			Console.WriteLine($"Data Transfer Completed");
		}
		
	}
}