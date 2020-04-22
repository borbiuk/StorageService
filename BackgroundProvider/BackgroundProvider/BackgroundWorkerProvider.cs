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
				var ex = new BackgroundProviderException(Messages.WorkersCount);
				Logger.Instance.Error(ex, ex.Message);
			}
			
			for (var i = 0; i < workersCount; i++)
				StartWorker();
		}

		public async Task ToQueue(Func<Task> workItem)
		{
			try
			{
				await QueueChannel.Writer.WriteAsync(workItem);
			}
			catch (Exception ex)
			{
				Logger.Instance.Error(ex, Messages.WorkerAdding);
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
						catch (Exception ex)
						{
							Logger.Instance.Error(ex, Messages.TaskExecution);
						}
			});
	}
}
