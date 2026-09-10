using LLSJ_Project_Alpha.Entities;

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
        }
    }

    public class MoveLocation()
    {
        public static Location Move(Location currentLocation, string direction)
        {
            if (currentLocation == null || direction == null)
            {
                return null;
            }

            switch (direction.Trim().ToLower())
            {
                case "north":
                case "n":
                    return currentLocation.LocationToNorth;

                case "south":
                case "s":
                    return currentLocation.LocationToSouth;

                case "east":
                case "e":
                    return currentLocation.LocationToEast;

                case "west":
                case "w":
                    return currentLocation.LocationToWest;

                default:
                    return null;
            }
        }

        // Geeft aan of er in de opgegeven richting daadwerkelijk een locatie is om naartoe te gaan.
        public static bool CanMove(Location currentLocation, string direction)
        {
            return Move(currentLocation, direction) != null;
        }
    }


}