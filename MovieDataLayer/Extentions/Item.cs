namespace MovieDataLayer
{
    public abstract class Item<U> 
    {
        public U Id { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
