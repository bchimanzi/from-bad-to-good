namespace BadProject.Infrastructure
{
	public class ApplicationSettings
	{
		public int RetryCount { get; set; }
		public int RetryDelay { get; set; }
    public int MinimumQueueTidyCount { get; set; }
		public int MaximumErrorCount { get; set; }
  }
}
