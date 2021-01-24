using System;

namespace API.Models
{
    public class Stock
    {
        public Guid Id {get; set;}

        public string Name {get; set;}

        public string Ticker { get; set; }

        public string Description { get; set; }

        public string ImageURL { get; set; }

        public Decimal? CurrentPrice { get; set; }

        public Decimal YearHigh { get; set; }

        public Decimal YearLow { get; set; }

        public Decimal PreviousClose { get; set; }

        public Decimal MarketCap { get; set; }

        public Decimal ChangePercent { get; set; }

    }
}