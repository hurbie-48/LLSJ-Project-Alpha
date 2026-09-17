namespace LLSJ_Project_Alpha.Entities;

public class PlayerStats
{
    public static void ShowStats(Player player)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("=== PLAYER STATS ===");
        Console.ResetColor();

        Console.WriteLine($"Name: {player.Name}");
        Console.WriteLine($"Health: {player.CurrentHitPoints}/{player.MaximumHitPoints}");

        DrawHealthBar(player.CurrentHitPoints, player.MaximumHitPoints);

        Console.WriteLine();
        Console.WriteLine($"Gold: {player.Gold}");
        player.Inventory.ShowInventory();
    }

    // Draws a simple [####------] health bar, colored green/yellow/red
    // depending on how much health remains.
    private static void DrawHealthBar(int current, int max, int barWidth = 20)
    {
        if (max <= 0)
        {
            return;
        }

        double ratio = (double)current / max;
        int filled = (int)Math.Round(ratio * barWidth);
        filled = Math.Clamp(filled, 0, barWidth);

        ConsoleColor barColor = ratio switch
        {
            >= 0.6 => ConsoleColor.Green,
            >= 0.3 => ConsoleColor.Yellow,
            _ => ConsoleColor.Red
        };

        Console.Write("[");
        Console.ForegroundColor = barColor;
        Console.Write(new string('#', filled));
        Console.ResetColor();
        Console.Write(new string('-', barWidth - filled));
        Console.WriteLine("]");
    }
}