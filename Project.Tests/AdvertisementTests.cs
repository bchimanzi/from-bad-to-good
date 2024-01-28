namespace Project.Tests
{
	using System;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	
	using Adv;
	using ThirdParty;

	[TestClass]
	public class AdvertisementTests : TestBase
	{

		[TestMethod]
		public void Can_GetAdvertisement()
		{
			//Arrange
			var advertisementService = new AdvertisementService(
				advertisementCache: this.advertisementCache,
				advertisementSqlProvider: this.advertisementSqlProvider,
				advertisementNoSqlProvider: this.advertisementNoSqlProvider);

			//Act
			var advertisement = advertisementService.GetAdvertisement(id: "1");

			//Assert
			Assert.IsNotNull(value: advertisement, message: "Failed to get advertisement");
		}

		[TestMethod]
		public void Can_Get_FromCache()
		{
			//Arrange
			var id = "1";
			var advertisement = new Advertisement 
			{ 
				Description = "xyz", 
				Name = "abcd", 
				WebId = id 
			};

			this.advertisementCache.SetAdvertisement(id: id, advertisement: advertisement);

			//Act
			advertisement = this.advertisementCache.GetAdvertisement(id: id);

			//Assert
			Assert.IsNotNull(value: advertisement, message: "Failed to get advertisement from cache");
		}

		[TestMethod]
		public void Can_Get_FromNoSqlProvider()
		{
			//Arrange
			var id = "1";

			//Act
			var advertisement = this.advertisementNoSqlProvider.GetAdvertisement(id: id);

			//Assert
			Assert.IsNotNull(value: advertisement, message: "Failed to get advertisement from NoSql provider");
		}

		[TestMethod]
		public void Can_Get_FromSqlProvider()
		{
			//Arrange
			var id = "1";

			//Act
			var advertisement = this.advertisementSqlProvider.GetAdvertisement(id: id);

			//Assert
			Assert.IsNotNull(value: advertisement, message: "Failed to get advertisement from cache");
		}

		[TestMethod]
		public void Can_FailGet_WhenIdIsNull()
		{
			//Arrange
			var advertisementService = new AdvertisementService(
				advertisementCache: this.advertisementCache,
				advertisementSqlProvider: this.advertisementSqlProvider,
				advertisementNoSqlProvider: this.advertisementNoSqlProvider);

			//Act
			var advertisement = advertisementService.GetAdvertisement(id: null);

			//Assert
			Assert.IsNull(value: advertisement, message: "Should not return advertisement for supplied Id");
		}

		[TestMethod]
		public void Can_FailGet_WhenIdIsEmpty()
		{
			//Arrange
			var advertisementService = new AdvertisementService(
				advertisementCache: this.advertisementCache,
				advertisementSqlProvider: this.advertisementSqlProvider,
				advertisementNoSqlProvider: this.advertisementNoSqlProvider);

			//Act
			var advertisement = advertisementService.GetAdvertisement(id: string.Empty);

			//Assert
			Assert.IsNull(value: advertisement, message: "Should not return advertisement for supplied Id");
		}

		[TestMethod]
		public void Can_FailSet_WhenIdIsEmpty()
		{
			//Arrange
			var id = string.Empty;
			var advertisement = new Advertisement
			{
				Description = "xyz",
				Name = "abcd",
				WebId = id
			};

			this.advertisementCache.SetAdvertisement(id: id, advertisement: advertisement);

			//Act
			advertisement = this.advertisementCache.GetAdvertisement(id: id);

			//Assert
			Assert.IsNull(value: advertisement, message: "Should not set advertisement for supplied Id");
		}

		[TestMethod]
		public void Can_FailSet_WhenAdvertisementIsNull()
		{
			//Arrange
			var id = Guid.NewGuid().ToString();
			Advertisement advertisement = null;

			this.advertisementCache.SetAdvertisement(id: id, advertisement: advertisement);

			//Act
			advertisement = this.advertisementCache.GetAdvertisement(id: id);

			//Assert
			Assert.IsNull(value: advertisement, message: "Should not set advertisement for supplied value");
		}
	}
}