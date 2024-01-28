namespace BadProject.Modules
{
	using BadProject.Modules.Interfaces;
	using ThirdParty;

	public class AdvertisementSqlProvider : IAdvertisement
	{
		public Advertisement GetAdvertisement(string id)
		{
			if (string.IsNullOrEmpty(value: id))
			{
				return null;
			}

			var advertisement = SQLAdvProvider.GetAdv(webId: id);

			return advertisement;
		}
	}
}
