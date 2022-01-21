namespace TechiesMoneyExchange.Model
{
    public class BankAccount
    {
        public BankAccount(Guid id, Currency currency, Bank bank, string accountNo)
        {
            Id = id;
            Currency = currency;
            Bank = bank;
            AccountNo = accountNo;
        }
        public Guid Id { get; private set; }        
        public Currency Currency { get; private set; }
        public Bank Bank { get; private set; }
        public string AccountNo { get; private set; }
    }
}
