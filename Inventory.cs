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
            if (Items.Count >= maxItems)
            {
                PrintColored(ConsoleColor.Red, $"Cannot add {itemToAdd.name}: Item capacity full ({maxItems}/{maxItems}).");
                break;
            }
            Items.Add(itemToAdd);
        }
    }

    public void AddItemToInventory(Potion potionToAdd, int amountToAdd)
    {
        for (int i = 0; i < amountToAdd; i++)
        {
            if (Potions.Count >= maxPotions)
            {
                PrintColored(ConsoleColor.Red, $"Cannot add {potionToAdd.name}: Potion capacity full ({maxPotions}/{maxPotions}).");
                break;
            }
            Potions.Add(potionToAdd);
        }
    }

    public void ShowInventory()
    {
        int remainingItems = maxItems - Items.Count;
        int remainingPotions = maxPotions - Potions.Count;

        if (Items.Count == 0 && Potions.Count == 0)
        {
            PrintColored(ConsoleColor.DarkYellow, "Your inventory is completely empty!");
            PrintColored(ConsoleColor.Gray, $"You can carry up to {remainingItems} more item(s) and {remainingPotions} more potion(s).");
            return;
        }

        string separator = new string('-', 85);
        PrintLine(ConsoleColor.DarkGray, separator);
        PrintLine(ConsoleColor.Yellow, $"| {"Category",-10} | {"Name",-18} | {"Qty",-5} | {"Description",-40} |");
        PrintLine(ConsoleColor.DarkGray, separator);

        foreach (var group in Items.GroupBy(i => i.name))
        {
            var sample = group.First();
            PrintLine(ConsoleColor.Cyan, $"| {"Item",-10} | {sample.name,-18} | x{group.Count(),-5} | {Truncate(sample.description, 40),-40} |");
        }

        foreach (var group in Potions.GroupBy(p => p.name))
        {
            var sample = group.First();
            PrintLine(ConsoleColor.Magenta, $"| {"Potion",-10} | {sample.name,-18} | x{group.Count(),-5} | {Truncate(sample.description, 40),-40} |");
        }

        PrintLine(ConsoleColor.DarkGray, separator);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Inventory Space Left: ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"You can still carry {FormatCount(remainingItems, "item", "items")} and {FormatCount(remainingPotions, "potion", "potions")}.\n");
        Console.ResetColor();
    }

    private void PrintLine(ConsoleColor color, string text)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
    }

    private void PrintColored(ConsoleColor color, string text)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    private string FormatCount(int count, string singular, string plural)
        => $"{count} {(count == 1 ? singular : plural)}";

    private string Truncate(string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text)) return "";
        return text.Length <= maxLength ? text : text.Substring(0, maxLength - 3) + "...";
    }
}