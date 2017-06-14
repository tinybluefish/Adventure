using System.Windows.Forms;

namespace Adventure
{
    internal class Monster : Entity
    {
        public Monster(string name, EntityType type, int health, int damage, PictureBox sprite) :
            base(name, type, health, damage, sprite)
        {
        }
    }
}