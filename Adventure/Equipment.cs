using System.Windows.Forms;

namespace Adventure
{
    enum EquipmentType
    {
        SWORD,
        SHIELD,
        BOW,
        MACE,
        RED_POTION,
        BLUE_POTION,
        QUIVER,
    }

    abstract class Equipment
    {
        public readonly string Name;
        public readonly EquipmentType Type;
        public readonly PictureBox Sprite;

        public Equipment(string name, EquipmentType type, PictureBox sprite)
        {
            this.Name = name;
            this.Type = type;
            this.Sprite = sprite;
        }

    }

}