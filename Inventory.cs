namespace LLSJ_Project_Alpha
{
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
                    Console.WriteLine($"Cannot add {itemToAdd.name}: Item capacity full ({maxItems}/{maxItems}).");
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
                    Console.WriteLine($"Cannot add {potionToAdd.name}: Potion capacity full ({maxPotions}/{maxPotions}).");
                    break;
                }
                Potions.Add(potionToAdd);
            }
        }

        public bool HasItemCapacity() => Items.Count < maxItems;

        public bool HasPotionCapacity() => Potions.Count < maxPotions;

        public bool RemoveItemFromInventory(Item itemToRemove)
        {
            return Items.Remove(itemToRemove);
        }

        public bool RemoveItemFromInventory(Potion potionToRemove)
        {
            return Potions.Remove(potionToRemove);
        }

        public void ShowInventory()
        {
            int remainingItems = maxItems - Items.Count;
            int remainingPotions = maxPotions - Potions.Count;

            Console.WriteLine("=== INVENTORY ===");

            if (Items.Count == 0 && Potions.Count == 0)
            {
                Console.WriteLine("Your inventory is completely empty!");
            }
            else
            {
                if (Items.Count > 0)
                {
                    Console.WriteLine("\nItems:");
                    foreach (var group in Items.GroupBy(i => i.name))
                    {
                        var sample = group.First();
                        Console.WriteLine($"- {sample.name} (x{group.Count()}): {sample.description}");
                    }
                }

                if (Potions.Count > 0)
                {
                    Console.WriteLine("\nPotions:");
                    foreach (var group in Potions.GroupBy(p => p.name))
                    {
                        var sample = group.First();
                        Console.WriteLine($"- {sample.name} (x{group.Count()}): {sample.description}");
                    }
                }
            }

            Console.WriteLine($"\nCapacity Remaining: {FormatCount(remainingItems, "item", "items")}, {FormatCount(remainingPotions, "potion", "potions")}.\n");
        }

        private string FormatCount(int count, string singular, string plural)
            => $"{count} {(count == 1 ? singular : plural)}";
    }
}

namespace LLSJ_Project_Alpha.Entities
{
    public partial class Player
    {
        public int Gold { get; set; } = 20;
        public LLSJ_Project_Alpha.Inventory Inventory { get; set; } = new LLSJ_Project_Alpha.Inventory();
    }
}