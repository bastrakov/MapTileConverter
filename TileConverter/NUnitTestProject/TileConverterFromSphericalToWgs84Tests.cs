
using NUnit.Framework;
using System.Linq;
using TileWorker;

namespace NUnitTestProject
{
		public class TileConverterFromSphericalToWgs84Tests
		{

				[SetUp]
				public void Setup()
				{
				}

				[Test]
				public void FromSphericalToWgs84Test0()
				{
						var result = new TileConverter().TileFromSphericalToWgs84(0, 0, 0);
						Assert.True(result.NeedTileIndex.Count == 1);
						Assert.True(result.Shift.X == 0 && result.Shift.Y == 0);
				}

				[Test]
				public void FromSphericalToWgs84Test1()
				{
						var result = new TileConverter().TileFromSphericalToWgs84(1, 0, 1);
						Assert.True(result.NeedTileIndex.Count == 2);
						Assert.True(result.Shift.X == 0 && result.Shift.Y == 1);
				}

				[Test]
				public void FromSphericalToWgs84Test3()
				{
						var result = new TileConverter().TileFromSphericalToWgs84(5, 2, 3);
						Assert.True(result.NeedTileIndex.Count == 2);
						Assert.True(result.Shift.X == 0 && result.Shift.Y == 2);
				}

				[Test]
				public void FromSphericalToWgs84Test4()
				{
						var result = new TileConverter().TileFromSphericalToWgs84(10, 4, 4);
						Assert.True(result.NeedTileIndex.Count == 2);
						Assert.True(result.Shift.X == 0 && result.Shift.Y == 4);
				}

				[Test]
				public void FromSphericalToWgs84Test18()
				{
						var result = new TileConverter().TileFromSphericalToWgs84(165944, 80801, 18);
						Assert.True(result.NeedTileIndex.Count == 2);
						Assert.True(result.Shift.X == 0 && result.Shift.Y == 159);
						var testNeedTileIndexFirst = result.NeedTileIndex.First();
						var testNeedTileIndexLast = result.NeedTileIndex.Last();
						Assert.True(testNeedTileIndexFirst.X == 165944 && testNeedTileIndexFirst.Y == 80801);
						Assert.True(testNeedTileIndexLast.X == 165944 && testNeedTileIndexLast.Y == 80802);
				}

				[Test]
				public void FromSphericalToWgs84Test19()
				{
						var result = new TileConverter().TileFromSphericalToWgs84(331889, 161602, 19);
						Assert.True(result.NeedTileIndex.Count == 2);
						Assert.True(result.Shift.X == 0 && result.Shift.Y == 62);
						var testNeedTileIndexFirst = result.NeedTileIndex.First();
						var testNeedTileIndexLast = result.NeedTileIndex.Last();
						Assert.True(testNeedTileIndexFirst.X == 331889 && testNeedTileIndexFirst.Y == 161602);
						Assert.True(testNeedTileIndexLast.X == 331889 && testNeedTileIndexLast.Y == 161603);
				}

		}

}