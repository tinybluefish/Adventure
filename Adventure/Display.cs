using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure
{
    public enum Direction {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public partial class Display : Form
    {
        private readonly int MOVEMENT = 10;
        private readonly Point TopLeft = new Point(100, 200);

        // Boundaries
        // TODO: seems like a crappy place to put this..? Also - variable?
        internal readonly int TopBoundary = 0;
        internal readonly int BottomBoundary = 650;
        internal readonly int LeftBoundary = 0;
        internal readonly int RightBoundary = 1430;
        


        public Display()
        {
            InitializeComponent();
        }


        internal Point MoveSprite(PictureBox sprite, Direction direction)
        {
            int x = 0;
            int y = 0;

            switch (direction)
            {
                case Direction.UP:
                    y = -MOVEMENT;
                    break;
                case Direction.DOWN:
                    y = MOVEMENT;
                    break;
                case Direction.LEFT:
                    x = -MOVEMENT;
                    break;
                case Direction.RIGHT:
                    x = MOVEMENT;
                    break;
            }

            x = sprite.Location.X + x;
            y = sprite.Location.Y + y;

            // Boundary checks
            if (y <= TopBoundary) y = TopBoundary;
            if (y + sprite.Size.Height >= BottomBoundary) y = BottomBoundary - sprite.Size.Height;
            if (x <= LeftBoundary) x = LeftBoundary;
            if (x + sprite.Size.Width >= RightBoundary) x = RightBoundary - sprite.Size.Width; 

            sprite.Location = new Point(x, y);

            sprite.Refresh();
            return sprite.Location;
        }

        internal void RenderLevel(Level l1)
        {
            throw new NotImplementedException();
        }

        private void moveUp(object sender, EventArgs e)
        {
            MoveSprite(playerSprite, Direction.UP);
        }

        private void moveDown(object sender, EventArgs e)
        {
            MoveSprite(playerSprite, Direction.DOWN);
        }

        private void moveLeft(object sender, EventArgs e)
        {
            MoveSprite(playerSprite, Direction.LEFT);
        }

        private void moveRight(object sender, EventArgs e)
        {
            MoveSprite(playerSprite, Direction.RIGHT);
        }

        //private void Display_KeyDown(object sender, KeyEventArgs e)
        //{
        //    MessageBox.Show("Pressed: {}", e.KeyCode.ToString());
        //}

        private void Display_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    moveDown(sender, e);
                    break;
                case Keys.Right:
                    moveRight(sender, e);
                    break;
                case Keys.Up:
                    moveUp(sender, e);
                    break;
                case Keys.Left:
                    moveLeft(sender, e);
                    break;
            }
        }
    }
}
