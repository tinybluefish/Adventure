using System;
using System.Collections.Generic;

namespace Adventure
{
    internal class Level
    {
        public readonly int Depth;
        public readonly string Name;

        internal List<Monster> Monsters = new List<Monster>();
        internal List<Equipment> Gear = new List<Equipment>();

        internal Player Player;

        public Level(int depth, string name)
        {
            this.Depth = depth;
            this.Name = name;
        }

        public void AddMonster(Monster m)
        {
            Monsters.Add(m);
            // TODO: randomly position moster in level - avoiding other objects
            // TODO: actually, need diff start points for diff monsters too....
            m.Sprite.Location = m.MoveToStartPosition();
        }

        public void AddGear(Equipment e)
        {
            Gear.Add(e);
            // TODO: randomly position gear - avoiding other objects
            e.Sprite.Location = e.MoveToStartPosition();
        }

        /**
         * This should trigger positioning of monsters, reset etc.
         */
        public void EnterPlayer(Player p)
        {
            this.Player = p;

            // TODO: need to decide where we're going to position the different pieces, fixed locations to start with
            // and then we can introduce some randomness.

            // NOTE: player always starts near the door...

            p.Sprite.Location = p.MoveToStartPosition();
        }

        internal void HideElements()
        {
            this.Player.Hide();
            foreach (Monster m in this.Monsters)
            {
                m.Hide();
            }
            foreach (Equipment e in this.Gear)
            {
                e.Hide();
            }
        }

        internal void ShowElements()
        {
            this.Player.Show();
            foreach (Monster m in this.Monsters)
            {
                m.Show();
            }
            foreach (Equipment e in this.Gear)
            {
                e.Show();
            }
        }
    }
}