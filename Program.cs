using LLSJ_Project_Alpha.Entities;

using LLSJ_Project_Alpha;

namespace LLSJProjectAlpha
{
    public class Program
    {
        public static void Main()
        {
            Console.Clear();
            ShowIntro();

            // Creating new Player instance
            Player player = new Player();
            player.Name = AskForPlayerName();
            ShowWelcomeMessage(player.Name);
            player.CurrentHitPoints = 100;
            player.MaximumHitPoints = 100;
            Location startingLocation = World.LocationByID(World.LOCATION_ID_HOME)!;
            player.CurrentLocation = startingLocation;
            MoveLocation.ExploreLoop(startingLocation, player);
        }

        // Prints the opening title/story blurb for the game.
        public static string Capitalize(string name)
        {
            if (string.IsNullOrEmpty(name))
                return name;

            return char.ToUpper(name[0]) + name[1..];
        }
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

        private static void ShowWelcomeMessage(string name)
        {
            Console.Write("Welcome ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{name}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("!");
            Wait(2);
        }

        private static void Wait(float timeToWaitInSeconds)
        {
            Thread.Sleep((int)(timeToWaitInSeconds * 1000));
        }
        private static string AskForPlayerName()
        {
            string name = string.Empty;

            while (name.Length < 2)
            {
                Console.WriteLine("What is your name, adventurer?");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Requirements:\nAt least 2 characters");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("> ");
        
                name = (Console.ReadLine() ?? string.Empty).Trim();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("A name can't be blank. Try again.");
                    Console.ResetColor();
                }
                else if (name.Length < 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("A name can't be one character. Try again.");
                    Console.ResetColor();
                }
            }

            return Capitalize(name);
        }
    }
}
