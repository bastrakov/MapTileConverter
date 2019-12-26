
using System;
using System.Configuration;

namespace TileWorker
{
		class Program
		{
				static void Main(string[] args)
				{

						Console.WriteLine("InTilesDir: " + ConfigurationManager.AppSettings["InTilesDir"]);
						var inTilesDir = ConfigurationManager.AppSettings["InTilesDir"].ToString();

						Console.WriteLine("InXYZPathFormat: " + ConfigurationManager.AppSettings["InXYZPathFormat"]);
						var inXYZPathFormat = ConfigurationManager.AppSettings["InXYZPathFormat"].ToString();

						Console.WriteLine("OutTilesDir: " + ConfigurationManager.AppSettings["OutTilesDir"]);
						var outTilesDir = ConfigurationManager.AppSettings["OutTilesDir"].ToString();

						Console.WriteLine("OutXYZPathFormat: " + ConfigurationManager.AppSettings["OutXYZPathFormat"]);
						var outXYZPathFormat = ConfigurationManager.AppSettings["OutXYZPathFormat"].ToString();


						Console.WriteLine(ConfigurationManager.AppSettings["FromTo"]);
						var fromTo = ConfigurationManager.AppSettings["FromTo"].ToString().ToLower();

						if (fromTo == "spherical-wgs84")
								new TileConverter().ConvertFromSphericalToWgs84(inTilesDir, inXYZPathFormat, outTilesDir, outXYZPathFormat);
						else if (fromTo == "wgs84-spherical")
								new TileConverter().ConvertFromWgs84ToSpherical(inTilesDir, inXYZPathFormat, outTilesDir, outXYZPathFormat);
						
						Console.WriteLine("TileConverter DONE");
				}
		}
}
