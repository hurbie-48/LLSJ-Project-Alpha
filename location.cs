using LLSJ_Project_Alpha.Entities;
using LLSJ_Project_Alpha.Quest;

namespace LLSJProjectAlpha;

public class Location
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public Quest? QuestAvailableHere { get; set; }
    public Monster? MonsterLivingHere { get; set; }
    public bool HasShop { get; set; }
    public bool HasQuest { get; set; }

    public Location? LocationToNorth { get; set; }
    public Location? LocationToEast { get; set; }
    public Location? LocationToSouth { get; set; }
    public Location? LocationToWest { get; set; }

    public Location(int id, string name, string description, Quest? questAvailableHere, Monster? monsterLivingHere)
    {
        ID = id;
        Name = name;
        Description = description;
        QuestAvailableHere = questAvailableHere;
        MonsterLivingHere = monsterLivingHere;
    }
}