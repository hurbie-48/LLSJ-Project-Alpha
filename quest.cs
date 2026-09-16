namespace LLSJ_Project_Alpha.Quest
{
    public class Quest
    {
        public int ID;
        public readonly string Name, Description;
        public bool IsCompleted;
        public Dictionary<string, int> Rewards;

        public Quest(int id, string name, string description)
        {
            ID = id;
            Name = name;
            Description = description;
            IsCompleted = false;
            Rewards = new Dictionary<string, int>();
        }

        public void GiveQuest()
        {
            Console.WriteLine("A new quest has arrived!");
            Console.WriteLine(Name);
            Console.WriteLine("Do you want to no about this quest? Y/N");
            Console.Write('>');
            string questInfoPrompt = Console.ReadLine()!;

            if (questInfoPrompt.ToLower()[0] == 'y')
            {
                Console.WriteLine(Description);
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
            Console.WriteLine(Name);
        }

        public void CompletedQuest()
        {
            IsCompleted = true;
            Console.WriteLine("=====================================================");
            Console.WriteLine($"YOU HAVE COMPLETED {Name}");
            Console.WriteLine("=====================================================");

            PromptRewards();
        }

        public void PromptRewards()
        {
            Console.WriteLine("Do you what to have your rewards? (Y/N)");
            Console.Write("> ");
            string userInput = Console.ReadLine()!.ToLower();

            if (userInput == "y")
            {
                GiveRewards();
            }
        }

        public Dictionary<string, int> GiveRewards()
        {
            return Rewards;
        }
    }
}