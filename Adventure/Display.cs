using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure
{
    public enum Direction {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public partial class Display : Form
    {
        internal static readonly int MOVEMENT = 10;
        //private readonly Point TopLeft = new Point(100, 200);
        internal Player ThePlayer;

        // Boundaries
        // TODO: seems like a crappy place to put this..? Also - variable?
        internal static readonly int TopBoundary = 90;
        internal static readonly int BottomBoundary = 400;
        internal static readonly int LeftBoundary = 120;
        internal static readonly int RightBoundary = 950;

        private Level CurrentLevel;

        internal readonly List<PictureBox> InventoryBoxes = new List<PictureBox>(5);

        internal List<Level> Levels { get; private set; }

        public Display()
        {
            InitializeComponent();

            // TODO: YUCK!
            Logger.LOG = this.logBox;

            // Need to package the inv display boxes together so we can hand to Player
            InventoryBoxes.Add(invBox1);
            InventoryBoxes.Add(invBox2);
            InventoryBoxes.Add(invBox3);
            InventoryBoxes.Add(invBox4);
            InventoryBoxes.Add(invBox5);
        }


        internal void StartLevel(Level newLevel)
        {
            Logger.Log("Entering level: " + newLevel.Name);
            Logger.Log("Good luck...");

            // TODO: this is a hack...
            ThePlayer = newLevel.Player;
            this.PlayerPositionTitleLabel.Text = ThePlayer.Name;

            // Fade to black
            // TODO: how?
            
            // Make all all level's sprites invisible invisible
            if (CurrentLevel != null)
                CurrentLevel.HideElements();
            
            // Switch to hold new level reference
            CurrentLevel = newLevel;

            // Set positions of new level's sprites
            // TODO: done when monsters added....?

            // Make sprites visible
            Logger.Log("...here come the monsters!");
            CurrentLevel.ShowElements();
            this.Focus();
            
            // Fade in
            // TODO: how?
        }

        internal void AddLevels(List<Level> levels)
        {
            this.Levels = levels;
        }

        private void Display_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    ThePlayer.MoveSprite(Direction.DOWN);
                    this.PlayerPositionXYLabel.Text = "(" + playerSprite.Location.X + "," + playerSprite.Location.Y + ")";
                    break;
                case Keys.Right:
                    ThePlayer.MoveSprite(Direction.RIGHT);
                    this.PlayerPositionXYLabel.Text = "(" + playerSprite.Location.X + "," + playerSprite.Location.Y + ")";
                    break;
                case Keys.Up:
                    ThePlayer.MoveSprite(Direction.UP);
                    this.PlayerPositionXYLabel.Text = "(" + playerSprite.Location.X + "," + playerSprite.Location.Y + ")";
                    break;
                case Keys.Left:
                    ThePlayer.MoveSprite(Direction.LEFT);
                    this.PlayerPositionXYLabel.Text = "(" + playerSprite.Location.X + "," + playerSprite.Location.Y + ")";
                    break;
            }
            ThePlayer.Sprite.Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CurrentLevel.MoveCreatures();
            PlayerHealthLabel.Text = ThePlayer.Health + " hitpoints";
            if (CurrentLevel.Monsters.Count == 0)
            {
                if (Levels.IndexOf(CurrentLevel) < Levels.Count - 1)
                {
                    
                }
                else
                {
                    this.MoveTimer.Stop();
                    this.endGameLabel.Visible = true;
                    this.playAgainButton.Visible = true;
                    this.quitButton.Visible = true;
                }
            }
            else if (ThePlayer.Health == 0)
            {
                this.MoveTimer.Stop();
                this.endGameLabel.Text = "You lost! :P";
                //this.endGameLabel.BackColor = Color;
                this.endGameLabel.ForeColor = Color.Red;
                this.endGameLabel.Visible = true;
                this.playAgainButton.Visible = true;
                this.quitButton.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void playAgainButton_Click(object sender, EventArgs e)
        {
            this.endGameLabel.Visible = false;
            Level l1 = new Level(1, "The Entrance");

            // Add monster, weapon, player.
            l1.AddMonster(new Monster("Bernie", EntityType.Bat, 5, 2, this.batSprite));
            l1.AddMonster(new Monster("Billy", EntityType.Bat, 5, 2, this.batSprite));
            l1.AddMonster(new Monster("Bessie", EntityType.Bat, 5, 2, this.batSprite));

            l1.AddMonster(new Monster("Spooky Gordon", EntityType.Ghost, 10, 3, this.ghostSprite));

            l1.AddMonster(new Monster("The Blob", EntityType.Ghoul, 20, 3, this.ghoulSprite));


            l1.AddGear(new Weapon("Mum's Breadknife", EquipmentType.SWORD, this.swordSprite, 1));
            l1.AddGear(new Weapon("Mace Of DOOOOOM!", EquipmentType.MACE, this.maceSprite, 1));
            l1.AddGear(new Weapon("Da Bomb", EquipmentType.SWORD, this.bombSprite, 1));

            // TODO: ask for player's name
            // TODO: update their description tag as they progress, separte from name
            Player p = new Adventure.Player("Bob the Weakling", this.playerSprite, this.InventoryBoxes);
            p.Sprite = this.playerSprite;
            l1.EnterPlayer(p);
            this.MoveTimer.Start();
            this.playAgainButton.Visible = false;
            this.quitButton.Visible = false;

            this.StartLevel(l1);

        }
    }
}
