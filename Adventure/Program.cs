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

            // Create a level
            Level l1 = new Level(1, "The Entrance");

            // Add monster, weapon, player.
            l1.AddMonster(new Monster("Bernie", EntityType.BAT, 2, 1, display.batSprite));
            l1.AddGear(new Weapon("Mum's Breadknife", EquipmentType.SWORD, display.swordSprite, 1));

            // TODO: ask for player's name
            // TODO: update their description tag as they progress, separte from name
            l1.EnterPlayer(new Adventure.Player("Bob the Weakling"));

            display.RenderLevel(l1);

            // Fire it up...
            Application.Run(new Display());
        }
    }
}
