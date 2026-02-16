namespace AdventureGame.Core
{
    public class Weapon : Item
    {
        public int AttackMod { get; }

        /// <summary>
        /// This is used to get the display of you getting a weapon, and we represent the extra 
        /// damage you do with "AttackMod"
        /// </summary>
        public Weapon(string name, int mod)
            :base(name, $"You picked up {name}! (+{mod} atk")
        {
            AttackMod = mod;
        }

        /// <summary>
        /// This is what happens when a player runs over a weapon 
        /// </summary>
        public override void Use(Player player)
        {
            player.AddWeapon(this);
        }
    }
}
