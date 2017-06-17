using System.Drawing;
using System.Windows.Forms;

namespace Adventure
{
    internal abstract class Element
    {
        public readonly string Name;
        public readonly PictureBox Sprite;

        public Element(string name, PictureBox sprite)
        {
            this.Name = name;
            this.Sprite = sprite;
        }

        public abstract Point MoveToStartPosition();

        public bool Hide()
        {
            return this.Sprite.Visible = false;
        }

        public bool Show()
        {
            return this.Sprite.Visible = true;
        }
    }
}
