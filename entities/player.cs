namespace LLSJ_Project_Alpha.Entities
{
    public partial class Player
    {
        public string Name { get; set; } = "Player";
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public Inventory Inventory { get; } = new();
        public Weapon EquippedWeapon { get; set; } = new(0, "Vuisten", 5);

        // Tijdelijke aliassen voor code die nog de oorspronkelijke veldnamen gebruikt.
        public string name { get => Name; set => Name = value; }
        public int currentHitPoints { get => CurrentHP; set => CurrentHP = value; }
        public int maximumHitPoints { get => MaxHP; set => MaxHP = value; }
        // @TODO: create field for current location

        public Player()
        {
            CurrentHP = 100;
            MaxHP = 100;
        }
    }
}
