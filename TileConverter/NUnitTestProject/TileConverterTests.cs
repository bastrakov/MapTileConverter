
using NUnit.Framework;
using System.Drawing;
using System.Linq;
using TileWorker;

namespace NUnitTestProject
{
		public class TileConverterTests
		{

				[SetUp]
				public void Setup()
				{
				}

				[Test]
				public void Test0()
				{
						TileConverter.ConvertFromSphericalToWgs84(
								@"D:\temp\SASPlanet_190707\SAS.Planet.Release.190707\cache_gmt\sat\",
								@"z{2}\{0}\{1}.jpg",
								@"D:\temp\0000000\",
								@"z{2}\x{0}\y{1}.jpg");
						Assert.True(true);
				}

		}

}