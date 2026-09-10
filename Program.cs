using System.Globalization;

namespace LLSJ_Project_Alpha
{
    public class Program
    {
        public static void Main()
        {
            Item apple = new Item("Apple",
                "An apple... That's it really.");
            Inventory inventory = new Inventory();
            Item stoneSword = new Item("Stone sword","This stone sword isn't very good...");
            Potion healingPotion = new Potion("Healing potion", "This potion will heal 4 points", 4);
            Potion poisonPotion = new Potion("Poison potion", "This potion will remove 2 points", -2);
            inventory.AddItemToInventory(stoneSword, 1);
            inventory.AddItemToInventory(apple, 3);
            inventory.AddItemToInventory(healingPotion, 3);
           
            inventory.ShowInventory();
        }
    }
}