using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    class Weapon : Equipment
    {
        public readonly int Damage;

        public Weapon(string name, EquipmentType type, string imageFile, int damage) :
            base(name, type, imageFile)
        {
            this.Damage = damage;
        }
    }

}
