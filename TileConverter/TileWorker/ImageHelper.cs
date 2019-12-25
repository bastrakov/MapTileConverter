
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using TileWorker.Model;
using TileWorker.TileMath;


namespace TileWorker
{
		public class ImageHelper
		{

				/// <summary>
				/// 
				/// </summary>
				/// <param name="tileReplace"></param>
				public static void JoinTilesToOneImageAndSave(TileReplace tileReplace, string xyzInPathFormat, string outTileFilePath)
				{
						switch (tileReplace.NeedTileIndex.Count)
						{
								case 1: //just copy.
										var needTile = tileReplace.NeedTileIndex.First();
										TouchFile(outTileFilePath);
										File.Copy(string.Format(xyzInPathFormat, needTile.X, needTile.Y, tileReplace.Zoom), outTileFilePath);
										break;
								case 2:
										CreateAndSaveFrom2tiles(tileReplace, xyzInPathFormat, outTileFilePath);
										break;
								case 4:
										CreateAndSaveFrom4tiles(tileReplace, xyzInPathFormat, outTileFilePath);
										break;
						}
				}

				/// <summary>
				/// top and bottom pics.
				/// </summary>
				/// <param name="tileReplace"></param>
				/// <param name="xyzInPathFormat"></param>
				/// <param name="outTileFilePath"></param>
				private static void CreateAndSaveFrom2tiles(TileReplace tileReplace, string xyzInPathFormat, string outTileFilePath)
				{
						var joinTiles = new Bitmap(TileMathBase.TileSize, TileMathBase.TileSize);
						var joinTilesGraphics = Graphics.FromImage(joinTiles);

						var needTile0 = tileReplace.NeedTileIndex.First();
						var image0 = GetImageFrom(string.Format(xyzInPathFormat, needTile0.X, needTile0.Y, tileReplace.Zoom));
						var needTile1 = tileReplace.NeedTileIndex.Last();
						var image1 = GetImageFrom(string.Format(xyzInPathFormat, needTile1.X, needTile1.Y, tileReplace.Zoom));

						if (image0 != null && image1 != null)
						{
								joinTilesGraphics.DrawImage(image0, 0, 0 - TileMathBase.TileSize + tileReplace.Shift.Y, TileMathBase.TileSize, TileMathBase.TileSize);
								joinTilesGraphics.DrawImage(image1, 0, TileMathBase.TileSize - TileMathBase.TileSize + tileReplace.Shift.Y, TileMathBase.TileSize, TileMathBase.TileSize);

								TouchFile(outTileFilePath);
								joinTiles.Save(outTileFilePath);
						}
				}


				/// <summary>
				/// TopLeft-BottomLeft-TopRight-BottomRight.
				/// </summary>
				/// <param name="tileReplace"></param>
				/// <param name="xyzInPathFormat"></param>
				/// <param name="outTileFilePath"></param>
				private static void CreateAndSaveFrom4tiles(TileReplace tileReplace, string xyzInPathFormat, string outTileFilePath)
				{
						var joinTiles = new Bitmap(TileMathBase.TileSize, TileMathBase.TileSize);
						var joinTilesGraphics = Graphics.FromImage(joinTiles);

						var arr = tileReplace.NeedTileIndex.ToArray();
						var image00 = GetImageFrom(string.Format(xyzInPathFormat, arr[0].X, arr[0].Y, tileReplace.Zoom));
						var image01 = GetImageFrom(string.Format(xyzInPathFormat, arr[1].X, arr[1].Y, tileReplace.Zoom));
						var image10 = GetImageFrom(string.Format(xyzInPathFormat, arr[2].X, arr[2].Y, tileReplace.Zoom));
						var image11 = GetImageFrom(string.Format(xyzInPathFormat, arr[3].X, arr[3].Y, tileReplace.Zoom));

						if (image00 != null && image01 != null && image10 != null && image11 != null)
						{
								joinTilesGraphics.DrawImage(image00, 0 - tileReplace.Shift.X, 0 - tileReplace.Shift.Y, TileMathBase.TileSize, TileMathBase.TileSize);
								joinTilesGraphics.DrawImage(image01, 0 - tileReplace.Shift.X, TileMathBase.TileSize - tileReplace.Shift.Y, TileMathBase.TileSize, TileMathBase.TileSize);
								joinTilesGraphics.DrawImage(image10, TileMathBase.TileSize - tileReplace.Shift.X, 0 - tileReplace.Shift.Y, TileMathBase.TileSize, TileMathBase.TileSize);
								joinTilesGraphics.DrawImage(image11, TileMathBase.TileSize - tileReplace.Shift.X, TileMathBase.TileSize - tileReplace.Shift.Y, TileMathBase.TileSize, TileMathBase.TileSize);

								TouchFile(outTileFilePath);
								joinTiles.Save(outTileFilePath);
						}
				}

				private static void TouchFile(string filePath)
				{
						if (!Directory.Exists(Path.GetDirectoryName(filePath))) Directory.CreateDirectory(Path.GetDirectoryName(filePath));
						if (File.Exists(filePath)) File.Delete(filePath);
				}

				/// <summary>
				/// 
				/// </summary>
				/// <param name="fullFilePath"></param>
				/// <returns></returns>
				private static Image GetImageFrom(string fullFilePath)
				{
						Image tileImage = null;

						if (File.Exists(fullFilePath))
						{
								try
								{
										tileImage = Image.FromFile(fullFilePath);
								}
								catch (Exception ex)
								{
										//TODO get EX!
								}
						}
						return tileImage;
				}

		}

}
