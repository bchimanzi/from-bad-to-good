namespace BadProject.Modules.Interfaces
{
	using ThirdParty;

	public interface IAdvertisementProvider
	{
		Advertisement GetAdvertisement(string id);
	}
}
