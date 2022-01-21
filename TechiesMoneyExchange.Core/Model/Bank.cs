namespace TechiesMoneyExchange.Model
{
    public class Bank
    {
        public Bank(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; private set; }
        public string Name { get; private set; }
    }
}
