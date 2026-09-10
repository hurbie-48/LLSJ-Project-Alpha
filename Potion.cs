namespace LLSJ_Project_Alpha;

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
}