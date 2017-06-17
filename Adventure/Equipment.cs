using System.Drawing;
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

    abstract class Equipment : Element
    {
        public readonly EquipmentType Type;

        public Equipment(string name, EquipmentType type, PictureBox sprite) :
            base(name, sprite)
        {
            this.Type = type;
        }

        public override Point MoveToStartPosition()
        {
            return new Point(400, 200);
        }
    }
}