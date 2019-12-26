
using NUnit.Framework;
using System.Linq;
using TileWorker;

namespace NUnitTestProject
{
		public class TileConverterFromWgs84ToSphericalTests
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
						var result = new TileConverter().TileFromSphericalToWgs84(165944, 81034, 18);
						Assert.True(result.NeedTileIndex.Count == 2);
						Assert.True(result.Shift.X == 0 && result.Shift.Y == 37);
						var testNeedTileIndexFirst = result.NeedTileIndex.First();
						var testNeedTileIndexLast = result.NeedTileIndex.Last();
						Assert.True(testNeedTileIndexFirst.X == 165944 && testNeedTileIndexFirst.Y == 81034);
						Assert.True(testNeedTileIndexLast.X == 165944 && testNeedTileIndexLast.Y == 81035);
				}

				[Test]
				public void FromWgs84ToSphericalTest19()
				{
						var result = new TileConverter().TileFromWgs84ToSpherical(331889, 162069, 19);
						Assert.True(result.NeedTileIndex.Count == 2);
						Assert.True(result.Shift.X == 0 && result.Shift.Y == 193);
						var testNeedTileIndexFirst = result.NeedTileIndex.First();
						var testNeedTileIndexLast = result.NeedTileIndex.Last();
						Assert.True(testNeedTileIndexFirst.X == 331889 && testNeedTileIndexFirst.Y == 162069);
						Assert.True(testNeedTileIndexLast.X == 331889 && testNeedTileIndexLast.Y == 162070);
				}

		}

}