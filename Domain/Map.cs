using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DeepMiner.Domain
{
	public class Map
	{
		private static int width => Mine.GetLength(0);
		private static int height => Mine.GetLength(1);

		private static Blocks[,] Mine;

		public static int CellSize { get { return 60; } }

		private static Image spriteSheet;

		public static int GetPixWidth => CellSize * width;
		public static int GetPixHeight => CellSize * height;

		public static List<BlockEntity> mapObjects { get; private set; }

		public Map(string[] lines)
		{
			if (lines == null || lines.Length == 0)
				throw new ArgumentNullException();
			if (lines.Length != lines.Where(s => s.Length == lines[0].Length).Count())
				throw new ArgumentException();

			mapObjects = new List<BlockEntity>();			
			Mine = new Blocks[lines[0].Length, lines.Length];
			for (var y = 0; y < lines.Length; y++)
			{
				for (var x = 0; x < lines[0].Length; x++)
				{
					switch (lines[y][x])
					{
						case ' ':
							Mine[x, y] = Blocks.Air;
							break;
						case 'T':
							Mine[x, y] = Blocks.Turf;
							CreateMapEntity(x, y);
							break;
						case 'R':
							Mine[x, y] = Blocks.Rock;
							CreateMapEntity(x, y);
							break;
						case 'C':
							Mine[x, y] = Blocks.Coal;
							CreateMapEntity(x, y);
							break;
						case 'I':
							Mine[x, y] = Blocks.Iron;
							CreateMapEntity(x, y);
							break;
						case 'G':
							Mine[x, y] = Blocks.Gold;
							CreateMapEntity(x, y);
							break;
						case 'E':
							Mine[x, y] = Blocks.Emerald;
							CreateMapEntity(x, y);
							break;
						case 'D':
							Mine[x, y] = Blocks.Diamond;
							CreateMapEntity(x, y);
							break;
						case 'O':
							Mine[x, y] = Blocks.Obsidian;
							CreateMapEntity(x, y);
							break;
						case 'B':
							Mine[x, y] = Blocks.Bayonet;
							CreateMapEntity(x, y);
							break;
						case 'F':
							Mine[x, y] = Blocks.Finish;
							CreateMapEntity(x, y);
							break;
						default:
							Mine[x, y] = Blocks.Air;
							CreateMapEntity(x, y);
							break;
					}
				}
			}
		}

		private void CreateMapEntity(int x, int y)
        {
			mapObjects.Add(new BlockEntity(new Point(x * CellSize, y * CellSize), new Size(CellSize, CellSize)));
		}

		public void DrawMap(Graphics g)
		{
			spriteSheet = new Bitmap("data\\blocks.png");
			for (var x = 0; x < width; x++)
			{
				for (var y = 0; y < height; y++)
                {
                    switch (Mine[x, y])
                    {
                        case Blocks.Turf:
							DrawBlock(g, x, y, 0, 1);
							break;
                        case Blocks.Rock:
							DrawBlock(g, x, y, 1, 1);
							break;
                        case Blocks.Coal:
							DrawBlock(g, x, y, 0, 0);
							break;
                        case Blocks.Iron:
							DrawBlock(g, x, y, 1, 0);
							break;
                        case Blocks.Gold:
							DrawBlock(g, x, y, 2, 0);
							break;
                        case Blocks.Emerald:
                            DrawBlock(g, x, y, 3, 0);
                            break;
                        case Blocks.Diamond:
							DrawBlock(g, x, y, 4, 0);
							break;
                        case Blocks.Obsidian:
							DrawBlock(g, x, y, 3, 1);
							break;
                        case Blocks.Bayonet:
							DrawBlock(g, x, y, 4, 2);
							break;
                        case Blocks.Finish:
							DrawBlock(g, x, y, 2, 2);
							break;
                        default:
                            Mine[x, y] = Blocks.Air;
                            break;
                    }
                }
            }
		}

        private static void DrawBlock(Graphics g, int x, int y, int column, int line)
        {
            g.DrawImage(spriteSheet,
                new Rectangle(new Point(x * CellSize, y * CellSize + GameScene.CamDelta),
                new Size(CellSize, CellSize)),
				column * CellSize, line * CellSize,
                CellSize, CellSize,
                GraphicsUnit.Pixel);
        }

        public static void RemoveBlock(int x, int y)
        {
			CheckBorders(x, y);
			Mine[x, y] = Blocks.Air;
            mapObjects = mapObjects
                .Where(b => b.position.X != x * CellSize || b.position.Y != y * CellSize)
                .ToList();
        }

        public static bool IsBLockHere(int x, int y)
        {
			CheckBorders(x, y);
			if (Mine[x, y] != Blocks.Air)
					return true;
			return false;
		}

		public static Blocks GetBlock(int x, int y)
        {
            CheckBorders(x, y);
            if (IsBLockHere(x, y))
                return Mine[x, y];
            return Blocks.Air;
        }

		public static bool isDestructibleBlockHere(int x, int y)
		{
			return IsBLockHere(x, y)
				&& GetBlock(x, y) != Blocks.Obsidian
				&& GetBlock(x, y) != Blocks.Finish;
		}

		private static void CheckBorders(int x, int y)
        {
            if (x >= width || x < 0 || y >= height || y < 0)
                throw new ArgumentOutOfRangeException();
        }
	}
}
	