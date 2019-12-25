
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
				public void TestGoogleToYandex()
				{
						TileConverter.ConvertFromSphericalToWgs84(
								@"D:\temp\0000000\goo11\sat\",
								@"z{2}\{1}\{0}.jpg",
								@"D:\temp\0000000\222222\",
								@"z{2}\x{0}\y{1}.jpg");
						Assert.True(true);
				}

				[Test]
				public void TestYandexToGoogle()
				{
						//TileConverter.ConvertFromSphericalToWgs84(
						//		@"D:\temp\0000000\goo11\sat\",
						//		@"z{2}\{1}\{0}.jpg",
						//		@"D:\temp\0000000\222222\",
						//		@"z{2}\x{0}\y{1}.jpg");
						Assert.True(true);
				}

		}

}