using System;
using System.Collections.Generic;
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
            base(name, EntityType.HUMAN, 100, 1, new PictureBox())
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
    }
}
