export interface Stock {
    id: string;
    name: string;
    ticker: string;
    description?: null;
    imageURL: string;
    currentPrice: number;
    yearHigh: number;
    yearLow: number;
    previousClose: number;
    marketCap: number;
    changePercent: number;
  }
  