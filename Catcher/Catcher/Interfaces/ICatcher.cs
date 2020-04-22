using EasyNetQ.Consumer;

namespace Catcher.Interfaces
{
	public interface ICatcher
	{
		void Register(IReceiveRegistration registration);
	}
}
