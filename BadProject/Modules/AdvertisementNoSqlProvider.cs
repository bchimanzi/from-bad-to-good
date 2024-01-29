namespace BadProject.Modules
{
	using System;
	using System.Threading;
	using System.Collections.Generic;
	using Microsoft.Extensions.Options;

	using ThirdParty;

	using BadProject.Infrastructure;
	using BadProject.Modules.Interfaces;

	public class AdvertisementNoSqlProvider : IAdvertisement
	{
		#region Fields

		private readonly NoSqlAdvProvider noSqlAdvProvider;
		private readonly ApplicationSettings applicationSettings;
		private static Queue<DateTime> errors = new Queue<DateTime>();
		private readonly object lockObj = new object();

		#endregion Fields

		#region Constructor

		public AdvertisementNoSqlProvider(
			IOptions<ApplicationSettings> applicationSettings, NoSqlAdvProvider noSqlAdvProvider)
		{
			this.applicationSettings = applicationSettings.Value;
			this.noSqlAdvProvider = noSqlAdvProvider;
		}

		#endregion Constructor

		#region Methods

		public Advertisement GetAdvertisement(string id)
		{
			if (string.IsNullOrEmpty(value: id))
			{
				return null;
			}

			Advertisement advertisement = null;

			lock (lockObj)
			{
				TidyErrorQueue();

				var errorCount = GetErrorCount();

				// If Cache is empty and ErrorCount<10 then use HTTP provider
				if ((advertisement == null) && (errorCount < this.applicationSettings.MaximumErrorCount))
				{
					int retry = 0;

					do
					{
						retry++;
						try
						{
							advertisement = this.noSqlAdvProvider.GetAdv(webId: id);
						}
						catch
						{
							Thread.Sleep(millisecondsTimeout: this.applicationSettings.RetryDelay);
							errors.Enqueue(item: DateTime.Now); // Store HTTP error timestamp              
						}
					} while ((advertisement == null) && (retry < this.applicationSettings.RetryCount));

				}
			}

			return advertisement;
		}

		#endregion Methods

		#region Private Methods

		private void TidyErrorQueue()
		{
			while (errors.Count > this.applicationSettings.MinimumQueueTidyCount)
			{
				errors.Dequeue();
			}
		}

		private int GetErrorCount()
		{
			var errorCount = 0;

			foreach (var errorDate in errors)
			{
				if (errorDate > DateTime.Now.AddHours(value: -1))
				{
					errorCount++;
				}
			}

			return errorCount;
		}

		#endregion Private Methods

	}
}
