using System.Threading.Tasks;

namespace Catcher.Interfaces
{
	public interface IDataAccessClient
	{
		/// <summary>
		/// Save data to the storage
		/// </summary>
		/// <param name="data"></param>
		Task Send(string data);

		/// <summary>
		/// Try save data to the storage
		/// </summary>
		/// <param name="data"></param>
		/// <returns>Returns `true` if data saved successful</returns>
		Task<bool> TrySend(string data);
	}
}
