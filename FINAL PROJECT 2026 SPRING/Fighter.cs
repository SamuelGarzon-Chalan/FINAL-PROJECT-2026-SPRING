using System;
using System.Collections.Generic;
using FinalBattler.Abilities;
using FinalBattler.Enums;

namespace FinalBattler.Characters
{
    public class Fighter : Character
    {
        public FighterType FighterType { get; set; }
        public int Energy { get; set; }
        public Dictionary<string, Skill> Skills { get; set; }
        public Dictionary<string, int> ActiveCooldowns { get; set; }

        public Fighter(string name, FighterType fighterType, int maxHealth, int attackPower, int energy)
            : base(name, maxHealth, attackPower)
        {
            FighterType = fighterType;
            Energy = energy;
            Skills = new Dictionary<string, Skill>();
            ActiveCooldowns = new Dictionary<string, int>();
        }

        public void AddSkill(Skill skill)
        {
            if (!Skills.ContainsKey(skill.Name))
            {
                Skills.Add(skill.Name, skill);
            }
        }
        public void AssingDefaultSkills()
        {
            if (FighterType == FighterType.PotentialMan)
            {
                AddSkill(new Skill("Divine Dogs", 30, 20, SkillType.Damage, 2));
                AddSkill(new Skill("Toad", 10, 10, SkillType.Damage, 3));
                AddSkill(new Skill("Max Elephant", 50, 60, SkillType.Damage, 4));
            }
            else if (FighterType == FighterType.Sayayin)
            {
                AddSkill(new Skill("Rising Rush", 25, 20, SkillType.Damage, 3));
                AddSkill(new Skill("Kamehameha", 40, 30, SkillType.Damage, 4));
                AddSkill(new Skill("Senzu Bean", 100, 50, SkillType.Heal, 5));
            }
            else if (FighterType == FighterType.Peruano)
            {
                AddSkill(new Skill("Backstab", 35, 20, SkillType.Damage, 2));
                AddSkill(new Skill("Pocket sand", 10, 0, SkillType.Damage, 1));
                AddSkill(new Skill("Pe causa", 0, 15, SkillType.Heal, 3));
            }
            else if (FighterType == FighterType.Boliviano)
            {
                AddSkill(new Skill("Earthquake", 45, 30, SkillType.Damage, 3));
                AddSkill(new Skill("Stone Skin", 0, 25, SkillType.Heal, 4));
            }
            else if (FighterType == FighterType.Veneco)
            {
                AddSkill(new Skill("Poison Dart", 25, 15, SkillType.Damage, 2));
                AddSkill(new Skill("Toxic Shield", 0, 20, SkillType.Heal, 3));
            }
            else if (FighterType == FighterType.GoodBrother)
            {
                AddSkill(new Skill("Piercing Blood", 70, 30, SkillType.Damage, 5));
                AddSkill(new Skill("SuperNova", 30, 15, SkillType.Damage, 3));
            }
        }

        public override void Attack(Character target)
        {
            Console.WriteLine($"{Name} attacks {target.Name}!");
            target.TakeDamage(AttackPower);
        }
        public bool IsSkillOnCooldown(string skillName)
        {
            return ActiveCooldowns.ContainsKey(skillName) && ActiveCooldowns[skillName] > 0;
        }

        public void UseSkill(string skillName, Character target)
        {
            if (!Skills.ContainsKey(skillName))
            {
                Console.WriteLine("Skill not found. check your grammar ");
                return;
            }

            Skill skill = Skills[skillName];

            if (Energy < skill.EnergyCost)
            {
                Console.WriteLine("Not enough energy. We are so cooked");
                return;
            }

            if (IsSkillOnCooldown(skillName))
            {
                Console.WriteLine("Skill is on cooldown. Please wait and use another technique .");
                return;
            }

            Energy -= skill.EnergyCost;

            if (skill.SkillType == SkillType.Damage)
            {
                target.TakeDamage(skill.Power);
            }
            else if (skill.SkillType == SkillType.Heal)
            {
                Heal(skill.Power);
            }
            
            ActiveCooldowns[skillName] = skill.Couldown;

            Console.WriteLine($"{Name} energy: {Energy}");
        }
        public void ReduceCooldowns()
        {
            var keys = new List<string>(ActiveCooldowns.Keys);
            foreach (var key in keys)
            {
                if (ActiveCooldowns[key] > 0)
                {
                    ActiveCooldowns[key]--;
                }
            }
        }

        public override void DisplayStats()
        {
            Console.WriteLine($"{Name} ({FighterType}) HP: {Health}/{MaxHealth} | ATK: {AttackPower} | Energy: {Energy}");
        }

        public void DisplaySkills()
        {
            foreach (var skill in Skills)
            {
                skill.Value.DisplaySkill();
            }
        }
    }
}