namespace BadProject.Modules.Interfaces
{
	using ThirdParty;

	public interface IAdvertisementCacheProvider
	{
		void SetAdvertisement(string id, Advertisement advertisement);
	}
}
