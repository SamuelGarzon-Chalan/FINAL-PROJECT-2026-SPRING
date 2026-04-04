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
            Skill Kamehameha = new Skill("Kamehameha ", 30, 10, SkillType.Damage);
            Skill heal = new Skill("Recover", 25, 8, SkillType.Heal);
            Skill pocketsand = new Skill("Pocket Sand", 15, 3, SkillType.Damage);
            Skill PECausa = new Skill("PE Causa", 35, 12, SkillType.Heal);//here i need to put more abilities for the other caracters but i will do it later but for testing i finished the peruano atacck and sayajin 

            Fighter Manolo = new Fighter("Manolo", FighterType.Sayayin, 120, 18, 40);
            Manolo.AddSkill(combo);
            Manolo.AddSkill(Kamehameha);
            Manolo.AddSkill(heal);

            Fighter enemy = new Fighter("Johan", FighterType.Peruano, 100, 15, 30);
            enemy.AddSkill(combo);
            enemy.AddSkill(pocketsand);
            enemy.AddSkill(PECausa);

            List<Fighter> teamA = new List<Fighter> { Manolo };
            List<Fighter> teamB = new List<Fighter> { enemy };

            while (teamA.Any(f => f.IsAlive) && teamB.Any(f => f.IsAlive))//labda expression to check if any fighter in the team is alive
            {
                Console.WriteLine("\n--- Player Turn ---");
                Manolo.DisplayStats();
                enemy.DisplayStats();

                Console.WriteLine("1. Attack");//here i need to put the normal atack do auto attack  but for the first test i will just put the normal attack and then i will do the auto attack 
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Manolo.Attack(enemy);
                }
                else
                {
                    Manolo.DisplaySkills();
                    Console.Write("\nSkill name: ");
                    string skill = Console.ReadLine();
                    Manolo.UseSkill(skill, enemy);
                }

                if (enemy.IsAlive)
                {
                    Console.WriteLine("\n--- Enemy Turn ---");
                    enemy.Attack(Manolo);
                }
            }

            Console.WriteLine(Manolo.IsAlive ? "Victory Royale ;)" : "YOU LOSE ;(");
        }
    }
}