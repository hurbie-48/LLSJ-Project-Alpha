namespace LLSJ_Project_Alpha.Entities;

public class Weapon
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Damage { get; set; }

    public Weapon(int id, string name, int damage)
    {
        ID = id;
        Name = name;
        Damage = damage;
    }
}