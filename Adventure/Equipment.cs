namespace Adventure
{
    enum EquipmentType
    {
        SWORD,
        SHIELD,
        BOW,
        MACE,
        RED_POTION,
        BLUE_POTION,
        QUIVER,
    }

    abstract class Equipment
    {
        public readonly string Name;
        public readonly EquipmentType Type;
        public readonly string ImageFileName;

        public Equipment(string name, EquipmentType type, string imageFile)
        {
            this.Name = name;
            this.Type = type;
            this.ImageFileName = imageFile;
        }

    }

}