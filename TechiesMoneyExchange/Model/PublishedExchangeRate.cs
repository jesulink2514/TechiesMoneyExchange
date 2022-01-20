namespace TechiesMoneyExchange.Model
{
    public class PublishedExchangeRate
    {
        public PublishedExchangeRate(
            Guid id, 
            DateTime requestedDateUTC, 
            TimeSpan validity, 
            ConversionRate leftRate, 
            ConversionRate rightRate)
        {
            ExchangeId = id;
            RequestedDateUTC = requestedDateUTC;
            Validity = validity;
            LeftRate = leftRate;
            RightRate = rightRate;
        }
        public Guid ExchangeId { get;}
        public DateTime RequestedDateUTC { get; }
        public TimeSpan Validity { get;  }
        public ConversionRate LeftRate { get; }
        public ConversionRate RightRate { get; }
    }
}
