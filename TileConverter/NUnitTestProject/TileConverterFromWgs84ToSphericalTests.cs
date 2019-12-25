
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
				public void FromSphericalToWgs84Test18()
				{
						var result = TileConverter.TileFromSphericalToWgs84(165944, 81034, 18);
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
						var result = TileConverter.TileFromWgs84ToSpherical(331889, 162069, 19);
						Assert.True(result.NeedTileIndex.Count == 4);
						Assert.True(result.Shift.X == 117 && result.Shift.Y == 7);
				}

		}

}