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
        private const int MAX_INVENTORY_SIZE = 5;

        // Only Player's can have an Inventory
        public List<Equipment> Inventory { get; internal set; }

        private List<PictureBox> InventoryDisplay;

        public Player(string name, PictureBox sprite, List<PictureBox> inventoryDisplay) :
            base(name, EntityType.Player, 10, 1, sprite)
        {
            Inventory = new List<Equipment>(MAX_INVENTORY_SIZE);
            this.InventoryDisplay = inventoryDisplay;
        }

        internal InteractionResult TakeEquipment(Equipment newEquipment)
        {
            if (Inventory.Count < MAX_INVENTORY_SIZE)
            {
                Inventory.Add(newEquipment);
                foreach (PictureBox p in InventoryDisplay)
                {
                    if (p.Image == null)
                    {
                        newEquipment.Sprite.Visible = false;
                        MyLevel.Gear.Remove(newEquipment);
                        p.Image = newEquipment.Sprite.Image;
                        p.Visible = true;
                        p.Invalidate();
                        break;
                    }
                }
                return InteractionResult.TAKE;
            }
            else
            {                
                return InteractionResult.MOVE;
            }
        }

        internal void DropEquipment(Equipment oldEquipment)
        {
            Inventory.Remove(oldEquipment);
        }

        public override Point MoveToStartPosition()
        {
            Point p = new Point(200, 200);
            this.Sprite.Location = p;
            return p;
        }
    }
}
