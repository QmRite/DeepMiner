using DeepMiner.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DeepMiner
{
    public class PhysicsController
    {
        public static bool IsCollide(Player entity, Point dir)
        {

            for (var i = 0; i < Map.mapObjects.Count; i++)
            {
                var currObject = Map.mapObjects[i];
                var delta = new PointF();
                delta.X = entity.pos.X - 3 - (currObject.position.X + currObject.size.Width / 2);
                delta.Y = entity.pos.Y + 50 - (currObject.position.Y + currObject.size.Height / 2);
                if (Math.Abs(delta.X) <= 3 + currObject.size.Width / 2)
                {
                    if (Math.Abs(delta.Y) <= 16 + currObject.size.Height / 2)
                    {
                        if (delta.X < 0 && dir.X == entity.speed && entity.pos.Y + 50 > currObject.position.Y && entity.pos.Y + 50 < currObject.position.Y + currObject.size.Height)
                        {
                            return true;
                        }
                        if (delta.X > 0 && dir.X == -entity.speed && entity.pos.Y + 50 > currObject.position.Y && entity.pos.Y + 50 < currObject.position.Y + currObject.size.Height)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool IsFall(Player entity)
        {
            if (Map.IsBLockHere((entity.pos.X - 15) / Map.CellSize, (entity.pos.Y + 66) / Map.CellSize) ||
                Map.IsBLockHere((entity.pos.X + 8) / Map.CellSize, (entity.pos.Y + 66) / Map.CellSize))
                    return false;
            return true;
        }

        public static void Falling(Player entity)
        {
            if (entity.pos.Y + 70 < Map.GetPixHeight)
                entity.pos = new Point(entity.pos.X, entity.pos.Y + 10);
            if (Map.GetBlock(entity.pos.X / Map.CellSize, (entity.pos.Y + 66) / Map.CellSize) == Blocks.Finish ||
                Map.GetBlock(entity.pos.X / Map.CellSize, (entity.pos.Y + 66) / Map.CellSize) == Blocks.Bayonet)
                {
                    GameScene.FinishGameCheck();
                }    
        }

    }
}
