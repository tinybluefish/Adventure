using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Adventure;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace Adventure
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void TestCreateGame()
        {
            String playerName = "Bob";
            Game g = new Game(playerName);
            Assert.AreEqual(playerName, g.PlayerName);
        }

        [TestMethod]
        public void TestCreatePlayer()
        {
            String playerName = "Bill";
            Player p = new Player(playerName, null, null);
            Assert.AreEqual(playerName, p.Name);
        }

        [TestMethod]
        public void TestCreateWeapon()
        {
            Weapon w = new Weapon("mysword", EquipmentType.SWORD, new PictureBox(), 10);
            Assert.AreEqual("mysword", w.Name);
            Assert.AreEqual(EquipmentType.SWORD, w.Type);
            Assert.AreEqual(10, w.Damage);
        }

        [TestMethod]
        public void TestPlayerGetsWeapon()
        {
            Player p = new Adventure.Player("Ken", new PictureBox(), new List<PictureBox>(1));
            Weapon swordOfDoom = new Weapon("Sword Of Dooooom!", EquipmentType.SWORD, new PictureBox(), 100);
            p.TakeEquipment(swordOfDoom);
            Assert.AreEqual(1, p.Inventory.Count);
            Assert.AreEqual(EquipmentType.SWORD, p.Inventory[0].Type);
        }

        [TestMethod]
        public void TestPlayerDropsWeapon()
        {
            Player p = new Adventure.Player("Barbie", new PictureBox(), new List<PictureBox>(1));
            Weapon shieldOfVirtue = new Weapon("Shield Of Virtue", EquipmentType.SHIELD, new PictureBox(), -1);
            p.TakeEquipment(shieldOfVirtue);
            Assert.AreEqual(1, p.Inventory.Count);
            p.DropEquipment(shieldOfVirtue);
            Assert.AreEqual(0, p.Inventory.Count);
        }

        [TestMethod]
        public void TestCreateMonster()
        {
            Monster m = new Monster("Bernard", EntityType.Bat, 4, 1, new PictureBox());
            Assert.AreEqual("Bernard", m.Name);
            Assert.AreEqual(EntityType.Bat, m.Type);
            Assert.AreEqual(4, m.Health);
            Assert.AreEqual(1, m.Damage);
        }

        [TestMethod]
        public void TestMovePlayer()
        {
            // Move left, right, right, down, down, up -> net should be +10, -10
            Display display = new Adventure.Display();
            Level l = new Level(1, "The Entrance");
            int startX = Display.LeftBoundary + 50;
            int startY = Display.TopBoundary + 50;
            Player p = new Player("Nigel", new PictureBox(), new List<PictureBox>(1));
            l.EnterPlayer(p);
            p.Sprite.Location = new System.Drawing.Point(startX, startY);

            p.MoveSprite(Direction.LEFT);
            p.MoveSprite(Direction.RIGHT);
            p.MoveSprite(Direction.RIGHT);
            p.MoveSprite(Direction.UP);
            p.MoveSprite(Direction.UP);
            p.MoveSprite(Direction.DOWN);

            Assert.AreEqual(startX + 10, p.Sprite.Location.X);
            Assert.AreEqual(startY - 10, p.Sprite.Location.Y);

        }

        /**
         * Check that we can't move the sprite beyond the display boundaries.
         */
        [TestMethod]
        public void TestMoveSpriteBoundaries()
        {
            Player p = new Player("Nigel", new PictureBox(), new List<PictureBox>(1));
            Level l = new Level(1, "The Entrance");
            l.EnterPlayer(p);
            PictureBox sprite = p.Sprite;
            Display display = new Adventure.Display();
            sprite.Location = new Point(Display.LeftBoundary, Display.TopBoundary);
            p.MoveSprite(Direction.LEFT);
            p.MoveSprite(Direction.UP);
            Assert.AreEqual(Display.LeftBoundary, sprite.Location.X);
            Assert.AreEqual(Display.TopBoundary, sprite.Location.Y);
            sprite.Location = new Point(Display.RightBoundary, Display.BottomBoundary);
            p.MoveSprite(Direction.RIGHT);
            p.MoveSprite(Direction.DOWN);
            Assert.AreEqual(Display.RightBoundary, sprite.Location.X + sprite.Size.Width);
            Assert.AreEqual(Display.BottomBoundary, sprite.Location.Y + sprite.Size.Height);
        }

        [TestMethod]
        public void TestCreateAndPopulateLevel()
        {
            Level l = new Level(1, "The Entrance");

            Monster m = new Monster("Bernie", EntityType.Bat, 2, 1, new PictureBox());
            Weapon w = new Weapon("Mum's Kitchen Knife", EquipmentType.SWORD, new PictureBox(), 1);
            Player p = new Player("Bob", new PictureBox(), new List<PictureBox>(1));

            l.AddMonster(m);
            l.AddGear(w);
            l.EnterPlayer(p);

            Assert.AreSame(m, l.Monsters[0]);
            Assert.AreSame(w, l.Gear[0]);
            Assert.IsNotNull(l.Player);

            Assert.IsTrue(m.Sprite.Visible);
            Assert.IsTrue(p.Sprite.Visible);
            Assert.IsTrue(w.Sprite.Visible);
        }

        [TestMethod]
        public void TestShowAndHideSprites()
        {
            Level l = new Adventure.Level(2, "The Cavern");
            Monster m = new Monster("Bernie", EntityType.Bat, 2, 1, new PictureBox());
            Weapon w = new Weapon("Mum's Kitchen Knife", EquipmentType.SWORD, new PictureBox(), 1);
            Player p = new Player("Bob", new PictureBox(), new List<PictureBox>(1));
            m.Sprite.Visible = true;
            p.Sprite.Visible = true;
            w.Sprite.Visible = true;
            l.AddMonster(m);
            l.AddGear(w);
            l.EnterPlayer(p);

            l.HideElements();

            Assert.IsFalse(m.Sprite.Visible);
            Assert.IsFalse(p.Sprite.Visible);
            Assert.IsFalse(w.Sprite.Visible);

            l.ShowElements();

            Assert.IsTrue(m.Sprite.Visible);
            Assert.IsTrue(p.Sprite.Visible);
            Assert.IsTrue(w.Sprite.Visible);
        }

        [TestMethod]
        public void TestInteractions()
        {
            Level l = new Adventure.Level(2, "The Cavern");
            Monster m = new Monster("Bernie", EntityType.Bat, 2, 1, new PictureBox());
            Player p = new Player("Bob", new PictureBox(), new List<PictureBox>(1));
            l.AddMonster(m);
            l.EnterPlayer(p);
            l.ShowElements();

            int startX = 300, startY = 300;

            // Place them next to each other
            p.Sprite.Location = new Point(startX, startY);
            // Position the monster just to the right of the player with a gap of MOVEMENT
            m.Sprite.Location = new Point(startX + p.Sprite.Width + Display.MOVEMENT, 300);
            int pw = p.Sprite.Width;            

            // Test 1: Move the monster towards the player and ensure it stops when it makes contact
            // First move gets them adjacent
            m.MoveSprite(Direction.LEFT);
            Assert.AreEqual(startX + pw, m.Sprite.Location.X); // Just to be sure
            // assert no interaction...
             
            m.MoveSprite(Direction.LEFT);
            Assert.AreEqual(startX + pw, m.Sprite.Location.X); // Should still be same
            // SHould also return attack/hit/whatever

            // Test 2: reposition monster above player, move down.
            
        }
    }

}
