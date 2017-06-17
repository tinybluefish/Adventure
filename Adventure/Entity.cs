using System.Drawing;
using System.Windows.Forms;

namespace Adventure
{
    public enum EntityType
    {
        BAT,
        GHOST,
        GHOUL,
        PLAYER,
    }

    internal abstract class Entity : Element
    {
        public readonly EntityType Type;
        public int Health { get; }
        public readonly int Damage;

        public Entity(string name, EntityType type, int health, int damage, PictureBox sprite) :
            base(name, sprite)
        {
            this.Type = type;
            this.Health = health;
            this.Damage = damage;
        }
    }
}