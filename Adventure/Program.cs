using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // TODO: "creating the beasts..." type startup text...?

            // Create the game display
            Display display = new Display();

            Level l1 = new Level(1, "The Entrance");
            l1.AddMonster(new Monster("Bernie", EntityType.Bat, 5, 1, display.batSprite));
            l1.AddMonster(new Monster("Bessie", EntityType.Bat, 5, 1, display.batSprite));
            l1.AddGear(new Weapon("Mum's Breadknife", EquipmentType.SWORD, display.swordSprite, 2));

            Level l2 = new Level(2, "The Graveyard");
            l1.AddMonster(new Monster("Gordon", EntityType.Ghost, 10, 2, display.ghostSprite));
            l1.AddGear(new Weapon("Mace of Mild Concern", EquipmentType.MACE, display.maceSprite, 3));

            Level l3 = new Level(3, "The Crypt");
            l1.AddMonster(new Monster("Jules", EntityType.Ghoul, 30, 3, display.ghoulSprite));
            l1.AddGear(new Weapon("Bow of Sturdiness", EquipmentType.BOW, display.bowSprite, 4));

            List<Level> levels = new List<Level>() { l1, l2, l3 };
            display.AddLevels(levels);

            // TODO: ask for player's name
            // TODO: update their description tag as they progress, separte from name
            Player p = new Adventure.Player("Terry the Timid", display.playerSprite, display.InventoryBoxes);
            p.Sprite = display.playerSprite;

            // Fire it up...
            Application.Run(display);
        }
    }
}
