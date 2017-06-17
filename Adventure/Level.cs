using System.Collections.Generic;

namespace Adventure
{
    internal class Level
    {
        public readonly int Depth;
        public readonly string Name;

        internal List<Monster> Monsters = new List<Monster>();
        internal List<Equipment> Gear = new List<Equipment>();

        public Level(int depth, string name)
        {
            this.Depth = depth;
            this.Name = name;
        }

        public void AddMonster(Monster m)
        {
            Monsters.Add(m);
            // TODO: randomly position moster in level - avoiding other objects
        }

        public void AddGear(Equipment e)
        {
            Gear.Add(e);
            // TODO: randomly position gear - avoiding other objects
        }

        /**
         * This should trigger positioning of monsters, reset etc.
         */
        public void EnterPlayer(Player p)
        {

        }       
    }
}