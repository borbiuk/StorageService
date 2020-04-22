using System.Threading.Tasks;

namespace Catcher.Interfaces
{
	public interface IDataHandler
	{
		/// <summary>
		/// Save data to the storage.
		/// </summary>
		/// <param name="data"></param>
		Task SaveAsync(string data);

		/// <summary>
		/// Try save data to the storage.
		/// </summary>
		/// <param name="data">Data to save</param>
		/// <returns>Returns `true` if data saved successful</returns>
		Task<bool> TrySaveAsync(string data);
	}
}
