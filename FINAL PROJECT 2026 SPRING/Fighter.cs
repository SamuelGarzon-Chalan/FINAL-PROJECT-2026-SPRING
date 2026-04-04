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

        public Fighter(string name, FighterType fighterType, int maxHealth, int attackPower, int energy)
            : base(name, maxHealth, attackPower)
        {
            FighterType = fighterType;
            Energy = energy;
            Skills = new Dictionary<string, Skill>();
        }

        public void AddSkill(Skill skill)
        {
            if (!Skills.ContainsKey(skill.Name))
            {
                Skills.Add(skill.Name, skill);
            }
        }

        public override void Attack(Character target)
        {
            Console.WriteLine($"{Name} attacks {target.Name}!");
            target.TakeDamage(AttackPower);
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

            Energy -= skill.EnergyCost;

            if (skill.SkillType == SkillType.Damage)
            {
                target.TakeDamage(skill.Power);
            }
            else if (skill.SkillType == SkillType.Heal)
            {
                Heal(skill.Power);
            }

            Console.WriteLine($"{Name} energy: {Energy}");
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