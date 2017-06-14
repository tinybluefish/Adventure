using System.Windows.Forms;

namespace Adventure
{
    public enum EntityType
    {
        BAT,
        GHOST,
        GHOUL,
        HUMAN,
    }

    internal class Entity
    {
        public readonly string Name;
        public readonly EntityType Type;
        public int Health { get; }
        public readonly int Damage;
        public readonly PictureBox Sprite;

        public Entity(string name, EntityType type, int health, int damage, PictureBox sprite)
        {
            this.Name = name;
            this.Type = type;
            this.Health = health;
            this.Damage = damage;
            this.Sprite = sprite;
        }
    }
}