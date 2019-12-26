
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TileWorker.Model;
using TileWorker.TileMath;

namespace TileWorker
{
		public class TileConverter
		{

				public void ConvertFromSphericalToWgs84(string inTilesDir, string xyzInPathFormat, string outTilesDir, string xyzOutPathFormat)
				{
						var tileIndexList = ReadAllFiles(inTilesDir, xyzInPathFormat);
						if (tileIndexList.Count > 0)
						{
								var tileReplacesList = tileIndexList.Select(i => TileFromSphericalToWgs84(i.X, i.Y, i.Zoom));

								SaveTiles(
										Path.Combine(inTilesDir, xyzInPathFormat),
										Path.Combine(outTilesDir, xyzOutPathFormat),
										tileReplacesList);
						}
				}

				public void ConvertFromWgs84ToSpherical(string inTilesDir, string xyzInPathFormat, string outTilesDir, string xyzOutPathFormat)
				{
						var tileIndexList = ReadAllFiles(inTilesDir, xyzInPathFormat);
						if (tileIndexList.Count > 0)
						{
								var tileReplacesList = tileIndexList.Select(i => TileFromWgs84ToSpherical(i.X, i.Y, i.Zoom));

								SaveTiles(
										Path.Combine(inTilesDir, xyzInPathFormat),
										Path.Combine(outTilesDir, xyzOutPathFormat),
										tileReplacesList);
						}
				}

				public TileReplace TileFromSphericalToWgs84(int sphericalTileIndexX, int sphericalTileIndexY, int zoom)
				{
						var leftTop = TileMathBase.TileXY2PixelXY(sphericalTileIndexX, sphericalTileIndexY);
						SphericalTileMath.PixelXYToLatLong(leftTop.X, leftTop.Y, zoom, out double latitude, out double longitude);
						var resultXY = Wgs84TileMath.LatLongToPixelXY(latitude, longitude, zoom);
						return FindReplace(sphericalTileIndexX, sphericalTileIndexY, zoom, resultXY);
				}

				public TileReplace TileFromWgs84ToSpherical(int wgs84TileIndexX, int wgs84TileIndexY, int zoom)
				{
						var leftTop = TileMathBase.TileXY2PixelXY(wgs84TileIndexX, wgs84TileIndexY);
						var latlon = Wgs84TileMath.PixelXYToLatLong(leftTop.X, leftTop.Y, zoom);
						SphericalTileMath.LatLongToPixelXY(latlon.X, latlon.Y, zoom, out int pixelX, out int pixelY);
						return FindReplace(wgs84TileIndexX, wgs84TileIndexY, zoom, new Point(pixelX, pixelY));
				}

				private void SaveTiles(string xyzInPathFormat, string xyzOutPathFormat, IEnumerable<TileReplace> tileReplacesList)
				{
						foreach (var tileIndex in tileReplacesList)
						{
								var isAllTileFilesExist = true;
								foreach (var tileFile in tileIndex.NeedTileIndex)
								{
										var path = string.Format(xyzInPathFormat, tileFile.X, tileFile.Y, tileIndex.Zoom);
										if (!File.Exists(string.Format(xyzInPathFormat, tileFile.X, tileFile.Y, tileIndex.Zoom)))
												isAllTileFilesExist = false;
								}

								if (isAllTileFilesExist)
								{
										var outTileFilePath = string.Format(xyzOutPathFormat, tileIndex.NewX, tileIndex.NewY, tileIndex.Zoom);
										ImageHelper.JoinTilesToOneImageAndSave(tileIndex, xyzInPathFormat, outTileFilePath);
								}
						}
				}

				private List<TileIndex> ReadAllFiles(string dirPath, string xyzPathFormat)
				{
						var result = new List<TileIndex>();
						var allfiles = Directory.GetFiles(dirPath, "*.*", SearchOption.AllDirectories);
						var pathFormat = new PathFormat(dirPath + xyzPathFormat);
						if (pathFormat != null)
						{
								result = allfiles.Select(i => new TileIndex(i, pathFormat))
										.Where(y => !string.IsNullOrWhiteSpace(y.FullPath))
										.ToList();
						}
						return result;
				}

				private TileReplace FindReplace(int sphericalTileIndexX, int sphericalTileIndexY, int zoom, Point resultXY)
				{
						var tileXYindex = TileMathBase.PixelXY2TileXY(resultXY.X, resultXY.Y);

						var xShift = resultXY.X % TileMathBase.TileSize;
						var yShift = resultXY.Y % TileMathBase.TileSize;

						if (xShift == 0 && yShift == 0)
						{
								//just copy.
								var result1 = new TileReplace(tileXYindex.X, tileXYindex.Y, zoom);
								result1.NeedTileIndex.Add(new Point(sphericalTileIndexX, sphericalTileIndexY));
								result1.Shift = new Point(0, 0);
								return result1;
						}
						else if (xShift == 0 && yShift != 0)
						{
								//can try to create Y+1 tile.
								var result2 = new TileReplace(tileXYindex.X, tileXYindex.Y + 1, zoom);
								result2.NeedTileIndex.Add(new Point(sphericalTileIndexX, sphericalTileIndexY));
								result2.NeedTileIndex.Add(new Point(sphericalTileIndexX, sphericalTileIndexY + 1));
								result2.Shift = new Point(0, yShift);
								return result2;
						}
						else
						{
								//need 4 tiles to create a one new.
								var result3 = new TileReplace(tileXYindex.X + 1, tileXYindex.Y + 1, zoom);
								result3.NeedTileIndex.Add(new Point(sphericalTileIndexX, sphericalTileIndexY));
								result3.NeedTileIndex.Add(new Point(sphericalTileIndexX, sphericalTileIndexY + 1));
								result3.NeedTileIndex.Add(new Point(sphericalTileIndexX + 1, sphericalTileIndexY));
								result3.NeedTileIndex.Add(new Point(sphericalTileIndexX + 1, sphericalTileIndexY + 1));
								result3.Shift = new Point(xShift, yShift);
								return result3;
						}
				}

				internal class PathFormat
				{
						public string XYZpathFormat { get; set; }

						public int Xindex { get; set; }
						public string Xremove0 { get; set; }
						public string Xremove1 { get; set; }
						public int Yindex { get; set; }
						public string Yremove0 { get; set; }
						public string Yremove1 { get; set; }
						public int Zindex { get; set; }
						public string Zremove0 { get; set; }
						public string Zremove1 { get; set; }


						public PathFormat (string xyzPathFormat)
						{
								var xyzPathFormatParts = xyzPathFormat.Split(@"\");
								for (var i=0; i<xyzPathFormatParts.Length; i++)
								{
										if (xyzPathFormatParts[i].IndexOf("{0}") != -1)
										{
												Xindex = i;
												var xStrParts = xyzPathFormatParts[i].Split(@"{0}");
												Xremove0 = xStrParts[0];
												Xremove1 = xStrParts[1];
										}
										else if (xyzPathFormatParts[i].IndexOf("{1}") != -1)
										{
												Yindex = i;
												var yStrParts = xyzPathFormatParts[i].Split(@"{1}");
												Yremove0 = yStrParts[0];
												Yremove1 = yStrParts[1];
										}
										else if (xyzPathFormatParts[i].IndexOf("{2}") != -1)
										{
												Zindex = i;
												var zStrParts = xyzPathFormatParts[i].Split(@"{2}");
												Zremove0 = zStrParts[0];
												Zremove1 = zStrParts[1];
										}

								}
						}
				}

				internal class TileIndex
				{
						public string FullPath { set; get; } = null;
						public int Zoom { set; get; } = 0;
						public int X { set; get; } = 0;
						public int Y { set; get; } = 0;

						public TileIndex(string tilePath, PathFormat pathFormat)
						{
								FullPath = null;
								var parts = tilePath.Split(@"\");

								var xStr = parts[pathFormat.Xindex];
								if (!string.IsNullOrWhiteSpace(pathFormat.Xremove0))
										xStr = xStr.Replace(pathFormat.Xremove0, "");
								if (!string.IsNullOrWhiteSpace(pathFormat.Xremove1))
										xStr = xStr.Replace(pathFormat.Xremove1, "");
								if (!int.TryParse(xStr, out int x))
										return;
								X = x;

								var yStr = parts[pathFormat.Yindex];
								if (!string.IsNullOrWhiteSpace(pathFormat.Yremove0))
										yStr = yStr.Replace(pathFormat.Yremove0, "");
								if (!string.IsNullOrWhiteSpace(pathFormat.Yremove1))
										yStr = yStr.Replace(pathFormat.Yremove1, "");
								if (!int.TryParse(yStr, out int y))
										return;
								Y = y;

								var zStr = parts[pathFormat.Zindex];
								if (!string.IsNullOrWhiteSpace(pathFormat.Zremove0))
										zStr = zStr.Replace(pathFormat.Zremove0, "");
								if (!string.IsNullOrWhiteSpace(pathFormat.Zremove1))
										zStr = zStr.Replace(pathFormat.Zremove1, "");
								if (!int.TryParse(zStr, out int z))
										return;
								Zoom = z;

								FullPath = tilePath;
						}

				}

		}

}
