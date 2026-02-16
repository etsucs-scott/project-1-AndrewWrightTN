using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.Core
{
    public class Player : ICharacter
    {
        public string Name { get; }
        public int Health { get; private set; }
        public bool IsDead => Health > 0;
        readonly List<Weapon> _weapons = new();
        int MaxHealth = 150;
        int BaseDamage = 10;

        public Player (string name)
        {
            Name = name;
            Health = 100;
        }
        public void Attack(ICharacter target)
        {
            int bestModifier = _weapons.Any() ? _weapons.Max(w => w.AttackMod) : 0;
            int damage = BaseDamage + bestModifier;
            target.TakeDamage(damage);
        }
        public void TakeDamage(int amount)
        {
            Health = Math.Max(0, Health - amount);
        }
        public void AddItem(Item item)
        {
            item.Use(this);
        }
        public void AddWeapon(Weapon weapon) 
        { 
            _weapons.Add(weapon);
        }
        public void Heal(int amount)
        { 
            Health = Math.Min(Health + amount, MaxHealth);
        }
    }
}
