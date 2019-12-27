
using System;
using System.Configuration;
using NLog;

namespace TileWorker
{
		class Program
		{

				static void Main(string[] args)
				{

						LogManager.GetCurrentClassLogger().Debug("InTilesDir: " + ConfigurationManager.AppSettings["InTilesDir"]);
						var inTilesDir = ConfigurationManager.AppSettings["InTilesDir"].ToString();

						LogManager.GetCurrentClassLogger().Debug("InXYZPathFormat: " + ConfigurationManager.AppSettings["InXYZPathFormat"]);
						var inXYZPathFormat = ConfigurationManager.AppSettings["InXYZPathFormat"].ToString();

						LogManager.GetCurrentClassLogger().Debug("OutTilesDir: " + ConfigurationManager.AppSettings["OutTilesDir"]);
						var outTilesDir = ConfigurationManager.AppSettings["OutTilesDir"].ToString();

						LogManager.GetCurrentClassLogger().Debug("OutXYZPathFormat: " + ConfigurationManager.AppSettings["OutXYZPathFormat"]);
						var outXYZPathFormat = ConfigurationManager.AppSettings["OutXYZPathFormat"].ToString();


						LogManager.GetCurrentClassLogger().Debug(ConfigurationManager.AppSettings["FromTo"]);
						var fromTo = ConfigurationManager.AppSettings["FromTo"].ToString().ToLower();

						if (fromTo == "spherical-wgs84")
								new TileConverter().ConvertFromSphericalToWgs84(inTilesDir, inXYZPathFormat, outTilesDir, outXYZPathFormat);
						else if (fromTo == "wgs84-spherical")
								new TileConverter().ConvertFromWgs84ToSpherical(inTilesDir, inXYZPathFormat, outTilesDir, outXYZPathFormat);

						
						LogManager.GetCurrentClassLogger().Debug("TileConverter DONE");
				}

		}
}
