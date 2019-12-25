
using System;
using System.Collections.Generic;
using System.Drawing;

namespace TileWorker.Model
{
		public class TileReplace
		{
				public int Zoom { set; get; } = 0;
				public int NewX { set; get; } = 0;
				public int NewY { set; get; } = 0;
				public Point Shift { set; get; }

				//1 tile: asis. 2 tiles: Top-Bottom. 4 tiles: LftTop-LeftBottom-RightTop-RightBottom
				public List<Point> NeedTileIndex = new List<Point>();

				public TileReplace(int x, int y, int zoom)
				{
						Zoom = zoom;
						NewX = x;
						NewY = y;
				}

		}

}
