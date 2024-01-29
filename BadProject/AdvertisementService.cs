using System;

using ThirdParty;
using BadProject.Modules;

namespace Adv
{
	public class AdvertisementService
	{
		#region Fields

		private readonly AdvertisementCache advertisementCache;
		private readonly AdvertisementSqlProvider advertisementSqlProvider;
		private readonly AdvertisementNoSqlProvider advertisementNoSqlProvider;

		#endregion Fields

		#region Constructor

		public AdvertisementService(
			AdvertisementCache advertisementCache,
			AdvertisementSqlProvider advertisementSqlProvider,
			AdvertisementNoSqlProvider advertisementNoSqlProvider)
		{
			this.advertisementCache = advertisementCache;
			this.advertisementSqlProvider = advertisementSqlProvider;
			this.advertisementNoSqlProvider = advertisementNoSqlProvider;
		}

		#endregion Constructor

		// **************************************************************************************************
		// Loads Advertisement information by id
		// from cache or if not possible uses the "mainProvider" or if not possible uses the "backupProvider"
		// **************************************************************************************************
		// Detailed Logic:
		// 
		// 1. Tries to use cache (and retuns the data or goes to STEP2)
		//
		// 2. If the cache is empty it uses the NoSqlDataProvider (mainProvider), 
		//    in case of an error it retries it as many times as needed based on AppSettings
		//    (returns the data if possible or goes to STEP3)
		//
		// 3. If it can't retrive the data or the ErrorCount in the last hour is more than 10, 
		//    it uses the SqlDataProvider (backupProvider)

		#region Methods

		public Advertisement GetAdvertisement(string id)
		{
			Advertisement advertisement = null;

			// Use Cache if available
			advertisement = this.advertisementCache.GetAdvertisement(id: id);

			if (advertisement == null)
			{
				// Use NoSql available
				advertisement = this.advertisementNoSqlProvider.GetAdvertisement(id: id);
			}

			// if needed try to use Backup provider
			if (advertisement == null)
			{
				advertisement = this.advertisementSqlProvider.GetAdvertisement(id: id);
			}

			//save to cache
			if (advertisement != null)
			{
				this.advertisementCache.SetAdvertisement(id: id, advertisement: advertisement);
			}

			return advertisement;
		}

		#endregion Methods


	}
}
