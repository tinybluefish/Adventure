using System;
using System.Drawing;
using System.Windows.Forms;

namespace Adventure
{
    internal class Monster : Entity
    {
        public Monster(string name, EntityType type, int health, int damage, PictureBox sprite) :
            base(name, type, health, damage, sprite)
        {
        }

        public override Point MoveToStartPosition()
        {
            Random r = new Random();
            int x = (int)(r.NextDouble() * 1000) % Display.RightBoundary;
            int y = (int)(r.NextDouble() * 1000) % Display.BottomBoundary;

            Point p = new Point(x, y);
            this.Sprite.Location = p;
            return p;
        }


    }
}