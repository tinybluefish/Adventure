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
            return new Point(600, 200);
        }
    }
}