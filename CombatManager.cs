using LLSJ_Project_Alpha.Entities;

namespace LLSJ_Project_Alpha;

public enum CombatResult { Won, Fled, PlayerDefeated }

// Runs one turn-based encounter. The player always takes the first turn.
public class CombatManager
{
    private readonly Random random = new();

    public CombatResult StartCombat(Player player, Monster monster)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"A wild {monster.Name} appears!");
        Console.ResetColor();

        while (player.CurrentHP > 0 && monster.CurrentHP > 0)
        {
            ShowCombatStatus(player, monster);
            if (!TakePlayerTurn(player, monster, out CombatResult? result)) continue;
            if (result.HasValue) return result.Value;

            if (monster.CurrentHP <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"You defeated the {monster.Name}!");
                Console.ResetColor();
                return CombatResult.Won;
            }

            TakeMonsterTurn(player, monster);
            if (player.CurrentHP <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You have been defeated...");
                Console.ResetColor();
                return CombatResult.PlayerDefeated;
            }
        }

        return monster.CurrentHP <= 0 ? CombatResult.Won : CombatResult.PlayerDefeated;
    }

    private static void ShowCombatStatus(Player player, Monster monster)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("=== COMBAT ===");
        Console.ResetColor();
        Console.WriteLine($"{player.Name}: {player.CurrentHP}/{player.MaxHP} HP");
        Console.WriteLine($"{monster.Name} (level {monster.Level}): {monster.CurrentHP}/{monster.MaxHP} HP");
        Console.WriteLine();
    }

    private bool TakePlayerTurn(Player player, Monster monster, out CombatResult? result)
    {
        result = null;
        Console.WriteLine("1. Attack");
        Console.WriteLine("2. Item");
        Console.WriteLine("3. Flee");
        Console.Write("Choose an action: ");

        switch ((Console.ReadLine() ?? "").Trim())
        {
            case "1": PlayerAttack(player, monster); return true;
            case "2": return UseItemMenu(player);
            case "3":
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"You fled from the {monster.Name}.");
                Console.ResetColor();
                result = CombatResult.Fled;
                return true;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Choose 1, 2 or 3.");
                Console.ResetColor();
                return false;
        }
    }

    private void PlayerAttack(Player player, Monster monster)
    {
        Weapon weapon = player.EquippedWeapon;
        Console.WriteLine($"You attack with {weapon.Name}!");
        if (!HitsTarget()) { Console.WriteLine("Your attack missed!"); return; }

        int dodgeChance = Math.Min(10 + (monster.Level - 1) * 5, 40);
        if (random.Next(1, 101) <= dodgeChance)
        {
            Console.WriteLine($"The {monster.Name} dodged or blocked your attack!");
            return;
        }

        int damage = weapon.Damage;
        if (IsCriticalHit())
        {
            damage = (int)Math.Ceiling(damage * 1.5);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Critical hit!");
            Console.ResetColor();
        }

        monster.CurrentHP = Math.Max(0, monster.CurrentHP - damage);
        Console.WriteLine($"You deal {damage} damage to the {monster.Name}.");
    }

    private bool UseItemMenu(Player player)
    {
        if (player.Inventory.Potions.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("You do not have any potions.");
            Console.ResetColor();
            return false;
        }

        Console.WriteLine("Choose a potion (or 0 to go back):");
        for (int i = 0; i < player.Inventory.Potions.Count; i++)
        {
            Potion potion = player.Inventory.Potions[i];
            Console.WriteLine($"{i + 1}. {potion.name} (+{potion.healingAmount} HP)");
        }

        Console.Write("Choice: ");
        if (!int.TryParse(Console.ReadLine(), out int choice) || choice <= 0 || choice > player.Inventory.Potions.Count)
        {
            Console.WriteLine("No item was used.");
            return false;
        }

        Potion selectedPotion = player.Inventory.Potions[choice - 1];
        selectedPotion.Heal(player);
        player.Inventory.Potions.RemoveAt(choice - 1);
        return true;
    }

    private void TakeMonsterTurn(Player player, Monster monster)
    {
        Console.WriteLine();
        Console.WriteLine($"The {monster.Name} attacks!");
        if (!HitsTarget()) { Console.WriteLine($"The {monster.Name} missed!"); return; }

        // BaseDamage distinguishes monster types; each level adds extra damage.
        int damage = monster.BaseDamage + (monster.Level - 1) * 2;
        if (IsCriticalHit())
        {
            damage = (int)Math.Ceiling(damage * 1.5);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"The {monster.Name} lands a critical hit!");
            Console.ResetColor();
        }

        player.CurrentHP = Math.Max(0, player.CurrentHP - damage);
        Console.WriteLine($"The {monster.Name} deals {damage} damage.");
    }

    private bool HitsTarget() => random.Next(1, 101) <= 80;
    private bool IsCriticalHit() => random.Next(1, 101) <= 10;
}
