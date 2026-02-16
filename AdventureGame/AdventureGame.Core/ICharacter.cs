using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.Core
{
    /// <summary>
    /// This is so the interface can deal with all the characters together
    /// </summary>
    public interface ICharacter
    {
       void Attack(ICharacter target);
       void TakeDamage(int amount);
       string Name { get; }
       int Health { get; }
       bool IsDead { get; }
    }
}
