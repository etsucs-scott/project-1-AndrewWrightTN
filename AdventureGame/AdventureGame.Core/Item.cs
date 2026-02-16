namespace AdventureGame.Core
{
    /// <summary>
    /// This is so all the items can be used the same and handled together
    /// </summary>
    public abstract class Item
    {
        public string Name {  get; }   
        public string ItemMessage { get; }
        protected Item(string name, string itemMessage) 
        {
            Name = name;
            ItemMessage = itemMessage;
        }
        public abstract void Use(Player player);        
    }
}
