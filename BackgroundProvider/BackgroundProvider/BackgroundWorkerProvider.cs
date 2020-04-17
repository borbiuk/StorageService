using System;
using System.Threading.Channels;
using System.Threading.Tasks;

using BackgroundProvider.Exceptions;

namespace BackgroundProvider
{
	public class BackgroundWorkerProvider
	{
		/// <summary>
		/// Channel with tasks to execute in background.
		/// </summary>
		private static readonly Channel<Func<Task>> QueueChannel =
			Channel.CreateUnbounded<Func<Task>>();

		/// <summary>
		/// Create new instance of BeckgroundProvider.
		/// </summary>
		/// <param name="workersCount">Count workers that was execute tasks from queue in background.</param>
		public BackgroundWorkerProvider(int workersCount)
		{
			if (workersCount <= 0)
			{
				var e = new BackgroundProviderException(Messages.WorkersCount);
				Logger.Instance.Fatal(e, e.Message);
				
				throw e;
			}
			
			for (var i = 0; i < workersCount; i++)
				StartWorker();

			Logger.Instance.Error("Love Daria");
		}

		public async Task ToQueue(Func<Task> workItem)
		{
			try
			{
				await QueueChannel.Writer.WriteAsync(workItem);
			}
			catch (Exception innerException)
			{
				var e = new BackgroundProviderException(Messages.WorkerAdding, innerException);
				Logger.Instance.Error(e, e.Message);
				
				throw e;
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
						catch (Exception innerException)
						{
							var e = new BackgroundProviderException(
								Messages.TaskExecution, innerException);
							Logger.Instance.Error(e, e.Message);

							throw e;
						}
			});
	}
}
