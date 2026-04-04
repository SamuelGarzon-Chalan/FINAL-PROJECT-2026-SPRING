using System;
using System.Collections.Generic;
using System.Linq;
using FinalBattler.Characters;
using FinalBattler.Abilities;
using FinalBattler.Enums;

namespace FinalBattler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Skill combo = new Skill("Combo", 20, 5, SkillType.Damage);
            Skill Kamehameha = new Skill("Blast", 30, 10, SkillType.Damage);
            Skill heal = new Skill("Recover", 25, 8, SkillType.Heal);
            Skill pocketsand = new Skill("Pocket Sand", 15, 3, SkillType.Damage);
            Skill PECausa = new Skill("PE Causa", 35, 12, SkillType.Heal);

            Fighter Manolo = new Fighter("Samuel", FighterType.Sayayin, 120, 18, 40);
            Manolo.AddSkill(combo);
            Manolo.AddSkill(Kamehameha);
            Manolo.AddSkill(heal);

            Fighter enemy = new Fighter("Carlos", FighterType.Peruano, 100, 15, 30);
            enemy.AddSkill(combo);
            enemy.AddSkill(pocketsand);
            enemy.AddSkill(PECausa);

            List<Fighter> teamA = new List<Fighter> { Manolo };
            List<Fighter> teamB = new List<Fighter> { enemy };

            while (teamA.Any(f => f.IsAlive) && teamB.Any(f => f.IsAlive))
            {
                Console.WriteLine("\n--- Player Turn ---");
                Manolo.DisplayStats();
                enemy.DisplayStats();

                Console.WriteLine("1. Attack");
                Console.WriteLine("2. Skill");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Manolo.Attack(enemy);
                }
                else
                {
                    Manolo.DisplaySkills();
                    Console.Write("Skill name: ");
                    string skill = Console.ReadLine();
                    Manolo.UseSkill(skill, enemy);
                }

                if (enemy.IsAlive)
                {
                    Console.WriteLine("\n--- Enemy Turn ---");
                    enemy.Attack(Manolo);
                }
            }

            Console.WriteLine(Manolo.IsAlive ? "YOU WIN" : "YOU LOSE");
        }
    }
}