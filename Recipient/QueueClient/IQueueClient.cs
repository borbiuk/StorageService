using System.Threading.Tasks;

namespace QueueClient
{
	public interface IQueueClient
	{
		/// <summary>
		/// Send data to the queue.
		/// </summary>
		/// <param name="queue">Queue name.</param>
		/// <param name="data">Data to send.</param>
		public Task SendAsync(string queue, object data);
	}
}
