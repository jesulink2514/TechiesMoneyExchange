namespace TechiesMoneyExchange.Model
{
    public class ConversionRate
    {
        public ConversionRate(Currency currency, decimal rate)
        {
            Currency = currency;
            Rate = rate;
        }
        public Currency Currency { get;}
        public decimal Rate { get; }
    }
}
