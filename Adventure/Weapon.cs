using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure
{
    class Weapon : Equipment
    {
        public readonly int Damage;

        public Weapon(string name, EquipmentType type, PictureBox sprite, int damage) :
            base(name, type, sprite)
        {
            this.Damage = damage;
        }
    }

}
