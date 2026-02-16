namespace AdventureGame.Core
{
    public class Potion : Item
    {
        public int HealAmount { get; }
        /// <summary>
        /// This is where it is displayed that you got a potion and how much it healed for,
        /// the HealAmount is how much the player actually got healed
        /// </summary>
        public Potion(string name, int healAmount = 20)
            : base (name, $"{name} has healed you + {healAmount} hp")
        {
            HealAmount = healAmount;
        }
        /// <summary>
        /// When the player runs over a potion it is used and the player is healed
        /// </summary>
        public override void Use(Player player)
        {
            player.Heal(HealAmount);
        }
    }
}
