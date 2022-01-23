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

        private const int DECIMAL_PLACES = 4;
        public decimal CalculateRecievingAmount(ExchangeOperation type, decimal amount)
        {
            var amountYouRecieve = Math.Round(type == ExchangeOperation.Buy ?
                (amount / BuyingRate) :
                (amount * SellingRate), DECIMAL_PLACES, MidpointRounding.AwayFromZero);

            return amountYouRecieve;
        }

        public decimal CalculateSendingAmount(ExchangeOperation type, decimal recievingAmount)
        {
            var newValue = Math.Round(type == ExchangeOperation.Buy ?
                (recievingAmount * BuyingRate) :
                (recievingAmount / SellingRate), DECIMAL_PLACES, MidpointRounding.AwayFromZero);

            return newValue;
        }

        public DraftExchangeRequest CreateExchangeRequestFor(ExchangeOperation type, decimal sendingAmount)
        {            
            var exchangeOperation = new DraftExchangeRequest(this,
               sendingAmount,
               type);

            return exchangeOperation;
        }
    }
}
