namespace Project.Tests
{
	using System;
	using System.IO;
	using Microsoft.Extensions.Options;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Caching.Memory;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using ThirdParty;

	using Adv;
	using BadProject.Modules;
	using BadProject.Infrastructure;

	[TestClass]
	public class TestBase
	{
		protected readonly IMemoryCache cache;
		private readonly IConfiguration configuration;
		private readonly IServiceProvider serviceProvider;
		protected readonly NoSqlAdvProvider noSqlAdvProvider;
		protected readonly AdvertisementCache advertisementCache;
		protected readonly AdvertisementSqlProvider advertisementSqlProvider;
		protected readonly AdvertisementNoSqlProvider advertisementNoSqlProvider;
		protected readonly IOptions<ApplicationSettings> applicationSettings;

		public TestBase()
		{
			var serviceCollection = new ServiceCollection();

			this.configuration = this.BuildConfiguration(serviceCollection: serviceCollection);
			this.serviceProvider = this.ConfigureServices(serviceCollection: serviceCollection);
			this.cache = this.serviceProvider.GetRequiredService<IMemoryCache>();
			this.noSqlAdvProvider = this.serviceProvider.GetRequiredService<NoSqlAdvProvider>();
			this.advertisementCache = this.serviceProvider.GetRequiredService<AdvertisementCache>();
			this.applicationSettings = this.serviceProvider.GetRequiredService<IOptions<ApplicationSettings>>();
			this.advertisementSqlProvider = this.serviceProvider.GetRequiredService<AdvertisementSqlProvider>();
			this.advertisementNoSqlProvider = this.serviceProvider.GetRequiredService<AdvertisementNoSqlProvider>();
		}

		private IConfigurationRoot BuildConfiguration(ServiceCollection serviceCollection)
		{
			var configuration = new ConfigurationBuilder()
					.SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
					.AddJsonFile("appsettings.json", false)
					.Build();

			serviceCollection.AddSingleton<IConfiguration>(configuration);


			return configuration;
		}

		private IServiceProvider ConfigureServices(ServiceCollection serviceCollection)
		{
			serviceCollection.Configure<ApplicationSettings>(configuration.GetSection("ApplicationSettings"));
			serviceCollection.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));
			serviceCollection.AddMemoryCache();
			serviceCollection.AddTransient<NoSqlAdvProvider>();
			serviceCollection.AddTransient<AdvertisementCache>();
			serviceCollection.AddTransient<AdvertisementSqlProvider>();
			serviceCollection.AddTransient<AdvertisementNoSqlProvider>();
			serviceCollection.AddTransient<AdvertisementService>();

			return serviceCollection.BuildServiceProvider();
		}

	}
}
