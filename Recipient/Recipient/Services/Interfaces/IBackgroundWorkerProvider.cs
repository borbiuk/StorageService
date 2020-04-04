using System;
using System.Threading.Tasks;

namespace Recipient.Services.Interfaces
{
	internal interface IBackgroundWorkerProvider
	{
		/// <summary>
		/// Add task to queue to run it in the background, without context of request.
		/// </summary>
		Task QueueBackgroundAsyncWorkItem(Func<Task> workItem);
	}
}
