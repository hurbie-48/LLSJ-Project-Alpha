using LLSJ_Project_Alpha.Entities;
using LLSJ_Project_Alpha.Quest;

namespace LLSJProjectAlpha
{
    public class Program
    {
        public static void Main()
        {
            // Testing new player class
            Player player = new Player();
            player.name = "john";

            Console.WriteLine(player.name);
            Console.WriteLine($"Current health: {player.currentHitPoints}");
            Console.WriteLine($"Current health: {player.maximumHitPoints}");

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

            // --- Movement demo ---
            // Starts the player at World's home location and lets them walk the
            // real map (World.Locations), with a compass shown at every stop,
            // until they type "quit".
            // Once your Player class has a CurrentLocation property, replace this with:
            //   player.CurrentLocation = MoveLocation.ExploreLoop(player.CurrentLocation);
            Location startingLocation = World.LocationByID(World.LOCATION_ID_HOME)!;
            MoveLocation.ExploreLoop(startingLocation);
        }
    }
}