namespace TechiesMoneyExchange.Model
{
    public class PublishedExchangeRate
    {
        public PublishedExchangeRate(
            Guid id,
            DateTime requestedDateUTC,
            TimeSpan validity, 
            Currency currency, 
            Currency baseCurrency, 
            decimal buyingRate, 
            decimal sellingRate)
        {
            ExchangeId = id;
            RequestedDateUTC = requestedDateUTC;
            Validity = validity;
            Currency = currency;
            BaseCurrency = baseCurrency;
            BuyingRate = buyingRate;
            SellingRate = sellingRate;
        }
        public Guid ExchangeId { get;}
        public DateTime RequestedDateUTC { get; }
        public TimeSpan Validity { get;  }
        public Currency Currency { get; }
        public Currency BaseCurrency { get; set; }
        public decimal BuyingRate { get; }
        public decimal SellingRate { get; }
    }
}
