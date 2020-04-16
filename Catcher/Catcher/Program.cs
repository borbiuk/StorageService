using System;

namespace Catcher
{
	class Program
	{
		static void Main(string[] args)
		{
			var catcher = new СatcherService();

			try
			{
				catcher.Run();
			}
			catch (Exception)
			{
				throw;
			}

			Console.WriteLine("");
		}
	}
}
