
using System;
using System.Drawing;

namespace TileWorker.TileMath
{
		public class TileMathBase
		{

				public const int TileSize = 256;

				public const double EarthRadius = 6378137;
				public const double MinLatitude = -85.05112878;
				public const double MaxLatitude = 85.05112878;
				public const double MinLongitude = -180;
				public const double MaxLongitude = 180;

				public const double DEG2RAD = Math.PI / 180.0;
				public const double RAD2Deg = 180.0 / Math.PI;
				public const double R_MAJOR = 6378137.0;
				public const double R_MINOR = 6356752.3142;
				public const double RATIO = R_MINOR / R_MAJOR; //‭0.996647189328169‬
				public static readonly double ECCENT = Math.Sqrt(1.0 - (RATIO * RATIO)); //‭‭0.0818191909289068‬
				public static readonly double COM = 0.5 * ECCENT;


				//public static readonly double PI_2 = Math.PI / 2.0;




				/// <summary>  
				/// Clips a number to the specified minimum and maximum values.  
				/// </summary>  
				/// <param name="n">The number to clip.</param>  
				/// <param name="minValue">Minimum allowable value.</param>  
				/// <param name="maxValue">Maximum allowable value.</param>  
				/// <returns>The clipped value.</returns>  
				public static double Clip(double n, double minValue, double maxValue)
				{
						return Math.Min(Math.Max(n, minValue), maxValue);
				}

				/// <summary> 
				/// Converts tile XY coordinates into pixel XY coordinates of the upper-left pixel of the specified tile.
				/// </summary>
				/// <param name="tile"></param>
				/// <returns></returns>
				public static Point TileXY2PixelXY(int x, int y)
				{
						return new Point(x * TileSize, y * TileSize);
				}

				/// <summary>
				/// Converts pixel XY coordinates into tile XY coordinates of the tile containing the specified pixel.
				/// </summary>
				/// <param name="pixel"></param>
				/// <returns></returns>
				public static Point PixelXY2TileXY(int x, int y)
				{
						return new Point(x / TileSize, y / TileSize);
				}


		}
}
