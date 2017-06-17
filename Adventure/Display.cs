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
        internal readonly int TopBoundary = 90;
        internal readonly int BottomBoundary = 400;
        internal readonly int LeftBoundary = 120;
        internal readonly int RightBoundary = 950;

        private Level CurrentLevel;
        
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

        internal void StartLevel(Level newLevel)
        {
            // Fade to black
            // TODO: how?
            
            // Make all all level's sprites invisible invisible
            if (CurrentLevel != null)
                CurrentLevel.HideElements();
            
            // Switch to hold new level reference
            CurrentLevel = newLevel;
            
            // Set positions of new level's sprites
            // TODO: done when monsters added....?
            
            // Make sprites visible
            CurrentLevel.ShowElements();
            
            // Fade in
            // TODO: how?
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
                    this.PlayerPositionXYLabel.Text = "(" + playerSprite.Location.X + "," + playerSprite.Location.Y + ")";
                    break;
                case Keys.Right:
                    moveRight(sender, e);
                    this.PlayerPositionXYLabel.Text = "(" + playerSprite.Location.X + "," + playerSprite.Location.Y + ")";
                    break;
                case Keys.Up:
                    moveUp(sender, e);
                    this.PlayerPositionXYLabel.Text = "(" + playerSprite.Location.X + "," + playerSprite.Location.Y + ")";
                    break;
                case Keys.Left:
                    moveLeft(sender, e);
                    this.PlayerPositionXYLabel.Text = "(" + playerSprite.Location.X + "," + playerSprite.Location.Y + ")";
                    break;
            }
        }

        private void Display_Load(object sender, EventArgs e)
        {

        }
    }
}
