using System;
using System.Threading.Channels;
using System.Threading.Tasks;

using Recipient.Services.Interfaces;

namespace Recipient.Services.Implementations
{
	internal class WebBackgroundWorkerProvider : IBackgroundWorkerProvider
	{
		/// <summary>
		/// Channel with tasks to execute in background.
		/// </summary>
		private static readonly Channel<Func<Task>> QueueChannel =
			Channel.CreateUnbounded<Func<Task>>();

		public WebBackgroundWorkerProvider(int workersCount)
		{
			for (var i = 0; i < workersCount; i++)
				StartWorker();
		}

		public async Task QueueBackgroundAsyncWorkItem(Func<Task> workItem)
		{
			try
			{
				await QueueChannel.Writer.WriteAsync(workItem);
			}
			catch (Exception e)
			{
				//TODO:logging
				throw new Exception("Work item not added to queue.", e);
			}
		}

		/// <summary>
		/// Run background worker that try get task and execute it.
		/// </summary>
		private static void StartWorker() =>
			Task.Run(async () =>
			{
				while (await QueueChannel.Reader.WaitToReadAsync())
					while (QueueChannel.Reader.TryRead(out var action))
						try
						{
							await action();
						}
						catch (Exception e)
						{
							//TODO:logging
							throw new Exception("Background exception.", e);
						}
			});
	}
}
