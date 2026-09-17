namespace LLSJ_Project_Alpha.Entities
{
    public class Monster
    {
        public int id;
        public string Name { get; set; } = "Monster";
        public int BaseDamage { get; set; }
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public int Level { get; set; } = 1;

        // Tijdelijke aliassen voor code die nog de oorspronkelijke veldnamen gebruikt.
        public string name { get => Name; set => Name = value; }
        public int maximumDamage { get => BaseDamage; set => BaseDamage = value; }
        public int currentHitPoints { get => CurrentHP; set => CurrentHP = value; }
        public int maximumHitPoints { get => MaxHP; set => MaxHP = value; }
    }
}
