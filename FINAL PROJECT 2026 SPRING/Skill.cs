using FinalBattler.Enums;

namespace FinalBattler.Abilities
{
    public class Skill
    {
        public string Name { get;  set; }
        public int Power { get;  set; }
        public int EnergyCost { get;  set; }
        public SkillType SkillType { get;  set; }

        public Skill(string name, int power, int energyCost, SkillType skillType)
        {
            Name = name;
            Power = power;
            EnergyCost = energyCost;
            SkillType = skillType;
        }

        public void DisplaySkill()
        {
            Console.WriteLine($"{Name} | Power: {Power} | Energy Cost: {EnergyCost} | Type: {SkillType}");
        }
    }
}