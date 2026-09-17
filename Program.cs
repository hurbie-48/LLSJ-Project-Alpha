using LLSJ_Project_Alpha.Entities;
using LLSJ_Project_Alpha.Quest;

namespace LLSJProjectAlpha
{
    public class Program
    {
        public static void Main()
        {
            Console.Clear();
            ShowIntro();

            // Testing new player class
            Player player = new Player();
            player.Name = AskForPlayerName();
            player.CurrentHitPoints = 100;
            player.MaximumHitPoints = 100;

            // Test for monster class
            Monster monster = new Monster();
            monster.id = 1;
            monster.name = "Rat";
            monster.maximumDamage = 3;
            monster.maximumHitPoints = 25;
            monster.currentHitPoints = 25;

            Console.WriteLine($"Monster: {monster.name}");
            Console.WriteLine($"Damage: {monster.maximumDamage}");
            Console.WriteLine($"Max health: {monster.maximumHitPoints}");
            Console.WriteLine($"current health: {monster.currentHitPoints}");
            Location startingLocation = World.LocationByID(World.LOCATION_ID_HOME)!;
            MoveLocation.ExploreLoop(startingLocation, player);
        }

        // Prints the opening title/story blurb for the game.
        private static void ShowIntro()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"
__/\\\______________/\\\_________________/\\\\\\\\\\\________/\\\\\\\\\\\_        
 _\/\\\_____________\/\\\_______________/\\\/////////\\\_____\/////\\\///__       
  _\/\\\_____________\/\\\______________\//\\\______\///__________\/\\\_____      
   _\/\\\_____________\/\\\_______________\////\\\_________________\/\\\_____     
    _\/\\\_____________\/\\\__________________\////\\\______________\/\\\_____    
     _\/\\\_____________\/\\\_____________________\////\\\___________\/\\\_____   
      _\/\\\_____________\/\\\______________/\\\______\//\\\___/\\\___\/\\\_____  
       _\/\\\\\\\\\\\\\\\_\/\\\\\\\\\\\\\\\_\///\\\\\\\\\\\/___\//\\\\\\\\\______ 
        _\///////////////__\///////////////____\///////////______\/////////_______
");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("The kingdom has fallen quiet. Rats gnaw at the alchemist's garden,");
            Console.WriteLine("snakes slither through the farmer's field, and something far worse");
            Console.WriteLine("waits in the forest beyond the bridge.");
            Console.WriteLine();
            Console.WriteLine("You wake up at home, unsure of what today will bring...");
            Console.WriteLine();
        }

        // Prompts the player for their name, re-asking until they enter something
        // other than blank/whitespace.
        private static string AskForPlayerName()
        {
            string? name = null;

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("What is your name, adventurer?");
                Console.Write("> ");
                name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("A name can't be blank. Try again.");
                    Console.ResetColor();
                }
            }

            return name.Trim();
        }
    }
}