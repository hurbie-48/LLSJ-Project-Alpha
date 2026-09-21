using LLSJ_Project_Alpha.Entities;
using LLSJ_Project_Alpha;
namespace LLSJProjectAlpha;

// Handles everything related to moving between Locations:
// - listing which directions are currently open
// - resolving a typed direction into a Location
// - running a simple "explore" loop that a Player can use to walk the map
public static class MoveLocation
{
    public static readonly List<string> ValidDirections = new List<string>
        { "north", "n", "east", "e", "south", "s", "west", "w" };

    // Returns null if the direction is unrecognised or there is nothing that way.
    public static Location? Move(Location currentLocation, string? direction)
    {
        if (currentLocation == null || string.IsNullOrWhiteSpace(direction))
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

    // True if there is a real location in the given direction.
    public static bool CanMove(Location currentLocation, string? direction)
    {
        return Move(currentLocation, direction) != null;
    }

    // All directions that currently lead somewhere, keyed by their display name.
    public static Dictionary<string, Location> GetAvailableExits(Location location)
    {
        var exits = new Dictionary<string, Location>();

        if (location.LocationToNorth != null) exits["North"] = location.LocationToNorth;
        if (location.LocationToEast != null) exits["East"] = location.LocationToEast;
        if (location.LocationToSouth != null) exits["South"] = location.LocationToSouth;
        if (location.LocationToWest != null) exits["West"] = location.LocationToWest;

        return exits;
    }

    // Prints the directions the player can currently travel in.
    public static void ShowAvailableDirections(Location location)
    {
        var exits = GetAvailableExits(location);

        if (exits.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("There are no paths leading away from here.");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("You can go:");
        foreach (var exit in exits)
        {
            Console.WriteLine($"  {exit.Key} -> {exit.Value.Name}");
        }

        Console.ResetColor();
    }

    // Draws a small ASCII compass (N/E/S/W) for the given location, lighting up
    // whichever directions actually lead somewhere and naming the destination.
    public static void ShowCompass(Location location)
    {
        var exits = GetAvailableExits(location);

        string northLabel = exits.ContainsKey("North") ? "N" : "-";
        string southLabel = exits.ContainsKey("South") ? "S" : "-";
        string eastLabel = exits.ContainsKey("East") ? "E" : "-";
        string westLabel = exits.ContainsKey("West") ? "W" : "-";

        Console.WriteLine();
        Console.WriteLine("     COMPASS");
        Console.WriteLine("     -------");
        Console.Write("        ");
        WriteDirectionLetter(northLabel, exits.ContainsKey("North"));
        Console.WriteLine();
        Console.WriteLine("        |");
        WriteDirectionLetter(westLabel, exits.ContainsKey("West"));
        Console.Write(" ----- + ----- ");
        WriteDirectionLetter(eastLabel, exits.ContainsKey("East"));
        Console.WriteLine();
        Console.WriteLine("        |");
        Console.Write("        ");
        WriteDirectionLetter(southLabel, exits.ContainsKey("South"));
        Console.WriteLine();
        Console.WriteLine();

        if (exits.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("There are no paths leading away from here.");
            Console.ResetColor();
            return;
        }

        WriteCompassLegendLine("North", exits);
        WriteCompassLegendLine("East", exits);
        WriteCompassLegendLine("South", exits);
        WriteCompassLegendLine("West", exits);
        Console.WriteLine();
    }

    // Writes a single cardinal-direction letter (N/E/S/W), lit up in green when
    // it leads somewhere and dimmed to dark gray when it's a dead end.
    private static void WriteDirectionLetter(string letter, bool available)
    {
        Console.ForegroundColor = available ? ConsoleColor.Green : ConsoleColor.DarkGray;
        Console.Write(letter);
        Console.ResetColor();
    }

    // Writes one line of the compass legend, e.g. "  N -> Town square" or a
    // dimmed "  N -> (nothing this way)" when that direction is blocked.
    private static void WriteCompassLegendLine(string direction, Dictionary<string, Location> exits)
    {
        string letter = direction[0].ToString();

        if (exits.TryGetValue(direction, out var destination))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  {letter} ({direction}) -> {destination.Name}");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"  {letter} ({direction}) -> (nothing this way)");
        }

        Console.ResetColor();
    }

    public static Location TryMove(Location currentLocation, string? direction)
    {
        if (string.IsNullOrWhiteSpace(direction))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please enter a direction: north, south, east or west.");
            Console.ResetColor();
            return currentLocation;
        }

        Location? destination = Move(currentLocation, direction);

        if (destination == null)
        {
            if (ValidDirections.Contains(direction.Trim().ToLower()))
            {
                string fullDirectionName = direction.Trim().ToLower() switch
                {
                    "north" or "n" => "North",
                    "east" or "e" => "East",
                    "south" or "s" => "South",
                    "west" or "w" => "West",
                    _ => direction.Trim()
                };

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"You cannot go {fullDirectionName}!");
                Console.ResetColor();
                return currentLocation;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{direction.Trim()} is an invalid input!");
            Console.ResetColor();
            return currentLocation;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"You travel {direction.Trim().ToLower()} to {destination.Name}.");
        Console.ResetColor();
        Console.WriteLine(destination.Description);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($">>> Current location: {destination.Name} <<<");
        Console.ResetColor();

        return destination;
    }

    public static Location ExploreLoop(Location startingLocation, Player player)
    {
        Location currentLocation = startingLocation;
        bool exploring = true;

        while (exploring)
        {
            Console.Clear();
            PlayerStats.ShowStats(player);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"You are at: {currentLocation.Name}");
            Console.ResetColor();
            Console.WriteLine(currentLocation.Description);

            if (currentLocation.HasShop)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("There is a shop here! Type 'shop' to browse it.");
                Console.ResetColor();
            }

            ShowCompass(currentLocation);

            Console.WriteLine("Enter a direction to move, 'shop' to browse a shop here, or 'quit' to stop exploring.");
            Console.Write("> ");
            string? input = Console.ReadLine();

            if (input != null && input.Trim().ToLower() is "quit" or "exit")
            {
                exploring = false;
                continue;
            }

            if (currentLocation.HasShop && input != null && input.Trim().ToLower() == "shop")
            {
                Shop.Enter(player);
                continue;
            }

            Location before = currentLocation;
            currentLocation = TryMove(currentLocation, input);

            if (ReferenceEquals(currentLocation, before))
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Clearing in 3 seconds...");
                Console.ResetColor();
                Thread.Sleep(3000);
            }
            else
            {
                player.CurrentLocation = currentLocation;
                CombatResult result = StartLocationCombat(player, currentLocation);

                if (result == CombatResult.PlayerDefeated)
                {
                    exploring = false;
                }
            }
        }

        return currentLocation;
    }

    // A monster is fought when the player enters its location. Winning clears
    // the location; fleeing leaves the monster there for a later visit.
    private static CombatResult StartLocationCombat(Player player, Location location)
    {
        if (location.MonsterLivingHere == null)
        {
            return CombatResult.Fled;
        }

        Monster monster = location.MonsterLivingHere;
        CombatResult result = new CombatManager().StartCombat(player, monster);

        if (result == CombatResult.Won)
        {
            location.MonsterLivingHere = null;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"The {location.Name} is safe for now.");
            Console.ResetColor();
            Console.WriteLine("Press Enter to continue exploring...");
            Console.ReadLine();
        }
        else if (result == CombatResult.PlayerDefeated)
        {
            Console.WriteLine("Your adventure ends here.");
        }

        return result;
    }
}
