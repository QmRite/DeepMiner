using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DeepMiner.Domain
{
    public class BlockEntity
    {
            public Point position { get; set; }
            public Size size { get; set; }
            public Point centre { get; set; }

            public BlockEntity(Point position, Size size)
            {
                this.position = position;
                this.size = size;
                centre = new Point(position.X + Map.CellSize / 2, position.Y + Map.CellSize / 2);
            }       
    }
}
