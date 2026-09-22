namespace LLSJ_Project_Alpha;

public class Item
{
    public int ID;
    public string name;
    public string description;

    public Item(int id, string itemName, string itemDescription)
    {
        ID = id;
        name = itemName;
        description = itemDescription;
    }
}