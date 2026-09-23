namespace LLSJ_Project_Alpha;

using LLSJ_Project_Alpha.Entities;

public class Potion
{
    public string name;
    public string description;
    public int healingAmount;

    public Potion(string itemName, string itemDescription, int itemhealingAmount)
    {
        name = itemName;
        description = itemDescription;
        healingAmount = itemhealingAmount;
    }

    public void Heal(Player player)
    {
        player.CurrentHP += healingAmount;

        if (player.CurrentHP > player.MaxHP)
        {
            player.CurrentHP = player.MaxHP;
        }

        Console.WriteLine($"Je gebruikt {name} en krijgt {healingAmount} HP erbij!");
    }
}
