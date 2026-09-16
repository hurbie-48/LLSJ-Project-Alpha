public static class CombatManager
{
    public const double HIT_CHANCE = 0.8;
    public const double CRITICAL_HIT_CHANCE = 0.1;
    public const double CRITICAL_HIT_MULTIPLIER = 1.5;
    public const double DODGE_CHANCE_PER_LEVEL = 0.05;

    private static readonly Random rng = new Random();

    public static void RunCombat(Player player, Monster monster)
    {
        // loop tot 1 van beide dood is of speler vlucht
        bool fled = false;

        while (player.currentHitPoints > 0 && monster.currentHitPoints > 0 && !fled)
        {
            Console.WriteLine($"\n--- {player.name}: {player.currentHitPoints}/{player.maximumHitPoints} HP | {monster.name}: {monster.currentHitPoints}/{monster.maximumHitPoints} HP ---");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Item");
            Console.WriteLine("3. Flee");
            Console.Write("Kies een optie: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    PlayerAttack(player, monster);
                    break;
                case "2":
                    // todo inventory nog koppelen
                    UseItem(player);
                    break;
                case "3":
                    Console.WriteLine($"{player.name} vlucht weg!");
                    fled = true;
                    break;
                default:
                    Console.WriteLine("Ongeldige keuze");
                    continue;
            }

            // monster valt terug aan tenzij dood of speler weg is
            if (!fled && monster.currentHitPoints > 0)
            {
                MonsterAttack(player, monster);
            }
        }

        if (player.currentHitPoints <= 0)
            Console.WriteLine($"{player.name} is verslagen...");
        else if (monster.currentHitPoints <= 0)
            Console.WriteLine($"{monster.name} is verslagen!");
    }

    public static void PlayerAttack(Player player, Monster monster)
    {
        // 80% kans raak
        if (rng.NextDouble() > HIT_CHANCE)
        {
            Console.WriteLine($"{player.name} mist!");
            return;
        }

        // todo player weapon nog niet af dus hardcoded
        int baseDamage = 5;

        bool isCritical = rng.NextDouble() < CRITICAL_HIT_CHANCE;
        double damage = isCritical ? baseDamage * CRITICAL_HIT_MULTIPLIER : baseDamage;

        // todo monster level nog niet af dus dodge altijd 0
        double dodgeChance = 0;
        if (rng.NextDouble() < dodgeChance)
        {
            Console.WriteLine($"{monster.name} ontwijkt!");
            return;
        }

        monster.currentHitPoints -= (int)damage;
        if (monster.currentHitPoints < 0) monster.currentHitPoints = 0;

        string critText = isCritical ? " crit!" : "";
        Console.WriteLine($"{player.name} doet {(int)damage} damage{critText} aan {monster.name}");
    }

    public static void MonsterAttack(Player player, Monster monster)
    {
        // zelfde raakkans als speler
        if (rng.NextDouble() > HIT_CHANCE)
        {
            Console.WriteLine($"{monster.name} mist!");
            return;
        }

        int damage = monster.maximumDamage;

        player.currentHitPoints -= damage;
        if (player.currentHitPoints < 0) player.currentHitPoints = 0;

        Console.WriteLine($"{monster.name} doet {damage} damage aan {player.name}");
    }

    private static void UseItem(Player player)
    {
        // todo inventory nog niet af
        Console.WriteLine("Geen inventory nog, todo");
    }
}