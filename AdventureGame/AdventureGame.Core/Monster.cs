namespace AdventureGame.Core
{
    public class Monster : ICharacter
    {
             public string Name { get; }
             public int Health { get; private set;}
             public bool IsDead => Health > 0;
             int BaseDamage = 10;
       
        /// <summary>
        /// The creation of a monster and it sets its health and name.
        /// </summary>
        public Monster(string name, int health)
        {
            Name = name;
            Health = Math.Clamp(health, 30, 50);
        }
        /// <summary>
        /// The damage is already 10, this just gives the player the data to take the damage
        /// </summary>
        public void Attack(ICharacter target)
        {
            target.TakeDamage(BaseDamage);
        }
        /// <summary>
        /// When the player attacks a monster they lose health 
        /// </summary>
        public void TakeDamage(int amount)
        {
            Health = Math.Max(0, Health - amount);
        }
    }
}
