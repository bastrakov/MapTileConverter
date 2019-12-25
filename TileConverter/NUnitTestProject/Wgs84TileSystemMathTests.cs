
using NUnit.Framework;
using System.Drawing;
using TileWorker.TileMath;

namespace NUnitTestProject
{
		public class Wgs84TileSystemMathTests
		{

				private double testLatitude = 56.631196;
				private double testLongitude = 47.890438;

				private Point testXY;
				private Point testTile;


				[SetUp]
				public void Setup()
				{
				}

				[Test]
				public void Test1()
				{
						testXY = Wgs84TileMath.LatLongToPixelXY(testLatitude, testLongitude, 1);
						Assert.True(testXY.X == 324 && testXY.Y == 158);
						testTile = Wgs84TileMath.LatLong2TileIndex(testLatitude, testLongitude, 1);
						Assert.True(testTile.X == 1 && testTile.Y == 0);
				}

				[Test]
				public void Test2()
				{
						testXY = Wgs84TileMath.LatLongToPixelXY(testLatitude, testLongitude, 2);
						Assert.True(testXY.X == 648 && testXY.Y == 317);
						testTile = Wgs84TileMath.LatLong2TileIndex(testLatitude, testLongitude, 2);
						Assert.True(testTile.X == 2 && testTile.Y == 1);
				}

				[Test]
				public void Test3()
				{
						testXY = Wgs84TileMath.LatLongToPixelXY(testLatitude, testLongitude, 3);
						Assert.True(testXY.X == 1296 && testXY.Y == 633);
						testTile = Wgs84TileMath.LatLong2TileIndex(testLatitude, testLongitude, 3);
						Assert.True(testTile.X == 5 && testTile.Y == 2);
				}

				[Test]
				public void Test4()
				{
						testXY = Wgs84TileMath.LatLongToPixelXY(testLatitude, testLongitude, 4);
						Assert.True(testXY.X == 2593 && testXY.Y == 1266);
						testTile = Wgs84TileMath.LatLong2TileIndex(testLatitude, testLongitude, 4);
						Assert.True(testTile.X == 10 && testTile.Y == 4);
				}

				[Test]
				public void Test18()
				{
						testXY = Wgs84TileMath.LatLongToPixelXY(testLatitude, testLongitude, 18);
						Assert.True(testXY.X == 42481857 && testXY.Y == 20744901);
						testTile = Wgs84TileMath.LatLong2TileIndex(testLatitude, testLongitude, 18);
						Assert.True(testTile.X == 165944 && testTile.Y == 81034);
				}

				[Test]
				public void Test19()
				{
						testXY = Wgs84TileMath.LatLongToPixelXY(testLatitude, testLongitude, 19);
						Assert.True(testXY.X == 84963713 && testXY.Y == 41489802);
						testTile = Wgs84TileMath.LatLong2TileIndex(testLatitude, testLongitude, 19);
						Assert.True(testTile.X == 331889 && testTile.Y == 162069);
				}

		}

}