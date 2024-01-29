namespace BadProject.Modules.Interfaces
{
	using ThirdParty;

	public interface IAdvertisementCache
	{
		void SetAdvertisement(string id, Advertisement advertisement);
	}
}
