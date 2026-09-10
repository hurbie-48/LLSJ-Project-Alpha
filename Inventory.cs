namespace LLSJ_Project_Alpha;

public class Inventory()
{
    public int maxItems = 5;
    public int maxPotions = 3;
    public List<Item> Items = [];

    public void AddItemToInventory(Item itemToAdd, int amountToAdd)
    {
        for (int i = 0; i < amountToAdd; i++)
        {
            if (Items.Count < 5)
            {
                Items.Add(itemToAdd);
            }
        }
        
        
    }

    public void ShowInventory()
    {
        Console.WriteLine($"You have the following items:");
        foreach (var item in Items)
        {
            Console.WriteLine($"{item.name}\nDescription: {item.description}");
        }
    }
}