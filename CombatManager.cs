using System;
using System.Linq;

namespace LLSJ_Project_Alpha;

public class CombatManager
{
    private Random random = new Random();

    public void StartCombat(Player player, Monster monster)
    {
        Console.WriteLine("Een wilde " + monster.Name + " verschijnt!");

        while (player.CurrentHP > 0 && monster.CurrentHP > 0)
        {
            Console.WriteLine("");
            Console.WriteLine("--- " + player.Name + ": " + player.CurrentHP + "/" + player.MaxHP + " HP | " + monster.Name + ": " + monster.CurrentHP + " HP ---");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Item");
            Console.WriteLine("3. Flee");
            Console.Write("Kies actie: ");

            string input = Console.ReadLine();

            if (input == "1")
            {
                PlayerTurn(player, monster);
            }
            else if (input == "2")
            {
                bool itemGebruikt = UseItemMenu(player);
                if (itemGebruikt == false)
                {
                    continue;
                }
            }
            else if (input == "3")
            {
                Console.WriteLine("Je bent weggerend!");
                return;
            }
            else
            {
                Console.WriteLine("Ongeldige keuze, probeer opnieuw.");
                continue;
            }

            if (monster.CurrentHP <= 0)
            {
                Console.WriteLine("Je hebt " + monster.Name + " verslagen!");
                break;
            }

            MonsterTurn(player, monster);

            if (player.CurrentHP <= 0)
            {
                Console.WriteLine("Je bent dood gegaan...");
                break;
            }
        }
    }

    private void PlayerTurn(Player player, Monster monster)
    {
        int hitRoll = random.Next(1, 101);
        if (hitRoll > 80)
        {
            Console.WriteLine("Je aanval miste!");
            return;
        }

        int dodgeChance = monster.Level * 10;
        int dodgeRoll = random.Next(1, 101);
        if (dodgeRoll <= dodgeChance)
        {
            Console.WriteLine(monster.Name + " ontweek of blokkeerde je aanval!");
            return;
        }

        int damage = player.EquippedWeapon.Damage;

        int critRoll = random.Next(1, 101);
        if (critRoll <= 10)
        {
            damage = (int)(damage * 1.5);
            Console.WriteLine("CRITICAL HIT!");
        }

        monster.CurrentHP = monster.CurrentHP - damage;
        if (monster.CurrentHP < 0)
        {
            monster.CurrentHP = 0;
        }

        Console.WriteLine("Je slaat " + monster.Name + " voor " + damage + " damage!");
    }

    private bool UseItemMenu(Player player)
    {
        if (player.Inventory.Potions.Count == 0)
        {
            Console.WriteLine("Je hebt geen potions in je inventory!");
            return false;
        }

        Console.WriteLine("");
        Console.WriteLine("Kies een potion:");
        
        int i = 0;
        while (i < player.Inventory.Potions.Count)
        {
            Potion p = player.Inventory.Potions[i];
            int nummer = i + 1;
            Console.WriteLine(nummer + ". " + p.name + " (+" + p.healingAmount + " HP)");
            i = i + 1;
        }
        Console.WriteLine("0. Terug");

        Console.Write("Optie: ");
        string choice = Console.ReadLine();

        if (int.TryParse(choice, out int index))
        {
            if (index > 0 && index <= player.Inventory.Potions.Count)
            {
                Potion selectedPotion = player.Inventory.Potions[index - 1];

                selectedPotion.Heal(player);

                player.Inventory.Potions.RemoveAt(index - 1);
                return true;
            }
        }

        return false;
    }

    private void MonsterTurn(Player player, Monster monster)
    {
        Console.WriteLine("");
        Console.WriteLine(monster.Name + " valt aan!");

        int hitRoll = random.Next(1, 101);
        if (hitRoll > 80)
        {
            Console.WriteLine(monster.Name + " miste zijn aanval!");
            return;
        }

        int dodgeRoll = random.Next(1, 101);
        if (dodgeRoll <= 10)
        {
            Console.WriteLine("Je ontweek de aanval van het monster!");
            return;
        }

        int damage = monster.BaseDamage + (monster.Level * 2);

        int critRoll = random.Next(1, 101);
        if (critRoll <= 10)
        {
            damage = (int)(damage * 1.5);
            Console.WriteLine(monster.Name + " pakt een CRITICAL HIT!");
        }

        player.CurrentHP = player.CurrentHP - damage;
        if (player.CurrentHP < 0)
        {
            player.CurrentHP = 0;
        }

        Console.WriteLine(monster.Name + " doet " + damage + " damage aan jou!");
    }
}