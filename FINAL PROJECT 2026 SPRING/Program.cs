using System;
using System.Collections.Generic;
using System.Linq;
using FinalBattler.Characters;
using FinalBattler.Abilities;
using FinalBattler.Enums;
using FINAL_Battler.Data;
using FinalBattler.Data;

namespace FinalBattler
{
    public  class Program
    {
        static void Main(string[] args)
        {
            Fighter goku = new Fighter("Goku", FighterType.Sayayin, 120, 18, 40);
            Fighter choso = new Fighter("Choso", FighterType.GoodBrother, 100, 15, 35);

            Fighter enemy1 = new Fighter("Johan", FighterType.Peruano, 95, 14, 30);
            Fighter enemy2 = new Fighter("Diego", FighterType.Boliviano, 110, 16, 30);
            Fighter enemy3 = new Fighter("Luis", FighterType.Veneco, 90, 17, 25);
            
            goku.AssingDefaultSkills();
            choso.AssingDefaultSkills();
            enemy1.AssingDefaultSkills();
            enemy2.AssingDefaultSkills();
            enemy3.AssingDefaultSkills();

            List<Fighter> teamA = new List<Fighter> { goku ,choso };
            List<Fighter> teamB = new List<Fighter> { enemy1 , enemy2 , enemy3  };
            int numberOfRounds = 1;

            GameData1 saveData = new GameData1();
            saveData.TeamA = teamA;
            saveData.TeamB = teamB;
            saveData.NumberOfRounds = numberOfRounds;
            saveData.SavedData=DateTime.Now;
            SaveManager.SaveGame(saveData);

            while (teamA.Any(f => f.IsAlive) && teamB.Any(f => f.IsAlive))
            {
                Console.WriteLine();
                Console.WriteLine($"========== ROUND {numberOfRounds} ==========");//TODO add a delay here to make it more dramatic and to give the player time to read the round number before the teams are displayed
                Console.WriteLine();//ALSO MAYBE HERE ADD THE TIME TABLE TO SAVE THE DATE WHEN WAS CREATED THE GAME AND WHIT THIS IDEA CREATE JSON 

                Console.WriteLine("TEAM A");
                DisplayTeam(teamA);

                Console.WriteLine();
                Console.WriteLine("TEAM B");
                DisplayTeam(teamB);

                Console.WriteLine();
                List<Fighter> aliveTeamA = teamA.Where(f => f.IsAlive).ToList();
                
                foreach (Fighter currentF in aliveTeamA)
                { 
                    if (!teamB.Any(f => f.IsAlive))
                    {
                        break;
                    }
                    Console.WriteLine();
                    Console.WriteLine($"--- {currentF.Name}'s turn ---");
                    currentF.DisplayStats();

                    Console.WriteLine("Choose an action:");
                    Console.WriteLine("1. Basic Attack");
                    Console.WriteLine("2. Use Skill");
                    Console.Write("Option: ");
                    string actionChoice = Console.ReadLine();

                    if (actionChoice == "1")//TODO i NEED TO DO Async / Awai for the enemies attack while you are thinking about your move 
                    {
                        Fighter target = SelectTarget(teamB, "choose an enemy");
                        if (target != null)
                        {
                            currentF.Attack(target);
                        }
                    }
                    else if (actionChoice == "2")
                    {
                        currentF.DisplaySkills();
                        Console.Write("\nSkill name: ");
                        string skill = Console.ReadLine();

                        if (currentF.Skills.ContainsKey(skill) && skill == "Recover")
                        {
                            currentF.UseSkill(skill, currentF);
                        }
                        else
                        {
                            Fighter target = SelectTarget(teamB, "choose an enemy");
                            if (target != null)//also i need to recover energy for all the character because they cant refill and the enemies cant fight 
                            {
                                currentF.UseSkill(skill, target);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid action. Turn skipped.");
                    }
                }

                if (!teamB.Any(f => f.IsAlive))
                {
                    break;
                }
                Console.WriteLine();
                Console.WriteLine("=== ENEMY TEAM TURN ===");

                List<Fighter> aliveTeamB = teamB.Where(f => f.IsAlive).ToList();

                foreach (Fighter enemy in aliveTeamB)
                {
                    if (!teamA.Any(f => f.IsAlive))
                    {
                        break;
                    }

                    Fighter target = teamA.Where(f => f.IsAlive).OrderBy(f => f.Health).FirstOrDefault();

                    if (target == null)
                    {
                        break;
                    }

                    if ((enemy.Name == "Diego" || enemy.Name == "Luis") && enemy.Skills.Count > 0)
                    {
                        string firstSkillName = enemy.Skills.Keys.First();

                        if (!enemy.IsSkillOnCooldown(firstSkillName))
                        {
                            enemy.UseSkill(firstSkillName, target);
                        }
                        else
                        {
                            enemy.Attack(target);
                        }
                    }
                    else
                    {
                        enemy.Attack(target);//jnohan only basic attack because he is a support and all this methods is like an ai but very simple
                    }
                }

                foreach (Fighter fighter in teamA)
                {
                    fighter.ReduceCooldowns();
                }

                foreach (Fighter fighter in teamB)
                {
                    fighter.ReduceCooldowns();
                }

                numberOfRounds++;
            }

            Console.WriteLine();
            Console.WriteLine();

            if (teamA.Any(f => f.IsAlive))
            {
                Console.WriteLine("TEAM A WINS!");
            }
            else
            {
                Console.WriteLine("TEAM B WINS!");
            }
        }

        static void DisplayTeam(List<Fighter> team)
        {
            List<Fighter> aliveFighters = team.Where(f => f.IsAlive).ToList();
            List<Fighter> defeatedFighters = team.Where(f => !f.IsAlive).ToList();

            Console.WriteLine("Alive Fighters:");
            for (int i = 0; i < aliveFighters.Count; i++)  
            {
                Console.Write($"{i + 1}. ");
                aliveFighters[i].DisplayStats();
            }

            if (defeatedFighters.Any())
            {
                Console.WriteLine("Defeated Fighters:");
                foreach (Fighter fighter in defeatedFighters)
                {
                    Console.WriteLine($"{fighter.Name} is defeated.");
                }
            }
        }

        static Fighter SelectTarget(List<Fighter> enemyTeam, string message)
        {
            List<Fighter> aliveTargets = enemyTeam.Where(f => f.IsAlive).ToList();

            if (!aliveTargets.Any())
            {
                Console.WriteLine("There are no available targets.");
                return null;
            }

            Console.WriteLine(message);

            for (int i = 0; i < aliveTargets.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                aliveTargets[i].DisplayStats();
            }

            Console.Write("Target number: ");
            string input = Console.ReadLine();

            bool validNumber = int.TryParse(input, out int targetChoice);

            if (!validNumber)
            {
                Console.WriteLine("Invalid input.");
                return null;
            }

            if (targetChoice < 1 || targetChoice > aliveTargets.Count)
            {
                Console.WriteLine("That target does not exist.");
                return null;
            }

            return aliveTargets[targetChoice - 1];
        }
    }
}