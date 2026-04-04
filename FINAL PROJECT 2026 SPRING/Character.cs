using System;

namespace FinalBattler.Characters
{
    public abstract class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int AttackPower { get; set; }

        public bool IsAlive => Health > 0;

        public Character(string name, int maxHealth, int attackPower)
        {
            Name = name;
            MaxHealth = maxHealth;
            Health = maxHealth;
            AttackPower = attackPower;
        }

        public void TakeDamage(int amount)
        {
            if (amount < 0) amount = 0;

            Health -= amount;

            if (Health < 0)
                Health = 0;

            Console.WriteLine($"{Name} takes {amount} damage. HP: {Health}/{MaxHealth}");
        }

        public void Heal(int amount)
        {
            if (amount < 0) amount = 0;

            Health += amount;

            if (Health > MaxHealth)
                Health = MaxHealth;

            Console.WriteLine($"{Name} heals {amount}. HP: {Health}/{MaxHealth}");
        }

        public virtual void DisplayStats()
        {
            Console.WriteLine($"{Name} | HP: {Health}/{MaxHealth} | Attack: {AttackPower}");
        }

        public abstract void Attack(Character target);
    }
}