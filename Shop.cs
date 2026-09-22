using LLSJ_Project_Alpha.Entities;

namespace LLSJ_Project_Alpha;

// A single thing the shop sells, and what it becomes in the player's
// inventory (an Item or a Potion) if bought.
public class ShopListing
{
    public int ID;
    public string Name;
    public string Description;
    public int Price;
    public bool IsPotion;
    public int HealingAmount; // only used when IsPotion is true

    public ShopListing(int id, string name, string description, int price, bool isPotion = false, int healingAmount = 0)
    {
        ID = id;
        Name = name;
        Description = description;
        Price = price;
        IsPotion = isPotion;
        HealingAmount = healingAmount;
    }
}

public static class Shop
{
    public static readonly List<ShopListing> Listings = new()
    {
        new ShopListing(4, "Bread", "A hearty loaf, takes the edge off hunger.", 5),
        new ShopListing(5, "Torch", "Lights up the darkest of corners.", 8),
        new ShopListing(6, "Minor Healing Potion", "Restores a small amount of health.", 15, isPotion: true, healingAmount: 10),
        new ShopListing(7, "Greater Healing Potion", "Restores a large amount of health.", 30, isPotion: true, healingAmount: 25),
    };

    // Runs the interactive shop menu until the player leaves.
    public static void Enter(Player player)
    {
        bool shopping = true;

        while (shopping)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("=== TOWN SQUARE SHOP ===");
            Console.ResetColor();
            Console.WriteLine($"Your gold: {player.Gold}");
            Console.WriteLine();
            Console.WriteLine("For sale:");

            for (int i = 0; i < Listings.Count; i++)
            {
                ShopListing listing = Listings[i];
                Console.WriteLine($"  {i + 1}. {listing.Name} - {listing.Price}g : {listing.Description}");
            }

            Console.WriteLine();
            Console.WriteLine("Commands: 'buy <number>', 'sell <item name>', 'leave'.");
            Console.Write("> ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input) || input.Trim().ToLower() is "leave" or "exit" or "quit")
            {
                shopping = false;
                continue;
            }

            string[] parts = input.Trim().Split(' ', 2);
            string command = parts[0].ToLower();

            if (command == "buy" && parts.Length > 1 && int.TryParse(parts[1], out int listingNumber))
            {
                BuyListing(player, listingNumber - 1);
            }
            else if (command == "sell" && parts.Length > 1)
            {
                SellByName(player, parts[1].Trim());
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("I didn't understand that. Try 'buy 1', 'sell Bread', or 'leave'.");
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("You leave the shop.");
        Console.ResetColor();
    }

    private static void BuyListing(Player player, int listingIndex)
    {
        if (listingIndex < 0 || listingIndex >= Listings.Count)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("That's not a valid item number.");
            Console.ResetColor();
            return;
        }

        ShopListing listing = Listings[listingIndex];

        if (player.Gold < listing.Price)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"You don't have enough gold for {listing.Name} ({listing.Price}gold). You have {player.Gold}gold.");
            Console.ResetColor();
            return;
        }

        if (listing.IsPotion)
        {
            if (!player.Inventory.HasPotionCapacity())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Your potion pouch is full. Sell or use a potion first.");
                Console.ResetColor();
                return;
            }

            player.Gold -= listing.Price;
            player.Inventory.AddItemToInventory(new Potion(listing.Name, listing.Description, listing.HealingAmount), 1);
        }
        else
        {
            if (!player.Inventory.HasItemCapacity())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Your item bag is full. Sell or drop an item first.");
                Console.ResetColor();
                return;
            }

            player.Gold -= listing.Price;
            player.Inventory.AddItemToInventory(new Item(listing.ID, listing.Name, listing.Description), 1);
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"You bought {listing.Name} for {listing.Price}gold.");
        Console.ResetColor();
    }

    private static void SellByName(Player player, string name)
    {
        Item? matchingItem = player.Inventory.Items
            .FirstOrDefault(i => i.name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (matchingItem != null)
        {
            int sellPrice = GetSellPrice(matchingItem.name);
            player.Inventory.RemoveItemFromInventory(matchingItem);
            player.Gold += sellPrice;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"You sold {matchingItem.name} for {sellPrice}gold.");
            Console.ResetColor();
            return;
        }

        Potion? matchingPotion = player.Inventory.Potions
            .FirstOrDefault(p => p.name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (matchingPotion != null)
        {
            int sellPrice = GetSellPrice(matchingPotion.name);
            player.Inventory.RemoveItemFromInventory(matchingPotion);
            player.Gold += sellPrice;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"You sold {matchingPotion.name} for {sellPrice}gold.");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"You don't have a '{name}' to sell.");
        Console.ResetColor();
    }

    // Sell price is half the shop's buy price (minimum 1g). Anything not in
    // the shop's own catalog (e.g. a quest drop) still sells for a flat 1g.
    private static int GetSellPrice(string name)
    {
        ShopListing? listing = Listings.FirstOrDefault(l => l.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        return listing != null ? Math.Max(1, listing.Price / 2) : 1;
    }
}