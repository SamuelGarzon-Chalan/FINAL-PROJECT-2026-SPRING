using FinalBattler.Enums;
using System.Runtime.CompilerServices;

namespace FinalBattler.Abilities
{
    public class Skill
    {
        public string Name { get;  set; }
        public int Power { get;  set; }
        public int EnergyCost { get;  set; }
        public SkillType SkillType { get;  set; }
        public int Couldown { get; set; }

        public Skill(string name, int power, int energyCost, SkillType skillType,int couldown )
        {
            Name = name;
            Power = power;
            EnergyCost = energyCost;
            SkillType = skillType;
            Couldown = couldown;
        }

        public void DisplaySkill()
        {
            Console.WriteLine($"{Name} | Power: {Power} | Energy Cost: {EnergyCost} | Type: {SkillType}");
        }
    }
}