
using System;
using System.Drawing;

namespace TileWorker.TileMath
{

		/// <summary>
		/// wiki.openstreetmap.org/wiki/Mercator#C.23_implementation
		/// </summary>
		public class Wgs84TileMath : TileMathBase
		{

				/// <summary>
				/// 
				/// </summary>
				/// <param name="latitude"></param>
				/// <param name="longitude"></param>
				/// <param name="zoom"></param>
				/// <returns></returns>
				public static Point LatLongToPixelXY(double latitude, double longitude, int zoom)
				{
						latitude = Clip(latitude, MinLatitude, MaxLatitude);
						longitude = Clip(longitude, MinLongitude, MaxLongitude);

						double rLon = longitude * DEG2RAD;
						var x = Math.Round((20037508.342789 + R_MAJOR * rLon) * 53.5865938 / Math.Pow(2.0, 23 - zoom));

						double rLat = latitude * DEG2RAD;
						var z1 = Math.Tan(Math.PI / 4 + rLat / 2) / Math.Pow(Math.Tan(Math.PI / 4 + Math.Asin(ECCENT * Math.Sin(rLat)) / 2), ECCENT);
						var y = Math.Round((20037508.342789 - R_MAJOR * Math.Log(z1)) * 53.5865938 / Math.Pow(2, 23 - zoom));

						return new Point((int)x, (int)y);
				}

				/// <summary>
				/// 
				/// </summary>
				/// <param name="latitude"></param>
				/// <param name="longitude"></param>
				/// <param name="zoom"></param>
				/// <returns></returns>
				public static Point LatLong2TileIndex (double latitude, double longitude, int zoom)
				{
						var xy = LatLongToPixelXY(latitude, longitude, zoom);
						return PixelXY2TileXY(xy.X, xy.Y);
				}

				/// <summary>
				/// 
				/// </summary>
				/// <param name="x"></param>
				/// <param name="y"></param>
				/// <param name="zoom"></param>
				/// <returns></returns>
				public static Point XY2TileIndex(int x, int y, int zoom)
				{
						return PixelXY2TileXY(x, y);
				}

				/// <summary>
				/// 
				/// </summary>
				/// <param name="x"></param>
				/// <param name="y"></param>
				/// <param name="zoom"></param>
				/// <returns></returns>
				public static PointF PixelXYToLatLong(int x, int y, int zoom)
				{
						double c1 = 0.00335655146887969;
						double c2 = 0.00000657187271079536;
						double c3 = 0.00000001764564338702;
						double c4 = 0.00000000005328478445;
						double z1 = (23 - zoom);
						double mercX = (x * Math.Pow(2, z1)) / 53.5865938 - 20037508.342789;
						double mercY = 20037508.342789 - (y * Math.Pow(2, z1)) / 53.5865938;

						double g = Math.PI / 2 - 2 * Math.Atan(1 / Math.Exp(mercY / EarthRadius));
						double z = g + c1 * Math.Sin(2 * g) + c2 * Math.Sin(4 * g) + c3 * Math.Sin(6 * g) + c4 * Math.Sin(8 * g);

						var lat = z * RAD2Deg;
						var lng = mercX / EarthRadius * RAD2Deg;

						return new PointF((float)lat, (float)lng);
				}

		}

}
