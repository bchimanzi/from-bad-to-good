namespace BadProject.Modules.Interfaces
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using ThirdParty;

	public interface IAdvertisement
    {
        Advertisement GetAdvertisement(string id);
    }
}
