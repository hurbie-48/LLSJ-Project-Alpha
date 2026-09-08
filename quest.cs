class Quest
{
    public int id;
    public string name, description;
    public bool isCompleted;
    public Dictionary<string, int> rewards;

    public Quest()
    {
        bool isCompleted = false;
        Dictionary<string, int> rewards = new Dictionary<string, int> {};
    }

    public void GiveQuest()
    {
        Console.WriteLine("A new quest has arrived!");
        Console.WriteLine(name);
        Console.WriteLine("Do you want to no about this quest? Y/N");
        Console.Write('>');
        string questInfoPrompt = Console.ReadLine()!;

        if (questInfoPrompt.ToLower()[0] == 'y')
        {
            Console.WriteLine(description);
        }

        Console.WriteLine("Do you want to start this quest? Y/N");
        Console.Write(">");
        string startQuestPrompt = Console.ReadLine()!;

        if (startQuestPrompt.ToLower()[0] == 'y')
        {
            StartQuest();
        }
        else
        {
            Console.WriteLine("That fine, you can always come back.");
        }
    }
    public void StartQuest()
    {
        Console.WriteLine("Quest has started!");
        Console.WriteLine(name);
    }
    public void CompletedQuest() {}
    public void PromptRewards() {} //@TODO: Change return type to bool
    public void GiveRewards() {} //@TODO: Change return type to dict

    static void Main()
    {
        Quest quest = new Quest();
        quest.id = 1;
        quest.name = "Go fetch apple for granny";
        quest.description = "Granny Anny needs 5 apples from the market, go get them and bring them back to her.\nReward:\n- 5 coins.";
        quest.rewards = new Dictionary<string, int>{{"coins", 5}};

        quest.GiveQuest();
    }
}