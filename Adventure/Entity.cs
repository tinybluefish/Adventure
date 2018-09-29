using System;
using System.Drawing;
using System.Windows.Forms;

namespace Adventure
{
    public enum EntityType
    {
        Bat,
        Ghost,
        Ghoul,
        Player,
    }

    public enum InteractionResult
    {
        HIT,
        MISS,
        KILL,
        DIE,
        LEGGIT,
        TAKE,
        MOVE
    }

    internal abstract class Entity : Element
    {
        public readonly EntityType Type;
        // TODO: blech - public health...lol
        public int Health;
        public int Damage;
        internal Level MyLevel;

        public Entity(string name, EntityType type, int health, int damage, PictureBox sprite) :
            base(name, sprite)
        {
            this.Type = type;
            this.Health = health;
            this.Damage = damage;
        }

        internal void EnterLevel(Level level)
        {
            MyLevel = level;
            // TODO: position yourself at base but then check for conficts
            this.MoveToStartPosition();
        }



        internal InteractionResult MoveSprite(Direction direction)
        {
            int x = 0;
            int y = 0;

            switch (direction)
            {
                case Direction.UP:
                    y = -Display.MOVEMENT;
                    break;
                case Direction.DOWN:
                    y = Display.MOVEMENT;
                    break;
                case Direction.LEFT:
                    x = -Display.MOVEMENT;
                    break;
                case Direction.RIGHT:
                    x = Display.MOVEMENT;
                    break;
            }

            x = Sprite.Location.X + x;
            y = Sprite.Location.Y + y;

            // Boundary checks
            if (y <= Display.TopBoundary) y = Display.TopBoundary;
            if (y + Sprite.Size.Height >= Display.BottomBoundary) y = Display.BottomBoundary - Sprite.Size.Height;
            if (x <= Display.LeftBoundary) x = Display.LeftBoundary;
            if (x + Sprite.Size.Width >= Display.RightBoundary) x = Display.RightBoundary - Sprite.Size.Width;


            // Box checks
            InteractionResult result = InteractionResult.MOVE;
            foreach (Element el in MyLevel.Elements())
            {
                // Obviously don't want to check for conflicts with yourself...
                if (el == this) continue;

                // Get the boundaries of the sprite
                int tSize = el.Sprite.Width; // NOTE: shortcut assuming boxes for everything
                int tTY = el.Sprite.Location.Y;
                int tBY = tTY + tSize;
                int tLX = el.Sprite.Location.X;
                int tRX = tLX + tSize;

                // Calculate the offsets
                int yOffset = y - tTY;
                int xOffset = x - tLX;

                // If negative, then target is lower, needs to be >= size lower to avoid conflict.
                // If the offset is greater than size of sprite then we have a conflict.
                if (System.Math.Abs(yOffset) < tSize && System.Math.Abs(xOffset) < tSize)
                {
                    result = this.Interact(el);
                }
            }

            // We actually only want to move our sprite if we didn't bump into anything, otherwise
            // leave it where it was.
            switch (result)
            {
                case InteractionResult.MOVE:
                case InteractionResult.KILL:
                case InteractionResult.TAKE:
                    Sprite.Location = new Point(x, y);
                    Sprite.Invalidate();
                    break;
                default:
                    break;
            }

            return result;
        }

        /**
         * Player -> Gear: pick up/ignore, move
         * Player -> Monster: attack, stop
         * Monster -> Player: attach, stop
         * Monster -> Gear: ignore, move
         */
        internal InteractionResult Interact(Element target)
        {
            InteractionResult result = InteractionResult.MOVE;

            if (this is Player)
            {
                if (target is Monster)
                {
                    Logger.Log(this.Name + " attacks " + target.Name + " the " + ((Entity)target).Type);
                    result = this.Attack((Entity)target);
                }
                else if (target is Equipment)
                {
                    result = ((Player)this).TakeEquipment((Equipment)target);
                    if (result == InteractionResult.TAKE)
                    {
                        target.Sprite.Visible = false;
                        target.Sprite.Invalidate();
                    }
                }
            }
            else if (this is Monster)
            {
                if (target is Player)
                {
                    Logger.Log(this.Name + " the " + ((Entity)this).Type + " attacks " + target.Name);
                    result = this.Attack((Entity)target);
                }
                else if (target is Equipment)
                {
                    Logger.Log(this.Name + " takes " + target.Name + ", a " + ((Equipment)target).Type);
                    result = InteractionResult.MOVE;
                }
            }
            else
            {
                throw new NotSupportedException("Invalid Entity type: " + this);
            }

            return result;
        }

        internal InteractionResult Attack(Entity entity)
        {
            InteractionResult result;

            // Toss a coin (roll a die...) to determine if we hit/miss.
            Random r = new Random();
            if (r.NextDouble() > 0.5)
            {
                int damage = this.GetDamage();
                Logger.Log(entity.Name + " is hit for " + damage + " damage...");
                if (entity.TakeDamage(damage) == InteractionResult.DIE)
                {
                    Logger.Log("...and dies!!!");
                    result = InteractionResult.KILL;
                }
                else
                    result = InteractionResult.HIT;
            }
            else
            {
                Logger.Log(this.Name + " misses!");
                result = InteractionResult.MISS;
            }
            return result;
        }

        private InteractionResult TakeDamage(int damage)
        {
            if (damage >= Health)
            {
                this.Sprite.Visible = false;
                this.Sprite.Invalidate();
                this.Health = 0;
                if (this is Player)
                {

                }
                else
                {
                    this.MyLevel.Monsters.Remove((Monster)this);
                }
                return InteractionResult.DIE;
            }
            else
            {
                this.Health -= damage;
                return InteractionResult.HIT;
            }
        }

        private int GetDamage()
        {
            // TODO: factor in weapon when it's a player and they have one...
            return this.Damage;
        }
    }
}