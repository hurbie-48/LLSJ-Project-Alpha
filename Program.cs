namespace LLSJ_Project_Alpha
{
    public class Program
    {
        public static void Main()
        {
            Item apple = new Item("Apple", "An apple is a round, edible fruit with crisp flesh, thin red, green, or yellow skin, and a small central core containing seeds. It ranges in flavor from sweet to tart and is widely grown on apple trees worldwide.");
            Inventory inventory = new Inventory();
            inventory.AddItemToInventory(apple, 1);
            inventory.ShowInventory();
        }
    }
}