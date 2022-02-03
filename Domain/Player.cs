using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace DeepMiner.Domain
{
    public class Player
    {
        public Point pos;

        private int idleFrames;
        private int runFrames;
        private int mineFrames;

        private int currentAnimation;
        private int currentFrame;
        private int currentLimit;

        private bool turnedRight;

        public int dirX { get; private set; }
        public int dirY { get; private set; }

        public int speed { get { return 4; } }
        private double force = 0.1;

        public bool isMoving { get; private set; }        

        private Tuple<int, int> size;

        private Image spriteSheet;

        public Player(Point position)
        {
            pos = position;
            idleFrames = Models.Player.idleFrames;
            runFrames = Models.Player.runFrames;
            mineFrames = Models.Player.mineFrames;
            size = new Tuple<int, int>(65, 70);
            currentAnimation = 0;
            currentFrame = 0;
            currentLimit = idleFrames;
            turnedRight = true;
        }

        public void Move()
        {
            if ((pos.X + 10 < Map.GetPixWidth || !turnedRight) && (pos.X - 10 > 0 || turnedRight))
                pos = new Point(pos.X + dirX, pos.Y);
            if (pos.Y + 70 < Map.GetPixHeight)
                pos = new Point(pos.X, pos.Y + dirY);
        }

        public void PlayAnimation(Graphics g)
        {
            spriteSheet = new Bitmap(Image.FromFile("data\\player.png"));

            if (currentFrame < currentLimit - 1)
                currentFrame++;
            else currentFrame = 0;

            g.DrawImage(spriteSheet,
                new Rectangle(new Point(pos.X - size.Item1 / 2, pos.Y + GameScene.CamDelta),
                new Size(size.Item1, size.Item2)),
                size.Item1 * currentFrame,
                size.Item2 * currentAnimation,
                size.Item1, size.Item2, GraphicsUnit.Pixel);
        }

        private void SetAnimationConfig(int currentAnimation)
        {
            this.currentAnimation = currentAnimation;
            switch (currentAnimation)
            {
                case 0:
                    currentLimit = idleFrames;
                    break;
                case 1:
                    currentLimit = runFrames;
                    break;
                case 2:
                    currentLimit = mineFrames;
                    break;
                case 3:
                    currentLimit = idleFrames;
                    break;
                case 4:
                    currentLimit = runFrames;
                    break;
                case 5:
                    currentLimit = mineFrames;
                    break;
            }
        }

        public void StopMove(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    dirY = 0;
                    force = 0.1;
                    break;
                case Keys.S:
                    dirY = 0;
                    force = 0.1;
                    break;
                case Keys.A:
                    dirX = 0;
                    force = 0.1;
                    break;
                case Keys.D:
                    dirX = 0;
                    force = 0.1;
                    break;
                case Keys.Space:
                    force = 0.1;
                    break;
            }

            isMoving = false;
            if (turnedRight)
                SetAnimationConfig(0);
            else
                SetAnimationConfig(3);
        }

        public void Act(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.S:
                    dirY = 0;
                    dirX = 0;
                    isMoving = false;
                    SetAnimationConfig(2);
                    TryDestroyBlock(e);
                    break;
                case Keys.A:
                    dirX = -speed;
                    isMoving = true;
                    turnedRight = false;
                    SetAnimationConfig(4);
                    if (Map.IsBLockHere((pos.X - 15) / Map.CellSize, (pos.Y + 30) / Map.CellSize))
                        TryDestroyBlock(e);
                    break;
                case Keys.D:
                    dirX = speed;
                    isMoving = true;
                    turnedRight = true;
                    SetAnimationConfig(1);
                    if (Map.IsBLockHere((pos.X + 10) / Map.CellSize, (pos.Y + 30) / Map.CellSize))
                        TryDestroyBlock(e);
                    break;
                case Keys.Space:
                    dirY = 0;
                    dirX = 0;
                    isMoving = false;
                    if (turnedRight)
                        SetAnimationConfig(2);
                    else
                        SetAnimationConfig(5);
                    TryDestroyBlock(e);
                    break;
            }
        }

        private void DestroyBlock(KeyEventArgs e)
        {
            var y = 0;
            var x = 0;
            switch (e.KeyCode)
            {
                case Keys.A:
                    y = (pos.Y + 30) / Map.CellSize;
                    x = (pos.X - 15) / Map.CellSize;
                    break;
                case Keys.D:
                    y = (pos.Y + 30) / Map.CellSize;
                    x = (pos.X + 10) / Map.CellSize;
                    break;
                case Keys.Space:
                    y = (pos.Y + 66) / Map.CellSize;
                    x = (pos.X) / Map.CellSize;
                    break;
                case Keys.S:
                    y = (pos.Y + 70) / Map.CellSize;
                    x = (pos.X) / Map.CellSize;
                    break;
            }

            if (Map.isDestructibleBlockHere(x, y))
            {
                GameScene.ChangeScore(Map.GetBlock(x, y));
                Map.RemoveBlock(x, y);
            }
        }

        private void TryDestroyBlock(KeyEventArgs e)
        {
            if (!PhysicsController.IsFall(this))
            {
                force += force;
                if (force > 1000000)
                {
                    DestroyBlock(e);
                    force = 0.1;
                }
            }
       
        }
    }
}