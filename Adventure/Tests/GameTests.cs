using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Adventure;
using System.Windows.Forms;
using System.Drawing;

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
            Player p = new Player(playerName);
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
            Player p = new Adventure.Player("Ken");
            Weapon swordOfDoom = new Weapon("Sword Of Dooooom!", EquipmentType.SWORD, new PictureBox(), 100);
            p.TakeEquipment(swordOfDoom);
            Assert.AreEqual(1, p.Inventory.Count);
            Assert.AreEqual(EquipmentType.SWORD, p.Inventory[0].Type);
        }

        [TestMethod]
        public void TestPlayerDropsWeapon()
        {
            Player p = new Adventure.Player("Barbie");
            Weapon shieldOfVirtue = new Weapon("Shield Of Virtue", EquipmentType.SHIELD, new PictureBox(), -1);
            p.TakeEquipment(shieldOfVirtue);
            Assert.AreEqual(1, p.Inventory.Count);
            p.DropEquipment(shieldOfVirtue);
            Assert.AreEqual(0, p.Inventory.Count);
        }

        [TestMethod]
        public void TestCreateMonster()
        {
            Monster m = new Monster("Bernard", EntityType.BAT, 4, 1, new PictureBox());
            Assert.AreEqual("Bernard", m.Name);
            Assert.AreEqual(EntityType.BAT, m.Type);
            Assert.AreEqual(4, m.Health);
            Assert.AreEqual(1, m.Damage);
        }

        [TestMethod]
        public void TestMoveSprite()
        {
            // Move left, right, right, down, down, up -> net should be +10, -10
            // NB: no boundary checks

            PictureBox sprite = new PictureBox();
            Display display = new Adventure.Display();
            int startX = display.LeftBoundary + 50;
            int startY = display.TopBoundary + 50;
            sprite.Location = new System.Drawing.Point(startX, startY);
            
            display.MoveSprite(sprite, Direction.LEFT);
            display.MoveSprite(sprite, Direction.RIGHT);
            display.MoveSprite(sprite, Direction.RIGHT);
            display.MoveSprite(sprite, Direction.UP);
            display.MoveSprite(sprite, Direction.UP);
            display.MoveSprite(sprite, Direction.DOWN);

            Assert.AreEqual(startX + 10, sprite.Location.X);
            Assert.AreEqual(startY - 10, sprite.Location.Y);

        }

        /**
         * Check that we can't move the sprite beyond the display boundaries.
         */
        [TestMethod]
        public void TestMoveSpriteBoundaries()
        {
            PictureBox sprite = new PictureBox();
            Display display = new Adventure.Display();
            sprite.Location = new Point(display.LeftBoundary, display.TopBoundary);
            display.MoveSprite(sprite, Direction.LEFT);
            display.MoveSprite(sprite, Direction.UP);
            Assert.AreEqual(display.LeftBoundary, sprite.Location.X);
            Assert.AreEqual(display.TopBoundary, sprite.Location.Y);
            sprite.Location = new Point(display.RightBoundary, display.BottomBoundary);
            display.MoveSprite(sprite, Direction.RIGHT);
            display.MoveSprite(sprite, Direction.DOWN);
            Assert.AreEqual(display.RightBoundary, sprite.Location.X + sprite.Size.Width);
            Assert.AreEqual(display.BottomBoundary, sprite.Location.Y + sprite.Size.Height);
        }

        [TestMethod]
        public void TestCreateAndPopulateLevel()
        {
            Level l = new Level(1, "The Entrance");

            Monster m = new Monster("Bernie", EntityType.BAT, 2, 1, new PictureBox());
            Weapon w = new Weapon("Mum's Kitchen Knife", EquipmentType.SWORD, new PictureBox(), 1);
            Player p = new Player("Bob");

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
            Monster m = new Monster("Bernie", EntityType.BAT, 2, 1, new PictureBox());
            Weapon w = new Weapon("Mum's Kitchen Knife", EquipmentType.SWORD, new PictureBox(), 1);
            Player p = new Player("Bob");
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
    }

}
