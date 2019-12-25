
using NUnit.Framework;
using System.Drawing;
using TileWorker.TileMath;

namespace NUnitTestProject
{
		public class SphericalTileSystemMathTests
		{

				private double testLatitude = 56.631196;
				private double testLongitude = 47.890438;

				private int testX;
				private int testY;
				private Point testTile;


				[SetUp]
				public void Setup()
				{
				}

				[Test]
				public void Test1()
				{
						SphericalTileMath.LatLongToPixelXY(testLatitude, testLongitude, 1, out testX, out testY);
						Assert.True(testX == 324 && testY == 158);
						testTile = TileMathBase.PixelXY2TileXY(testX, testY);
						Assert.True(testTile.X == 1 && testTile.Y == 0);
				}

				[Test]
				public void Test2()
				{
						SphericalTileMath.LatLongToPixelXY(testLatitude, testLongitude, 2, out testX, out testY);
						Assert.True(testX == 648 && testY == 316);
						testTile = TileMathBase.PixelXY2TileXY(testX, testY);
						Assert.True(testTile.X == 2 && testTile.Y == 1);
				}

				[Test]
				public void Test3()
				{
						SphericalTileMath.LatLongToPixelXY(testLatitude, testLongitude, 3, out testX, out testY);
						Assert.True(testX == 1296 && testY == 631);
						testTile = TileMathBase.PixelXY2TileXY(testX, testY);
						Assert.True(testTile.X == 5 && testTile.Y == 2);
				}

				[Test]
				public void Test4()
				{
						SphericalTileMath.LatLongToPixelXY(testLatitude, testLongitude, 4, out testX, out testY);
						Assert.True(testX == 2593 && testY == 1263);
						testTile = TileMathBase.PixelXY2TileXY(testX, testY);
						Assert.True(testTile.X == 10 && testTile.Y == 4);
				}

				[Test]
				public void Test18()
				{
						SphericalTileMath.LatLongToPixelXY(testLatitude, testLongitude, 18, out testX, out testY);
						Assert.True(testX == 42481857 && testY == 20685094);
						testTile = TileMathBase.PixelXY2TileXY(testX, testY);
						Assert.True(testTile.X == 165944 && testTile.Y == 80801);
				}

				[Test]
				public void Test19()
				{
						SphericalTileMath.LatLongToPixelXY(testLatitude, testLongitude, 19, out testX, out testY);
						Assert.True(testX == 84963713 && testY == 41370188);
						testTile = TileMathBase.PixelXY2TileXY(testX, testY);
						Assert.True(testTile.X == 331889 && testTile.Y == 161602);
				}

		}

}