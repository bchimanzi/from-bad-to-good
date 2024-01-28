namespace BadProject.Modules
{
	using System;
	using Microsoft.Extensions.Options;
	using Microsoft.Extensions.Caching.Memory;

	using ThirdParty;
	using BadProject.Infrastructure;

	public class AdvertisementCache
	{
		private readonly IMemoryCache cache;
		private readonly CacheSettings cacheSettings;
		public AdvertisementCache(
		IMemoryCache cache,
		IOptions<CacheSettings> cacheSettings)
		{
			this.cache = cache;
			this.cacheSettings = cacheSettings.Value;
		}

		public Advertisement GetAdvertisement(string id)
		{
			if (string.IsNullOrEmpty(value: id))
			{
				return null;
			}

			var advertisement = (Advertisement)cache.Get($"AdvKey_{id}");

			return advertisement;
		}

		public void SetAdvertisement(string id, Advertisement advertisement)
		{
			if (string.IsNullOrEmpty(id) || advertisement == null)
			{
				return;
			}

			cache.Set($"AdvKey_{id}", advertisement, DateTimeOffset.Now.AddMinutes(cacheSettings.MinutesToLive));
		}
	}
}
