using System.Threading.Tasks;
using Catcher.Interfaces;
using EasyNetQ.Consumer;

namespace Catcher.Implementations
{
	public class CatcherService : ICatcher
	{
		private readonly IDataHandler _dataHandler;

		public CatcherService(IDataHandler dataHandler)
		{
			_dataHandler = dataHandler;
		}

		public void Register(IReceiveRegistration registration)
		{
			registration.Add<string>(Catch);
		}

		private async Task Catch(string message) => await _dataHandler.SaveAsync(message);
	}
}
