namespace TechiesMoneyExchange.Model
{
    public class BankAccount
    {
        public BankAccount(Guid id, Currency currency, string nameOnAccount, Bank bank, string accountNo, BankAccountType accountType)
        {
            Id = id;
            Currency = currency;
            NameOnAccount = nameOnAccount;
            Bank = bank;
            AccountNo = accountNo;
            AccountType = accountType;
        }
        public Guid Id { get; private set; }        
        public Currency Currency { get; private set; }
        public string NameOnAccount { get; set; }
        public Bank Bank { get; private set; }
        public string AccountNo { get; private set; }
        public BankAccountType AccountType { get; private set; }
    }
    public enum BankAccountType
    {
        Savings,
        Current
    }
}
