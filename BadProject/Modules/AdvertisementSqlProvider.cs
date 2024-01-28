namespace BadProject.Modules
{
	using ThirdParty;

	public class AdvertisementSqlProvider
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
