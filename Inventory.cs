namespace LLSJ_Project_Alpha;

public class Inventory
{
    public int maxItems = 5;
    public int maxPotions = 3;
    public List<Item> Items = [];
    public List<Potion> Potions = [];

    public void AddItemToInventory(Item itemToAdd, int amountToAdd)
    {
        for (int i = 0; i < amountToAdd; i++)
        {
            if (Items.Count < maxItems)
            {
                Items.Add(itemToAdd);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Cannot add {itemToAdd.name}: Item capacity full ({maxItems}/{maxItems}).");
                Console.ResetColor();
                break;
            }
        }
    }
    
    public void AddItemToInventory(Potion potionToAdd, int amountToAdd)
    {
        for (int i = 0; i < amountToAdd; i++)
        {
            if (Potions.Count < maxPotions)
            {
                Potions.Add(potionToAdd);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Cannot add {potionToAdd.name}: Potion capacity full ({maxPotions}/{maxPotions}).");
                Console.ResetColor();
                break;
            }
        }
    }
    string FormatCount(int count, string singular, string plural)
    {
        return $"{count} {(count == 1 ? singular : plural)}";
    }
    public void ShowInventory()
    {
        int remainingItems = maxItems - Items.Count;
        int remainingPotions = maxPotions - Potions.Count;

        if (Items.Count == 0 && Potions.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Your inventory is completely empty!");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"You can carry up to {remainingItems} more item(s) and {remainingPotions} more potion(s).");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(new string('-', 85));
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"| {"Category",-10} | {"Name",-18} | {"Qty",-5} | {"Description",-40} |");
        
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(new string('-', 85));

        if (Items.Count > 0)
        {
            var itemGroups = Items.GroupBy(i => i.name);
            foreach (var group in itemGroups)
            {
                var sample = group.First();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"| {"Item",-10} | {sample.name,-18} | x{group.Count(),-5} | {Truncate(sample.description, 40),-40} |");
            }
        }

        if (Potions.Count > 0)
        {
            var potionGroups = Potions.GroupBy(p => p.name);
            foreach (var group in potionGroups)
            {
                var sample = group.First();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"| {"Potion",-10} | {sample.name,-18} | x{group.Count(),-5} | {Truncate(sample.description, 40),-40} |");
            }
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(new string('-', 85));

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Inventory Space Left: ");
        Console.ForegroundColor = ConsoleColor.White;
        string itemText = FormatCount(remainingItems, "item", "items");
        string potionText = FormatCount(remainingPotions, "potion", "potions");

        Console.WriteLine($"You can still carry {itemText} and {potionText}.\n");        Console.ResetColor();
    }

    private string Truncate(string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text)) return "";
        return text.Length <= maxLength ? text : text.Substring(0, maxLength - 3) + "...";
    }
}