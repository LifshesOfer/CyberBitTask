using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberBit
{
    class Program
    {
        static void Main()
        {
            int[] prices = {5,4,9,2};
            Program.PayoneerQ1(prices);
        }

        public static void PayoneerQ1(int[] prices)
        {
            BidPrice min = new(0,prices.First());
            BidPrice max = new(prices.Length, prices.Last());
            // int revenue = 0;
            for(int priceIndex=0;priceIndex<prices.Length;priceIndex++)
            {
                int revenue = max.Price - min.Price;
                // if(prices[priceIndex] < min.Price && priceIndex < max.Index)
                // {
                //     min = new(priceIndex, prices[priceIndex]);
                // }
                // else if(prices[priceIndex] >= max.Price && priceIndex > min.Index)
                // {
                //     max = new(priceIndex, prices[priceIndex]);
                // }

                if(max.Price - prices[priceIndex] > revenue)
                {
                    min = max.Index < priceIndex ? new(priceIndex, prices[priceIndex]) : min;
                }
                else if(prices[priceIndex] - min.Price >= revenue)
                {
                    max = new(priceIndex, prices[priceIndex]);
                }
            }
            Console.WriteLine($"The best revenue is from:");
            Console.WriteLine($"Buy at {min.Index} for {min.Price}");
            Console.WriteLine($"Sell at {max.Index} for {max.Price}");
            Console.WriteLine($"Revenue: {max.Price - min.Price}");
        }
    }

    public class BidPrice
    {
        public int Index{get;set;}
        public int Price{get;set;}
        public BidPrice(int index, int price)
        {
            Index = index;
            Price = price;
        }
    }
}