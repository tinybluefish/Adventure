using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure
{
    class Player : Entity
    {
        // Only Player's can have an Inventory
        public List<Equipment> Inventory { get; internal set; }

        public Player(string name) :
            // TODO: hard code in the image file here...
            base(name, EntityType.PLAYER, 100, 1, new PictureBox())
        {
            Inventory = new List<Equipment>();
        }

        internal void TakeEquipment(Equipment newEquipment)
        {
            Inventory.Add(newEquipment);
        }

        internal void DropEquipment(Equipment oldEquipment)
        {
            Inventory.Remove(oldEquipment);
        }

        public override Point MoveToStartPosition()
        {
            return new Point(200, 200);
        }
    }
}
